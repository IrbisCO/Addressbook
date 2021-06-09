/// Модификация контакта

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
            ///проверяется есть ли контакт, который можно изменить
            ///если нет, то создается
            if (!app.Contacts.ContactIsHere())
            {
                ContactData contact = new ContactData
                {
                    FirstName = GenerateRandomString(10),
                    SecondName = GenerateRandomString(10)
                };
                app.Contacts.Create(contact);
            }

            /// Метод возвращает список контактов, список объектов типа ContactData
            /// List - контейнер (коллекция), который хранит набор других объектов 
            /// oldGroups - Старый список групп
            List<ContactData> oldContacts = app.Contacts.GetContactList();

            /// Запоминаем элемент с [0] ID
            ContactData oldData = oldContacts[0];

            /// логин и переход на главную в TestBase
            /// оставшийся метод состоит из кучи методов в GroupHelper
            /// модификация нужного элемента + новые данные
            ContactData newData = new ContactData
            {
                FirstName = GenerateRandomString(10),
                SecondName = GenerateRandomString(10)
            };
            app.Contacts.Modify(0, newData);

            /// Операция сравнивает количесвто контактов, не читая их названия
            Assert.AreEqual(oldContacts.Count, app.Contacts.GetContactCount());

            /// Метод возвращает список групп, список объектов типа GroupData
            /// List - контейнер (коллекция), который хранит набор других объектов 
            /// newGroups - новый список групп
            List<ContactData> newContacts = app.Contacts.GetContactList();

            /// Берем контакт с нулевым индексом, который модифицировали, и меняем ему имя Name = newData.Name
            oldContacts[0].FirstName = newData.FirstName;
            oldContacts[0].SecondName = newData.SecondName;

            /// Сортируем списки перед сравнением 
            oldContacts.Sort();
            newContacts.Sort();
            /// И сравнивается старыый список с добавленным контактом и новый список из приложения
            Assert.AreEqual(oldContacts, newContacts);

            /// Для каждого контакта в новом списке проверить, что Id этого элемента равен Id измененного
            /// Можно заменить group на contact1, но выглядит не оч
            foreach (ContactData contact in newContacts)
            {
                /// Найти нужный элемент и проверить, что его имя изменилось
                if (contact.Id == oldData.Id)
                {
                    Assert.AreEqual(newData.FirstName, contact.FirstName);
                }
            }
        }
    }
}
