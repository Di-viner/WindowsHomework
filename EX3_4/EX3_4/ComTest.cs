using COM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EX3_4
{
    public class ComTest
    {
        public static ITransaction CreateTransaction(string _guid, string connectionStr)
        {
            ITransaction iTransaction = null;
            try
            {
                Guid guid = new Guid(_guid);
                Type transactionType = Type.GetTypeFromCLSID(guid);
                object transaction = Activator.CreateInstance(transactionType);
                iTransaction = transaction as ITransaction;
                iTransaction.Connect(connectionStr);
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
            }
            return iTransaction;
        }

        public static string add(string guid, string connectionStr, int a, int b)
        {
            ITransaction transaction = CreateTransaction(guid, connectionStr);
            return transaction.Add1(a, b);
        }

        public static string multi(string guid, string connectionStr, int a, int b)
        {
            ITransaction transaction = CreateTransaction(guid, connectionStr);
            return transaction.Multi1(a, b);
            //return transaction.multi(a, b);
        }

        public static string sub(string guid, string connectionStr, int a, int b)
        {
            ITransaction transaction = CreateTransaction(guid, connectionStr);
            return transaction.Sub1(a,b);
        }

        public static string diverse(string guid, string connectionStr, int a, int b)
        {
            ITransaction transaction = CreateTransaction(guid, connectionStr);
            return transaction.Diverse1(a, b);
        }
    }
}
