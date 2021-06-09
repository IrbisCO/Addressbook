/// Создание группы
/// 7 видео 1 урока 13 минута

using NUnit.Framework;
using System.Collections.Generic;
using WebAddressbookTests.Model;

namespace WebAddressbookTests.Tests
{
    [TestFixture]
    public class GroupCreationTests : AuthTestBase
    {
        /// <summary>
        /// Внешний источник тестовых данных 
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<GroupData> RandomGroupDataProvider()
        {
            /// Новый список
            List<GroupData> groups = new List<GroupData>();
            /// 5 тестовых данных
            for (int i = 0; i < 5; i++)
            {
                /// Генерация случайных данных до 10 символов 
                groups.Add(new GroupData(GenerateRandomString(10))
                {
                    /// Генерация случайных данных до 5 символов 
                    Header = GenerateRandomString(5),
                    Footer = GenerateRandomString(5)
                });
            }
            return groups;
        }

        [Test, TestCaseSource("RandomGroupDataProvider")] //TestCaseSource - внешний источник тестовых данных 
        public void GroupCreationTest(GroupData group)
        {
            /// Метод возвращает список групп, список объектов типа GroupData
            /// List - контейнер (коллекция), который хранит набор других объектов 
            /// oldGroups - Старый список групп
            List<GroupData> oldGroups = app.Groups.GetGroupList();

            /// логин и переход на главную сидят в TestBase (там же их описание), а так как он от него наследуется, то сам значет что делать
            /// через ApplicationManager взываем к помощникам (app.Navigator, app.Auth, app.Groups)
            /// Единсвенное действие
            /// все одинаковые методы были пересены в GroupHelper и теперь вызывается один метод, в котором вызываются другие методы
            app.Groups.Create(group);

            /// Операция возвращает количесвто групп, не читая их названия
            Assert.AreEqual(oldGroups.Count + 1, app.Groups.GetGroupCount());

            /// Метод возвращает список групп, список объектов типа GroupData
            /// List - контейнер (коллекция), который хранит набор других объектов 
            /// newGroups - новый список групп
            List<GroupData> newGroups = app.Groups.GetGroupList();

            /// Количество элементов в списке
            /// Сравнение не только длины, но и содержимого списков
            /// К старому списку добавляется новая группа, которую только создали
            oldGroups.Add(group);
            /// Сортируем списки перед сравнением 
            oldGroups.Sort();
            newGroups.Sort();
            /// И сравнивается старыый список с добавленной группой и новый список из приложения
            Assert.AreEqual(oldGroups, newGroups);
        }       
    }
}
