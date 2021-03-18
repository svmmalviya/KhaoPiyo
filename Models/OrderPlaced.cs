using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

//namespace KhaoPiyo.Models
//{
//    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
//    [DataContract]
//    public class Address
//    {
//        [DataMember]
//        public string city { get; set; }
//        [DataMember]
//        public bool is_guest_mode { get; set; }
//        [DataMember]
//        public string line_1 { get; set; }
//        [DataMember]
//        public string line_2 { get; set; }
//        [DataMember]
//        public string landmark { get; set; }
//        [DataMember]
//        public double latitude { get; set; }
//        [DataMember]
//        public double longitude { get; set; }
//        [DataMember]
//        public string sub_locality { get; set; }
//        [DataMember]
//        public string pin { get; set; }
//        [DataMember]
//        public string tag { get; set; }
//    }

//    [DataContract]
//    public class Customer
//    {
//        [DataMember]
//        public Address address { get; set; }
//        [DataMember]
//        public string email { get; set; }
//        [DataMember]
//        public string name { get; set; }
//        [DataMember]
//        public string phone { get; set; }
//    }

//    [DataContract]
//    public class Tax
//    {
//        [DataMember]

//        public string title { get; set; }
//        [DataMember]

//        public double value { get; set; }
//        [DataMember]

//        public double rate { get; set; }
//    }

//    [DataContract]
//    public class Charge
//    {
//        [DataMember]

//        public List<Tax> taxes { get; set; }
//        [DataMember]

//        public string title { get; set; }
//        [DataMember]

//        public double value { get; set; }
//    }

//    public class Extras
//    {
//        public string order_type { get; set; }
//        public bool thirty_minutes_delivery { get; set; }
//        public double cash_to_be_collected { get; set; }
//    }

//    public class Discount
//    {
//        public bool is_merchant_discount { get; set; }
//        public double rate { get; set; }
//        public string title { get; set; }
//        public double value { get; set; }
//        public string code { get; set; }
//    }

//    public class ExtPlatform
//    {
//        public string id { get; set; }
//        public string kind { get; set; }
//        public string name { get; set; }
//        public string delivery_type { get; set; }
//        public Extras extras { get; set; }
//        public List<Discount> discounts { get; set; }
//    }

//    [DataContract]
//    public class Details
//    {
//        [DataMember]
//        public int biz_id { get; set; }
//        [DataMember]
//        public string channel { get; set; }
//        [DataMember]
//        public List<Charge> charges { get; set; }
//        [DataMember]
//        public string coupon { get; set; }
//        [DataMember]
//        public long created { get; set; }
//        [DataMember]
//        public long delivery_datetime { get; set; }
//        [DataMember]
//        public double discount { get; set; }
//        [DataMember]
//        public double total_external_discount { get; set; }
//        [DataMember]
//        public List<ExtPlatform> ext_platforms { get; set; }
//        [DataMember]
//        public int id { get; set; }
//        [DataMember]
//        public string instructions { get; set; }
//        [DataMember]
//        public double item_level_total_charges { get; set; }
//        [DataMember]
//        public double item_level_total_taxes { get; set; }
//        [DataMember]
//        public double item_taxes { get; set; }
//        [DataMember]
//        public string merchant_ref_id { get; set; }
//        [DataMember]
//        public int order_level_total_charges { get; set; }
//        [DataMember]
//        public double order_level_total_taxes { get; set; }
//        [DataMember]
//        public string order_state { get; set; }
//        [DataMember]
//        public double order_subtotal { get; set; }
//        [DataMember]
//        public double order_total { get; set; }
//        [DataMember]
//        public double payable_amount { get; set; }
//        [DataMember]
//        public string time_slot_end { get; set; }
//        [DataMember]
//        public string time_slot_start { get; set; }
//        [DataMember]
//        public string order_type { get; set; }
//        [DataMember]
//        public string state { get; set; }
//        [DataMember]
//        public List<Tax> taxes { get; set; }
//        [DataMember]
//        public double total_charges { get; set; }
//        [DataMember]
//        public double total_taxes { get; set; }
//    }

//    [DataContract]
//    public class OptionsToAdd
//    {
//        [DataMember]
//        public int id { get; set; }
//        [DataMember]
//        public string merchant_id { get; set; }
//        [DataMember]
//        public double price { get; set; }
//        [DataMember]
//        public string title { get; set; }
//    }

//    [DataContract]
//    public class Item
//    {
//        [DataMember]
//        public List<Charge> charges { get; set; }
//        [DataMember]

//        public double discount { get; set; }
//        [DataMember]

//        public string food_type { get; set; }
//        [DataMember]

//        public int id { get; set; }
//        [DataMember]

//        public object image_landscape_url { get; set; }
//        [DataMember]
//        public object image_url { get; set; }
//        [DataMember]
//        public string merchant_id { get; set; }
//        [DataMember]
//        public List<OptionsToAdd> options_to_add { get; set; }
//        [DataMember]
//        public List<object> options_to_remove { get; set; }
//        [DataMember]
//        public double price { get; set; }
//        [DataMember]
//        public int quantity { get; set; }
//        [DataMember]
//        public List<Tax> taxes { get; set; }
//        [DataMember]
//        public string title { get; set; }
//        [DataMember]
//        public double total { get; set; }
//        [DataMember]
//        public double total_with_tax { get; set; }
//        [DataMember]
//        public double unit_weight { get; set; }
//    }

//    [DataContract]
//    public class Payment
//    {
//        [DataMember]
//        public double amount { get; set; }
//        [DataMember]
//        public string option { get; set; }
//        [DataMember]
//        public object srvr_trx_id { get; set; }
//    }

//    public class OStore
//    {
//        public string address { get; set; }
//        public int id { get; set; }
//        public double latitude { get; set; }
//        public double longitude { get; set; }
//        public string merchant_ref_id { get; set; }
//        public string name { get; set; }
//    }

//    [DataContract]
//    public class Order
//    {
//        [DataMember]
//        public Details details { get; set; }
//        [DataMember]
//        public List<Item> items { get; set; }
//        [DataMember]
//        public string next_state { get; set; }
//        [DataMember]
//        public List<string> next_states { get; set; }
//        [DataMember]
//        public List<Payment> payment { get; set; }
//        [DataMember]
//        public OStore store { get; set; }
//    }

//    [DataContract]
//    public class OrderPlaced
//    {
//        [DataMember]
//        public Customer customer { get; set; }
//        [DataMember]
//        public Order order { get; set; }
//    }

//    [DataContract]
//    public class OS
//    {
//        [DataMember]
//        public string address { get; set; }
//        [DataMember]
//        public int id { get; set; }
//        [DataMember]
//        public List<string> merchant_ref_id { get; set; }
//        [DataMember]
//        public string name { get; set; }
//    }

//}

namespace System.MyOrderModel {

    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
    [DataContract]
    public class Address
    {
        [DataMember]
        public string city { get; set; }
        [DataMember]
        public bool is_guest_mode { get; set; }
        [DataMember]
        public string line_1 { get; set; }
        [DataMember]
        public object line_2 { get; set; }
        [DataMember]
        public object landmark { get; set; }
        [DataMember]
        public double latitude { get; set; }
        [DataMember]
        public double longitude { get; set; }
        [DataMember]
        public string sub_locality { get; set; }
        [DataMember]
        public string pin { get; set; }
        [DataMember]
        public string tag { get; set; }
    }

    [DataContract]
    public class Customer
    {
        [DataMember]
        public Address address { get; set; }
        [DataMember]
        public string email { get; set; }
        [DataMember]
        public string name { get; set; }
        [DataMember]
        public string phone { get; set; }
    }


    [DataContract]
    public class Tax
    {
        [DataMember]
        public string title { get; set; }
        [DataMember]
        public double value { get; set; }
        [DataMember]
        public double rate { get; set; }
    }

    [DataContract]
    public class Charge
    {
        [DataMember]
        public List<Tax> taxes { get; set; }
        [DataMember]
        public string title { get; set; }
        [DataMember]
        public double value { get; set; }
    }


    [DataContract]
    public class Extras
    {
        [DataMember]
        public string order_type { get; set; }
        [DataMember]
        public bool thirty_minutes_delivery { get; set; }
        [DataMember]
        public double cash_to_be_collected { get; set; }
    }

    [DataContract]
    public class Discount
    {
        [DataMember]
        public bool is_merchant_discount { get; set; }
        [DataMember]
        public double rate { get; set; }
        [DataMember]
        public string title { get; set; }
        [DataMember]
        public double value { get; set; }
        [DataMember]
        public string code { get; set; }
    }

    [DataContract]
    public class ExtPlatform
    {
        [DataMember]
        public string id { get; set; }
        [DataMember]
        public string kind { get; set; }
        [DataMember]
        public string name { get; set; }
        [DataMember]
        public string delivery_type { get; set; }
        [DataMember]
        public Extras extras { get; set; }
        [DataMember]
        public List<Discount> discounts { get; set; }
    }

    [DataContract]
    public class Details
    {
        [DataMember]
        public int biz_id { get; set; }
        [DataMember]
        public string channel { get; set; }
        [DataMember]
        public List<Charge> charges { get; set; }
        [DataMember]
        public string coupon { get; set; }
        [DataMember]
        public long created { get; set; }
        [DataMember]
        public long delivery_datetime { get; set; }
        [DataMember]
        public double discount { get; set; }
        [DataMember]
        public double total_external_discount { get; set; }
        [DataMember]
        public List<ExtPlatform> ext_platforms { get; set; }
        [DataMember]
        public int id { get; set; }
        [DataMember]
        public string instructions { get; set; }
        [DataMember]
        public double item_level_total_charges { get; set; }
        [DataMember]
        public double item_level_total_taxes { get; set; }
        [DataMember]
        public double item_taxes { get; set; }
        [DataMember]
        public object merchant_ref_id { get; set; }
        [DataMember]
        public double order_level_total_charges { get; set; }
        [DataMember]
        public double order_level_total_taxes { get; set; }
        [DataMember]
        public string order_state { get; set; }
        [DataMember]
        public double order_subtotal { get; set; }
        [DataMember]
        public double order_total { get; set; }
        [DataMember]
        public double payable_amount { get; set; }
        [DataMember]
        public string time_slot_end { get; set; }
        [DataMember]
        public string time_slot_start { get; set; }
        [DataMember]
        public string order_type { get; set; }
        [DataMember]
        public string state { get; set; }
        [DataMember]
        public List<Tax> taxes { get; set; }
        [DataMember]
        public double total_charges { get; set; }
        [DataMember]
        public double total_taxes { get; set; }
    }


    [DataContract]
    public class OptionsToAdd
    {
        [DataMember]
        public int id { get; set; }
        [DataMember]
        public string merchant_id { get; set; }
        [DataMember]
        public double price { get; set; }
        [DataMember]
        public object translations { get; set; }
        [DataMember]
        public object unit_weight { get; set; }

        [DataMember]
        public string title { get; set; }
    }


    [DataContract]
    public class Item
    {
        [DataMember]
        public List<Charge> charges { get; set; }

        [DataMember]
        public double discount { get; set; }
        [DataMember]
        public string food_type { get; set; }
        [DataMember]
        public int id { get; set; }
        [DataMember]
        public object image_landscape_url { get; set; }
        [DataMember]
        public object image_url { get; set; }
        [DataMember]
        public string merchant_id { get; set; }
        [DataMember]
        public List<OptionsToAdd> options_to_add { get; set; }
        [DataMember]
        public List<object> options_to_remove { get; set; }
        [DataMember]
        public double price { get; set; }
        [DataMember]
        public int quantity { get; set; }
        [DataMember]
        public List<Tax> taxes { get; set; }
        [DataMember]
        public string title { get; set; }
        [DataMember]
        public double total { get; set; }
        [DataMember]
        public double total_with_tax { get; set; }
        public object translations { get; set; }
        [DataMember]
        public object unit_weight { get; set; }
    }

    [DataContract]
    public class Payment
    {

        [DataMember]
        public double amount { get; set; }
        [DataMember]
        public string option { get; set; }
        [DataMember]
        public object srvr_trx_id { get; set; }
    }

    [DataContract]
    public class Store
    {
        [DataMember]
        public string address { get; set; }
        [DataMember]
        public int id { get; set; }
        [DataMember]
        public object latitude { get; set; }
        [DataMember]
        public object longitude { get; set; }
        [DataMember]
        public string merchant_ref_id { get; set; }
        [DataMember]
        public string name { get; set; }
    }

    [DataContract]
    public class Order
    {
        [DataMember]
        public Details details { get; set; }
        [DataMember]
        public List<Item> items { get; set; }
        [DataMember]
        public string next_state { get; set; }
        [DataMember]
        public List<string> next_states { get; set; }
        [DataMember]
        public List<Payment> payment { get; set; }
        [DataMember]
        public Store store { get; set; }
    }

    [DataContract]
    public class SetOrderObject
    {
        [DataMember]
        public Customer customer { get; set; }
        [DataMember]
        public Order order { get; set; }
    }



}