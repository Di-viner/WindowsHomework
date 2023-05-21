using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Text;
using System.Threading.Tasks;

namespace RestfulService
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Single, IncludeExceptionDetailInFaults = true)]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class PersonInfoQueryServices : IPersonInfoQuery
    {
        private List<User> UserList = new List<User>();
        private int num_user = 0;

        public PersonInfoQueryServices()
        {
            for(int i = 0; i < 10; i++)
            {
                User user = new User() { ID = ++num_user,Name ="User"+num_user,Age = 18+num_user,Score = 100 - num_user};
                UserList.Add(user);
            }
        }

        public User GetScore(string name)
        {
            var user = UserList.FirstOrDefault(n => n.Name == name);
            if (user == null)
            {
                Console.WriteLine("请求数据失败，无法找到该用户\n");
                return null;
            }
            return user;
        }

        public User GetScore2(string name)
        {
            var user = UserList.FirstOrDefault(n => n.Name == name);
            if (user == null)
            {
                Console.WriteLine("请求数据失败，无法找到该用户\n");
                return null;
            }
            return user;
        }

        public User AddUser(User user)
        {
            if (user == null)
            {
                Console.WriteLine("添加失败，用户为null\n");
                return null;
            }
            foreach (var item in UserList)
            {
                if(item.ID == user.ID)
                {
                    Console.WriteLine("添加失败，用户已存在\n");
                    return item;
                }
            }
            num_user++;
            UserList.Add(user);
            return user;
        }

        public User UpdateScore(string name)
        {
            User user = UserList.FirstOrDefault(n => n.Name == name);
            if (user == null)
            {
                Console.WriteLine("更新成绩失败，无法找到用户名\n");
                return null;
            }
            user.Score = 60;
            return user;
        }

        public User DeleteUser(string name)
        {
            User user = UserList.FirstOrDefault(n => n.Name == name);
            if (user == null)
            {
                Console.WriteLine("删除用户失败，无此用户\n");
                return null;
            }
            num_user--;
            UserList.Remove(user); 
            return user;
        }

    }
}