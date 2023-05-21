using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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

namespace WpfApp1
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void RedirectCmd(string command)
        {
            Process process = new Process();
            process.StartInfo.FileName = "cmd.exe";
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.RedirectStandardInput = true;
            process.StartInfo.RedirectStandardOutput = true;
            process.OutputDataReceived += (sender, args) => AppendResult(args.Data);
            process.Exited += (sender, eventArgs) => btn_getMac.Dispatcher.BeginInvoke(new Action(() => btn_getMac.IsEnabled = true));

            try
            {
                process.Start();
                process.StandardInput.WriteLine(command);
                process.StandardInput.WriteLine("exit");
                process.EnableRaisingEvents = true;
                process.BeginOutputReadLine();
            }
            catch (Exception ex)
            { Console.WriteLine(ex.ToString()); }
        }

        private void AppendResult(string data)
        {
            tbx_result.Dispatcher.BeginInvoke(new Action(() => tbx_result.AppendText(data + "\n")));
        }

        private void btn_getMac_Click(object sender, RoutedEventArgs e)
        {
            const string strCmd = "getmac";
            RedirectCmd(strCmd);
        }

        private void btn_shutDown_Click(object sender, RoutedEventArgs e)
        {
            const string strCmd = "shutdown /s /t 3000";
            RedirectCmd(strCmd);
        }
    }
}
