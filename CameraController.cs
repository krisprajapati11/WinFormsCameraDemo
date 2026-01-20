using System;
using System.Runtime.InteropServices;

namespace MindVisionDemo
{
    public class CameraController
    {
        private int hCamera = 0;
        private bool isRunning = false;
            

        // ================= SDK IMPORTS =================

        [DllImport("MVCAMSDK.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern int CameraSdkInit(int iLanguage);

        [DllImport("MVCAMSDK.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern int CameraEnumerateDevice(out IntPtr pCameraList, ref int piNums);

        [DllImport("MVCAMSDK.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern int CameraInit(
            IntPtr pCameraInfo,
            int iParam,
            int iReserved,
            out int phCamera 
        );

        [DllImport("MVCAMSDK.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern int CameraPlay(int hCamera);

        [DllImport("MVCAMSDK.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern int CameraPause(int hCamera);

        [DllImport("MVCAMSDK.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern int CameraUnInit(int hCamera);

        // ================= PUBLIC API =================

        // 🔹 cameraList()
        public int CameraList()
        {
            CameraSdkInit(0);

            IntPtr listPtr = IntPtr.Zero;
            int count = 0;

            int ret = CameraEnumerateDevice(out listPtr, ref count);

            // count = number of connected cameras
            return count;
        }

        // 🔹 cameraStart()
        public bool CameraStart()   
        {
            IntPtr listPtr = IntPtr.Zero;
            int count = 0;

            CameraEnumerateDevice(out listPtr, ref count);
            if (count == 0) return false;

            // first camera only
            CameraInit(listPtr, -1, -1, out hCamera);
            CameraPlay(hCamera);    

            isRunning = true;
            return true;
        }

        // 🔹 cameraPause()
        public void CameraPauseCapture()
        {
            if (!isRunning) return;
            CameraPause(hCamera);
            isRunning = false;
        }   

        // 🔹 cameraStop()
        public void CameraStop()
        {
            if (hCamera != 0)
            {
                CameraUnInit(hCamera);
                hCamera = 0;
                isRunning = false;
            }
        }
    }
}
