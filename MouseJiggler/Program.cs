using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MouseJiggler
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var cursor = new Cursor(Cursor.Current.Handle);
            
            (int x, int y) initialPosition;
            (int x, int y) finalPosition;
            
            do
            {
                do
                {
                    initialPosition = (Cursor.Position.X, Cursor.Position.Y);
                    Thread.Sleep(60000);
                    finalPosition = (Cursor.Position.X, Cursor.Position.Y);
                } while (finalPosition.x != initialPosition.x || finalPosition.y != initialPosition.y);
                AutomaticMove();
            } while (true);
        }

        private static void AutomaticMove()
        {
            (int x, int y) initialPosition = (Cursor.Position.X, Cursor.Position.Y);

            do
            {
                Cursor.Position = new Point(Cursor.Position.X - 50, Cursor.Position.Y - 50);
                btnSet_Click(null, null);
                Thread.Sleep(2000);
                Cursor.Position = new Point(Cursor.Position.X + 50, Cursor.Position.Y + 50);
                btnSet_Click(null, null);
                Thread.Sleep(2000);
            } while (Cursor.Position.X == initialPosition.x && Cursor.Position.Y == initialPosition.y);
        }

        private const UInt32 MOUSEEVENTF_LEFTDOWN = 0x0002;
        private const UInt32 MOUSEEVENTF_LEFTUP = 0x0004;
        [DllImport("user32.dll")]
        private static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint dwData, uint dwExtraInf);
        private static void btnSet_Click(object sender, EventArgs e)
        {
            mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);//make left button down
            mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);//make left button up
        }
    }
}
