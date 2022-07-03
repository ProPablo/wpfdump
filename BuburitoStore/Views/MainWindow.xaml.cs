using BuburitoStore.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BuburitoStore.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        [DllImport("User32.dll")]
        static extern bool MoveWindow(IntPtr handle, int x, int y, int width, int height, bool redraw);

        internal delegate int WindowEnumProc(IntPtr hwnd, IntPtr lparam);
        [DllImport("user32.dll")]
        internal static extern bool EnumChildWindows(IntPtr hwnd, WindowEnumProc func, IntPtr lParam);

        [DllImport("user32.dll")]
        static extern int SendMessage(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);

        private Process process;
        private IntPtr unityHWND = IntPtr.Zero;

        private const int WM_ACTIVATE = 0x0006;
        private readonly IntPtr WA_ACTIVE = new IntPtr(1);
        private readonly IntPtr WA_INACTIVE = new IntPtr(0);

        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainViewModel(this);
            try
            {
                //process = new Process();
                //process.StartInfo.FileName = "UnityGame.exe";
                //process.StartInfo.Arguments = "-parentHWND " + panel1.Handle.ToInt32() + " " + Environment.CommandLine;

                //Console.WriteLine(process.StartInfo.FileName);
                //Console.WriteLine(process.StartInfo.Arguments);

                //Console.WriteLine(panel1.Handle);

                //process.StartInfo.UseShellExecute = true;
                //process.StartInfo.CreateNoWindow = true;

                ////process.WaitForInputIdle();

                ////Methods.PipeServer.SartDuplexPipe("Pipe");

                //process.Start();

                //process.WaitForInputIdle();

                //// Doesn't work for some reason ?!
                ////unityHWND = process.MainWindowHandle;

                //EnumChildWindows(panel1.Handle, WindowEnum, IntPtr.Zero);

                ////unityHWNDLabel.Text = "Unity HWND: 0x" + unityHWND.ToString("X8");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ".\nCheck if Container.exe is placed next to UnityGame.exe.");
            }


        }

        //Relay command doesnt work on Mouse down
        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
            //TODO do with single element after ribbon
            //Buttons absorb this input
        }

    }
}
