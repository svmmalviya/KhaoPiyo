using Classes;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.IO;
using System.Messaging;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using Newtonsoft.Json;
using System.Text;
using Ionic.Zip;
using KhaoPiyo.Models;
using Org.BouncyCastle.Asn1.Ocsp;
using System.MyOrderModel;
using System.Linq;
using System.Data.Entity.Validation;

namespace KhaoPiyo
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {


        MyDBContext entities;

        List<ConnectionsWithIds> ConnectionsWithIds = new List<ConnectionsWithIds>();

        public Service1()
        {

            Log.strLogPath = ConfigurationManager.AppSettings["LogDirectory"].ToString();

            //string[] connectionStrings = ConfigurationManager.AppSettings["ConnectionStrings"].ToString().Split('|');
            //string[] restaurauntIds = ConfigurationManager.AppSettings["RestaurantIds"].ToString().Split('|');


            //if (connectionStrings.Length > 0 && restaurauntIds.Length > 0)
            //{
            //    var ConWithIDs = connectionStrings.Zip(restaurauntIds, (c, rid) => new { connnectionstring = c, rastrauntid = rid });

            //    foreach (var str in ConWithIDs)
            //    {
            //        ConnectionsWithIds.Add(new KhaoPiyo.ConnectionsWithIds { ConnectionString = str.connnectionstring, RstaurantId = str.rastrauntid });
            //    }
            //}
        }



        public Reply test(string ss)
        {
            return new Reply { Message = ss, Response = true, StatusCode = HttpCustCode.success };
        }


        public Reply SetOrder(SetOrderObject order)  //With object
        {



            //SetConnection(Merchent_Ref_Id: order.order.store.merchant_ref_id);

            entities = new MyDBContext();


            //        entities.ChangeDatabase
            //(
            //    initialCatalog: GlobalMembers.database,
            //    userId: "sa",
            //    password: "Pranav@6",
            //    dataSource: @"127.0.0.1", // could be ip address 120.273.435.167 etc
            //    configConnectionStringName:"MyDBContext"
            //);

            //local properties
            Reply reply = new Reply();                                //local properties
            string opetions_to_add = "";                              //local properties
            string item_Options_To_Add_Rate = "";                     //local properties
            System.MyOrderModel.Charge OrderDetailcharges = new System.MyOrderModel.Charge { };               //local properties
                                               //local properties


            Log.Write("In SetOrder", "");

            Log.Write("-----------------------------------------------Request object-----------------------------------------------------------", "nt");

            Log.Write(Newtonsoft.Json.JsonConvert.SerializeObject(order), "nt");

            Log.Write("-------------------------------------------------------------------------------------------------------------------------", "nt");

            if (order != null && order.customer != null && order.order != null)
            {
                //if (order.order.details.channel != "")
                //    Log.Write("Data received :-" + Newtonsoft.Json.JsonConvert.SerializeObject(order), order.order.details.channel);


                try
                {
                    if (order.order.items != null)
                    {
                        entities.Customer_Master.Add(new Customer_Master
                        {
                            Biz_Id = order.order.details.biz_id,
                            Order_Id = order.order.details.ext_platforms.Count > 0 ? order.order.details.ext_platforms[0].id : "0",
                            Customer_Name = order.customer.name,
                            UPR_Order_Id = order.order.details.id,
                            Customer_Address = order.customer.address.sub_locality,
                            Customer_Phone = order.customer.phone,
                            Customer_Email = order.customer.email,
                            Customer_Landmark = order.customer.address.landmark != null ? order.customer.address.landmark.ToString() : "",
                            Store_Id = order.order.store.merchant_ref_id != null ? order.order.store.merchant_ref_id.ToString() : "",
                        });

                        if (order.order.details.charges != null && order.order.details.charges.Count != 0)
                        {
                            OrderDetailcharges.title = order.order.details.charges[0].title;
                            OrderDetailcharges.value = order.order.details.charges[0].value;
                        }
                        else
                        {
                            OrderDetailcharges.title = "";
                            OrderDetailcharges.value = 0;
                        }

                        entities.Order_Master.Add(new Order_Master
                        {
                            Biz_Id = order.order.details.biz_id,
                            Store_Id = order.order.store != null ? order.order.store.merchant_ref_id.ToString() : "",
                            Order_Id = order.order.details.ext_platforms.Count > 0 ? order.order.details.ext_platforms[0].id : "0",
                            Channel = order.order.details.channel,
                            UPR_Order_Id = order.order.details.id,
                            Order_Subtotal = order.order.details.order_subtotal,
                            Order_Tax = order.order.details.total_taxes,
                            Order_Total = order.order.details.order_total,
                            OrderTime = Convert.ToString(order.order.details.created),
                            DeliveryTime = Convert.ToString(order.order.details.delivery_datetime),
                            OrderValue = 1,
                            Order_Charges = order.order.details.total_charges,
                            Order_Coupon = order.order.details.coupon,
                            Order_Instructions = order.order.details.instructions,
                            Order_Discount = order.order.details.discount,
                            Charges_Value = OrderDetailcharges.value,
                            Charges_Title = OrderDetailcharges.title,
                            External_Discount = order.order.details.total_external_discount,
                            Order_Type = order.order.details.order_type,
                            Delivery_Type = order.order.details.ext_platforms.Count > 0 ? order.order.details.ext_platforms[0].delivery_type : "",
                            Payment_Type = order.order.payment.Count > 0 ? order.order.payment[0].option : "",
                        });


                        foreach (var item in order.order.items)
                        {
                            double charges = 0;
                            opetions_to_add = "";
                            item_Options_To_Add_Rate = "";

                            foreach (var item1 in item.options_to_add)
                            {
                                opetions_to_add += item1.title;
                                item_Options_To_Add_Rate += item1.price;

                                if (order.order.items.Count > 1)
                                {
                                    opetions_to_add += " / ";
                                    item_Options_To_Add_Rate += " / ";
                                }
                            }


                            if (item.charges.Count > 0 && item.charges != null)
                            {
                                foreach (var item1 in item.charges)
                                {
                                    charges += item1.value;
                                }
                            }
                            else
                            {

                                charges = 0;
                            }

                            if (item != null)
                            {
                                double ItemTax = 0;

                                foreach (var tax in item.taxes)
                                {
                                    ItemTax += tax.value;
                                }

                                entities.Item_Master.Add(new Item_Master
                                {

                                    Biz_Id = order.order.details.biz_id,
                                    UPR_Order_Id = order.order.details.id,
                                    Order_Id = order.order.details.ext_platforms.Count > 0 ? order.order.details.ext_platforms[0].id : "0",
                                    Item_Name = item.title,
                                    Item_Cd = item.merchant_id,
                                    Rate = item.price,
                                    Qty = item.quantity,
                                    Amount = item.total,
                                    Item_Tax = ItemTax,
                                    Discount = item.discount,
                                    Food_Type = item.food_type,
                                    Options_To_Add_Title = opetions_to_add!=""&& opetions_to_add.Length>0&& opetions_to_add.Contains("/") ? opetions_to_add.Remove(opetions_to_add.LastIndexOf("/")):"",
                                    Options_To_Add_Rate = item_Options_To_Add_Rate!="" && item_Options_To_Add_Rate .Length>0&& item_Options_To_Add_Rate.Contains("/") ? item_Options_To_Add_Rate.Remove(item_Options_To_Add_Rate.LastIndexOf("/")):"",
                                    Options_To_Remove = "",
                                    Store_Id = order.order.store.merchant_ref_id != null ? order.order.store.merchant_ref_id.ToString() : "",
                                    Item_Total = item.total_with_tax,
                                    Charges = charges
                                });
                            }
                        }


                        if (entities.SaveChanges() > 0)
                        {
                            reply.StatusCode = HttpCustCode.success;
                            reply.Response = true;
                            reply.Message = "Success";

                            Log.Write("Data saved", order.order.details.channel);
                        }
                        else
                        {
                            reply.StatusCode = HttpCustCode.failed;
                            reply.Response = false;
                            reply.Message = "Failed";

                            Log.Write("Data saving failed", order.order.details.channel);
                        }


                    }
                    else
                    {
                        reply.StatusCode = HttpCustCode.invalid_request;
                        reply.Response = false;
                        reply.Message = "Failed";

                        Log.Write("Data saving failed", "");
                    }
                }
                catch (Exception ex)
                {
                    Log.Write(ex.Message + " " + ex.InnerException, order.order.details.channel);

                    reply.StatusCode = HttpCustCode.unableto_process;
                    reply.Response = false;
                    reply.Message = ex.Message + " " + ex.InnerException;

                }
            }
            else
            {
                reply.StatusCode = HttpCustCode.failed;
                reply.Response = false;
                reply.Message = "Failed";

                Log.Write("Data saving failed", "");
            }


             Log.Write("-----------------------------------------------Response object-----------------------------------------------------------", "nt");

            Log.Write(Newtonsoft.Json.JsonConvert.SerializeObject(reply), "nt");

            Log.Write("-------------------------------------------------------------------------------------------------------------------------", "nt");
            return reply;
        }

        //public Reply RiderStatus(string StrriderStatus)
        //{
        //    entities = new MyDBContext();
        //    Reply reply = new Reply();

        //    DataContractJsonSerializer js = new DataContractJsonSerializer(typeof(RiderStatus));

        //    MemoryStream msObj = new MemoryStream(Encoding.Unicode.GetBytes(StrriderStatus));

        //    try
        //    {

        //        RiderStatus riderStatus = js.ReadObject(msObj) as RiderStatus;

        //        Log.Write("In Rider Status", "");

        //        if (riderStatus != null)
        //        {

        //            entities.Rider_Status.Add(new Rider_Status
        //            {
        //                Store_Id = Convert.ToString(riderStatus.store.id),
        //                Channel = riderStatus.additional_info.external_channel.name,
        //                UPR_Order_Id = riderStatus.order_id,
        //                Order_Id = riderStatus.additional_info.external_channel.order_id,
        //                Current_State = riderStatus.delivery_info.current_state,
        //                Name = riderStatus.delivery_info.delivery_person_details.name,
        //                Phone = riderStatus.delivery_info.delivery_person_details.name,
        //                Alt_Phone = riderStatus.delivery_info.delivery_person_details.alt_phone,
        //                User_Id = riderStatus.delivery_info.delivery_person_details.user_id,
        //                Comments = riderStatus.delivery_info.status_updates[0].comments,
        //                Status = riderStatus.delivery_info.status_updates[0].status,
        //                Created = Convert.ToString(riderStatus.delivery_info.status_updates[0].created),

        //            });

        //            if (entities.SaveChanges() > 0)
        //            {
        //                reply.StatusCode = HttpCustCode.success;
        //                reply.Response = true;
        //                reply.Message = "Success";

        //                Log.Write("Data saved", "");
        //            }
        //            else
        //            {
        //                reply.StatusCode = HttpCustCode.failed;
        //                reply.Response = false;
        //                reply.Message = "Failed";

        //                Log.Write("Data saving failed", "");
        //            }
        //        }
        //        else
        //        {
        //            reply.StatusCode = HttpCustCode.invalid_request;
        //            reply.Response = false;
        //            reply.Message = "Failed";

        //            Log.Write("Data saving failed", "");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.Write(ex.Message + " " + ex.InnerException + " " + ex.InnerException, "");

        //        reply.StatusCode = HttpCustCode.unableto_process;
        //        reply.Response = false;
        //        reply.Message = ex.Message + " " + ex.InnerException;

        //    }
        //    return reply;
        //}

        public Reply SetRiderStatus(RiderStatus riderStatus)
        {
            entities = new MyDBContext();
            Reply reply = new Reply();

            try
            {


                Log.Write("In SetRiderStatus", "");


               Log.Write("-----------------------------------------------Request object-----------------------------------------------------------", "nt");

                Log.Write(Newtonsoft.Json.JsonConvert.SerializeObject(riderStatus), "nt");

                Log.Write("-------------------------------------------------------------------------------------------------------------------------", "nt");

                if (riderStatus != null)
                {

                    entities.Rider_Status.Add(new Rider_Status
                    {
                        Store_Id = riderStatus.store.ref_id,
                        Channel = riderStatus.additional_info.external_channel.name,
                        UPR_Order_Id = riderStatus.order_id,
                        Order_Id = riderStatus.additional_info.external_channel.order_id,
                        Current_State = riderStatus.delivery_info.current_state,
                        Name = riderStatus.delivery_info.delivery_person_details.name,
                        Phone = riderStatus.delivery_info.delivery_person_details.phone,
                        Alt_Phone = riderStatus.delivery_info.delivery_person_details.alt_phone,
                        User_Id = riderStatus.delivery_info.delivery_person_details.user_id,
                        Comments = riderStatus.delivery_info.status_updates[0].comments,
                        Status = riderStatus.delivery_info.status_updates[0].status,
                        Created = Convert.ToString(riderStatus.delivery_info.status_updates[0].created),

                    });

                    if (entities.SaveChanges() > 0)
                    {
                        reply.StatusCode = HttpCustCode.success;
                        reply.Response = true;
                        reply.Message = "Success";

                        Log.Write("Data saved", "");
                    }
                    else
                    {
                        reply.StatusCode = HttpCustCode.failed;
                        reply.Response = false;
                        reply.Message = "Failed";

                        Log.Write("Data saving failed", "");
                    }
                }
                else
                {
                    reply.StatusCode = HttpCustCode.invalid_request;
                    reply.Response = false;
                    reply.Message = "Failed";

                    Log.Write("Data saving failed", "");
                }
            }
            catch (Exception ex)
            {
                Log.Write(ex.Message + " " + ex.InnerException + " " + ex.InnerException, "");

                reply.StatusCode = HttpCustCode.unableto_process;
                reply.Response = false;
                reply.Message = ex.Message + " " + ex.InnerException;

            }


            Log.Write("-----------------------------------------------Response object-----------------------------------------------------------", "nt");

            Log.Write(Newtonsoft.Json.JsonConvert.SerializeObject(reply), "nt");

            Log.Write("-------------------------------------------------------------------------------------------------------------------------", "nt");

            return reply;
        }  //With object



        public Reply SetOrderStatus(OrderStatus orderStatus)
        {
            entities = new MyDBContext();
            Reply reply = new Reply();


            Log.Write("In SetOrderStatus", "");


            Log.Write("-----------------------------------------------Request object-----------------------------------------------------------", "nt");

            Log.Write(Newtonsoft.Json.JsonConvert.SerializeObject(orderStatus), "");

            Log.Write("-------------------------------------------------------------------------------------------------------------------------", "nt");

            try
            {


                if (orderStatus != null)
                {
                    entities.Order_Status.Add(new Order_Status
                    {
                        Channel = orderStatus.additional_info.external_channel.name,
                        UPR_Order_Id = orderStatus.order_id,
                        Order_Id = orderStatus.additional_info.external_channel.order_id,
                        dUpdate_Dt = orderStatus.timestamp,
                        Order_Status1 = orderStatus.new_state,
                        Order_Message = orderStatus.message,
                        Order_Status_Pre = orderStatus.prev_state,
                        Store_Id = orderStatus.store_id
                    });


                    if (entities.SaveChanges() > 0)
                    {
                        reply.StatusCode = HttpCustCode.success;
                        reply.Response = true;
                        reply.Message = "Success";

                        Log.Write("Data saved", "");
                    }
                    else
                    {
                        reply.StatusCode = HttpCustCode.failed;
                        reply.Response = false;
                        reply.Message = "Failed";

                        Log.Write("Data saving failed", "");
                    }


                }
                else
                {
                    reply.StatusCode = HttpCustCode.invalid_request;
                    reply.Response = false;
                    reply.Message = "Failed";

                    Log.Write("Data saving failed", "");
                }
            }
            catch (Exception ex)
            {
                Log.Write(ex.Message + " " + ex.InnerException, "");

                reply.StatusCode = HttpCustCode.unableto_process;
                reply.Response = false;
                reply.Message = ex.Message + " " + ex.InnerException;

            }


             Log.Write("-----------------------------------------------Response object-----------------------------------------------------------", "nt");

            Log.Write(Newtonsoft.Json.JsonConvert.SerializeObject(reply), "nt");

            Log.Write("-------------------------------------------------------------------------------------------------------------------------", "nt");

            return reply;
        }



        public Reply SetRandomStatus(Random_Message1 random_Message2)
        {
            entities = new MyDBContext();
            Reply reply = new Reply();

            Log.Write("In SetRandomStatus", "");

            Log.Write("-----------------------------------------------Request object-----------------------------------------------------------", "nt");

            Log.Write(Newtonsoft.Json.JsonConvert.SerializeObject(random_Message2), "");

            Log.Write("-------------------------------------------------------------------------------------------------------------------------", "nt");

            try
            {

                if (random_Message2 != null)
                {
                    entities.Reference_Master.Add(new Reference_Master
                    {
                        Reference_Id = random_Message2.reference != null && random_Message2.reference != "" ? random_Message2.reference : random_Message2.reference_id,
                        sError = random_Message2.message
                    });


                    if (entities.SaveChanges() > 0)
                    {
                        reply.StatusCode = HttpCustCode.success;
                        reply.Response = true;
                        reply.Message = "Success";

                        Log.Write("Data saved", "");
                    }
                    else
                    {
                        reply.StatusCode = HttpCustCode.failed;
                        reply.Response = false;
                        reply.Message = "Failed";

                        Log.Write("Data saving failed", "");
                    }


                }
                else
                {
                    reply.StatusCode = HttpCustCode.invalid_request;
                    reply.Response = false;
                    reply.Message = "Failed";

                    Log.Write("Data saving failed", "");
                }
            }
            catch (Exception ex)
            {
                Log.Write(ex.Message + " " + ex.InnerException, "");

                reply.StatusCode = HttpCustCode.unableto_process;
                reply.Response = false;
                reply.Message = ex.Message + " " + ex.InnerException;

            }
            //catch (DbEntityValidationException ex) {
            //    foreach (var eve in ex.EntityValidationErrors)
            //    {
            //        Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
            //            eve.Entry.Entity.GetType().Name, eve.Entry.State);
            //        foreach (var ve in eve.ValidationErrors)
            //        {
            //            Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
            //                ve.PropertyName, ve.ErrorMessage);
            //        }
            //    }
            //}


            Log.Write("-----------------------------------------------Response object-----------------------------------------------------------", "nt");

            Log.Write(Newtonsoft.Json.JsonConvert.SerializeObject(reply), "nt");

            Log.Write("-------------------------------------------------------------------------------------------------------------------------", "nt");

            return reply;
        }

        public void SetConnection(string Merchent_Ref_Id)
        {
            try
            {
                if (ConnectionsWithIds.Count > 0)
                {

                    ConnectionsWithIds.ForEach(X => { if (Merchent_Ref_Id.ToLower().Contains(X.RstaurantId.ToLower())) GlobalMembers.conString = X.ConnectionString; });
                    //foreach (var con in ConnectionsWithIds)
                    //{
                    //    if (Merchent_Ref_Id.ToLower().Trim().Contains(con.RstaurantId.ToLower()))
                    //    {
                    //        GlobalMembers.conString = con.ConnectionString;
                    //    }
                    //}
                }
                else
                {
                    GlobalMembers.conString = "MyDbContext";
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }

    public class ConnectionsWithIds
    {
        public string ConnectionString { get; set; }
        public string RstaurantId { get; set; }
    }

    public enum HttpCustCode
    {
        success = 200,
        failed = 500,
        unableto_process = 422,
        permission_denied = 403,
        invalid_request = 400,
    }
    class INIFile
    {
        private string filePath;
        [DllImport("Kernel32.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        private static extern UInt32 GetPrivateProfileSection
            (
                [In] [MarshalAs(UnmanagedType.LPStr)] string strSectionName,
                // Note that because the key/value pars are returned as null-terminated
                // strings with the last string followed by 2 null-characters, we cannot
                // use StringBuilder.
                [In] IntPtr pReturnedString,
                [In] UInt32 nSize,
                [In] [MarshalAs(UnmanagedType.LPStr)] string strFileName
            );

        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);

        public INIFile(string filePath)
        {
            this.filePath = filePath;
        }

        public void Write(string section, string key, string value)
        {
            long a = WritePrivateProfileString(section, key, value, this.filePath);
            System.Threading.Thread.Sleep(60);
        }

        public string Read(string section, string key, string def)
        {
            string strReturnVal = "";
            try
            {
                StringBuilder SB = new StringBuilder(255);
                int i = GetPrivateProfileString(section, key, def, SB, 255, this.filePath);
                strReturnVal = SB.ToString();
            }
            catch (Exception)
            {
                strReturnVal = "";
            }
            return strReturnVal;
        }

        public bool IniReadDateValue(string Section, string Key, out DateTime objDT, out string strExcp)
        {
            try
            {
                StringBuilder temp = new StringBuilder(25);
                int i = GetPrivateProfileString(Section, Key, "", temp, 255, this.filePath);

                objDT = new DateTime(Convert.ToInt32(temp.ToString().Substring(0, 4)), Convert.ToInt32(temp.ToString().Substring(5, 2)), Convert.ToInt32(temp.ToString().Substring(8, 2)), Convert.ToInt32(temp.ToString().Substring(11, 2)), Convert.ToInt32(temp.ToString().Substring(14, 2)), Convert.ToInt32(temp.ToString().Substring(17, 2)));
                strExcp = "";   //Added [Shubhit 03May13]
                return true;
            }
            catch (Exception excp)
            {
                objDT = DateTime.Now;
                strExcp = excp.Message.ToString();  //Added [Shubhit 03May13]
                return false;
            }
        }

        public double IniReadDoubleValue(string Section, string Key)
        {
            StringBuilder temp = new StringBuilder(255);
            int i = GetPrivateProfileString(Section, Key, "", temp, 255, this.filePath);
            double dRes;
            Double.TryParse(temp.ToString(), out dRes);
            return dRes;
        }

        public string[] GetAllKeysInIniFileSection(string strSectionName)
        {
            string[] strArray = null;
            try
            {

                // Allocate in unmanaged memory a buffer of suitable size.
                // I have specified here the max size of 32767 as documentated 
                // in MSDN.
                IntPtr pBuffer = Marshal.AllocHGlobal(32767);
                // Start with an array of 1 string only. 
                // Will embellish as we go along.

                strArray = new string[0];
                UInt32 uiNumCharCopied = 0;

                uiNumCharCopied = GetPrivateProfileSection(strSectionName, pBuffer, 32767, this.filePath);

                // iStartAddress will point to the first character of the buffer,
                int iStartAddress = pBuffer.ToInt32();
                // iEndAddress will point to the last null char in the buffer.
                int iEndAddress = iStartAddress + (int)uiNumCharCopied;

                // Navigate through pBuffer.
                while (iStartAddress < iEndAddress)
                {
                    // Determine the current size of the array.
                    int iArrayCurrentSize = strArray.Length;
                    // Increment the size of the string array by 1.
                    Array.Resize<string>(ref strArray, iArrayCurrentSize + 1);
                    // Get the current string which starts at "iStartAddress".
                    string strCurrent = Marshal.PtrToStringAnsi(new IntPtr(iStartAddress));
                    // Insert "strCurrent" into the string array.
                    strArray[iArrayCurrentSize] = strCurrent;
                    // Make "iStartAddress" point to the next string.
                    iStartAddress += (strCurrent.Length + 1);
                }

                Marshal.FreeHGlobal(pBuffer);
                pBuffer = IntPtr.Zero;
                for (int i = 0; i < strArray.Length; i++)
                {
                    strArray[i] = strArray[i].Substring(strArray[i].LastIndexOf('=') + 1).ToLower();
                }
            }
            catch (Exception) { }//EventLog.WriteEntry("LipiWhiteListing", "Exception Message: " + ex.Message+" "+ex.InnerException, //EventLogEntryType.Error); }

            return strArray;
        }
        public string FilePath
        {
            get { return this.filePath; }
            set { this.filePath = value; }
        }
    }
    public enum Database
    {
        MSSQL,
        MySQL
    }
    public class ReqConnectDB
    {
        [DataMember(Order = 0)]
        public string ServerIP { get; set; }

        [DataMember(Order = 1)]
        public string UserName { get; set; }

        [DataMember(Order = 2)]
        public string Password { get; set; }

        [DataMember(Order = 3)]
        public Database DBType { get; set; }

        [DataMember(Order = 4)]
        public string DBName { get; set; }

        [DataMember(Order = 5)]
        public bool ColumnEncryption { get; set; }
    }

    [DataContract]
    public class ResConnectDB
    {
        [DataMember(Order = 0)]
        public bool Result { get; set; }

        [DataMember(Order = 1)]
        public string Error { get; set; }
    }

    [DataContract]
    public class ReqSelect
    {
        [DataMember(Order = 0)]
        public ReqConnectDB Connection { get; set; }

        [DataMember(Order = 1)]
        public string Query { get; set; }

        [DataMember(Order = 2)]
        public List<string> Parameters { get; set; }
    }

    [DataContract]
    public class ResSelect
    {
        [DataMember(Order = 0)]
        public bool Result { get; set; }

        [DataMember(Order = 1)]
        public string Error { get; set; }

        [DataMember(Order = 2)]
        public DataSet DS { get; set; }
    }

    [DataContract]
    public class ReqInsert
    {
        [DataMember(Order = 0)]
        public ReqConnectDB Connection { get; set; }

        [DataMember(Order = 1)]
        public string Query { get; set; }

        [DataMember(Order = 2)]
        public List<object> Parameters { get; set; }
    }

    [DataContract]
    public class ResInsert
    {
        [DataMember(Order = 0)]
        public bool Result { get; set; }

        [DataMember(Order = 1)]
        public string Error { get; set; }

        [DataMember(Order = 2)]
        public int RecordsInserted { get; set; }
    }

    [DataContract]
    public class ReqDelete
    {
        [DataMember(Order = 0)]
        public ReqConnectDB Connection { get; set; }

        [DataMember(Order = 1)]
        public string Query { get; set; }

        [DataMember(Order = 2)]
        public List<string> Parameters { get; set; }
    }

    [DataContract]
    public class ResDelete
    {
        [DataMember(Order = 0)]
        public bool Result { get; set; }

        [DataMember(Order = 1)]
        public string Error { get; set; }

        [DataMember(Order = 2)]
        public int RecordsDeleted { get; set; }
    }

    [DataContract]
    public class ReqUpdate
    {
        [DataMember(Order = 0)]
        public ReqConnectDB Connection { get; set; }

        [DataMember(Order = 1)]
        public string Query { get; set; }

        [DataMember(Order = 2)]
        public List<string> Parameters { get; set; }
    }

    [DataContract]
    public class ResUpdate
    {
        [DataMember(Order = 0)]
        public bool Result { get; set; }

        [DataMember(Order = 1)]
        public string Error { get; set; }

        [DataMember(Order = 2)]
        public int RecordsUpdated { get; set; }
    }

    public class Reply
    {
        public bool Response { get; set; }
        public HttpCustCode StatusCode { get; set; }
        public string Message { get; set; }
    }

    public class PatchUpdateINI
    {
        public string[] KioskIP;

        public string[] MachineSrNo;

        public string patch;
        public string PatchName { get; set; }
        public string StartDateTime { get; set; }
        public string EndDateTime { get; set; }
        public bool Instant { get; set; }

    }

    public class CommandIniUpdate
    {
        public string[] KioskIP;

        public string[] MachineSrNo;

        public string CommandCount;
        public string Command { get; set; }
        public bool Instant { get; set; }

    }

    public class UserDetails
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Question { get; set; }

        public string Answer { get; set; }
        public string Location { get; set; }
        public string Role { get; set; }
    }
    public struct UserDetailsInfo
    {
        public string UserName { get; set; }
        public string Pwd { get; set; }
        public string Role { get; set; }
    }
}