/// Создание контакта

using NUnit.Framework;
using System;
using System.Collections.Generic;
using WebAddressbookTests.Model;

namespace WebAddressbookTests.Tests
{
    [TestFixture]
    public class ContactCreationTests : ContactTestBase
    {
        [Test, TestCaseSource("ContactDataFromJsonFile")]
        public void ContactCreationTest(ContactData contact)
        {

            /// Метод возвращает список групп, список объектов типа ContactData
            /// List - контейнер (коллекция), который хранит набор других объектов 
            /// oldContacts - Старый список групп
            List<ContactData> oldContacts = ContactData.GetAll();


            /// логин и переход на главную в TestBase (там же их описание), а так как он от него наследуется, то сам значет что делать
            /// через ApplicationManager взываем к помощникам (app.Navigator, app.Auth, app.Contacts)
            /// Единсвенное действие:
            /// все одинаковые методы были пересены в ContactHelper и теперь вызывается один метод, в котором вызываются другие методы
            app.Contacts.Create(contact);

            /// Операция возвращает количесвто контактов, не читая их названия
            Assert.AreEqual(oldContacts.Count + 1, app.Contacts.GetContactCount());

            /// Метод возвращает список групп, список объектов типа GroupData
            /// List - контейнер (коллекция), который хранит набор других объектов 
            /// newGroups - новый список групп
            List<ContactData> newContacts = ContactData.GetAll();
            Console.WriteLine(contact);
            /// Количество элементов в списке
            /// Сравнение не только длины, но и содержимого списков
            /// К старому списку добавляется новый контакт, который только создали
            oldContacts.Add(contact);
            /// Сортируем списки перед сравнением 
            oldContacts.Sort();
            newContacts.Sort();
            /// И сравнивается старыый список с добавленным контактом и новый список из приложения
            Assert.AreEqual(oldContacts, newContacts);
        }
    }
}