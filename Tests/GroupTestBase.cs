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
    public class GroupTestBase : AuthTestBase
    {
        [TearDown] //для выполнения после каждого тестового метода
        public void CompareGroupsUI_DB()
        {
            if (PERFORM_LONG_UI_CHECKS)
            {
                List<GroupData> fromUI = app.Groups.GetGroupList();
                List<GroupData> fromDB = GroupData.GetAll();
                fromUI.Sort();
                fromDB.Sort();
                Assert.AreEqual(fromUI, fromDB);
            }
        }
        /// <summary>
        /// Чтение данных из CSV
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<GroupData> GroupDataFromCsvFile()
        {
            List<GroupData> groups = new List<GroupData>();
            // ReadAllLines - читаем все строчки из файла. Возвращаем массив строк string[] lines
            string[] lines = File.ReadAllLines(@"C:\Addressbook\generator-data\generator-data\bin\Debug\groups.csv");
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
                .Deserialize(new StreamReader(@"C:\Addressbook\generator-data\generator-data\bin\Debug\groups.xml")); // из файла groups.xml
        }

        /// <summary>
        /// Чтение данных из JSON
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<GroupData> GroupDataFromJsonFile()
        {
            return JsonConvert.DeserializeObject<List<GroupData>>(
                File.ReadAllText(@"C:\Addressbook\generator-data\generator-data\bin\Debug\groups.json"));
        }

        /// <summary>
        /// Чтение данных из Excel
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<GroupData> GroupDataFromExcelFile()
        {
            List<GroupData> groups = new List<GroupData>();
            Excel.Application app = new Excel.Application(); //создаем приложение
            Excel.Workbook wb = app.Workbooks.Open(Path.Combine(Directory.GetCurrentDirectory(), @"C:\Addressbook\generator-data\generator-data\bin\Debug\groups.xlsx")); //открываем документ
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
    }
}
