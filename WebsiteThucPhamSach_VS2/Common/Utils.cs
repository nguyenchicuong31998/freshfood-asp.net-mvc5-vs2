using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;
using System.Text;
using System.Net.Mail;
using System.Configuration;
using WebsiteThucPhamSach_VS2.Models;

namespace WebsiteThucPhamSach_VS2.Common
{
    public class Utils
    {
        MD5 md5Hash = MD5.Create();

        public string GetMd5Hash(string input)
        {

            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }

        public bool VerifyMd5Hash(string input, string hash)
        {
            // Hash the input.
            string hashOfInput = GetMd5Hash(input);

            // Create a StringComparer an compare the hashes.
            StringComparer comparer = StringComparer.OrdinalIgnoreCase;

            if (0 == comparer.Compare(hashOfInput, hash))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public string RandomChar(int numberRD)
        {
            string randomStr = "";
            try
            {

                string[] myIntArray = new string[numberRD];
                int x;
                //that is to create the random # and add it to uour string
                Random autoRand = new Random();
                for (x = 0; x < numberRD; x++)
                {
                    myIntArray[x] = Convert.ToChar(Convert.ToInt32(autoRand.Next(65, 87))).ToString();
                    randomStr += (myIntArray[x].ToString());
                }
            }
            catch (Exception ex)
            {
                randomStr = "error";
                throw ex;
            }
            return randomStr;
        }
        public bool SendEmail(string toEmail, string subJect, string body, string cc, string bcc)
        {
            UsersModels usersModel = new UsersModels();
            string emailAdmin = ConfigurationManager.AppSettings["EmailAdmin"];
            string passwordEmailAdmin = ConfigurationManager.AppSettings["PasswordEmailAdmin"];
            var user = usersModel.getUserByEmail(toEmail);
            if (user != null)
            {
                MailMessage mail = new MailMessage();
                mail.To.Add(toEmail);
                mail.From = new MailAddress(emailAdmin);
                mail.Subject = subJect;
                mail.Body = body;
                if(cc != "") { 
                   mail.CC.Add(cc);
                }
                if(bcc != "") { 
                   mail.Bcc.Add(bcc);
                }
                mail.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = ConfigurationManager.AppSettings["EmailHost"];
                smtp.Port = int.Parse(ConfigurationManager.AppSettings["EmailPort"]);
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = new System.Net.NetworkCredential(emailAdmin, passwordEmailAdmin);// tài khoản Gmail của bạn
                smtp.EnableSsl = true;
                smtp.Send(mail);
                return true;
            }
            return false;
        }

        public string add3Dots(string text,int limit)
        {
            var dots = "...";
            if (text.Length > limit)
            {
                text = text.Substring(0, limit) + dots;
            }
            return text;
        }


        public string FormatPrice(string _strInput)
        {
            string strInput = _strInput;
            int Length = 0;
            if (strInput.IndexOf('.') > 0)
                Length = strInput.Length - (strInput.Length - strInput.IndexOf('.'));
            else
                Length = strInput.Length;
            string afterFormat = "";
            if (Length <= 3)
                afterFormat = strInput;
            else if (Length > 3)
            {
                afterFormat = strInput.Insert(Length - 3, ".");
                Length = afterFormat.IndexOf(".");
                while (Length > 3)
                {
                    afterFormat = afterFormat.Insert(Length - 3, ".");
                    Length = Length - 3;
                }
            }
            return afterFormat;
        }
    }
}