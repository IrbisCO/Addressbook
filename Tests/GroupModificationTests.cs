/// Модафикация группы

using NUnit.Framework;
using System;
using System.Collections.Generic;
using WebAddressbookTests.Model;

namespace WebAddressbookTests.Tests
{
    [TestFixture]
    class GroupModificationTests : AuthTestBase
    {
        [Test]
        public void GroupModificationTest()
        {

            /// Проверка наличия хотя бы одной группы
            if (!app.Groups.GroupIsHere())
            {
                GroupData group = new GroupData()
                {
                    Name = GenerateRandomString(10),
                    Header = GenerateRandomString(10),
                    Footer = GenerateRandomString(10)
                };
                /// Создание группы, если ее нет
                app.Groups.Create(group);
            }

            GroupData newData = new GroupData()
            {
                Name = GenerateRandomString(10),
                Header = GenerateRandomString(10),
                Footer = GenerateRandomString(10)
            };
            /// Метод возвращает список групп, список объектов типа GroupData
            /// List - контейнер (коллекция), который хранит набор других объектов 
            /// oldGroups - Старый список групп
            List<GroupData> oldGroups = app.Groups.GetGroupList();

            /// Запоминаем элемент с [0] ID
            GroupData oldData = oldGroups[0];

            /// логин и переход на главную сидят в TestBase
            /// оставшийся метод состоит из кучи методов и сидит в GroupHelper
            /// модификация нужного элемента + новые данные
            app.Groups.Modify(0, newData);

            /// Операция сравнивает количесвто групп, не читая их названия
            Assert.AreEqual(oldGroups.Count, app.Groups.GetGroupCount());

            /// Метод возвращает список групп, список объектов типа GroupData
            /// List - контейнер (коллекция), который хранит набор других объектов 
            /// newGroups - новый список групп
            List<GroupData> newGroups = app.Groups.GetGroupList();
            Console.WriteLine(newData);

            /// Количество элементов в списке
            /// Сравнение не только длины, но и содержимого списков

            /// Берем группу с нулевым индексом, который модифицировали, и меняем ему имя Name = newData.Name
            oldGroups[0].Name = newData.Name;
            /// Сортируем списки перед сравнением 
            oldGroups.Sort();
            newGroups.Sort();
            /// И сравнивается старый список с добавленной группой и новый список из приложения
            Assert.AreEqual(oldGroups, newGroups);

            /// Для каждой группы в новом списке проверить, что Id этого элемента равен Id измененного
            foreach (GroupData group in newGroups)
            {
                /// Найти нужный элемент и проверить, что его имя изменилось
                if (group.Id == oldData.Id)
                {
                    Assert.AreEqual(newData.Name, group.Name);
                }
            }
        }
    }
}
