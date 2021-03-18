using KhaoPiyo.Models;
using System;
using System.Data.Entity.Core.EntityClient;
using System.Data.SqlClient;
using System.MyOrderModel;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace KhaoPiyo
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IService1
    {
        //Client Function Refrence
        //[OperationContract]
        //[WebInvoke(Method = "POST",
        //ResponseFormat = WebMessageFormat.Json,
        //RequestFormat = WebMessageFormat.Json,
        //BodyStyle = WebMessageBodyStyle.Bare,
        //UriTemplate = "HealthData")]
        //EncResponse HealthData(EncRequest objEncRequest);

      

        [OperationContract]
        [WebInvoke(Method = "POST",
    ResponseFormat = WebMessageFormat.Json,
    RequestFormat = WebMessageFormat.Json,
    BodyStyle = WebMessageBodyStyle.Bare,
    UriTemplate = "SetRiderStatus")]
        Reply SetRiderStatus(RiderStatus riderStatus);

        [OperationContract]
        [WebInvoke(Method = "POST",
    ResponseFormat = WebMessageFormat.Json,
    RequestFormat = WebMessageFormat.Json,
    BodyStyle = WebMessageBodyStyle.Bare,
    UriTemplate = "SetRandomStatus")]
        Reply SetRandomStatus(Random_Message1 random_Message2);



        [OperationContract]
        [WebInvoke(Method = "POST",
  ResponseFormat = WebMessageFormat.Json,
  RequestFormat = WebMessageFormat.Json,
  BodyStyle = WebMessageBodyStyle.Bare,
  UriTemplate = "test")]
        Reply test(string ss);

        [OperationContract]
        [WebInvoke(Method = "POST",
 ResponseFormat = WebMessageFormat.Json,
 RequestFormat = WebMessageFormat.Json,
 BodyStyle = WebMessageBodyStyle.Bare,
 UriTemplate = "SetOrderStatus")]
        Reply SetOrderStatus(OrderStatus orderStatus);

     //   [OperationContract]
     //   [WebInvoke(Method = "POST",
     //ResponseFormat = WebMessageFormat.Json,
     //RequestFormat = WebMessageFormat.Json,
     //BodyStyle = WebMessageBodyStyle.Bare,
     //UriTemplate = "RandomStatus")]
     //   Reply RandomStatus(string StrRandomStatus);


        [OperationContract]
        [WebInvoke(Method = "POST",
     ResponseFormat = WebMessageFormat.Json,
     RequestFormat = WebMessageFormat.Json,
     BodyStyle = WebMessageBodyStyle.Bare,
     UriTemplate = "SetOrder")]
        Reply SetOrder(SetOrderObject orderPlaced);
    }



    public static class ConnectionTools
    {
        // all params are optional
        public static void ChangeDatabase(
            this  MyDBContext source,
            string initialCatalog = "",
            string dataSource = "",
            string userId = "",
            string password = "",
            bool integratedSecuity = true,
            string configConnectionStringName = "")
        /* this would be used if the
        *  connectionString name varied from 
        *  the base EF class name */
        {
            try
            {
                // use the const name if it's not null, otherwise
                // using the convention of connection string = EF contextname
                // grab the type name and we're done
                var configNameEf = string.IsNullOrEmpty(configConnectionStringName)
                    ? source.GetType().Name
                    : configConnectionStringName;

                // add a reference to System.Configuration
                var entityCnxStringBuilder = new EntityConnectionStringBuilder
                    (System.Configuration.ConfigurationManager
                        .ConnectionStrings[configNameEf].ConnectionString);

                // init the sqlbuilder with the full EF connectionstring cargo
                var sqlCnxStringBuilder = new SqlConnectionStringBuilder
                    (entityCnxStringBuilder.ProviderConnectionString);

                // only populate parameters with values if added
                if (!string.IsNullOrEmpty(initialCatalog))
                    sqlCnxStringBuilder.InitialCatalog = initialCatalog;
                if (!string.IsNullOrEmpty(dataSource))
                    sqlCnxStringBuilder.DataSource = dataSource;
                if (!string.IsNullOrEmpty(userId))
                    sqlCnxStringBuilder.UserID = userId;
                if (!string.IsNullOrEmpty(password))
                    sqlCnxStringBuilder.Password = password;

                // set the integrated security status
                sqlCnxStringBuilder.IntegratedSecurity = integratedSecuity;

                // now flip the properties that were changed
                source.Database.Connection.ConnectionString
                    = sqlCnxStringBuilder.ConnectionString;
            }
            catch (Exception ex)
            {
                // set log item if required
            }
        }
    }

}
