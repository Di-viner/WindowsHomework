using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Pipes;
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

namespace PipeServer
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            txb_tips2.Text += "等待连接中...";
        }

        private void btn_receive_Click(object sender, RoutedEventArgs e)
        {
            NamedPipeServerStream pipeServer = new NamedPipeServerStream("TestNamedPipe");
            pipeServer.WaitForConnection();
            StreamReader sr = new StreamReader(pipeServer);
            txb_msg2.Text += sr.ReadToEnd();

            txb_tips2.Text += "消息通过管道传送成功";
        }
    }
}
