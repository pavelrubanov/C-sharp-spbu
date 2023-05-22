using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace FileManager
{
    [Serializable] 
    public class Account
    {
        [NonSerialized] private const string Key = "9y$B&E)";
        private string Password { get; set; }
        public string Name { get; private set; }
        public Account(string Name, string Password)
        {
            this.Name = Name;
            this.Password = Password;
        }
        public bool CompareWithPassword(string pass)
        {
            if (pass == Password) 
                return true;
            else
                return false;
        }
        public bool ChangePassword (string newPass, string oldPass)
        {
            if (oldPass == Password)
            {
                Password = newPass;
                return true;
            }
            else
                return false;
        }
        public bool ChangeName (string newName, string pass)
        {
            if (pass == Password)
            {
                Name = newName;
                return true;
            }
            else
                return false;
        }

        [OnSerializing] public void OnSerializing(StreamingContext context)
        {
            Name = Crypto.EncryptStringAES(Name, Key);
            Password = Crypto.EncryptStringAES(Password, Key);
        }
        [OnDeserialized] public void OnDeserialized (StreamingContext context)
        {
            Name = Crypto.DecryptStringAES(Name, Key);
            Password = Crypto.DecryptStringAES(Password, Key);
        }
    }
}
