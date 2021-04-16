﻿/// Создание группы
/// 7 видео 1 урока 13 минута

using NUnit.Framework;
using System.Collections.Generic;
using WebAddressbookTests.Model;

namespace WebAddressbookTests.Tests
{
    [TestFixture]
    public class GroupCreationTests : AuthTestBase
    {

        [Test]
        public void GroupCreationTest()
        {
            ///передаем значения в зависимости от конструктора. Если выбран первый (где только name, то передаем только "aaa"
            /// а если выбран второй, то надо передать все  три значения (теперь в нем нет смысла)
            /// FillGroupForm(new GroupData("aaa", "sss", "ddd"));
            GroupData group = new GroupData("aaa")
            {
                /// поля Header\Footer, если они не нужны, можно в любой момент убрать, они будут заполнен дефолтными значениями
                Header = "sss",
                Footer = "ddd"
            };
            /// Метод возвращает список групп, список объектов типа GroupData
            /// List - контейнер (коллекция), который хранит набор других объектов 
            /// oldGroups - Старый список групп
            List<GroupData> oldGroups = app.Groups.GetGroupList();

            /// логин и переход на главную сидят в TestBase (там же их описание), а так как он от него наследуется, то сам значет что делать
            /// через ApplicationManager взываем к помощникам (app.Navigator, app.Auth, app.Groups)
            /// Единсвенное действие
            /// все одинаковые методы были пересены в GroupHelper и теперь вызывается один метод, в котором вызываются другие методы
            app.Groups.Create(group);

            /// Метод возвращает список групп, список объектов типа GroupData
            /// List - контейнер (коллекция), который хранит набор других объектов 
            /// newGroups - новый список групп
            List<GroupData> newGroups = app.Groups.GetGroupList();

            /// Количество элементов в списке
            /// Cравниваем старое количество, добавляем 1, должно получиться новое значение 
            Assert.AreEqual(oldGroups.Count + 1, newGroups.Count);
        }

        [Test]
        /// Группа с пустыми строками
        public void EmptyGroupCreationTest()
        {
            GroupData group = new GroupData("")
            {
                Header = "",
                Footer = ""
            };

            /// Метод возвращает список групп, список объектов типа GroupData
            /// List - контейнер (коллекция), который хранит набор других объектов 
            /// oldGroups - Старый список групп
            List<GroupData> oldGroups = app.Groups.GetGroupList();

            /// логин и переход на главную сидят в TestBase (там же их описание), а так как он от него наследуется, то сам значет что делать
            /// через ApplicationManager взываем к помощникам (app.Navigator, app.Auth, app.Groups)
            /// Единсвенное действие
            /// все одинаковые методы были пересены в GroupHelper и теперь вызывается один метод, в котором вызываются другие методы
            app.Groups.Create(group);

            /// Метод возвращает список групп, список объектов типа GroupData
            /// List - контейнер (коллекция), который хранит набор других объектов 
            /// newGroups - новый список групп
            List<GroupData> newGroups = app.Groups.GetGroupList();

            /// Количество элементов в списке
            /// Cравниваем старое количество, добавляем 1, должно получиться новое значение 
            Assert.AreEqual(oldGroups.Count + 1, newGroups.Count);
        }

        [Test]
        /// Имя с запрещенным символом. Сообщение о создании группы будет, но группа не создастся
        public void BadNameGroupCreationTest()
        {
            GroupData group = new GroupData("a'a")
            {
                Header = "",
                Footer = ""
            };

            /// Метод возвращает список групп, список объектов типа GroupData
            /// List - контейнер (коллекция), который хранит набор других объектов 
            /// oldGroups - Старый список групп
            List<GroupData> oldGroups = app.Groups.GetGroupList();

            /// логин и переход на главную сидят в TestBase (там же их описание), а так как он от него наследуется, то сам значет что делать
            /// через ApplicationManager взываем к помощникам (app.Navigator, app.Auth, app.Groups)
            /// Единсвенное действие
            /// все одинаковые методы были пересены в GroupHelper и теперь вызывается один метод, в котором вызываются другие методы
            app.Groups.Create(group);

            /// Метод возвращает список групп, список объектов типа GroupData
            /// List - контейнер (коллекция), который хранит набор других объектов 
            /// newGroups - новый список групп
            List<GroupData> newGroups = app.Groups.GetGroupList();

            /// Количество элементов в списке
            /// Cравниваем старое количество, добавляем 1, должно получиться новое значение 
            Assert.AreEqual(oldGroups.Count + 1, newGroups.Count);
        }
    }
}
