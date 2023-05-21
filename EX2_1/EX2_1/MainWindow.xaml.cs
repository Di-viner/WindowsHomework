using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EX2_1
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

        public static string dir;                 //选择监视文件的目录string
        public static ManualResetEvent dir_end;     //信号量，false为进入循环    

        public ManualResetEvent cap_termi;//置为有效时抓屏终止
        public ManualResetEvent cap_start; //置为有效时抓屏
        public static ManualResetEvent[] me_cap = new ManualResetEvent[2];  
        public void WatchDir()
        {
            long nowTime = DateTime.Now.ToFileTime();         //转换为windows文件时间
            DirectoryInfo dirInfo = new DirectoryInfo(dir);//获取用户选择目录信息    
            FileInfo[] f_ins = dirInfo.GetFiles();           //获取目录下的文件

            //有信号为true，则不进入循环，直接跳过
            //无信号为false，取反后则进入循环
            while(!dir_end.WaitOne(500))
            {
                for(int i = 0; i < f_ins.Length; i++)
                {
                    nowTime = DateTime.Now.ToFileTime();
                    if(File.Exists(f_ins[i].FullName))
                    {
                        string strFileInfo = String.Format("监视到文件{0}\r\n", f_ins[i].FullName);
                        update(strFileInfo);
                        string newDir = dir + "\\new\\";
                        if(!Directory.Exists(newDir))
                        {
                            Directory.CreateDirectory(newDir);
                        }
                        string new_filename = newDir + nowTime.ToString() + f_ins[i].Name;
                        if(!File.Exists(new_filename))
                        {
                            File.Move(f_ins[i].FullName, new_filename);
                        }
                    }
                }
                f_ins = dirInfo.GetFiles();//重新获取目录信息
            }
        }

        public void Capture_screen()
        {
            int width = Screen.PrimaryScreen.Bounds.Width;
            int height = Screen.PrimaryScreen.Bounds.Height;
            Bitmap b = new Bitmap(width, height);
            Graphics g = Graphics.FromImage(b);
            /*
             * Graphics.FromImage在图b上创建一个Graphics类型的可编辑图层，该图层会直接被添加到b本体上。
             * 当图层发生绘画变化后，再展示b时，b将展示出图层发生变化后的复合图像。
             */
            string desk_file = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string dest_fn = null;

            int index = WaitHandle.WaitAny(me_cap, 500);
            /*
             * 当index为0，capture_terminate置为有效，直接跳过循环体；
             * 当index为-1，进入循环体；
             * 当index为1，capture_start为有效，开始抓屏
             */
            while (index != 0) 
            {
                if(index == 1)
                {
                    dest_fn = desk_file + "\\windows_img\\";
                    if(!Directory.Exists(dest_fn))
                        Directory.CreateDirectory(dest_fn); 
                    long now_t = DateTime.Now.ToFileTime();
                    dest_fn += now_t + ".bmp";
                    g.CopyFromScreen(0, 0, 0, 0, new System.Drawing.Size(width, height));//源矩形和目标矩形左上角的x, y坐标和传输区域的大小
                    b.Save(dest_fn, System.Drawing.Imaging.ImageFormat.Bmp);
                    update("抓屏成功,图片保存到" + dest_fn + "\r\n");
                    cap_start.Reset();
                }
                index = WaitHandle.WaitAny(me_cap, 500);
            }
            g.Dispose();
            b.Dispose();

        }
        /// <summary>
        /// 选择文件目录进行监视
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            dir_end = new ManualResetEvent(false);
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            if(dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                dir = dialog.SelectedPath;
                string s = String.Format("已选择监视目录{0}\r\n", dir);
                update(s);
            }
        }

        /// <summary>
        /// 启动文件目录监视
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            dir_end.Reset();//信号设置为false
            Thread workthread = new Thread(new ThreadStart(WatchDir));//开启工作线程
            workthread.IsBackground = true;
            workthread.Start();
        }

        /// <summary>
        /// 结束文件目录监视线程
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            dir_end.Set();
            update("终止文件监视线程\r\n");

        }

        /// <summary>
        /// 启动抓屏线程
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            me_cap[0] = cap_termi = new ManualResetEvent(false);  
            me_cap[1] = cap_start = new ManualResetEvent(false);
            update("抓屏线程已启动\r\n");
            Thread workThread = new Thread(new ThreadStart(Capture_screen));
            workThread.IsBackground = true;
            workThread.Start();
        }

        /// <summary>
        /// 抓屏
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            cap_start.Set();
        }

        /// <summary>
        /// 结束抓屏线程
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            cap_termi.Set();
            update("终止抓屏线程\r\n");
        }

        private delegate void updateDelegate(string comment);
        public void update(string comment)
        {
            if(!lbx_disp.Dispatcher.CheckAccess())
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
