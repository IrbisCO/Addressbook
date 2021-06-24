using LinqToDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAddressbookTests.Model
{
    /// <summary>
    /// Класс для коннекта с БД
    /// </summary>
    public class AddressBookDB : LinqToDB.Data.DataConnection
    {
        public AddressBookDB() : base("AddressBook") { }

        /// <summary>
        /// Метод возвращает таблицу данных GroupData
        /// get извлекает некие данные
        /// </summary>
        public ITable<GroupData> Groups { get { return GetTable<GroupData>(); } }

        /// <summary>
        /// Метод возвращает таблицу данных ContactData
        /// get извлекает некие данные
        /// </summary>
        public ITable<ContactData> Contacts { get { return GetTable<ContactData>(); } }

        /// <summary>
        /// Извлекает данные из таблицы группы-контакты
        /// </summary>
        public ITable<GroupContactRelation> GCR { get { return GetTable<GroupContactRelation>(); } }
    }
}
