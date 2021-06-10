/// Удаление группы
/// 6:41 - ошибка в уроке 4_5

using NUnit.Framework;
using System.Collections.Generic;
using WebAddressbookTests.Model;

namespace WebAddressbookTests.Tests
{
    [TestFixture]
    public class GroupRemovalTests : AuthTestBase
    {
        
        [Test]
        public void GroupRemovalTest()
        {
            /// Данные для заполнения группы при создании группы для удаления
            GroupData group = new GroupData();

            /// Проверка наличия хотя бы одной группы
            if (!app.Groups.GroupIsHere())
            {
                /// Создание группы, если ее нет
                app.Groups.Create(group);
            }

            /// Метод возвращает список групп, список объектов типа GroupData
            /// List - контейнер (коллекция), который хранит набор других объектов 
            /// oldGroups - Старый список групп
            List<GroupData> oldGroups = app.Groups.GetGroupList();

            /// логин и переход на главную сидят в TestBase
            /// оставшийся метод состоит из кучи методов и сидит в GroupHelper
            app.Groups.Remove(0);

            /// Операция возвращает количесвто групп, не читая их названия
            Assert.AreEqual(oldGroups.Count - 1, app.Groups.GetGroupCount());

            /// Метод возвращает список групп, список объектов типа GroupData
            /// List - контейнер (коллекция), который хранит набор других объектов 
            /// newGroups - новый список групп
            List<GroupData> newGroups = app.Groups.GetGroupList();

            /// Берем элемент из старого списка и удаляем элемент при помощи метода RemoveAt
            /// Элемент с порядковым номером 1 имеет индекс 0
            oldGroups.RemoveAt(0);

            /// В данном случае сравниваются не размеры, а списки
            /// сравнивается старый список с удаленным элементом (вот для чего метод RemoveAt() с новым списком
            Assert.AreEqual(oldGroups, newGroups);

            /// Для каждой группы в новом списке проверить, что Id этого элемента не равен Id удаленного
            /// ДЛЯ ИСПРАВЛЕНИЯ ОШБИКИ НАДО УБРАТЬ GroupData group = new GroupData("aaa");
            /// НО УБИРАТЬ НЕЛЬЗЯ, ТАК КАК ЭТО ЮЗАЕТСЯ ДЛЯ СОЗДАНИЯ ГРУППЫ В СЛУЧАЕ ЧЕГО
            /// ТАК ЧТО НАДО ДУМАТЬ
            /// 
            /// Можно заменить group на group1, но выглядит не оч + все еще есть ошибка при отсутсвии групп (ошибка Баранцева)
            foreach (GroupData group1 in newGroups)
            {
                /// Сравнивается с [0], так удаляли элемент с нулевым индексом
                Assert.AreNotEqual(group1.Id, oldGroups[0]);
            }
        }
    }
}
