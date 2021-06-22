/// Создание группы

using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using Excel = Microsoft.Office.Interop.Excel;
using WebAddressbookTests.Model;

namespace WebAddressbookTests.Tests
{
    [TestFixture]
    public class GroupCreationTests : GroupTestBase
    {
        /// <summary>
        /// Чтение данных из CSV
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<GroupData> GroupDataFromCsvFile()
        {
            List<GroupData> groups = new List<GroupData>();
            // ReadAllLines - читаем все строчки из файла. Возвращаем массив строк string[] lines
            string[] lines = File.ReadAllLines(@"C:\Users\User\source\repos\IrbisCO\Addressbook\groups.csv");
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

        /// <summary>
        /// Чтение данных из XML
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<GroupData> GroupDataFromXmlFile()
        {
            return (List<GroupData>) //приведение типа явно 
                new XmlSerializer(typeof(List<GroupData>)) // читает данные типа List<GroupData>
                .Deserialize(new StreamReader(@"C:\Users\User\source\repos\IrbisCO\Addressbook\groups.xml")); // из файла groups.xml
        }

        /// <summary>
        /// Чтение данных из JSON
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<GroupData> GroupDataFromJsonFile()
        {
            return JsonConvert.DeserializeObject<List<GroupData>>(
                File.ReadAllText(@"C:\Users\User\source\repos\IrbisCO\Addressbook\groups.json"));
        }

        /// <summary>
        /// Чтение данных из Excel
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<GroupData> GroupDataFromExcelFile()
        {
            List<GroupData> groups = new List<GroupData>();
            Excel.Application app = new Excel.Application(); //создаем приложение
            Excel.Workbook wb = app.Workbooks.Open(Path.Combine(Directory.GetCurrentDirectory(), @"C:\Users\User\source\repos\IrbisCO\Addressbook\groups.xlsx")); //открываем документ
            Excel.Worksheet sheet = wb.ActiveSheet; //текущая страница
            Excel.Range range = sheet.UsedRange; //берем прямоугольник с данными
            for (int i = 1; i <= range.Rows.Count; i++)
            {
                groups.Add(new GroupData()
                {
                    Name = range.Cells[i, 1].Value,
                    Header = range.Cells[i, 2].Value,
                    Footer = range.Cells[i, 3].Value
                });
            }           
            wb.Close();
            app.Visible = false;
            app.Quit();
            return groups;
        }

        [Test, TestCaseSource("GroupDataFromXmlFile")]
        public void GroupCreationTest(GroupData group)
        {
            /// Метод возвращает список групп, список объектов типа GroupData
            /// List - контейнер (коллекция), который хранит набор других объектов 
            /// oldGroups - Старый список групп
            /// GroupData.GetAll(); - получение списка групп из бд
            List<GroupData> oldGroups = GroupData.GetAll();

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
            /// GroupData.GetAll(); - получение списка групп из бд
            List<GroupData> newGroups = GroupData.GetAll();
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

        /// <summary>
        /// Читает информацию из БД и выводит на консоль
        /// </summary>
        [Test]
        public void TestDBConnectivity()
        {
            DateTime start = DateTime.Now;
            List<GroupData> fromUi = app.Groups.GetGroupList(); //список групп через web
            DateTime end = DateTime.Now;
            Console.Out.WriteLine("From UI: " + end.Subtract(start)); //вычитаем время начала из времени окончания

            start = DateTime.Now;
            List<GroupData> fromDb = GroupData.GetAll();
            end = DateTime.Now;
            Console.Out.WriteLine("From DB: " + end.Subtract(start));
        }
    }
}
