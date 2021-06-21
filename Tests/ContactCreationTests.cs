/// Создание контакта

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
    public class ContactCreationTests : AuthTestBase
    {
        /// <summary>
        /// Чтение данных из CSV
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<ContactData> ContactDataFromFile()
        {
            List<ContactData> contacts = new List<ContactData>();
            string[] lines = File.ReadAllLines(@"C:\Users\User\source\repos\IrbisCO\Addressbook\contacts.csv");
            foreach (string l in lines)
            {
                string[] parts = l.Split(',');
                contacts.Add(new ContactData()
                {
                    FirstName = parts[0],
                    MiddleName = parts[1],
                    Lastname = parts[2],
                    Nickname = parts[3],
                    Title = parts[4],
                    Company = parts[5],
                    Address = parts[6],
                    HomePhone = parts[7],
                    MobilePhone = parts[8],
                    WorkPhone = parts[9],
                    Fax = parts[10],
                    Email1 = parts[11],
                    Email2 = parts[12],
                    Email3 = parts[13],
                    Homepage = parts[14],
                    Birthday = parts[15],
                    MonthOfBirth = parts[16],
                    YearhOfBirth = parts[17],
                    AnniversaryDay = parts[18],
                    MonthOfAnniversary = parts[19],
                    YearOfAnniversary = parts[20],
                    SecondaryAddress = parts[21],
                    SecondaryHomePhone = parts[22],
                    Notes = parts[23],
                });
            }
            return contacts;
        }

        /// <summary>
        /// Чтение данных из XML
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<ContactData> ContactDataFromXmlFile()
        {
            return (List<ContactData>) //приведение типа явно 
                new XmlSerializer(typeof(List<ContactData>)) // читает данные типа List<GroupData>
                .Deserialize(new StreamReader(@"C:\Users\User\source\repos\IrbisCO\Addressbook\contacts.xml")); // из файла groups.xml
        }

        /// <summary>
        /// Чтение данных из JSON
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<ContactData> ContactDataFromJsonFile()
        {
            return JsonConvert.DeserializeObject<List<ContactData>>(
                File.ReadAllText(@"C:\Users\User\source\repos\IrbisCO\Addressbook\contacts.json"));
        }

        /// <summary>
        /// Чтение данных из Excel
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<ContactData> ContactDataFromExcelFile()
        {
            List<ContactData> groups = new List<ContactData>();
            Excel.Application app = new Excel.Application(); //создаем приложение
            Excel.Workbook wb = app.Workbooks.Open(Path.Combine(Directory.GetCurrentDirectory(), @"C:\Users\User\source\repos\IrbisCO\Addressbook\contacts.xlsx")); //открываем документ
            Excel.Worksheet sheet = wb.ActiveSheet; //текущая страница
            Excel.Range range = sheet.UsedRange; //берем прямоугольник с данными
            for (int i = 1; i <= range.Rows.Count; i++)
            {
                groups.Add(new ContactData()
                {
                    FirstName = range.Cells[i, 1].Value,
                    MiddleName = range.Cells[i, 2].Value,
                    Lastname = range.Cells[i, 3].Value,
                    Nickname = range.Cells[i, 4].Value,
                    Title = range.Cells[i, 5].Value,
                    Company = range.Cells[i, 6].Value,
                    Address = range.Cells[i, 7].Value,
                    HomePhone = range.Cells[i, 8].Value,
                    MobilePhone = range.Cells[i, 9].Value,
                    WorkPhone = range.Cells[i, 10].Value,
                    Fax = range.Cells[i, 11].Value,
                    Email1 = range.Cells[i, 12].Value,
                    Email2 = range.Cells[i, 13].Value,
                    Email3 = range.Cells[i, 14].Value,
                    Homepage = range.Cells[i, 15].Value,
                    Birthday = range.Cells[i, 16].Value,
                    MonthOfBirth = range.Cells[i, 17].Value,
                    YearhOfBirth = range.Cells[i, 18].Value,
                    AnniversaryDay = range.Cells[i, 19].Value,
                    MonthOfAnniversary = range.Cells[i, 20].Value,
                    YearOfAnniversary = range.Cells[i, 21].Value,
                    SecondaryAddress = range.Cells[i, 22].Value,
                    SecondaryHomePhone = range.Cells[i, 23].Value,
                    Notes = range.Cells[i, 24].Value
                });
            }
            wb.Close();
            app.Visible = false;
            app.Quit();
            return groups;
        }

        [Test, TestCaseSource("ContactDataFromJsonFile")]
        public void ContactCreationTest(ContactData contact)
        {

            /// Метод возвращает список групп, список объектов типа GroupData
            /// List - контейнер (коллекция), который хранит набор других объектов 
            /// oldGroups - Старый список групп
            List<ContactData> oldContacts = app.Contacts.GetContactList();


            /// логин и переход на главную сидят в TestBase (там же их описание), а так как он от него наследуется, то сам значет что делать
            /// через ApplicationManager взываем к помощникам (app.Navigator, app.Auth, app.Contacts)
            /// Единсвенное действие:
            /// все одинаковые методы были пересены в ContactHelper и теперь вызывается один метод, в котором вызываются другие методы
            app.Contacts.Create(contact);

            /// Операция возвращает количесвто контактов, не читая их названия
            Assert.AreEqual(oldContacts.Count + 1, app.Contacts.GetContactCount());

            /// Метод возвращает список групп, список объектов типа GroupData
            /// List - контейнер (коллекция), который хранит набор других объектов 
            /// newGroups - новый список групп
            List<ContactData> newContacts = app.Contacts.GetContactList();
            Console.WriteLine(contact);
            /// Количество элементов в списке
            /// Сравнение не только длины, но и содержимого списков
            /// К старому списку добавляется новый контакт, который только создали
            oldContacts.Add(contact);
            /// Сортируем списки перед сравнением 
            oldContacts.Sort();
            newContacts.Sort();
            /// И сравнивается старыый список с добавленным контактом и новый список из приложения
            Assert.AreEqual(oldContacts, newContacts);
        }
    }
}