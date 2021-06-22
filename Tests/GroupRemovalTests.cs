/// Удаление группы

using NUnit.Framework;
using System.Collections.Generic;
using WebAddressbookTests.AppManager;
using WebAddressbookTests.Model;

namespace WebAddressbookTests.Tests
{
    [TestFixture]
    public class GroupRemovalTests : GroupTestBase
    {

        [Test]
        public void GroupRemovalTest()
        {
            /// Проверка наличия хотя бы одной группы
            if (!app.Groups.GroupIsHere())
            {
                GroupData groups = new GroupData(HelperBase.GenerateRandomString(10))
                {
                    Header = HelperBase.GenerateRandomString(10),
                    Footer = HelperBase.GenerateRandomString(10)
                };
                /// Создание группы, если ее нет
                app.Groups.Create(groups);
            }

            /// Метод возвращает список групп, список объектов типа GroupData
            /// List - контейнер (коллекция), который хранит набор других объектов 
            /// oldGroups - Старый список групп
            /// GroupData.GetAll(); - получение списка групп из бд
            List<GroupData> oldGroups = GroupData.GetAll();
            //присваиваем объект с ID
            GroupData toBeRemoved = oldGroups[0];

            /// логин и переход на главную в TestBase
            /// оставшийся метод состоит из кучи методов и находится в GroupHelper
            /// удаляем объект по ID
            app.Groups.Remove(toBeRemoved);

            /// Операция возвращает количесвто групп, не читая их названия
            Assert.AreEqual(oldGroups.Count - 1, app.Groups.GetGroupCount());

            /// Метод возвращает список групп, список объектов типа GroupData
            /// List - контейнер (коллекция), который хранит набор других объектов 
            /// newGroups - новый список групп
            /// GroupData.GetAll(); - получение списка групп из бд
            List<GroupData> newGroups = GroupData.GetAll();

            /// Берем элемент из старого списка и удаляем элемент при помощи метода RemoveAt
            /// Элемент с порядковым номером 1 имеет индекс 0
            oldGroups.RemoveAt(0);

            /// В данном случае сравниваются не размеры, а списки
            /// сравнивается старый список с удаленным элементом (вот для чего метод RemoveAt() с новым списком
            Assert.AreEqual(oldGroups, newGroups);

            /// Для каждой группы в новом списке проверить, что Id этого элемента не равен Id удаленного
            foreach (GroupData groups in newGroups)
            {
                /// Сравнивается с [0], так удаляли элемент с нулевым индексом
                Assert.AreNotEqual(groups.Id, toBeRemoved.Id);
            }
        }
    }
}
