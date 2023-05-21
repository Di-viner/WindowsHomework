using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace RestfulClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Restful客户端Demo测试";

            RestClient client = new RestClient();
            client.EndPoint = @"http://127.0.0.1:7788/";

            client.Method = EnumHttpVerb.GET;
            string resultGet = client.HttpRequest("PersonInfoQuery/User1");
            Console.WriteLine("GET方式获取结果：" + resultGet + "\n");

            client.Method = EnumHttpVerb.GET;
            string resultGet2 = client.HttpRequest("PersonInfoQuery/Xml/User1");
            Console.WriteLine("GET方式获取Xml结果：" + resultGet2 + "\n");

            client.Method = EnumHttpVerb.POST;
            User u = new User() { ID = 11043, Name = "Wu Siye", Age = 21, Score = 100 };
            client.PostData = JsonConvert.SerializeObject(u);
            var resultPost2 = client.HttpRequest("PersonInfoQuery/User");
            Console.WriteLine("POST方式添加结果：" + resultPost2 + "\n");

            client.Method = EnumHttpVerb.PUT;
            var resultPut2 = client.HttpRequest("PersonInfoQuery/User1");
            Console.WriteLine("PUT方式更改后的结果：" + resultPut2 + "\n");

            client.Method = EnumHttpVerb.DELETE;
            var resultDelete1 = client.HttpRequest("PersonInfoQuery/User2");
            Console.WriteLine("DELETE方式删除的结果：" + resultDelete1 + "\n");

            Console.Read();

        }
    }

    [Serializable]
    public class Info
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }

    [Serializable]
    public class User
    {
        public int ID { get; set; }
        public string Name { get; set; }       
        public int Age { get; set; }
        public int Score { get; set; }
    }
}