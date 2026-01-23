//using MvCameraControl;


namespace WinFormsCameraDemo
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            btnStart = new Button();
            btnStop = new Button();
            btnCapcture = new Button();
            lblStatus = new Label();
            pictureBox2 = new PictureBox();
            flowCapturedImages = new FlowLayoutPanel();
            fileSystemWatcher1 = new FileSystemWatcher();
            rbSoftware = new RadioButton();
            rbHardware = new RadioButton();
            rbContinuous = new RadioButton();
            imageList1 = new ImageList(components);
            groupBox1 = new GroupBox();
            pictureBox1 = new PictureBox();
            label1 = new Label();
            groupBox2 = new GroupBox();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)fileSystemWatcher1).BeginInit();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            groupBox2.SuspendLayout();
            SuspendLayout();
            // 
            // btnStart
            // 
            btnStart.Location = new Point(33, 96);
            btnStart.Margin = new Padding(3, 2, 3, 2);
            btnStart.Name = "btnStart";
            btnStart.Size = new Size(82, 22);
            btnStart.TabIndex = 0;
            btnStart.Text = "Start";
            btnStart.UseVisualStyleBackColor = true;
            btnStart.Click += btnStart_Click;
            // 
            // btnStop
            // 
            btnStop.Location = new Point(135, 96);
            btnStop.Margin = new Padding(3, 2, 3, 2);
            btnStop.Name = "btnStop";
            btnStop.Size = new Size(82, 22);
            btnStop.TabIndex = 0;
            btnStop.Text = "Stop";
            btnStop.UseVisualStyleBackColor = true;
            btnStop.Click += btnStop_Click;
            // 
            // btnCapcture
            // 
            btnCapcture.Location = new Point(235, 96);
            btnCapcture.Margin = new Padding(3, 2, 3, 2);
            btnCapcture.Name = "btnCapcture";
            btnCapcture.Size = new Size(82, 22);
            btnCapcture.TabIndex = 0;
            btnCapcture.Text = "Capcture";
            btnCapcture.UseVisualStyleBackColor = true;
            btnCapcture.Click += btnCapcture_Click;
            // 
            // lblStatus
            // 
            lblStatus.AutoSize = true;
            lblStatus.Location = new Point(269, 142);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(26, 15);
            lblStatus.TabIndex = 2;
            lblStatus.Text = "Idle";
            // 
            // pictureBox2
            // 
            pictureBox2.BorderStyle = BorderStyle.FixedSingle;
            pictureBox2.Location = new Point(6, 21);
            pictureBox2.Margin = new Padding(3, 2, 3, 2);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(198, 114);
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox2.TabIndex = 1;
            pictureBox2.TabStop = false;
            // 
            // flowCapturedImages
            // 
            flowCapturedImages.AutoScroll = true;
            flowCapturedImages.BorderStyle = BorderStyle.FixedSingle;
            flowCapturedImages.Location = new Point(475, 177);
            flowCapturedImages.Name = "flowCapturedImages";
            flowCapturedImages.Size = new Size(515, 355);
            flowCapturedImages.TabIndex = 3;
            flowCapturedImages.Paint += flowCapturedImages_Paint;
            // 
            // fileSystemWatcher1
            // 
            fileSystemWatcher1.EnableRaisingEvents = true;
            fileSystemWatcher1.SynchronizingObject = this;
            // 
            // rbSoftware
            // 
            rbSoftware.AutoSize = true;
            rbSoftware.Location = new Point(10, 38);
            rbSoftware.Name = "rbSoftware";
            rbSoftware.Size = new Size(71, 19);
            rbSoftware.TabIndex = 4;
            rbSoftware.TabStop = true;
            rbSoftware.Text = "Software";
            rbSoftware.UseVisualStyleBackColor = true;
            // 
            // rbHardware
            // 
            rbHardware.AutoSize = true;
            rbHardware.Location = new Point(10, 60);
            rbHardware.Name = "rbHardware";
            rbHardware.Size = new Size(76, 19);
            rbHardware.TabIndex = 4;
            rbHardware.TabStop = true;
            rbHardware.Text = "Hardware";
            rbHardware.UseVisualStyleBackColor = true;
            // 
            // rbContinuous
            // 
            rbContinuous.AutoSize = true;
            rbContinuous.Location = new Point(10, 17);
            rbContinuous.Name = "rbContinuous";
            rbContinuous.Size = new Size(87, 19);
            rbContinuous.TabIndex = 4;
            rbContinuous.TabStop = true;
            rbContinuous.Text = "Continuous";
            rbContinuous.UseVisualStyleBackColor = true;
            rbContinuous.CheckedChanged += rbContinuous_CheckedChanged;
            // 
            // imageList1
            // 
            imageList1.ColorDepth = ColorDepth.Depth32Bit;
            imageList1.ImageSize = new Size(16, 16);
            imageList1.TransparentColor = Color.Transparent;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(rbSoftware);
            groupBox1.Controls.Add(rbContinuous);
            groupBox1.Controls.Add(rbHardware);
            groupBox1.Location = new Point(29, 12);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(288, 79);
            groupBox1.TabIndex = 5;
            groupBox1.TabStop = false;
            groupBox1.Text = "TriggerMode";
            // 
            // pictureBox1
            // 
            pictureBox1.BorderStyle = BorderStyle.FixedSingle;
            pictureBox1.Location = new Point(12, 159);
            pictureBox1.Margin = new Padding(3, 2, 3, 2);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(392, 242);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 2;
            pictureBox1.TabStop = false;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 132);
            label1.Name = "label1";
            label1.Size = new Size(69, 15);
            label1.TabIndex = 6;
            label1.Text = "LiveCamera";
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(pictureBox2);
            groupBox2.Location = new Point(496, 12);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(210, 145);
            groupBox2.TabIndex = 7;
            groupBox2.TabStop = false;
            groupBox2.Text = "C";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1028, 745);
            Controls.Add(groupBox2);
            Controls.Add(label1);
            Controls.Add(pictureBox1);
            Controls.Add(groupBox1);
            Controls.Add(flowCapturedImages);
            Controls.Add(lblStatus);
            Controls.Add(btnCapcture);
            Controls.Add(btnStop);
            Controls.Add(btnStart);
            Margin = new Padding(3, 2, 3, 2);
            Name = "Form1";
            Text = "Form1";
            WindowState = FormWindowState.Maximized;
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)fileSystemWatcher1).EndInit();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            groupBox2.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnStart;
        private Button btnStop;
        private Button btnCapcture;
        private Label lblStatus;
        private PictureBox pictureBox2;
        private FlowLayoutPanel flowCapturedImages;
        private FileSystemWatcher fileSystemWatcher1;
        private RadioButton rbSoftware;
        private RadioButton rbContinuous;
        private RadioButton rbHardware;
        private ImageList imageList1;
        private GroupBox groupBox1;
        private Label label1;
        private PictureBox pictureBox1;
        private GroupBox groupBox2;
    }
}
