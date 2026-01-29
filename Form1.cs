//using MindVisionDemo;
using System;
using System.Drawing;
using System.Windows.Forms;
using MVSDK;
using System.Runtime.InteropServices;
using System.Drawing.Imaging;
using CameraHandle = System.Int32;
namespace WinFormsCameraDemo
{
    public partial class Form1 : Form
    {
        protected IntPtr m_Grabber = IntPtr.Zero;
        protected ColorPalette m_GrayPal;
        protected tSdkCameraDevInfo m_DevInfo;
        protected pfnCameraGrabberFrameCallback m_FrameCallback;
        protected CameraHandle m_hCamera = 0;
        string ImageFolderPath;
        string csvFilePath;

        int currentId = 1;

        //string folderpath = System.IO.Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "CapturedImages");
        public Form1()
        {
            InitializeComponent();
            m_FrameCallback = new pfnCameraGrabberFrameCallback(CameraGrabberFrameCallback);

        }
        private void Form1_Load(object sender, EventArgs e)
        {
            //MessageBox.Show(folderpath);
            string basepath = AppDomain.CurrentDomain.BaseDirectory;
            ImageFolderPath = System.IO.Path.Combine(basepath, "CapturedImages");
            csvFilePath = System.IO.Path.Combine(basepath, "ImageData.csv");


            if (!System.IO.Directory.Exists(ImageFolderPath))
            {
                System.IO.Directory.CreateDirectory(ImageFolderPath);
            }
            if(!System.IO.File.Exists(csvFilePath))
            {
                System.IO.File.WriteAllText(csvFilePath, "ID,Date,ImageName\n");
            }
            LoadCsvtoGrid();
        }
        void LoadCsvtoGrid() {
            dataGridView1.Rows.Clear();
            string[] lines = System.IO.File.ReadAllLines(csvFilePath);

            for(int i = 1; i < lines.Length; i++)
            {
                if (string.IsNullOrEmpty(lines[i])) continue;

                string[] parts = lines[i].Split(',');
                dataGridView1.Rows.Add(parts);

                int lastId;
                if (int.TryParse(parts[0], out lastId))
                {
                    currentId = lastId + 1;

                }
            }
        }
        void SetContinuousMode()
        {
            MvApi.CameraSetTriggerMode(m_hCamera, 0);
            MvApi.CameraPlay(m_hCamera);

        }
        void SetSoftwareTriggerMode()
        {
            MvApi.CameraSetTriggerMode(m_hCamera, 1);
            //MvApi.CameraSetTriggerSource(m_hCamera, 1);
            MvApi.CameraPlay(m_hCamera);
        }
        void SetHardwareTriggerMode()
        {
            MvApi.CameraSetTriggerMode(m_hCamera, 1);
            //MvApi.CameraSetTriggerSource(m_hCamera, 0);
            MvApi.CameraPlay(m_hCamera);
        }


        private async void btnStart_Click(object sender, EventArgs e)
        {
            InitCamera();
            if (m_Grabber != IntPtr.Zero)
            {
                MvApi.CameraGrabber_StartLive(m_Grabber);
                await System.Threading.Tasks.Task.Delay(300);

                // auto first capture
                CaptureAndSaveImage();

                // auto first capture
                CaptureAndSaveImage();
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {

            if (m_Grabber != IntPtr.Zero)
            {
                MvApi.CameraGrabber_StopLive(m_Grabber);
                //MvApi.CameraGrabber_Stop(m_Grabber);
                //MvApi.CameraGrabber_Close(m_Grabber);
                MvApi.CameraGrabber_Destroy(m_Grabber);
                m_Grabber = IntPtr.Zero;
            }
            m_hCamera = 0;
            //pictureBox1.Image ?.Dispose();
            //pictureBox1.Image = null;

            lblStatus.Text = "Idle";

        }


        private void InitCamera()
        {

            //MvApi.CameraSetTriggerMode(m_hCamera, 0);
            CameraSdkStatus status = 0;

            tSdkCameraDevInfo[] DevList;
            MvApi.CameraEnumerateDevice(out DevList);
            int NumDev = (DevList != null ? DevList.Length : 0);
            if (NumDev < 1)
            {
                MessageBox.Show("Camera not scanned");
                return;
            }
            else if (NumDev == 1)
            {
                status = MvApi.CameraGrabber_Create(out m_Grabber, ref DevList[0]);
            }
            else
            {
                status = MvApi.CameraGrabber_CreateFromDevicePage(out m_Grabber);
            }

            if (status == 0)
            {
                MvApi.CameraGrabber_GetCameraDevInfo(m_Grabber, out m_DevInfo);
                MvApi.CameraGrabber_GetCameraHandle(m_Grabber, out m_hCamera);
                MvApi.CameraCreateSettingPage(m_hCamera, this.Handle, m_DevInfo.acFriendlyName, null, (IntPtr)0, 0);

                MvApi.CameraGrabber_SetRGBCallback(m_Grabber, m_FrameCallback, IntPtr.Zero);
                tSdkCameraCapbility cap;
                MvApi.CameraGetCapability(m_hCamera, out cap);
                if (cap.sIspCapacity.bMonoSensor != 0)
                {
                    MvApi.CameraSetIspOutFormat(m_hCamera, (uint)MVSDK.emImageFormat.CAMERA_MEDIA_TYPE_MONO8);

                    Bitmap Image = new Bitmap(1, 1, PixelFormat.Format8bppIndexed);
                    m_GrayPal = Image.Palette;
                    for (int Y = 0; Y < m_GrayPal.Entries.Length; Y++)
                        m_GrayPal.Entries[Y] = Color.FromArgb(255, Y, Y, Y);
                }

                // set VFlip, because the data output by the SDK is from bottom to top by default, VFlip can be directly converted to Bitmap
                MvApi.CameraSetMirror(m_hCamera, 1, 1);

                // To illustrate how to use the camera data to create a Bitmap in a callback and display it in a PictureBox, we do not use the SDK's built-in drawing operations
                //MvApi.CameraGrabber_SetHWnd(m_Grabber, this.DispWnd.Handle);

                MvApi.CameraGrabber_StartLive(m_Grabber);
            }
            else
            {
                MessageBox.Show(String.Format("Failed to open the camera, reason:{0}", status));
            }
            ApplyTriggerMode();
        }
        void ApplyTriggerMode()
        {
            if (rbContinuous.Checked)
            {
                MvApi.CameraSetTriggerMode(m_hCamera, 0);
                btnCapcture.Enabled = false;
                lblStatus.Text = "Continuous Mode";
            }
             if (rbSoftware.Checked)
            {
                MvApi.CameraSetTriggerMode(m_hCamera, 1);
                btnCapcture.Enabled = true;
                lblStatus.Text = "Software Trigger Mode";
            }
            else if (rbHardware.Checked)
            {
                MvApi.CameraSetTriggerMode(m_hCamera, 1);
                btnCapcture.Enabled = false;
                lblStatus.Text = "Frame Trigger Mode";
            }
        }

        private void CameraGrabberFrameCallback(
            IntPtr Grabber,
            IntPtr pFrameBuffer,
            ref tSdkFrameHead pFrameHead,
            IntPtr Context)
        {
            GC.Collect();

            int w = pFrameHead.iWidth;
            int h = pFrameHead.iHeight;
            Boolean gray = (pFrameHead.uiMediaType == (uint)MVSDK.emImageFormat.CAMERA_MEDIA_TYPE_MONO8);
            Bitmap Image = new Bitmap(w, h,
                gray ? w : w * 3,
                gray ? PixelFormat.Format8bppIndexed : PixelFormat.Format24bppRgb,
                pFrameBuffer);

            // If the grayscale to set the color palette
            if (gray)
            {
                Image.Palette = m_GrayPal;
            }

            this.Invoke((EventHandler)delegate
            {
                AssignImage(Image);
            });
        }
        public void AssignImage(Bitmap Image)
        {
            pictureBox1.Image = Image;
            pictureBox1.Refresh();
            //pictureBox2.Image = Image;
        }

        void addCapcturedImage(Bitmap Image)
        {
            PictureBox pb = new PictureBox();
            pb.Width = 160;
            pb.Height = 120;
            pb.SizeMode = PictureBoxSizeMode.Zoom;
            pb.BorderStyle = BorderStyle.FixedSingle;

            pb.Image = Image;


            //flowCapturedImages.Invoke(new Action(() =>
            //{
            //    flowCapturedImages.Controls.Add(pb);
            //}));
        }
        void CaptureAndSaveImage()
        {
            if (m_Grabber == IntPtr.Zero)
                return;

            IntPtr pImage;

            if (MvApi.CameraGrabber_SaveImage(m_Grabber, out pImage, 2000)
                == CameraSdkStatus.CAMERA_STATUS_SUCCESS)
            {
                // generate image name
                string imageName = $"IMG_{DateTime.Now:yyyyMMdd_HHmmss}_{currentId}.bmp";
                string imageFullPath = System.IO.Path.Combine(ImageFolderPath, imageName);

                // save image
                MvApi.CameraImage_SaveAsBmp(pImage, imageFullPath);
                 

                // log entry (CSV + Grid)
                AddLogEntry(imageName);

                // release SDK image
                MvApi.CameraImage_Destroy(pImage);
            }
            else
            {
                MessageBox.Show("Image capture failed");
            }
        }


        private void btnCapcture_Click(object sender, EventArgs e)
        {
            //string imageName = $"IMG_{DateTime.Now:yyyyMMdd_HHmmss}_{currentId}.bmp";
            //string imagePath = System.IO.Path.Combine(ImageFolderPath, imageName);

            if (!rbSoftware.Checked)
            {
                MessageBox.Show("Switch to Software Trigger Mode");
                return;
            }

            CaptureAndSaveImage();

            if (m_Grabber == IntPtr.Zero)
            {
                lblStatus.Text = "Camera is not running";
                MessageBox.Show("Camera is not running");
                return;
            }
            if (m_Grabber != IntPtr.Zero)
            {
                lblStatus.Text = "Capturing...";
                IntPtr pImage;

                if (MvApi.CameraGrabber_SaveImage(m_Grabber, out pImage, 2000) == CameraSdkStatus.CAMERA_STATUS_SUCCESS)
                {
                    // 1. Get image information using IntPtr for the header
                    IntPtr pDataBuffer;
                    IntPtr pHeadPtr;
                    MvApi.CameraImage_GetData(pImage, out pDataBuffer, out pHeadPtr);

                    // 2. Marshal the pointer data into the tSdkFrameHead struct
                    tSdkFrameHead frameHead = (tSdkFrameHead)Marshal.PtrToStructure(pHeadPtr, typeof(tSdkFrameHead));

                    // 3. Create a Bitmap object from the data buffer
                    int w = frameHead.iWidth;
                    int h = frameHead.iHeight;
                    bool gray = (frameHead.uiMediaType == (uint)MVSDK.emImageFormat.CAMERA_MEDIA_TYPE_MONO8);


                    using (Bitmap tempImg = new Bitmap(w, h,
                        gray ? w : w * 3,
                        gray ? PixelFormat.Format8bppIndexed : PixelFormat.Format24bppRgb,
                        pDataBuffer))
                    {
                        if (gray) tempImg.Palette = m_GrayPal;

                        // 4. Update PictureBox2 with a DEEP COPY
                        // This is critical because tempImg is invalid once CameraImage_Destroy is called
                        Bitmap CapcturedImg = new Bitmap(tempImg);

                        //string ImgName = 


                        pictureBox2.Image?.Dispose();
                        pictureBox2.Image = new Bitmap(CapcturedImg);
                        addCapcturedImage(new Bitmap(CapcturedImg));
                        //MessageBox.Show("Image capctured ");
                    }

                    // 5. Save to file
                    string filename = System.IO.Path.Combine(
                        AppDomain.CurrentDomain.BaseDirectory.ToString(),
                        string.Format("{0}.bmp", System.Environment.TickCount));
                    MvApi.CameraImage_SaveAsBmp(pImage, ImageFolderPath);
                    //AddLogEntry(imageName);
                    //MvApi.CameraImage_SaveAsBmp(pImage, filename);

                    // 6. Release the SDK image handle
                    MvApi.CameraImage_Destroy(pImage);

                    pictureBox2.Refresh();
                }
                else
                {
                    MessageBox.Show("Snap failed");
                }
            }
        }
        void AddLogEntry(string imageName)
        {
            string date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            // 1. Append to CSV
            string line = $"{currentId},{date},{imageName}\n";
            System.IO.File.AppendAllText(csvFilePath, line);

            // 2. Add to DataGridView
            dataGridView1.Rows.Add(currentId, date, imageName);

            // 3. Increment ID
            currentId++;
        }


        private void flowCapturedImages_Paint(object sender, PaintEventArgs e)
        {

        }

        private void rbContinuous_CheckedChanged(object sender, EventArgs e)
        {
            if (rbContinuous.Checked)
            {
                SetContinuousMode();

            }
        }

        private void label2_Click(object sender, EventArgs e)
        {
            
        }
    }
}

    
