using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;

namespace RestfulService
{
    /// <summary>
    /// 简单定义两种方法，1、GetScore方法：通过GET请求传入name，返回对应的成绩；2、GetInfo方法：通过POST请求，传入Info对象，查找对应的User并返回给客户端
    /// </summary>
    [ServiceContract(Name = "PersonInfoQueryServices")]
    public interface IPersonInfoQuery
    {

        /// <summary>
        /// Json格式获取成绩
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [OperationContract]
        [WebGet(UriTemplate = "PersonInfoQuery/{name}", BodyStyle = WebMessageBodyStyle.Bare, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        User GetScore(string name);

        /// <summary>
        /// Xml格式获取数据
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [OperationContract]
        [WebGet(UriTemplate = "PersonInfoQuery/Xml/{name}", BodyStyle = WebMessageBodyStyle.Bare, RequestFormat = WebMessageFormat.Xml, ResponseFormat = WebMessageFormat.Xml)]
        User GetScore2(string name);

        /// <summary>
        /// 添加记录
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "PersonInfoQuery/User", BodyStyle = WebMessageBodyStyle.Bare, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        User AddUser(User user);

        /// <summary>
        /// 更新成绩
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "PUT", UriTemplate = "PersonInfoQuery/{name}", BodyStyle = WebMessageBodyStyle.Bare, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        User UpdateScore(string name);

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "DELETE", UriTemplate = "PersonInfoQuery/{name}", BodyStyle = WebMessageBodyStyle.Bare, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        User DeleteUser(string name);
    }
}