/// Создание контакта

using NUnit.Framework;
using System.Collections.Generic;
using WebAddressbookTests.Model;

namespace WebAddressbookTests.Tests
{
    [TestFixture]
    public class ContactCreationTests : AuthTestBase
    {

        [Test]
        public void ContactCreationTest()
        {
            /// передаем значения в зависимости от конструктора. Если выбран только name\surname, то передаем только "Name", "Surname"
            /// поля Name\Surname, если они не нужны, можно в любой момент убрать, они будут заполнены дефолтными значениями (при указании Name = "")
            /// если написано NULL, то с полем не выполняется каких-либо действий
            ContactData contact = new ContactData("Имя", "Фамилия");

            /// Метод возвращает список групп, список объектов типа GroupData
            /// List - контейнер (коллекция), который хранит набор других объектов 
            /// oldGroups - Старый список групп
            List<ContactData> oldContacts = app.Contacts.GetContactList();

            /// логин и переход на главную сидят в TestBase (там же их описание), а так как он от него наследуется, то сам значет что делать
            /// через ApplicationManager взываем к помощникам (app.Navigator, app.Auth, app.Contacts)
            /// Единсвенное действие:
            /// все одинаковые методы были пересены в ContactHelper и теперь вызывается один метод, в котором вызываются другие методы
            app.Contacts.Create(contact);

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

        [Test]
        public void EmptyContactCreationTest()
        {
            /// аналогично ContactCreationTest
            ContactData contact = new ContactData("", "");

            /// Метод возвращает список групп, список объектов типа GroupData
            /// List - контейнер (коллекция), который хранит набор других объектов 
            /// oldGroups - Старый список групп
            List<ContactData> oldContacts = app.Contacts.GetContactList();

            /// логин и переход на главную сидят в TestBase (там же их описание), а так как он от него наследуется, то сам значет что делать
            /// через ApplicationManager взываем к помощникам (app.Navigator, app.Auth, app.Contacts)
            /// Единсвенное действие:
            /// все одинаковые методы были пересены в ContactHelper и теперь вызывается один метод, в котором вызываются другие методы
            app.Contacts.Create(contact);

            /// Метод возвращает список групп, список объектов типа GroupData
            /// List - контейнер (коллекция), который хранит набор других объектов 
            /// newGroups - новый список групп
            List<ContactData> newContacts = app.Contacts.GetContactList();

            /// Количество элементов в списке
            /// Сравнение не только длины, но и содержимого списков
            /// К старому списку добавляется новая группа, которую только создали
            oldContacts.Add(contact);
            /// Сортируем списки перед сравнением 
            oldContacts.Sort();
            newContacts.Sort();
            /// И сравнивается старыый список с добавленной группой и новый список из приложения
            Assert.AreEqual(oldContacts, newContacts);
        }

        [Test]
        public void BadNameContactCreationTest()
        {
            /// аналогично ContactCreationTest
            ContactData contact = new ContactData("'", "'");

            /// Метод возвращает список групп, список объектов типа GroupData
            /// List - контейнер (коллекция), который хранит набор других объектов 
            /// oldGroups - Старый список групп
            List<ContactData> oldContacts = app.Contacts.GetContactList();

            /// логин и переход на главную сидят в TestBase (там же их описание), а так как он от него наследуется, то сам значет что делать
            /// через ApplicationManager взываем к помощникам (app.Navigator, app.Auth, app.Contacts)
            /// Единсвенное действие:
            /// все одинаковые методы были пересены в ContactHelper и теперь вызывается один метод, в котором вызываются другие методы
            app.Contacts.Create(contact);

            /// Метод возвращает список групп, список объектов типа GroupData
            /// List - контейнер (коллекция), который хранит набор других объектов 
            /// newGroups - новый список групп
            List<ContactData> newContacts = app.Contacts.GetContactList();

            /// Количество элементов в списке
            /// Сравнение не только длины, но и содержимого списков

            /// Сортируем списки перед сравнением 
            oldContacts.Sort();
            newContacts.Sort();
            /// И сравнивается старыый список с добавленной группой и новый список из приложения
            Assert.AreEqual(oldContacts, newContacts);
        }
    }
}

