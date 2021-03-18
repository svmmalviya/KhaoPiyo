using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace KhaoPiyo.Models
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
    [DataContract]
    public class ExternalChannel
    {
        [DataMember]
        public string name { get; set; }
        [DataMember]
        public string order_id { get; set; }
    }

    [DataContract]
    public class AdditionalInfo
    {
        [DataMember]
        public ExternalChannel external_channel { get; set; }
    }

    [DataContract]
    public class Updater
    {
        [DataMember]
        public string name { get; set; }
        [DataMember]
        public string username { get; set; }
    }


    [DataContract]
    public class OrderStatus
    {
        [DataMember]
        public AdditionalInfo additional_info { get; set; }
        [DataMember]
        public string message { get; set; }
        [DataMember]
        public string new_state { get; set; }
        [DataMember]
        public int order_id { get; set; }
        [DataMember]
        public string prev_state { get; set; }
        [DataMember]
        public string store_id { get; set; }
        [DataMember]
        public string timestamp { get; set; }
        [DataMember]
        public long timestamp_unix { get; set; }
        [DataMember]
        public Updater updater { get; set; }

    }


}