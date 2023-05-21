using System;
using System.Collections.Generic;
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

namespace EX3_4
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
        
        private void btnMulti_Click(object sender, RoutedEventArgs e)
        {
            string s1 = txbMultiA.Text;
            string s2 = txbMultiB.Text;
            //string ret = ComTest.multi("B4877881-85CA-442D-9910-00D230F21691", "aTransaction", int.Parse(s1), int.Parse(s2));
            string ret = ComTest.multi("F4FC1645-CFEA-482D-A5CA-7E31075AE381", "bTransaction", int.Parse(s1), int.Parse(s2));
            txbMultiRes.Text = String.Concat(ret);
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            string s1 = txbAddA.Text.Trim();
            string s2 = txbAddB.Text.Trim();
            //string ret = ComTest.add("B4877881-85CA-442D-9910-00D230F21691", "aTransaction", int.Parse(s1), int.Parse(s2));
            string ret = ComTest.add("F4FC1645-CFEA-482D-A5CA-7E31075AE381", "bTransaction", int.Parse(s1), int.Parse(s2));
            txbAddRes.Text = String.Concat(ret);
        }

        private void btnSub_Click(object sender, RoutedEventArgs e)
        {
            string s1 = txbSubA.Text.Trim();
            string s2 = txbSubB.Text.Trim();
            //string ret = ComTest.add("B4877881-85CA-442D-9910-00D230F21691", "aTransaction", int.Parse(s1), int.Parse(s2));
            string ret = ComTest.sub("F4FC1645-CFEA-482D-A5CA-7E31075AE381", "bTransaction", int.Parse(s1), int.Parse(s2));
            txbSubRes.Text = String.Concat(ret);
        }

        private void btnDiverse_Click(object sender, RoutedEventArgs e)
        {
            string s1 = txbDiverseA.Text.Trim();
            string s2 = txbDiverseB.Text.Trim();
            //string ret = ComTest.add("B4877881-85CA-442D-9910-00D230F21691", "aTransaction", int.Parse(s1), int.Parse(s2));
            string ret = ComTest.diverse("F4FC1645-CFEA-482D-A5CA-7E31075AE381", "bTransaction", int.Parse(s1), int.Parse(s2));
            txbDiverseRes.Text = String.Concat(ret);
        }
    }
}
