/// Удаление контакта

using NUnit.Framework;
using System.Collections.Generic;
using WebAddressbookTests.Model;

namespace WebAddressbookTests.Tests
{
    [TestFixture]
    public class ContactRemovalTests : AuthTestBase
    {

        [Test]
        public void ContactRemovalTest()
        {
            /// Данные для заполнения группы при создании группы для удаления
            ContactData contact = new ContactData("Name", "Surname");

            /// Метод возвращает список групп, список объектов типа GroupData
            /// List - контейнер (коллекция), который хранит набор других объектов 
            /// oldGroups - Старый список групп
            List<ContactData> oldContacts = app.Contacts.GetContactList();

            /// логин и переход на главную сидят в TestBase
            /// оставшийся метод состоит из кучи методов и сидит в GroupHelper
            app.Contacts.Remove(0, contact);

            /// Метод возвращает список групп, список объектов типа GroupData
            /// List - контейнер (коллекция), который хранит набор других объектов 
            /// newGroups - новый список групп
            List<ContactData> newContacts = app.Contacts.GetContactList();

            /// Берем элемент из старого списка и удаляем элемент при помощи метода RemoveAt
            /// Элемент с порядковым номером 1 имеет индекс 0
            oldContacts.RemoveAt(0);

            /// И сравнивается старыый список с добавленным контактом и новый список из приложения
            Assert.AreEqual(oldContacts, newContacts);
        }
    }
}
