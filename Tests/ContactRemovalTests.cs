/// Удаление контакта

using NUnit.Framework;
using OpenQA.Selenium;
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
            ContactData contact = new ContactData()
            {
                Name = "Name",
                Surname = "Surname"
            };
            ///проверяется есть ли контакт, который можно изменить
            ///если нет, то создается
            if (!app.Contacts.ContactIsHere())
            {
                app.Contacts.Create(contact);
            }
            /// Метод возвращает список групп, список объектов типа GroupData
            /// List - контейнер (коллекция), который хранит набор других объектов 
            /// oldGroups - Старый список групп
            List<ContactData> oldContacts = app.Contacts.GetContactList();

            /// логин и переход на главную сидят в TestBase
            /// оставшийся метод состоит из кучи методов и сидит в GroupHelper
            app.Contacts.Remove(0);

            /// Операция возвращает количесвто контактов, не читая их названия
            Assert.AreEqual(oldContacts.Count - 1, app.Contacts.GetContactCount());

            /// Метод возвращает список групп, список объектов типа ContactData
            /// List - контейнер (коллекция), который хранит набор других объектов 
            /// newGroups - новый список групп
            List<ContactData> newContacts = app.Contacts.GetContactList();

            /// Запоминаем элемент с [0] ID
            ContactData oldData = oldContacts[0];

            /// Берем элемент из старого списка и удаляем элемент при помощи метода RemoveAt
            /// Элемент с порядковым номером 1 имеет индекс 0
            oldContacts.RemoveAt(0);

            /// В данном случае сравниваются не размеры, а списки
            /// сравнивается старый список с удаленным элементом (вот для чего метод RemoveAt() с новым списком
            Assert.AreEqual(oldContacts, newContacts);

            /// Для каждого контакта в новом списке проверить, что Id этого элемента равен Id измененного
            /// ДЛЯ ИСПРАВЛЕНИЯ ОШБИКИ НАДО УБРАТЬ ContactData contact = new ContactData("Name", "Surname");
            /// НО УБИРАТЬ НЕЛЬЗЯ, ТАК КАК ЭТО ЮЗАЕТСЯ ДЛЯ СОЗДАНИЯ ГРУППЫ В СЛУЧАЕ ЧЕГО
            /// ТАК ЧТО НАДО ДУМАТЬ
            /// 
            /// Можно заменить group на contact1, но выглядит не оч + все еще есть ошибка при отсутсвии контактов (ошибка Баранцева)
            foreach (ContactData contact1 in newContacts)
            {
                /// Сравнивается с [0], так как удаляли элемент с нулевым индексом
                Assert.AreNotEqual(contact1.Id, oldData.Id);
            }
        }
    }
}
