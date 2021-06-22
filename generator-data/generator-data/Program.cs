using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using WebAddressbookTests.AppManager;
using WebAddressbookTests.Model;
using Excel = Microsoft.Office.Interop.Excel;

namespace generator_data
{
    public class Program
    {
        static void Main(string[] args)
        {

            // Удобнее пользоваться генератором
            Console.Write("Введите количество тестовых данных, которые надо сгенерировать: ");

            string count1 = Console.ReadLine();
            int count = Convert.ToInt32(count1);

            Console.Write("Введите формат тестовых данных (csv \\ xml \\ json \\ excel): ");

            string format = Console.ReadLine();

            string filename;
            if (format == "excel")
            {
                Console.Write("Введите имя файла: ");
                filename = Console.ReadLine() + ".xlsx";
            }
            else
            {
                Console.Write("Введите имя файла: ");

                filename = Console.ReadLine() + "." + format;
            }

            Console.Write("Введите тип данных contacts \\ groups: ");
            string type = Console.ReadLine();

            Console.WriteLine("Результат: Создан файл для " + type + " " + filename + " c " + count + " случайными наборами данных");
            Console.ReadKey();

            /*
            //args[0] - количество тествоых данных, которые надо сгенерировать
            int count = Convert.ToInt32(args[0]);
            //args[1] - название файла, в который передается значение
            string filename = args[1];
            //формат тестовых данных
            string format = args[2];
            //тип данных контакты или группы
            string type = args[3];
            */
            if (type == "groups")
            {
                //Создание данных для групп
                List<GroupData> groups = new List<GroupData>();
                //генерация тестовых данных
                for (int i = 0; i < count; i++)
                {
                    groups.Add(new GroupData(HelperBase.GenerateRandomString(10))
                    {
                        Header = HelperBase.GenerateRandomString(10),
                        Footer = HelperBase.GenerateRandomString(10)
                    });
                }

                // Выбор формата для групп
                if (format == "excel")
                {
                    WriteGroupsToExcelFile(groups, filename);
                }
                else
                {
                    //Пишем данные в файл. filename - название файла, в который передается значение
                    StreamWriter writer = new StreamWriter(filename);
                    if (format == "csv")
                    {
                        WriteGroupsToCsvFile(groups, writer);
                    }
                    else if (format == "xml")
                    {
                        WriteGroupsToXmlFile(groups, writer);
                    }
                    else if (format == "json")
                    {
                        WriteGroupsToJsonFile(groups, writer);
                    }
                    else
                    {
                        Console.Out.Write("Unrecognized format" + format);
                    }
                    writer.Close();
                }
            }

            else if (type == "contacts")
            {            //Создание данных для контактов
                List<ContactData> contacts = new List<ContactData>();
                //генерация тестовых данных
                for (int i = 0; i < count; i++)
                {
                    contacts.Add(new ContactData()
                    {
                        FirstName = HelperBase.GenerateRandomString(10),
                        MiddleName = HelperBase.GenerateRandomString(10),
                        Lastname = HelperBase.GenerateRandomString(10),
                        Nickname = HelperBase.GenerateRandomString(10),
                        Company = HelperBase.GenerateRandomString(10),
                        Title = HelperBase.GenerateRandomString(10),
                        Address = HelperBase.GenerateRandomString(10),
                        HomePhone = HelperBase.GenerateRandomString(10),
                        MobilePhone = HelperBase.GenerateRandomString(10),
                        WorkPhone = HelperBase.GenerateRandomString(10),
                        Fax = HelperBase.GenerateRandomString(10),
                        Email1 = HelperBase.GenerateRandomString(10),
                        Email2 = HelperBase.GenerateRandomString(10),
                        Email3 = HelperBase.GenerateRandomString(10),
                        Homepage = HelperBase.GenerateRandomString(10),
                        Birthday = HelperBase.GenerateRandomString(10),
                        MonthOfBirth = HelperBase.GenerateRandomString(10),
                        YearhOfBirth = HelperBase.GenerateRandomString(10),
                        AnniversaryDay = HelperBase.GenerateRandomString(10),
                        MonthOfAnniversary = HelperBase.GenerateRandomString(10),
                        YearOfAnniversary = HelperBase.GenerateRandomString(10),
                        SecondaryAddress = HelperBase.GenerateRandomString(10),
                        SecondaryHomePhone = HelperBase.GenerateRandomString(10),
                        Notes = HelperBase.GenerateRandomString(10)
                    });
                }

                // Выбор формата для контактов
                if (format == "excel")
                {
                    WriteContactsToExcelFile(contacts, filename);
                }
                else
                {
                    //Пишем данные в файл. filename - название файла, в который передается значение
                    StreamWriter writer = new StreamWriter(filename);
                    if (format == "csv")
                    {
                        WriteContactsToCsvFile(contacts, writer);
                    }
                    else if (format == "xml")
                    {
                        WriteContactsToXmlFile(contacts, writer);
                    }
                    else if (format == "json")
                    {
                        WriteContactsToJsonFile(contacts, writer);
                    }
                    else
                    {
                        Console.Out.Write("Unrecognized format" + format);
                    }
                    writer.Close();
                }
            }

            else Console.WriteLine("Unknown data type");




        }

        //генератор данных Excel для групп
        static void WriteGroupsToExcelFile(List<GroupData> groups, string filename)
        {
            //Запускаем Excel через COM-интерфейс
            Excel.Application app = new Excel.Application();
            //Для просмотра происходящего в окне
            app.Visible = true;
            //Создается одна страница
            Excel.Workbook wb = app.Workbooks.Add();
            //Получения страницы 
            Excel.Worksheet sheet = wb.ActiveSheet;

            //Записываем данные в Excel
            int row = 1;
            foreach (GroupData group in groups)
            {
                sheet.Cells[row, 1] = group.Name;
                sheet.Cells[row, 2] = group.Header;
                sheet.Cells[row, 3] = group.Footer;
                row++;
            }
            //Сохраняем файл предварительно удалив с таким же названием 
            string fullPath = Path.Combine(Directory.GetCurrentDirectory(), filename);
            File.Delete(fullPath);
            wb.SaveAs(fullPath);
            //закрываем файл
            wb.Close();
            //закрываем Excel
            app.Visible = false;
            //полностью закрываем Excel
            app.Quit();
        }

        //генератор данных Excel для контактов
        static void WriteContactsToExcelFile(List<ContactData> contacts, string filename)
        {
            //Запускаем Excel через COM-интерфейс
            Excel.Application app = new Excel.Application();
            //Для просмотра происходящего в окне
            app.Visible = true;
            //Создается одна страница
            Excel.Workbook wb = app.Workbooks.Add();
            //Получения страницы 
            Excel.Worksheet sheet = wb.ActiveSheet;

            //Записываем данные в Excel
            int row = 1;
            foreach (ContactData contact in contacts)
            {
                sheet.Cells[row, 1] = contact.FirstName;
                sheet.Cells[row, 2] = contact.MiddleName;
                sheet.Cells[row, 3] = contact.Lastname;
                sheet.Cells[row, 4] = contact.Nickname;
                sheet.Cells[row, 5] = contact.Company;
                sheet.Cells[row, 6] = contact.Title;
                sheet.Cells[row, 7] = contact.Address;
                sheet.Cells[row, 8] = contact.HomePhone;
                sheet.Cells[row, 9] = contact.MobilePhone;
                sheet.Cells[row, 10] = contact.WorkPhone;
                sheet.Cells[row, 11] = contact.Fax;
                sheet.Cells[row, 12] = contact.Email1;
                sheet.Cells[row, 13] = contact.Email2;
                sheet.Cells[row, 14] = contact.Email3;
                sheet.Cells[row, 15] = contact.Homepage;
                sheet.Cells[row, 16] = contact.Birthday;
                sheet.Cells[row, 17] = contact.MonthOfBirth;
                sheet.Cells[row, 18] = contact.YearhOfBirth;
                sheet.Cells[row, 19] = contact.AnniversaryDay;
                sheet.Cells[row, 20] = contact.MonthOfAnniversary;
                sheet.Cells[row, 21] = contact.YearOfAnniversary;
                sheet.Cells[row, 22] = contact.SecondaryAddress;
                sheet.Cells[row, 23] = contact.SecondaryHomePhone;
                sheet.Cells[row, 24] = contact.Notes;
                row++;
            }
            //Сохраняем файл предварительно удалив с таким же названием 
            string fullPath = Path.Combine(Directory.GetCurrentDirectory(), filename);
            File.Delete(fullPath);
            wb.SaveAs(fullPath);
            //закрываем файл
            wb.Close();
            //закрываем Excel
            app.Visible = false;
            //полностью закрываем Excel
            app.Quit();
        }

        /// <summary>
        /// Запись в CSV файл групп
        /// </summary>
        /// <param name="groups"></param>
        /// <param name="writer"></param>
        static void WriteGroupsToCsvFile(List<GroupData> groups, StreamWriter writer)
        {
            foreach (GroupData group in groups)
            {
                writer.WriteLine(String.Format("${0},${1},${2}",
                    group.Name, group.Header, group.Footer));
            }
        }

        /// <summary>
        /// Запись в CSV файл контактов
        /// </summary>
        /// <param name="groups"></param>
        /// <param name="writer"></param>
        static void WriteContactsToCsvFile(List<ContactData> contacts, StreamWriter writer)
        {
            foreach (ContactData contact in contacts)
            {
                writer.WriteLine(String.Format("${0},${1},${2},${3},${4},${5},${6},${7},${8},${9},${10},${11},${12},${13},${14},${15},${16},${17},${18},${19},${20},${21},${22},${23}",
                    contact.FirstName,
                    contact.MiddleName,
                    contact.Lastname,
                    contact.Nickname,
                    contact.Company,
                    contact.Title,
                    contact.Address,
                    contact.HomePhone,
                    contact.MobilePhone,
                    contact.WorkPhone,
                    contact.Fax,
                    contact.Email1,
                    contact.Email2,
                    contact.Email3,
                    contact.Homepage,
                    contact.Birthday,
                    contact.MonthOfBirth,
                    contact.YearhOfBirth,
                    contact.AnniversaryDay,
                    contact.MonthOfAnniversary,
                    contact.YearOfAnniversary,
                    contact.SecondaryAddress,
                    contact.SecondaryHomePhone,
                    contact.Notes));
            }
        }

        /// <summary>
        /// Запись в XML файл для групп
        /// </summary>
        /// <param name="groups"></param>
        /// <param name="writer"></param>
        static void WriteGroupsToXmlFile(List<GroupData> groups, StreamWriter writer)
        {
            new XmlSerializer(typeof(List<GroupData>)).Serialize(writer, groups);
        }

        /// <summary>
        /// Запись в XML файл для контактов
        /// </summary>
        /// <param name="contacts"></param>
        /// <param name="writer"></param>
        static void WriteContactsToXmlFile(List<ContactData> contacts, StreamWriter writer)
        {
            new XmlSerializer(typeof(List<ContactData>)).Serialize(writer, contacts);
        }

        /// <summary>
        /// Запись в JSON файл для групп
        /// </summary>
        /// <param name="contacts"></param>
        /// <param name="writer"></param>
        static void WriteGroupsToJsonFile(List<GroupData> groups, StreamWriter writer)
        {
            writer.Write(JsonConvert.SerializeObject(groups, Formatting.Indented));
        }

        /// <summary>
        /// Запись в JSON файл для контактов
        /// </summary>
        /// <param name="contacts"></param>
        /// <param name="writer"></param>
        static void WriteContactsToJsonFile(List<ContactData> contacts, StreamWriter writer)
        {
            writer.Write(JsonConvert.SerializeObject(contacts, Formatting.Indented));
        }

    }
}
