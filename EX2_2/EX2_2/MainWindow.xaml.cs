using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace EX2_2
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public int fullSize = 20; //默认最大生产数
        public int num = 0;             //当前产品数
        public Mutex mutex;             //互斥量mutex
        public Semaphore full, empty;   //两个信号量full，empty

        public MainWindow()
        {
            InitializeComponent();

        }

        /// <summary>
        /// 生产者线程
        /// </summary>
        private void produce()
        {
            while(true)
            {
                empty.WaitOne();//empty信号量大于0
                mutex.WaitOne();//获得mutex互斥锁条件
                //满足条件，可以开始生产
                num++;
                update(Thread.CurrentThread.Name + "生产了一个产品，剩余产品数：" + num + "\r\n");
                //生产完成
                mutex.ReleaseMutex();
                full.Release();
                Thread.Sleep(3000);
            }
        }

        /// <summary>
        /// 消费者线程
        /// </summary>
        private void consume()
        {
            while(true)
            {
                full.WaitOne(); //full信号量大于0
                mutex.WaitOne();//获得mutex互斥锁条件
                //满足条件，可以消费
                num--;
                update(Thread.CurrentThread.Name + "消费了一个产品，剩余产品数：" + num + "\r\n");
                //消费完成
                mutex.ReleaseMutex();
                empty.Release();
                Thread.Sleep(4000);
            }
        }
        private void sld_pro_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            int value = (int)e.NewValue;
            lbl_pro.Content = value.ToString();
        }

        private void sld_con_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            int value = (int)e.NewValue;
            lbl_con.Content = value.ToString();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int num_pro = Convert.ToInt16(lbl_pro.Content.ToString());
            int num_con = Convert.ToInt16(lbl_con.Content.ToString());

            fullSize = Convert.ToInt16(txb_proNum.Text.ToString());
            mutex = new Mutex();
            full = new Semaphore(0, fullSize);
            empty = new Semaphore(fullSize, fullSize);

            for (int i = 0; i < num_pro; i++)
            {
                Thread proThread = new Thread(new ThreadStart(produce))
                { Name = "生产者" + (i + 1).ToString() + "号" };
                proThread.IsBackground = true;
                proThread.Start();
            }

            for (int i = 0; i < num_con; i++)
            {
                Thread conThread = new Thread(new ThreadStart(consume))
                { Name = "消费者" + (i + 1).ToString() + "号" };
                conThread.IsBackground = true;
                conThread.Start();
            }
        }

        private delegate void updateDelegate(string comment);
        public void update(string comment)
        {
            if (!lbx_disp.Dispatcher.CheckAccess())
            {
                updateDelegate d = update;
                lbx_disp.Dispatcher.Invoke(d, comment);
            }
            else
            {
                showComment(comment);
            }
        }

        private void showComment(string comment)
        {
            lbx_disp.Items.Add(comment);
        }
    }
}
