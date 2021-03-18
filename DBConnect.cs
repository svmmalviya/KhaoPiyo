using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.IO;
//Add MySql Library
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI;
using System.Web;
using System.Security.Cryptography;

namespace System
{
    // Encryption decryption unit
    public static class Encryption
    {
        // It encrypts string into md5 string 
        public static string MD5Hash(string text)
        {
            MD5 md5 = new MD5CryptoServiceProvider();

            //compute hash from the bytes of text  
            md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(text));

            //get hash result after compute it  
            byte[] result = md5.Hash;

            StringBuilder strBuilder = new StringBuilder();
            for (int i = 0; i < result.Length; i++)
            {
                //change it into 2 hexadecimal digits  
                //for each byte  
                strBuilder.Append(result[i].ToString("x2"));
            }

            return strBuilder.ToString();
        }
    }
}

namespace Classes
{
    public class DBConnect : IDisposable
    {
        private string server;
        // private string database;
        // private string database_type;
        public string database;
        public DBType database_type;
        private string uid;
        private string password;
        public SqlConnection connection;
        private SqlDataAdapter adp;
        private DataSet dtset;
        string dbType;

        public string connectionString;

        /// <summary>
        /// TRUE when db created or checked, FALSE when not checked or created
        /// </summary>
        public static bool bIsDBCreated = false;
        //Constructor
        public DBConnect()
        {
            string strError = "";
            try
            {
                Initialize();
            }
            catch (Exception excp)
            {
                strError = excp.Message;
            }
        }

        //Initialize values
        private void Initialize()
        {
            server = ConfigurationManager.AppSettings["DBServerIP"].ToString();//   "WIN-T04S48MMAPI"; //IP where Database is present

            //uid = "lipi";
            uid = ConfigurationManager.AppSettings["UserId"].ToString();
            //password = "L!p!d@t@";
            password = ConfigurationManager.AppSettings["Password"].ToString();

            database = ConfigurationManager.AppSettings["Database"].ToString(); //"kmsdatabase";


            //database_type = ConfigurationManager.AppSettings["DataBaseType"].ToString();

            database_type = DBType.mssql;


            string connectionString;
            connectionString = "SERVER=" + server + ";" + "DATABASE=" + database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";



            //MessageBox.Show("database initialize");

            connection = new SqlConnection(connectionString);





        }

        public string DatabaseName
        {
            get { return database; }
        }



        //open connection to database
        private bool OpenConnection(out string strError)
        {
            strError = "";
            try
            {

                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                    //ThreadSafe.WriteFile("S", "Database connection :", "Connection Open", "MainFolder");
                }
                return true;
            }
            catch (SqlException ex)
            {
                //When handling errors, you can your application's response based on the error number.
                //The two most common error numbers when connecting are as follows:
                //0: Cannot connect to server.
                //1045: Invalid user name and/or password.
                //switch (ex.Number)
                //{
                //    case 0:
                //        MessageBox.Show("Cannot connect to server.  Contact administrator");
                //        //ThreadSafe.WriteFile("Cannot connect to server");

                //        break;

                //    case 1045:
                //        MessageBox.Show("Invalid username/password, please try again");
                //        //ThreadSafe.WriteFile("Invalid username/password for database connectivity");
                //        break;

                //    //added ankush for the server not present
                //    case 1042:
                //        MessageBox.Show("Server Not Present for connection");
                //        //ThreadSafe.WriteFile("Server Not Presented for connection");
                //        break;
                //    default:
                //        ThreadSafe.WriteFile("S", "Excp -> Database connection :", "Connection Open", "MainFolder");
                //        break;

                //}
                strError = ex.Message;
                return false;
            }
        }

        //Close connection
        private bool CloseConnection(out string strError)
        {
            strError = "";
            try
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                    //ThreadSafe.WriteFile("S", "Database connection :", "Connection Close", "MainFolder");
                }
                return true;

            }
            catch (SqlException ex)
            {
                //MessageBox.Show(ex.Message);
                //ThreadSafe.WriteFile("Exception DBConnect : CloseConnection()", "", "", "MainFolder");
                strError = ex.Message;
                return false;
            }
        }

        //Select statement
        public bool Select(string query, out DataSet ds, out string strError)
        {
            //string query = "SELECT * FROM tableinfo";
            ds = null;
            strError = "";

            try
            {
                if (this.OpenConnection(out strError))
                {
                    adp = new SqlDataAdapter(query, connection);
                    //   adp.SelectCommand.CommandTimeout = 180;
                    dtset = new DataSet();
                    adp.Fill(dtset);
                    ds = dtset;


                    //ThreadSafe.WriteFile("S","Query :","Select Query :[" +query+ "]","MainFolder" );
                    if (this.CloseConnection(out strError))
                        return true;

                }
                return false;
            }
            catch (Exception excp)
            {
                //ThreadSafe.WriteFile("Exception in Select query" + excp.ToString(), "", "", "MainFolder");
                //string msg = excp.ToString();

                strError = excp.Message;
                return false;
            }
        }
        //Insert statement
        public bool Insert(string query, out string strError)
        {
            //string query = "INSERT INTO tableinfo (name, age) VALUES('John Smith', '33')";
            strError = "";
            try
            {
                //open connection
                if (this.OpenConnection(out strError))
                {
                    //create command and assign the query and connection from the constructor
                    int status;

                    SqlCommand cmd = new SqlCommand(query, connection);
                    status = cmd.ExecuteNonQuery();

                    //MySqlDataReader dataReader = cmd.ExecuteReader();


                    //Execute command

                    //if (status <= 0)
                    //ThreadSafe.WriteFile("S", "Query :", "Insert Query :Unsuccessful Execution [" + query + "]", "MainFolder");
                    //else
                    //     ThreadSafe.WriteFile("S", "Query :", "Insert Query :Unsuccessful Execution [" + query + "]", "MainFolder");


                    //close connection
                    if (this.CloseConnection(out strError) && status != 0)
                        return true;
                }
                return false;
            }
            catch (SqlException ex)
            {
                //ThreadSafe.WriteFile("Exception DBCOnnect : Insert()");
                //MessageBox.Show("insert dbconnect " + ex.Message);
                strError = ex.Message;
                return false;
            }

            //catch { return false; }
        }

        //Update statement
        public bool Update(string query, out string strError)
        {
            //string query = "UPDATE tableinfo SET name='Joe', age='22' WHERE name='John Smith'";
            strError = "";
            try
            {
                //Open connection
                if (this.OpenConnection(out strError))
                {

                    //create mysql command
                    SqlCommand cmd = new SqlCommand();
                    //Assign the query using CommandText
                    cmd.CommandText = query;
                    //Assign the connection using Connection
                    cmd.Connection = connection;

                    //Execute query
                    int status = cmd.ExecuteNonQuery();

                    //ThreadSafe.WriteFile("Updated query executed successfully " + "[ " + query + " ]");
                    //if (status <= 0)
                    //ThreadSafe.WriteFile("S", "Query :", "Update Query :Executed with no changes [" + query + "]", "MainFolder");
                    //else
                    //ThreadSafe.WriteFile("S", "Query :", "Update Query :Executed with no changes [" + query + "]", "MainFolder");

                    //close connection
                    if (this.CloseConnection(out strError))
                        return true;
                }
                return false;
            }
            catch (Exception excp)
            {
                //ThreadSafe.WriteFile("Exception executing Update query" + excp.ToString(), "", "", "MainFolder");
                //string msg = excp.ToString();
                strError = excp.Message;
                return false;
            }
            //catch { return false; }
        }

        //Delete statement
        public bool Delete(string query, out string strError)
        {
            //string query = "DELETE FROM tableinfo WHERE name='John Smith'";

            strError = "";
            try
            {
                if (this.OpenConnection(out strError))
                {

                    SqlCommand cmd = new SqlCommand(query, connection);
                    int status = cmd.ExecuteNonQuery();
                    //ThreadSafe.WriteFile("Delete query executed successfully " + "[ " + query + " ]");
                    //if (status <= 0)
                    //ThreadSafe.WriteFile("S", "Query :", "Delete Query :Executed with no changes [" + query + "]", "MainFolder");
                    //else
                    //ThreadSafe.WriteFile("S", "Query :", "Delete Query :Executed with no changes [" + query + "]", "MainFolder");

                    if (this.CloseConnection(out strError))
                        return true;

                }
                return false;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                //ThreadSafe.WriteFile("Exception in Delete query " + ex.ToString(), "", "", "MainFolder");
                return false;
            }
        }


        #region IDisposable Members

        public void Dispose()
        {
            connection = null;
            server = "";
            database = "";
            uid = "";
            password = "";
            adp = null;
            dtset = null;
            GC.Collect();

            //throw new NotImplementedException();
        }

        #endregion

    }

    public enum DBType
    {
        mssql = 1,
        mysql = 2,
        oracle = 3
    }
}

