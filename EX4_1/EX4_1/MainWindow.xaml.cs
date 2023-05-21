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

namespace EX4_1
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

        private void btn1_Click(object sender, RoutedEventArgs e)
        {
            FireAlarm fireAlarm = new FireAlarm("厨房", 3);
            fireAlarm.FireEvent += FireEventHandler1;//订阅
            fireAlarm.alarm();
        }

        private void btn2_Click(object sender, RoutedEventArgs e)
        {
            FireAlarm fireAlarm = new FireAlarm("后院", 10);
            fireAlarm.FireEvent += FireEventHandler2;//订阅
            fireAlarm.alarm();
        }

        public void FireEventHandler1(string room, int ferocity)    //事件处理函数1，与delegate对象具有相同的参数和返回值类型
        {
            lsb.Items.Add("当前" + room + "发生火情，火势程度为" + ferocity);
            lsb.Items.Add("火势得到有效控制\n");
        }

        public void FireEventHandler2(string room, int ferocity)
        {
            lsb.Items.Add("当前" + room + "发生火情，火势程度为" + ferocity + "\n");
            MessageBox.Show("火势不能控制，请拨打119");
        }
    }

    public class FireAlarm//发布者
    {
        string room;
        int ferocity;
        public delegate void FireDelegate(string room, int ferocity); //委托声明一个函数类型，返回值为void，参数列表为(string, int)
        public event FireDelegate FireEvent;                          //声明一个事件类似于声明一个进行了封装的委托类型的变量
        public FireAlarm(string room, int ferocity)
        {
            this.room = room;
            this.ferocity = ferocity;
        }
        public void alarm()
        {
            FireEvent(room, ferocity);
        }
    }
}
