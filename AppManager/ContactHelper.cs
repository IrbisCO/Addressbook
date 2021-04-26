using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using WebAddressbookTests.Model;

namespace WebAddressbookTests.AppManager
{
    public class ContactHelper : HelperBase
    {
        /// <summary>
        /// чтобы было видно driver, надо создать конструктор, в качестве параметра передается driver
        /// так как есть БАЗОВЫЙ класс, то обращаемся к ЕГО конструктору и передается в качесве параметра ссылка на driver
        /// теперь передаем ссылку на manager, так как в ApplicationManager 
        /// вместо ссылки на драйвер (в скобках) передается ссылка на ApplicationManager
        /// </summary>
        /// <param name="manager"></param>
        public ContactHelper(ApplicationManager manager) : base(manager)
        {
        }

        /// <summary>
        /// этот метод описыват действия при создании контактов
        /// </summary>
        /// <param name="contact">Данные, которыми заполняются строки при создании контакта</param>
        /// <returns></returns>
        public ContactHelper Create(ContactData contact)
        {
            manager.Navigator.GoToHome();
            InitContactCreation();
            FillContactForm(contact);
            SubmitContactCreation();
            ReturnToHome();
            return this;
        }

        /// <summary>
        /// этот метод описыват действия для модификации контакта
        /// </summary>
        /// <param name="p">Номер контакта, который надо модифицировать</param>
        /// <param name="newData">новые данные, которые указываются вместо старых</param>
        /// <param name="contact">данные для создания контакта</param>
        /// <returns></returns>
        public ContactHelper Modify(int p, ContactData newData, ContactData contact)
        {
            ///проверяется есть ли контакт, который можно изменить
            ///если нет, то создается
            if (!ContactHere())
            {
                Create(contact);
            }
            ///модификация
            manager.Navigator.GoToHome();
            SelectContact(p);
            InitContactModification(p);
            FillContactForm(newData);
            SubmitContactModification();
            ReturnToHome();
            return this;
        }

        /// <summary>
        /// этот метод описыват действия для удаления контакта
        /// </summary>
        /// <param name="p">Номер контакта, который надо удалить</param>
        /// <param name="contact">данные для создания контакта</param>
        /// <returns></returns>
        public ContactHelper Remove(int p, ContactData contact)
        {
            ///проверяется есть ли контакт, который можно изменить
            ///если нет, то создается
            if (!ContactHere())
            {
                Create(contact);
            }
            ///удаление
            manager.Navigator.GoToHome();
            SelectContact(p);
            DeleteContact();
            return this;
        }

        /// <summary>
        /// клик по "add new"
        /// Когда вызывается метод в результате возвращается ссылка на него самого
        /// поэтому после public идет не void, а сам GroupHelper
        /// ну и retutn this в конце
        /// </summary>
        /// <returns>Возвращает сам себя</returns>
        public ContactHelper InitContactCreation()
        {
            driver.FindElement(By.LinkText("add new")).Click();
            return this;
        }

        /// <summary>
        /// Клик по Edit для модификации контакта
        /// </summary>
        /// <param name="index">номер выбранного Edit</param>
        /// <returns></returns>
        public ContactHelper InitContactModification(int index)
        {
            driver.FindElement(By.XPath("(//img[@alt='Edit'])[" + (index + 1) + "]")).Click();
            return this;
        }

        /// <summary>
        /// Заполнение формы при создании контакта
        /// </summary>
        /// <param name="contact">Данные для заполнения (указываются в тесте)</param>
        /// <returns></returns>
        public ContactHelper FillContactForm(ContactData contact)
        {
            Type(By.Name("firstname"), contact.Name);
            Type(By.Name("lastname"), contact.Surname);
            return this;
        }

        /// <summary>
        /// Подтверждение создания контакта
        /// </summary>
        /// <returns></returns>
        public ContactHelper SubmitContactCreation()
        {
            driver.FindElement(By.Name("submit")).Click();
            contactCache = null;
            return this;
        }

        /// <summary>
        /// Выбор контакта
        /// </summary>
        /// <param name="index">Номер выбранного контакта, задается в тесте</param>
        private void SelectContact(int index)
        {
            /// index + 1 чтобы в тесте указать удаление нулевого элемента, а он как бы удалит первый 
            driver.FindElement(By.XPath("(//input[@name='selected[]'])[" + (index + 1) + "]")).Click();
        }

        /// <summary>
        /// Удаление контакта
        /// </summary>
        /// <returns></returns>
        public ContactHelper DeleteContact()
        {
            driver.FindElement(By.XPath("//input[@value='Delete']")).Click();
            ///Для закрытия алёрта
            ///Если надоест можно использовать driver.SwitchTo().Alert().Accept();
            Assert.IsTrue(Regex.IsMatch(CloseAlertAndGetItsText(), "^Delete 1 addresses[\\s\\S]$"));
            contactCache = null;
            return this;
        }

        /// <summary>
        /// Подтверждение модификации контакта
        /// </summary>
        /// <returns></returns>
        public ContactHelper SubmitContactModification()
        {
            driver.FindElement(By.Name("update")).Click();
            contactCache = null;
            return this;
        }

        /// <summary>
        /// Возврат на страницу Home
        /// </summary>
        /// <returns></returns>
        public ContactHelper ReturnToHome()
        {
            driver.FindElement(By.LinkText("home page")).Click();
            return this;
        }

        /// <summary>
        /// Проверка наличия хотя бы одного контакта (по кнопке Details)
        /// </summary>
        /// <returns></returns>
        public bool ContactHere()
        {
            return IsElementPresent(By.XPath("//img[@alt='Details']"));
        }

        /// <summary>
        /// Кэширование списка контактов
        /// Тут хранится запомненный сохраненный список контактов
        /// </summary>
        /// <returns></returns>
        private List<ContactData> contactCache = null;

        /// <summary>
        /// Cписок контактов
        /// </summary>
        /// <returns>Возвращаем заполненный список</returns>
        public List<ContactData> GetContactList()
        {
            /// Если кэш еще не заполнен
            if (contactCache == null)
            {
                /// Заполняем кэш
                contactCache = new List<ContactData>();
                manager.Navigator.GoToHome();
                /// Поиск элементов с именем entry
                /// Для сохранения списка используется переменная elements
                /// Добавляем коллекцию типа ICollection
                /// IWebElement потому что при наведении на FindElements есть IWebElement
                ICollection<IWebElement> elements = driver.FindElements(By.CssSelector("tr[name='entry']"));
                /// Превратить объекты типа IWebElement в объекты типа ContactData
                /// Для каждого элемента в такой-то коллекции выполнить такое-то действие
                foreach (IWebElement element in elements)
                {
                    /// Выбираем поочередно поля имя и фамилию
                    string Name = element.FindElement(By.XPath("td[3]")).Text;
                    string Surname = element.FindElement(By.XPath("td[2]")).Text;
                    /// element.Text передать в качестве параметра в конструкторе ContactData
                    /// Объект передан явно (через .Text) выше и дальше уже используется Name\Surname
                    /// После создания объекта его необходимо поместить в contacts
                    /// В данном случае извлекается Id элемента
                    contactCache.Add(new ContactData(Name, Surname)
                    {
                        Id = element.FindElement(By.TagName("input")).GetAttribute("value")
                    });                   
                }
            }
            /// Возвращаем запомненный кэш. Новый список, построенный из старого
            /// Защита от добавления\удаления\ новых элементов в кэш
            return new List<ContactData>(contactCache);
        }

        /// <summary>
        /// Считаем количество контактов для удобства хэширования
        /// </summary>
        /// <returns></returns>
        public int GetContactCount()
        {
            return driver.FindElements(By.CssSelector("tr[name='entry']")).Count;
        }
    }
}
