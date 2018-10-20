using System.Runtime.InteropServices;
using System.Drawing;

namespace FG01_Test_prototype
{
    class Mouse
    {
        [StructLayout(LayoutKind.Sequential)]
        public struct POINT
        {
            public int X;
            public int Y;

            public static implicit operator Point(POINT point) => new Point(point.X, point.Y);
        }

        [DllImport("user32.dll")]
            static extern void mouse_event(uint dwFlags, int dx, int dy, uint dwData, int dwExtraInfo);

        [DllImport("user32.dll")]
            static extern bool SetCursorPos(int X, int Y);
        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
            static extern bool GetCursorPos(out POINT lpPoint);

        static int LBM = 1;
        static int RBM = 1;

        public static void mouseState(int statM0, int statM1){
            POINT curpoint;
            GetCursorPos(out curpoint);



            if (statM0 == 1 && LBM == 0)
            {
                mouse_event(0x0004, curpoint.X, curpoint.Y, 0, 0);
                LBM = 1;
                return;
            }

            if (statM0 == 0 && LBM == 1)
            {
                mouse_event(0x0002, curpoint.X, curpoint.Y, 0, 0);
                LBM = 0;
                return;
            }

            if (statM1 == 1 && RBM == 0)
            {
                mouse_event(0x0010, curpoint.X, curpoint.Y, 0, 0);
                RBM = 1;
                return;
            }

            if (statM1 == 0 && RBM == 1)
            {
                mouse_event(0x0008, curpoint.X, curpoint.Y, 0, 0);
                RBM = 0;
                return;
            }
            
        }

        public static void moveCursor(int x, int y){
            POINT curpoint;
            GetCursorPos(out curpoint);
            SetCursorPos(curpoint.X + x, curpoint.Y + y);
        }
    }
}
