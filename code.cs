using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;
using System.Windows.Forms;
using System.IO;
using System.Net;
using System.Net.Mail;

namespace WindowsStart
{   // http://www.fluxbytes.com/csharp/start-application-at-windows-startup/
    class Program
    {
        // Set value to registry
        public static void AddAplicationToStartup()
        {
            using (RegistryKey key = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true))
            {
                key.SetValue("My Program", "\"" + Application.ExecutablePath + "\"");
            }
        }

        // Delete value from registry
        public static void RemoveApplicationFromStratup()
        {
            using (RegistryKey key = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", false))
            {
                key.DeleteValue("My Program", false);
            }
        }

        public static void ConsoleWriteLineText()
        {
            // Create a StreamWriter instance
            //StreamWriter writer = new StreamWriter("log_Info.txt", true);
            //DateTime[] date = new DateTime[1];
            //Ensure the writer will be closed when no longer used
            //date[0] = DateTime.Now;
            //Console.WriteLine(date[0]);
            //using (writer)
            //{
                //writer.WriteLine(text);
                //date = DateTime.Now;
                //writer.WriteLine(DateTime.Now);
            //}
            TextWriter txt = new StreamWriter(@"D:\Learning\WindowsStart_check\WindowsStart\WindowsStart\bin\Debug\log_Info_d.txt", true);
            txt.WriteLine(DateTime.Now);
            txt.Close();
        }

        private static void SendNotification()
        {

            try
            {
                var fromAddress = new MailAddress("/*enteraddress*/");
                var toAddress = new MailAddress("/*enteraddress*/");

                const string password = "workingprogram";
                const string suject = "rostik-PC is on";
                string text = "Your pc is on. " + DateTime.Now.ToString();

                var smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    Credentials = new NetworkCredential(fromAddress.Address, password),
                    Timeout = 50
                };
                using (MailMessage message = new MailMessage(fromAddress, toAddress)
                {
                    Subject = suject,
                    Body = text
                })
                {
                    smtp.Send(message);
                }
            }
            catch (FormatException fe)
            {
                MessageBox.Show(fe.Message);
                Console.WriteLine(fe.StackTrace);
            }
            catch (TimeoutException te)
            {
                MessageBox.Show("Timeout: " + te.Message);
            }
            catch (SmtpFailedRecipientException sfre)
            {
                MessageBox.Show("Cannot send: " + sfre.Message);
            }
            catch (Exception e)
            {
                MessageBox.Show("Exception: " + e.Message);
                Console.WriteLine("Exception stacktrace: " + e.StackTrace);
            }
            finally
            {
                Environment.Exit(0);
            }

        }

        static void Main(string[] args)
        {
            AddAplicationToStartup();
            ConsoleWriteLineText();
            SendNotification();
        }
    }
}
