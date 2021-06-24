// Добавление контакта в группу

using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAddressbookTests.Model;

namespace WebAddressbookTests.Tests
{
    public class AddingContactToGroupTests : AuthTestBase
    {
        [Test]
        public void TestAddingContactToGroup()
        {
            //получаем список из бд
            GroupData group = GroupData.GetAll()[0];
            List<ContactData> oldList = group.GetContacts();
            //Except(oldList) - исключаем контакты, которые уже в выбранной группе
            //First(); - выбираем первый из оставшихся
            ContactData contact = ContactData.GetAll().Except(oldList).First();
            //выполняем добавление
            app.Contacts.AddContactToGroup(contact, group);
            //сортировка
            List<ContactData> newList = group.GetContacts();
            oldList.Add(contact);
            newList.Sort();
            oldList.Sort();
            //сравенение 
            Assert.AreEqual(oldList, newList);
        }
    }
}
