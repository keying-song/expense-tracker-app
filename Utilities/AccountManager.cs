using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using assignment4.Models;
using System.Xml.Serialization;
using System.IO;

namespace assignment4.Utilities
{
    public static class AccountManager
    {
        private static string root = "C:\\assignment4\\";
        static AccountManager()
        { 
            if (!Directory.Exists(root))
                Directory.CreateDirectory(root);
        }

        public static void Create(Account account) {

            XmlSerializer serializer = new XmlSerializer(typeof(Account));

            using (Stream stream = new FileStream($"{root}{account.AccountId}.xml", FileMode.Create))
            {
                serializer.Serialize(stream, account);
            }
        }
        public static Queue<Account> ShowAll() {
            XmlSerializer serializer = new XmlSerializer(typeof(Account));
            string[] files = Directory.GetFiles(root);
            Queue<Account> accounts = new Queue<Account>();
            foreach (string file in files)
            {
                using (Stream stream = new FileStream(file, FileMode.Open))
                {
                    Account account = (Account)serializer.Deserialize(stream);

                    accounts.Enqueue(account);
                }
            }

            return accounts;
        }
        public static void Delete(string accountID)
        {
            string filename = $"{root}{accountID}.xml";

            if (File.Exists(filename))
                File.Delete(filename);
            else
                throw new Exception($"ERROR: Tried to delete an account that does not exist ({accountID}).");
        }

        public static void Update(Account account)
        {
            Delete(account.AccountId);
            Create(account);
        }

        public static Account Get(string accountID)
        {
            string filename = $"{root}{accountID}.xml";
            XmlSerializer serializer = new XmlSerializer(typeof(Account));

            if (File.Exists(filename))
            {
                using (Stream stream = new FileStream(filename, FileMode.Open))
                {
                    Account account = (Account)serializer.Deserialize(stream);

                    return account;
                }
            }
            else
            {
                throw new Exception($"ERROR: Tried to load an account that does not exist ({accountID}).");
            }
        }




    }  
}
