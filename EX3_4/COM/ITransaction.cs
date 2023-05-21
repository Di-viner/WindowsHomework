using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;


namespace COM
{
    
    [Guid("24F0FA46-A80B-43C6-9120-AEC450E654CC")]
    [ComVisible(true)]
    public interface ITransaction
    {    
        void Connect(string connectString);
        void Disconnect();

        string GetVersion();
        string Add1(double a, double b);
        string Sub1(double a, double b);
        string Multi1(double a, double b);
        string Diverse1(double a, double b);

        //string add(int a, int b);
        //string multi(int a, int b);
        
    }
}
