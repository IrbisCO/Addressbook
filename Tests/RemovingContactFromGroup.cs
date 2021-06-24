//Удаление контакта из группы

using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAddressbookTests.Model;

namespace WebAddressbookTests.Tests
{
    public class RemovingContactFromGroup : AuthTestBase
    {
        [Test]
        public void TestRemovingContactFromGroup()
        {
            //получаем список из бд
            GroupData group = GroupData.GetAll()[0];
            app.Contacts.ContactInGroup(group);
            List<ContactData> oldList = group.GetContacts();
            ContactData contact = group.GetContacts().First();
            //выполняем удаление
            app.Contacts.RemoveContactFromGroup(contact, group);
            //сортировка
            List<ContactData> newList = group.GetContacts();
            oldList.Remove(contact);
            newList.Sort();
            oldList.Sort();
            //сравенение 
            Assert.AreEqual(oldList, newList);
        }
    }
}
