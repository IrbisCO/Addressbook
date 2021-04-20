/// Модафикация группы

using NUnit.Framework;
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
            ///новые данные для модификации. Взяли из GroupCreationTests и заменили название на newData
            GroupData newData = new GroupData("www");
            /// Добавил данные для group, чтобы заполнять контакт, если его нет. Возможно есть вариант лучше
            GroupData group = new GroupData("aaa");
            ///поля Header\Footer, если они не нужны, можно в любой момент убрать, они будут заполнены дефолтными значениями
            ///если написано NULL, то с полем не выполняется каких-либо действий
            newData.Header = null;
            newData.Footer = null;

            /// Метод возвращает список групп, список объектов типа GroupData
            /// List - контейнер (коллекция), который хранит набор других объектов 
            /// oldGroups - Старый список групп
            List<GroupData> oldGroups = app.Groups.GetGroupList();

            /// логин и переход на главную сидят в TestBase
            /// оставшийся метод состоит из кучи методов и сидит в GroupHelper
            /// модификация нужного элемента + новые данные
            app.Groups.Modify(0, newData, group);

            /// Операция возвращает количесвто групп, не читая их названия
            Assert.AreEqual(oldGroups.Count, app.Groups.GroupsGetGroupCount());

            /// Метод возвращает список групп, список объектов типа GroupData
            /// List - контейнер (коллекция), который хранит набор других объектов 
            /// newGroups - новый список групп
            List<GroupData> newGroups = app.Groups.GetGroupList();

            /// Количество элементов в списке
            /// Сравнение не только длины, но и содержимого списков
            
            /// Берем группу с нулевым индексом, который модифицировали, и меняем ему имя Name = newData.Name
            oldGroups[0].Name = newData.Name;
            /// Сортируем списки перед сравнением 
            oldGroups.Sort();
            newGroups.Sort();
            /// И сравнивается старый список с добавленной группой и новый список из приложения
            Assert.AreEqual(oldGroups, newGroups);
        }
    }
}
