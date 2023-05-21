using System;
using System.Collections.Generic;
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

namespace EX3_2
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

        [DllImport(@"D:\Learning\Windows\EX3_2\x64\Release\CppDLL.dll", EntryPoint = "Fact", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
        public static extern int Fact(int n);

        [DllImport(@"D:\Learning\Windows\EX3_2\x64\Release\CppDLL.dll", EntryPoint = "Sub", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
        public static extern double Sub(double a, double b);

        [DllImport(@"D:\Learning\Windows\EX3_2\x64\Release\CppDLL.dll", EntryPoint = "Multi", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
        public static extern double Multi(double a, double b);
        private void btnResFac_Click(object sender, RoutedEventArgs e)
        {
            string strFac = txbFac.Text.Trim();
            try
            {
                int res = Fact(int.Parse(strFac));
                lblFactResult.Content = res;
            }
            catch (Exception ex)
            {
                MessageBox.Show("请输入正确的值，错误信息："+ex.Message);
            }
        }

        private void btnResSub_Click(object sender, RoutedEventArgs e)
        {
            string strA = txbA.Text.Trim();
            string strB = txbB.Text.Trim();
            try
            {
                double res = Sub(double.Parse(strA), double.Parse(strB));
                lblSubResult.Content= res;
            }
            catch(Exception ex)
            {
                MessageBox.Show("请输入正确的值，错误信息：" + ex.Message);
            }
        }
    }
}
