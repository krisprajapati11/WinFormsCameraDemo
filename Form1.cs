using MindVisionDemo;
using System;
using System.Drawing;
using System.Windows.Forms;
namespace WinFormsCameraDemo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

        }

        CameraController camera = new CameraController();

        private void btnStart_Click(object sender, EventArgs e)
        {
            bool started = camera.CameraStart();
            MessageBox.Show(started ? "Camera Started" : "No Camera");
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            camera.CameraStop();
            MessageBox.Show("Camera Stopped");
        }

    }
}
