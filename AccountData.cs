using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAddressbookTests
{
    class AccountData
    {
        private string username;
        private string password;

        //конструктор, чтобы конструировать новые объекты
        public AccountData(string username, string password)
        {
            //в поле присваиваем значение, которое передано как параметр
            this.username = username;
            this.password = password;
        }
        
        //get\set позволят менять значения свойств объекта
        public string Username
        {
            get
            {
                return username;
            }
            set
            {
                username = value;
            }
        }

        public string Password
        {
            get
            {
                return password;
            }
            set
            {
                password = value;
            }
        }
    }
}
