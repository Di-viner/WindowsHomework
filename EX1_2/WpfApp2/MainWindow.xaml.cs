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

namespace WpfApp2
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

        private void btn_send_Click(object sender, RoutedEventArgs e)
        {
            NamedPipeClientStream pipeClient = new NamedPipeClientStream("TestNamedPipe");
            pipeClient.Connect();
            txb_tips1.Text += "\r\n管道连接成功";

            try
            {
                StreamWriter sw = new StreamWriter(pipeClient);
                sw.AutoFlush = true;
                sw.WriteLine(txb_msg1.Text);
                sw.Close();
                txb_tips1.Text += "\r\n消息成功通过管道发送";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }
    }
}
