using Microsoft.Win32;
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

namespace EX3_3
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

		static readonly IntPtr HKEY_CURRENT_USER = new IntPtr(unchecked((int)0x80000001));
		static IntPtr currenthKey = HKEY_CURRENT_USER;//打开的注册表项的句柄

		static int STANDARD_RIGHTS_ALL = (0x001F0000);
		static int KEY_QUERY_VALUE = (0x0001);
		static int KEY_SET_VALUE = (0x0002);
		static int KEY_CREATE_SUB_KEY = (0x0004);
		static int KEY_ENUMERATE_SUB_KEYS = (0x0008);
		static int KEY_NOTIFY = (0x0010);
		static int KEY_CREATE_LINK = (0x0020);
		static int SYNCHRONIZE = (0x00100000);
		static int REG_OPTION_NON_VOLATILE = (0x00000000);
		static int KEY_ALL_ACCESS = (STANDARD_RIGHTS_ALL | KEY_QUERY_VALUE | KEY_SET_VALUE |
									 KEY_CREATE_SUB_KEY | KEY_ENUMERATE_SUB_KEYS | KEY_NOTIFY | KEY_CREATE_LINK) & (~SYNCHRONIZE);//一个掩码，指定要创建的密钥的访问权限





		[DllImport("Advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern int RegOpenKeyEx(IntPtr hKey, string lpSubKey, uint ulOptions, int samDesired, out IntPtr phkResult);

		//创建或打开Key值
		[DllImport("Advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern int RegCreateKeyEx(IntPtr hKey, string lpSubKey, int reserved, string type, int dwOptions, int REGSAM,
			IntPtr lpSecurityAttributes, out IntPtr phkResult, out int lpdwDisposition);

		[DllImport("advapi32.dll", SetLastError = true)]
		private static extern uint RegQueryValueEx(IntPtr hKey, string lpValueName, int lpReserved, ref RegistryValueKind lpType,
			System.Text.StringBuilder lpData, ref int lpcbData);

		//设置Key值
		[DllImport("Advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern int RegSetValueEx(IntPtr hKey, string lpValueName, uint unReserved, uint unType, byte[] lpData, uint dataCount);

		//删除键
		[DllImport("Advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern int RegDeleteKeyValue(IntPtr hKey, string lpSubKey, string lpValueName);

		//关闭Key值
		[DllImport("Advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern int RegCloseKey(IntPtr hKey);

		[DllImport("Advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern int RegDeleteKey(IntPtr hKey, string lpSubKey);

        private void btnSubKeyCreate_Click(object sender, RoutedEventArgs e)
        {
			//private static extern int RegCreateKeyEx(IntPtr hKey, string lpSubKey, int reserved, string type, int dwOptions, int REGSAM,
			//IntPtr lpSecurityAttributes, out IntPtr phkResult, out int lpdwDisposition);
			
			IntPtr pHKey = IntPtr.Zero;
			int lpdwDisposition = 0;
			int ret = RegCreateKeyEx(currenthKey, txbSubKey.Text, 0, "", REG_OPTION_NON_VOLATILE,
				KEY_ALL_ACCESS, IntPtr.Zero, out pHKey, out lpdwDisposition);
			if (ret == 0)
			{
				MessageBox.Show("创建成功！");
			}
			else
			{
				MessageBox.Show("创建失败！");
			}
			RegCloseKey(pHKey);
		}

        private void btnSubKeyDelete_Click(object sender, RoutedEventArgs e)
        {
			if (0 == RegDeleteKey(currenthKey, txbSubKey.Text))
			{
				MessageBox.Show("删除项成功！");
			}
			else
			{
				MessageBox.Show("删除项失败！");
			}
		}

        private void btnKeyCreate_Click(object sender, RoutedEventArgs e)
        {
			IntPtr pHKey = IntPtr.Zero;//一个变量的指针，此变量指向已打开或已创建的项的句柄。 
			int lpdwDisposition = 0;
			int ret = RegCreateKeyEx(currenthKey, txbSubKey.Text, 0, "", REG_OPTION_NON_VOLATILE,
				KEY_ALL_ACCESS, IntPtr.Zero, out pHKey, out lpdwDisposition);
			//设置访问的Key值
			uint REG_SZ = 1;
			//要存储的数据
			byte[] data = Encoding.Unicode.GetBytes(txbValue.Text);
			int success = RegSetValueEx(pHKey, txbKey.Text, 0, REG_SZ, data, (uint)data.Length);
			MessageBox.Show("创建/修改键成功！");
			RegCloseKey(pHKey);
		}

        private void btnKeyRead_Click(object sender, RoutedEventArgs e)
        {
			IntPtr hKey;//一个变量的指针，此变量指向已打开键的句柄。
			//打开目录
			if (0 == RegOpenKeyEx(currenthKey, txbSubKey.Text, 0, KEY_ALL_ACCESS, out hKey))
			{
				int size = 0;
				RegistryValueKind type = RegistryValueKind.Unknown;
				RegQueryValueEx(hKey, txbKey.Text, 0, ref type, null, ref size);
				System.Text.StringBuilder data = new System.Text.StringBuilder(size);
				if (0 == RegQueryValueEx(hKey, txbKey.Text, 0, ref type, data, ref size))
				{
					MessageBox.Show("当前查询键：" + txbKey.Text + "  对应值：" + data.ToString());
				}
				else
				{
					MessageBox.Show("查询失败，不存在该键！");
				}
			}
			else
			{
				MessageBox.Show("不存在");
			}
		}

        private void btnKeyDelete_Click(object sender, RoutedEventArgs e)
        {
			if (0 == RegDeleteKeyValue(currenthKey, txbSubKey.Text, txbKey.Text))
			{
				MessageBox.Show("删除键成功！");
			}
			else
			{
				MessageBox.Show("删除键失败！");
			}
		}
    }
}
