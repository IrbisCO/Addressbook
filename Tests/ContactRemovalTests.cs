/// Удаление контакта

using NUnit.Framework;
using System.Collections.Generic;
using WebAddressbookTests.AppManager;
using WebAddressbookTests.Model;

namespace WebAddressbookTests.Tests
{
    [TestFixture]
    public class ContactRemovalTests : ContactTestBase
    {

        [Test]
        public void ContactRemovalTest()
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
                    YearhOfBirth = HelperBase.GenerateRandomString(10),
                    AnniversaryDay = HelperBase.GenerateRandomDay(),
                    MonthOfAnniversary = HelperBase.GenerateRandomMonth(),
                    YearOfAnniversary = HelperBase.GenerateRandomString(10),
                    SecondaryAddress = HelperBase.GenerateRandomString(10),
                    SecondaryHomePhone = HelperBase.GenerateRandomString(10),
                    Notes = HelperBase.GenerateRandomString(10)
                };
                app.Contacts.Create(contact);
            }
            /// Метод возвращает список групп, список объектов типа GroupData
            /// List - контейнер (коллекция), который хранит набор других объектов 
            /// oldGroups - Старый список групп
            List<ContactData> oldContacts = ContactData.GetAll();
            //присваиваем объект с ID
            ContactData toBeRemoved = oldContacts[0];

            /// логин и переход на главную сидят в TestBase
            /// оставшийся метод состоит из кучи методов и сидит в GroupHelper
            app.Contacts.Remove(toBeRemoved);

            /// Операция возвращает количесвто контактов, не читая их названия
            Assert.AreEqual(oldContacts.Count - 1, app.Contacts.GetContactCount());

            /// Метод возвращает список групп, список объектов типа ContactData
            /// List - контейнер (коллекция), который хранит набор других объектов 
            /// newGroups - новый список групп
            List<ContactData> newContacts = ContactData.GetAll();

            /// Берем элемент из старого списка и удаляем элемент при помощи метода RemoveAt
            /// Элемент с порядковым номером 1 имеет индекс 0
            oldContacts.RemoveAt(0);

            /// В данном случае сравниваются не размеры, а списки
            /// сравнивается старый список с удаленным элементом (вот для чего метод RemoveAt() с новым списком
            Assert.AreEqual(oldContacts, newContacts);

            /// Для каждого контакта в новом списке проверить, что Id этого элемента равен Id измененного
            foreach (ContactData contact in newContacts)
            {
                /// Сравнивается с [0], так как удаляли элемент с нулевым индексом
                Assert.AreNotEqual(contact.Id, toBeRemoved.Id);
            }
        }
    }
}
