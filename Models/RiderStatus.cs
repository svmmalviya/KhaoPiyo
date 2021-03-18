using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace KhaoPiyo.Models
{
    public class RiderExternalChannel
    {
        public string name { get; set; }
        public string order_id { get; set; }
    }

    [DataContract]
    public class RiderAdditionalInfo
    {
        [DataMember]
        public RiderExternalChannel external_channel { get; set; }
    }

    [DataContract]
    public class DeliveryPersonDetails
    {
        [DataMember]
        public string alt_phone { get; set; }
        [DataMember]
        public string name { get; set; }
        [DataMember]
        public string phone { get; set; }
        [DataMember]
        public long user_id { get; set; }
    }

    [DataContract]
    public class StatusUpdate
    {
        [DataMember]
        public string comments { get; set; }
        [DataMember]
        public string created { get; set; }
        [DataMember]
        public string status { get; set; }
    }

    [DataContract]
    public class DeliveryInfo
    {
        [DataMember]
        public string current_state { get; set; }
        [DataMember]
        public DeliveryPersonDetails delivery_person_details { get; set; }
        [DataMember]
        public string mode { get; set; }
        [DataMember]
        public List<StatusUpdate> status_updates { get; set; }
    }

    [DataContract]
    public class Store
    {
        [DataMember]
        public long id { get; set; }

        [DataMember]
        public string ref_id { get; set; }
    }

    [DataContract]
    public class RiderStatus
    {
        [DataMember]
        public RiderAdditionalInfo additional_info { get; set; }
        [DataMember]
        public DeliveryInfo delivery_info { get; set; }
        [DataMember]
        public long order_id { get; set; }
        [DataMember]
        public Store store { get; set; }
    }




}