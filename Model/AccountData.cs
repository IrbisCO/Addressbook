///Класс для описания данных дял входа

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAddressbookTests
{
    public class AccountData
    {
        private string username;
        private string password;

        /// <summary>
        /// конструктор, чтобы конструировать новые объекты
        /// </summary>
        /// <param name="username">Имя пользователя</param>
        /// <param name="password">Пароль</param>
        public AccountData(string username, string password)
        {
            ///в поле присваиваем значение, которое передано как параметр
            this.username = username;
            this.password = password;
        }

        /// <summary>
        /// get\set позволят менять значения свойств объекта
        /// </summary>
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
