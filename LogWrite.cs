using System;
using System.IO;
using System.Reflection;

namespace KhaoPiyo
{
    /// <summary>
    /// Global static class for logging
    /// </summary>
    public static class Log
    {
        #region Member variables
        public static bool bEncryptLog;     // To encrypt logs or not
        public static string strLogPath;    // Log file path
        #endregion

        /// <summary>
        /// Static function to write log
        /// </summary>
        /// <param name="inText">String to log in the file</param>
        /// <param name="strKID">Kiosk ID of the machine from which message received</param>
        /// <param name="strMSN">Serial number of the machine from which message received</param>
        public static void Write(string inText, string strFoldername)
        {
            try
            {

                string strPath = "";

                //if (strClient != "")
                //    strPath = strLogPath + "\\" + "Client- " + strClient + "\\" + DateTime.Now.ToString("MMM yy");
                //else
                    strPath = strLogPath + "\\" + DateTime.Now.ToString("MMM yy");

                // Create directory if not exist
                if (!Directory.Exists(strPath))
                    Directory.CreateDirectory(strPath);

                if (!File.Exists(strPath + "\\khaoPiyoLog_" + DateTime.Today.ToString("dd_MM_yyyy") + ".txt"))
                {
                    if (bEncryptLog)
                        File.WriteAllText(strPath + "\\khaoPiyoLog_" + DateTime.Today.ToString("dd_MM_yyyy") + ".txt",
                                      SSTCryptographer.Encrypt(DateTime.Now.ToString("HH:mm:ss.fff") + "\t***** Service started V" + Assembly.GetExecutingAssembly().GetName().Version.ToString() + " *****\n"));
                    else
                        File.WriteAllText(strPath + "\\khaoPiyoLog_" + DateTime.Today.ToString("dd_MM_yyyy") + ".txt",
                                      DateTime.Now.ToString("HH:mm:ss.fff") + "\t***** Service started V" + Assembly.GetExecutingAssembly().GetName().Version.ToString() + " *****\n");
                }

                // Create or map file objct with append mode
                StreamWriter swFile = new StreamWriter(strPath + "\\khaoPiyoLog_" + DateTime.Today.ToString("dd_MM_yyyy") + ".txt", true);

                // If created or mapped
                if (swFile != null)
                {
                    if (bEncryptLog)     // If write encrypted log
                        swFile.WriteLine(SSTCryptographer.Encrypt(DateTime.Now.ToString("HH:mm:ss.fff") + "\t" + inText));
                    else                // Normla log write
                    {
                        if (strFoldername == "nt") {
                            swFile.WriteLine("\t\t\n" +inText+"\n");
                        }
                        else
                        swFile.WriteLine(DateTime.Now.ToString("HH:mm:ss.fff") + "\t" + inText);
                    }
                    // Close the file
                    swFile.Close();
                }
            }
            catch
            { }
        }
    }
}