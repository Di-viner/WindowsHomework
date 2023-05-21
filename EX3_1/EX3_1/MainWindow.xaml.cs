using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
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
using static System.Environment;

namespace EX3_1
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        string path = null;
        string destination;
        string[] files; 
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btn_selectPath_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            folderBrowserDialog.ShowDialog();
            if (folderBrowserDialog.SelectedPath != null)
            {
                path = folderBrowserDialog.SelectedPath;
                lbl_pathName.Content = path;
            }
        }

        private void btn_findFile_Click(object sender, RoutedEventArgs e)
        {
            if (!Directory.Exists(path))
                return;
            files = Directory.GetFiles(path, txb_findFileName.Text, SearchOption.AllDirectories);
            lbx_allFile.Items.Clear();
            foreach (string file in files)
                lbx_allFile.Items.Add(file);
            lbx_allFile.SelectedIndex = lbx_allFile.Items.Count - 1;
        }

        private void btn_addToTarget_Click(object sender, RoutedEventArgs e)
        {
            var item = lbx_allFile.SelectedItem as string;
            if (item != null && !lbx_target.Items.Contains(item))
                lbx_target.Items.Add(item);
            lbx_target.SelectedIndex = lbx_target.Items.Count - 1;
        }

        private void btn_clrTarget_Click(object sender, RoutedEventArgs e)
        {
            lbx_target.Items.Clear();
        }

        private void btn_up_Click(object sender, RoutedEventArgs e)
        {
            int selectedIndex = lbx_target.SelectedIndex;
            if (selectedIndex <= 0)
                return;
            string s = (string)lbx_target.Items[selectedIndex];
            lbx_target.Items[selectedIndex] = lbx_target.Items[selectedIndex - 1];
            lbx_target.Items[selectedIndex - 1] = s;
            
        }

        private void btn_down_Click(object sender, RoutedEventArgs e)
        {
            int selectedIndex = lbx_target.SelectedIndex;
            if (selectedIndex >= lbx_target.Items.Count - 1)
                return;
            string s = (string)lbx_target.Items[selectedIndex];
            lbx_target.Items[selectedIndex] = lbx_target.Items[selectedIndex + 1];
            lbx_target.Items[selectedIndex + 1] = s;           
        }

        private void btn_save_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Title = "设置合并文件";
            saveFileDialog.InitialDirectory = SpecialFolder.DesktopDirectory.ToString();
            if(saveFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                destination = saveFileDialog.FileName;
                saveFileDialog.OverwritePrompt = false;
                lbl_fileSaved.Content = destination;
            }
        }

        private void btn_merge_Click(object sender, RoutedEventArgs e)
        {
            if(destination == null || destination.Length == 0)
            {
                System.Windows.MessageBox.Show("请设置合并文件名和路径");
                return;
            }
            if(File.Exists(destination))
                File.Delete(destination);     
            foreach (string item in lbx_target.Items)
            {
                using (StreamReader sr = new StreamReader(item))
                {
                    using (StreamWriter sw = new StreamWriter(destination, true)) 
                    {
                        while(!sr.EndOfStream)
                            sw.WriteLine(sr.ReadLine());
                    }
                }
            }
        }
    }
}
