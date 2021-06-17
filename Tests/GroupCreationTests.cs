﻿/// Создание группы

using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using WebAddressbookTests.Model;

namespace WebAddressbookTests.Tests
{
    [TestFixture]
    public class GroupCreationTests : AuthTestBase
    {
        public static IEnumerable<GroupData> GroupDataFromFile()
        {
            List<GroupData> groups = new List<GroupData>();
            string[] lines = File.ReadAllLines(@"C:\Users\User\source\repos\IrbisCO\Addressbook\group.csv");
            foreach (string l in lines)
            {
                string[] parts = l.Split(',');
                groups.Add(new GroupData(parts[0])
                {
                    Header = parts[1],
                    Footer = parts[2]
                });
            }
            return groups;
        }

        [Test, TestCaseSource("GroupDataFromFile")]
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
            Console.WriteLine(group);

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
