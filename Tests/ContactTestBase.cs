//Проверка через бд+UI, если выставлен чекбокс PERFORM_LONG_UI_CHECKS

using Newtonsoft.Json;
using NUnit.Framework;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using WebAddressbookTests.Model;
using Excel = Microsoft.Office.Interop.Excel;

namespace WebAddressbookTests.Tests
{
    public class ContactTestBase : AuthTestBase
    {
        [TearDown] //для выполнения после каждого тестового метода
        public void CompareContactsUI_DB()
        {
            if (PERFORM_LONG_UI_CHECKS)
            {
                List<ContactData> fromUI = app.Contacts.GetContactList();
                List<ContactData> fromDB = ContactData.GetAll();
                fromUI.Sort();
                fromDB.Sort();
                Assert.AreEqual(fromUI, fromDB);
            }
        }

        /// <summary>
        /// Чтение данных из CSV
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<ContactData> ContactDataFromFile()
        {
            List<ContactData> contacts = new List<ContactData>();
            string[] lines = File.ReadAllLines(@"C:\Addressbook\generator-data\generator-data\bin\Debug\contacts.csv");
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
                .Deserialize(new StreamReader(@"C:\Addressbook\generator-data\generator-data\bin\Debug\contacts.xml")); // из файла groups.xml
        }

        /// <summary>
        /// Чтение данных из JSON
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<ContactData> ContactDataFromJsonFile()
        {
            return JsonConvert.DeserializeObject<List<ContactData>>(
                File.ReadAllText(@"C:\Addressbook\generator-data\generator-data\bin\Debug\contacts.json"));
        }

        /// <summary>
        /// Чтение данных из Excel
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<ContactData> ContactDataFromExcelFile()
        {
            List<ContactData> groups = new List<ContactData>();
            Excel.Application app = new Excel.Application(); //создаем приложение
            Excel.Workbook wb = app.Workbooks.Open(Path.Combine(Directory.GetCurrentDirectory(), @"C:\Addressbook\generator-data\generator-data\bin\Debug\contacts.xlsx")); //открываем документ
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
    }
}
