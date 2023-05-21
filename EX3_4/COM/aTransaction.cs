using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace COM
{
    [Guid("B4877881-85CA-442D-9910-00D230F21691")]
    [ComVisible(true)]
    [ClassInterface(ClassInterfaceType.None)]
    [Description("第一个实现")]
    public class aTransaction:ITransaction
    {        
        [DllImport(@"D:\Learning\Windows\EX3_2\x64\Release\CppDLL.dll", EntryPoint = "Add", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
        public static extern double Add(double a, double b);

        [DllImport(@"D:\Learning\Windows\EX3_2\x64\Release\CppDLL.dll", EntryPoint = "Sub", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
        public static extern double Sub(double a, double b);

        [DllImport(@"D:\Learning\Windows\EX3_2\x64\Release\CppDLL.dll", EntryPoint = "Multi", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
        public static extern double Multi(double a, double b);

        [DllImport(@"D:\Learning\Windows\EX3_2\x64\Release\CppDLL.dll", EntryPoint = "Diverse", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
        public static extern double Diverse(double a, double b);
        public void Connect(string connectString){}
        public void Disconnect(){}
        public string GetVersion()
        {
            return "1.0";
        }
        public string Add1(double a, double b)
        {
            return Add(a, b).ToString();
        }

        public string Sub1(double a, double b)
        {
            return Sub(a, b).ToString();
        }

        public string Multi1(double a, double b)
        {
            return Multi(a, b).ToString();
        }

        public string Diverse1(double a, double b)
        {
            return Diverse(a, b).ToString();
        }
    }
}
