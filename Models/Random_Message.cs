using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace KhaoPiyo.Models
{
    [DataContract]
    public class Random_Message1
    {
        [DataMember]
        public string status { get; set; }
        [DataMember]
        public string message { get; set; }
        [DataMember]
        public string reference_id { get; set; }
        [DataMember]
        public string reference { get; set; }
    }
    [DataContract]
    public class Random_Message2
    {
        [DataMember]
        public string status { get; set; }
        [DataMember]
        public string message { get; set; }
        [DataMember]
        public string reference_id { get; set; }
    }

   

}