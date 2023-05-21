using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;

namespace RestfulService
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.Title = "Restful服务端测试";
                PersonInfoQueryServices service = new PersonInfoQueryServices();
                WebServiceHost _serviceHost = new WebServiceHost(service, new Uri("http://127.0.0.1:7788/"));
                //或者第二种方法：WebServiceHost _serviceHost = new WebServiceHost(typeof(PersonInfoQueryServices), new Uri("http://127.0.0.1:7788/"));
                _serviceHost.Open();
                Console.WriteLine("Web服务已开启...\n");
                Console.ReadKey();
                _serviceHost.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Web服务开启失败：{0}\r\n{1}", ex.Message, ex.StackTrace);
            }

        }
    }
}
