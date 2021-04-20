/// Модафикация контакта

using NUnit.Framework;
using System.Collections.Generic;
using WebAddressbookTests.Model;

namespace WebAddressbookTests.Tests
{
    public class ContactModificationTests : AuthTestBase
    {
        [Test]
        public void ContactModificationTest()
        {
            /// новые данные для модификации. Взяли из ContactCreationTests и заменили название на newData
            ContactData newData = new ContactData("Name1", "Surname1");
            /// Добавил данные для contact, чтобы заполнять контакт, если его нет. Возможно есть вариант лучше
            ContactData contact = new ContactData("Name", "Surname");

            /// Метод возвращает список контактов, список объектов типа ContactData
            /// List - контейнер (коллекция), который хранит набор других объектов 
            /// oldGroups - Старый список групп
            List<ContactData> oldContacts = app.Contacts.GetContactList();

            /// логин и переход на главную сидят в TestBase
            /// оставшийся метод состоит из кучи методов и сидит в GroupHelper
            /// модификация нужного элемента + новые данные
            app.Contacts.Modify(0, newData, contact);

            /// Метод возвращает список групп, список объектов типа GroupData
            /// List - контейнер (коллекция), который хранит набор других объектов 
            /// newGroups - новый список групп
            List<ContactData> newContacts = app.Contacts.GetContactList();

            /// Берем контакт с нулевым индексом, который модифицировали, и меняем ему имя Name = newData.Name
            oldContacts[0].Name = newData.Name;
            oldContacts[0].Surname = newData.Surname;

            /// Сортируем списки перед сравнением 
            oldContacts.Sort();
            newContacts.Sort();
            /// И сравнивается старыый список с добавленным контактом и новый список из приложения
            Assert.AreEqual(oldContacts, newContacts);
        }
    }
}
