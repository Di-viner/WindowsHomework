using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace RestfulService
{
    [DataContract]
    public class Info
    {
        [DataMember]
        public int ID { get; set; }

        [DataMember]
        public string Name { get; set; }
    }
}