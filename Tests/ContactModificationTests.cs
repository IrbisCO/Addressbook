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
            /// новые данные для модификации. Взяли из ContactCreationTests и заменили название на newData
            ContactData newData = new ContactData("Name1", "Surname1");
            /// Добавил данные для contact, чтобы заполнять контакт, если его нет. Возможно есть вариант лучше
            ContactData contact = new ContactData("Name", "Surname");

            /// Метод возвращает список контактов, список объектов типа ContactData
            /// List - контейнер (коллекция), который хранит набор других объектов 
            /// oldGroups - Старый список групп

            ///проверяется есть ли контакт, который можно изменить
            ///если нет, то создается
            if (!app.Contacts.ContactIsHere())
            {
                app.Contacts.Create(contact);
            }

            List<ContactData> oldContacts = app.Contacts.GetContactList();

            /// Запоминаем элемент с [0] ID
            ContactData oldData = oldContacts[0];

            /// логин и переход на главную сидят в TestBase
            /// оставшийся метод состоит из кучи методов и сидит в GroupHelper
            /// модификация нужного элемента + новые данные
            app.Contacts.Modify(0, newData);

            /// Операция сравнивает количесвто контактов, не читая их названия
            Assert.AreEqual(oldContacts.Count, app.Contacts.GetContactCount());

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

            /// Для каждого контакта в новом списке проверить, что Id этого элемента равен Id измененного
            /// ДЛЯ ИСПРАВЛЕНИЯ ОШБИКИ НАДО УБРАТЬ ContactData contact = new ContactData("Name", "Surname");
            /// НО УБИРАТЬ НЕЛЬЗЯ, ТАК КАК ЭТО ЮЗАЕТСЯ ДЛЯ СОЗДАНИЯ ГРУППЫ В СЛУЧАЕ ЧЕГО
            /// ТАК ЧТО НАДО ДУМАТЬ
            /// 
            /// Можно заменить group на contact1, но выглядит не оч + все еще есть ошибка при отсутсвии контактов (ошибка Баранцева)
            foreach (ContactData contact1 in newContacts)
            {
                /// Найти нужный элемент и проверить, что его имя изменилось
                if (contact1.Id == oldData.Id)
                {
                    Assert.AreEqual(newData.Name, contact1.Name);
                }
            }
        }
    }
}
