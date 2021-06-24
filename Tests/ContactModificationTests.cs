/// Модификация контакта

using NUnit.Framework;
using System;
using System.Collections.Generic;
using WebAddressbookTests.AppManager;
using WebAddressbookTests.Model;

namespace WebAddressbookTests.Tests
{
    public class ContactModificationTests : ContactTestBase
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
                    FirstName = HelperBase.GenerateRandomString(10),
                    MiddleName = HelperBase.GenerateRandomString(10),
                    Lastname = HelperBase.GenerateRandomString(10),
                    Nickname = HelperBase.GenerateRandomString(10),
                    Company = HelperBase.GenerateRandomString(10),
                    Title = HelperBase.GenerateRandomString(10),
                    Address = HelperBase.GenerateRandomString(10),
                    HomePhone = HelperBase.GenerateRandomString(10),
                    MobilePhone = HelperBase.GenerateRandomString(10),
                    WorkPhone = HelperBase.GenerateRandomString(10),
                    Fax = HelperBase.GenerateRandomString(10),
                    Email1 = HelperBase.GenerateRandomString(10),
                    Email2 = HelperBase.GenerateRandomString(10),
                    Email3 = HelperBase.GenerateRandomString(10),
                    Homepage = HelperBase.GenerateRandomString(10),
                    Birthday = HelperBase.GenerateRandomDay(),
                    MonthOfBirth = HelperBase.GenerateRandomMonth(),
                    YearhOfBirth = HelperBase.GenerateRandomYear(),
                    AnniversaryDay = HelperBase.GenerateRandomDay(),
                    MonthOfAnniversary = HelperBase.GenerateRandomMonth(),
                    YearOfAnniversary = HelperBase.GenerateRandomYear(),
                    SecondaryAddress = HelperBase.GenerateRandomString(10),
                    SecondaryHomePhone = HelperBase.GenerateRandomString(10),
                    Notes = HelperBase.GenerateRandomString(10)
                };
                app.Contacts.Create(contact);
            }

            /// Метод возвращает список контактов, список объектов типа ContactData
            /// List - контейнер (коллекция), который хранит набор других объектов 
            /// oldGroups - Старый список групп
            List<ContactData> oldContacts = ContactData.GetAll();

            /// Запоминаем элемент с [0] ID
            ContactData toBeModified = oldContacts[0];

            /// логин и переход на главную в TestBase
            /// оставшийся метод состоит из кучи методов в GroupHelper
            /// модификация нужного элемента + новые данные
            ContactData newData = new ContactData
            {
                FirstName = HelperBase.GenerateRandomString(10),
                MiddleName = HelperBase.GenerateRandomString(10),
                Lastname = HelperBase.GenerateRandomString(10),
                Nickname = HelperBase.GenerateRandomString(10),
                Company = HelperBase.GenerateRandomString(10),
                Title = HelperBase.GenerateRandomString(10),
                Address = HelperBase.GenerateRandomString(10),
                HomePhone = HelperBase.GenerateRandomString(10),
                MobilePhone = HelperBase.GenerateRandomString(10),
                WorkPhone = HelperBase.GenerateRandomString(10),
                Fax = HelperBase.GenerateRandomString(10),
                Email1 = HelperBase.GenerateRandomString(10),
                Email2 = HelperBase.GenerateRandomString(10),
                Email3 = HelperBase.GenerateRandomString(10),
                Homepage = HelperBase.GenerateRandomString(10),
                Birthday = HelperBase.GenerateRandomDay(),
                MonthOfBirth = HelperBase.GenerateRandomMonth(),
                YearhOfBirth = HelperBase.GenerateRandomYear(),
                AnniversaryDay = HelperBase.GenerateRandomDay(),
                MonthOfAnniversary = HelperBase.GenerateRandomMonth(),
                YearOfAnniversary = HelperBase.GenerateRandomYear(),
                SecondaryAddress = HelperBase.GenerateRandomString(10),
                SecondaryHomePhone = HelperBase.GenerateRandomString(10),
                Notes = HelperBase.GenerateRandomString(10)
            };
            app.Contacts.Modify(toBeModified, newData);

            /// Операция сравнивает количесвто контактов, не читая их названия
            Assert.AreEqual(oldContacts.Count, app.Contacts.GetContactCount());

            /// Метод возвращает список групп, список объектов типа GroupData
            /// List - контейнер (коллекция), который хранит набор других объектов 
            /// newGroups - новый список групп
            List<ContactData> newContacts = ContactData.GetAll();
            Console.WriteLine(newData);

            /// Берем контакт с нулевым индексом, который модифицировали, и меняем ему имя Name = newData.Name
            oldContacts[0].FirstName = newData.FirstName;
            oldContacts[0].Lastname = newData.Lastname;

            /// Сортируем списки перед сравнением 
            oldContacts.Sort();
            newContacts.Sort();
            /// И сравнивается старыый список с добавленным контактом и новый список из приложения
            Assert.AreEqual(oldContacts, newContacts);

            /// Для каждого контакта в новом списке проверить, что Id этого элемента равен Id измененного
            foreach (ContactData contact in newContacts)
            {
                /// Найти нужный элемент и проверить, что его имя изменилось
                if (contact.Id == toBeModified.Id)
                {
                    Assert.AreEqual(newData.FirstName, contact.FirstName);
                }
            }
        }
    }
}
