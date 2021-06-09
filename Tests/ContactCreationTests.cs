/// Создание контакта

using NUnit.Framework;
using System.Collections.Generic;
using WebAddressbookTests.Model;

namespace WebAddressbookTests.Tests
{
    [TestFixture]
    public class ContactCreationTests : AuthTestBase
    {
        /// <summary>
        /// Внешний источник тестовых данных 
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<ContactData> RandomContactDataProvider()
        {
            /// Новый список
            List<ContactData> contacts = new List<ContactData>();
            /// 5 тестовых данных
            for (int i = 0; i < 5; i++)
            {
                /// Генерация случайных данных 
                contacts.Add(new ContactData()
                {
                    /// Генерация случайных данных до 10 символов 
                    FirstName = GenerateRandomString(10),
                    SecondName = GenerateRandomString(10)
                });
            }
            return contacts;
        }

        [Test, TestCaseSource("RandomContactDataProvider")] //TestCaseSource - внешний источник тестовых данных 
        public void ContactCreationTest(ContactData contact)
        {
            /// Метод возвращает список групп, список объектов типа GroupData
            /// List - контейнер (коллекция), который хранит набор других объектов 
            /// oldGroups - Старый список групп
            List<ContactData> oldContacts = app.Contacts.GetContactList();


            /// логин и переход на главную сидят в TestBase (там же их описание), а так как он от него наследуется, то сам значет что делать
            /// через ApplicationManager взываем к помощникам (app.Navigator, app.Auth, app.Contacts)
            /// Единсвенное действие:
            /// все одинаковые методы были пересены в ContactHelper и теперь вызывается один метод, в котором вызываются другие методы
            app.Contacts.Create(contact);

            /// Операция возвращает количесвто контактов, не читая их названия
            Assert.AreEqual(oldContacts.Count + 1, app.Contacts.GetContactCount());

            /// Метод возвращает список групп, список объектов типа GroupData
            /// List - контейнер (коллекция), который хранит набор других объектов 
            /// newGroups - новый список групп
            List<ContactData> newContacts = app.Contacts.GetContactList();

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