using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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
using System.Windows.Interop;
using System.Messaging;

namespace EX1_3
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {

        #region 1124

        #region 定义常量消息值
        public const int TRAN_FINISHED = 0x500;
        public const int WM_COPYDATA = 0x004A;


        //public static Process cmdP;
        public static StreamWriter cmdStreamInput;
        private static StringBuilder cmdOutput = null;

        public static IntPtr main_whandle;
        public static IntPtr text_whandle;
        #endregion

        #region 定义结构体
        public struct COPYDATASTRUCT
        {
            public IntPtr dwData;
            public int cbData;
            [MarshalAs(UnmanagedType.LPStr)]
            public string lpData;
        }
        #endregion

        //动态链接库引入
        [DllImport("User32.dll", EntryPoint = "SendMessage")]
        private static extern int SendMessage(
        IntPtr hWnd, //目标窗口句柄
        int Msg, //信息值
        int wParam, //第一个参数
        ref COPYDATASTRUCT lParam //第二个参数
        );

        [DllImport("User32.dll", EntryPoint = "FindWindow")]
        private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        #endregion


        public const string strCmd = "ping www.163.com -n 5";
        public static Process cmdP;



        public MainWindow()
        {
            InitializeComponent();

            SourceInitialized += OnSourceInitialized;
        }

        //ping同步读取方式
        private void startCmdSync(string strCmd)
        {
            Process process = new Process();
            //是否使用外壳程序
            process.StartInfo.FileName = "cmd.exe";
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.CreateNoWindow = true;
            //重定向输入流
            process.StartInfo.RedirectStandardInput = true;
            //重定向输出流
            process.StartInfo.RedirectStandardOutput = true;
            try
            {
                process.Start();
                process.StandardInput.WriteLine(strCmd);
                process.StandardInput.WriteLine("exit");
                //获取输出信息
                txb_result.Text = process.StandardOutput.ReadToEnd();
                process.WaitForExit();
                process.Close();
            }
            catch (Exception ex)
            { Console.WriteLine(ex.Message); }
        }
        private void startCmdAsync(string strCmd)
        {
            if(cmdP != null)
            {
                if(!cmdP.HasExited)
                {
                    cmdP.Close();
                }
            }
            cmdP = new Process();
            cmdP.StartInfo.FileName = "cmd.exe";
            cmdP.StartInfo.CreateNoWindow = true;
            cmdP.StartInfo.UseShellExecute = false;
            //重定向输入流
            cmdP.StartInfo.RedirectStandardInput = true;
            //重定向输出流
            cmdP.StartInfo.RedirectStandardOutput = true;
            cmdP.OutputDataReceived += (sender, dataReceivedEventArgs)
                     => AppendResult(dataReceivedEventArgs.Data);    
            //异步处理中通知应用程序某个进程已退出
            cmdP.EnableRaisingEvents = true;
            cmdP.Start();
            cmdP.StandardInput.WriteLine(strCmd);
            cmdP.StandardInput.WriteLine("exit");
            //cmdStreamInput = cmdP.StandardInput;
            //开始异步输出的读入
            cmdP.BeginOutputReadLine();
        }

        private void startCmdAsync2(string strCmd)
        {
            if (cmdP != null)
            {
                if (!cmdP.HasExited)
                {
                    cmdP.Close();
                }
            }
            cmdOutput = new StringBuilder("");

            cmdP = new Process();
            cmdP.StartInfo.FileName = "cmd.exe";
            cmdP.StartInfo.CreateNoWindow = true;
            cmdP.StartInfo.UseShellExecute = false;
            //重定向输入流
            cmdP.StartInfo.RedirectStandardInput = true;
            //重定向输出流
            cmdP.StartInfo.RedirectStandardOutput = true;

            //异步处理中通知应用程序某个进程已退出
            cmdP.EnableRaisingEvents = true;
            cmdP.OutputDataReceived += new DataReceivedEventHandler(strOutputHandler);
            cmdP.Start();
            cmdP.StandardInput.WriteLine(strCmd);
            cmdP.StandardInput.WriteLine("exit");
            //cmdStreamInput = cmdP.StandardInput;
            //开始异步输出的读入
            cmdP.BeginOutputReadLine();
        }

        private void AppendResult(string data)
        {
            txb_result.Dispatcher.BeginInvoke(new Action(() => txb_result.AppendText(data + "\n")));
        }


        private void btn_pingSync_Click(object sender, RoutedEventArgs e)
        {
            string s = txb_addr.Text;
            if (s.Length == 0)
                startCmdSync(strCmd);
            else
                startCmdSync("ping " + s);
        }

        private void btn_pingAsync_Click(object sender, RoutedEventArgs e)
        {
            string s = txb_addr.Text;
            if (s.Length == 0)
                startCmdAsync2(strCmd);
            else
                startCmdAsync2("ping " + s);
        }

        private void btn_clear_Click(object sender, RoutedEventArgs e)
        {
            txb_result.Text = "";
        }

        #region
        private void OnSourceInitialized(object sender, EventArgs e)
        {
            var windowInteropHelper = new WindowInteropHelper(this);
            var hwnd = windowInteropHelper.Handle;

            HwndSource source = HwndSource.FromHwnd(hwnd);
            source.AddHook(Hook);
        }

        private IntPtr Hook(IntPtr hwnd, int msg, IntPtr wparam, IntPtr lparam, ref bool handled)
        {
            switch (msg)
            {
                case WM_COPYDATA:
                    try
                    {
                        COPYDATASTRUCT cpdt = new COPYDATASTRUCT();
                        Type t = cpdt.GetType();
                        COPYDATASTRUCT MyKeyboardHookStruct = (COPYDATASTRUCT)Marshal.PtrToStructure(lparam, typeof(COPYDATASTRUCT));
                        showComment(MyKeyboardHookStruct.lpData);
                        break;
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                        break;
                    }
                case TRAN_FINISHED:
                    {
                        showComment(cmdOutput.ToString());
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
            return hwnd;
        }

        private void showComment(String comment)
        {
            //txb_result.Text = comment;
            txb_result.AppendText(comment + "\n");
        }

        private void strOutputHandler(object sendingProcess, DataReceivedEventArgs outLine)
        {
            cmdOutput.AppendLine(outLine.Data);
            IntPtr WINDOW_HANDLER = FindWindow(null, "MainWindow");
            if (WINDOW_HANDLER != IntPtr.Zero)
            {
                COPYDATASTRUCT cpdt = new COPYDATASTRUCT();
                cpdt.dwData = (IntPtr)0;

                if (outLine.Data == null)
                    return;
                if (outLine.Data.Length == 0)
                {
                    cpdt.cbData = 0;
                    cpdt.lpData = "";
                }
                else
                {
                    byte[] bData = System.Text.Encoding.Unicode.GetBytes(outLine.Data);
                    cpdt.cbData = bData.Length + 1;
                    cpdt.lpData = outLine.Data;
                }

                SendMessage(WINDOW_HANDLER, WM_COPYDATA, 0, ref cpdt);
            }
        }
            #endregion
    }
}
