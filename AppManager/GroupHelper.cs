using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using WebAddressbookTests.Model;

namespace WebAddressbookTests.AppManager
{
    public class GroupHelper : HelperBase
    {
        /// <summary>
        /// чтобы было видно driver, надо создать конструктор, в качестве параметра передается driver
        /// так как есть БАЗОВЫЙ класс, то обращаемся к ЕГО конструктору и передается в качесве параметра ссылка на driver
        /// теперь передаем ссылку на manager, так как в ApplicationManager 
        /// вместо ссылки на драйвер (в скобках) передается ссылка на ApplicationManager
        /// </summary>
        /// <param name="manager"></param>
        public GroupHelper(ApplicationManager manager) : base(manager)
        {
        }

        /// <summary>
        /// метод описыват действия при создании групп
        /// </summary>
        /// <param name="group">Данные, которыми заполняются строки при создании группы</param>
        /// <returns></returns>
        public GroupHelper Create(GroupData group)
        {
            manager.Navigator.GoToGroupsPage();
            InitGroupCreation();
            FillGroupForm(group);
            SubmitGroupCreation();
            ReturnToGroupsPage();
            return this;
        }

        /// <summary>
        /// метод описыват действия для модификации групп
        /// </summary>
        /// <param name="p">Номер группы, который надо модифицировать</param>
        /// <param name="newData">данные для изменения группы</param>
        /// <param name="group">данные для создания группы</param>
        /// <returns></returns>
        public GroupHelper Modify(int p, GroupData newData)
        {
            manager.Navigator.GoToGroupsPage();
            SelectGroup(p);
            InitGroupModification();
            FillGroupForm(newData);
            SubmitGroupModification();
            ReturnToGroupsPage();
            return this;
        }

        /// <summary>
        /// этот метод описыват действия для удаления групп
        /// </summary>
        /// <param name="p">Номер группы, который надо удалить</param>
        /// <returns></returns>
        public GroupHelper Remove(int p)
        {
            manager.Navigator.GoToGroupsPage();
            SelectGroup(p);
            RemoveGroup();
            ReturnToGroupsPage();
            return this;
        }

        /// <summary>
        /// клик по "new group"
        /// Когда вызывается метод в результате возвращается ссылка на него самого
        /// поэтому после public идет не void, а сам GroupHelper
        /// ну и retutn this в конце
        /// </summary>
        /// <returns>Возвращает сам себя</returns>
        public GroupHelper InitGroupCreation()
        {
            driver.FindElement(By.Name("new")).Click();
            return this;
        }

        /// <summary>
        /// Заполняем форму группы
        /// Заолнение выглядит иначе из-за метода Type, который теперь в HelperBase
        /// </summary>
        /// <param name="group">Данные, которыми заполняются строки при создании группы</param>
        /// <returns></returns>
        public GroupHelper FillGroupForm(GroupData group)
        {
            Type(By.Name("group_name"), group.Name);
            Type(By.Name("group_header"), group.Header);
            Type(By.Name("group_footer"), group.Footer);
            return this;
        }

        /// <summary>
        /// подтверждаем создание группы
        /// </summary>
        /// <returns></returns>
        public GroupHelper SubmitGroupCreation()
        {
            driver.FindElement(By.Name("submit")).Click();
            /// Очистка кэша после создания группы
            groupCache = null;
            return this;
        }

        /// <summary>
        /// Возврат на страницу с группами
        /// </summary>
        /// <returns></returns>
        public GroupHelper ReturnToGroupsPage()
        {
            driver.FindElement(By.LinkText("group page")).Click();
            return this;
        }

        /// <summary>
        /// Выбор группы для удаления\модификации (на странице групп). 
        /// </summary>
        /// <param name="index">Добавили int index и указали в тесте какой именно номер надо брать</param>
        /// <returns></returns>
        public GroupHelper SelectGroup(int index)
        {
            /// index + 1 чтобы в тесте указать удаление нулевого элемента, а он как бы удалит первый 
            driver.FindElement(By.XPath("(//input[@name='selected[]'])[" + (index + 1) + "]")).Click();
            return this;
        }

        /// <summary>
        /// Удаление группы
        /// </summary>
        /// <returns></returns>
        public GroupHelper RemoveGroup()
        {
            driver.FindElement(By.Name("delete")).Click();
            /// Очистка кэша после удаления группы
            groupCache = null;
            return this;
        }

        /// <summary>
        /// Подтверждение модификации группы
        /// </summary>
        public GroupHelper SubmitGroupModification()
        {
            driver.FindElement(By.Name("update")).Click();
            /// Очистка кэша после изменения группы
            groupCache = null;
            return this;
        }

        /// <summary>
        /// Клик по кнопке Edit для модификации группы
        /// </summary>
        public GroupHelper InitGroupModification()
        {
            driver.FindElement(By.Name("edit")).Click();
            return this;
        }

        /// <summary>
        /// Проверяет наличие хотя бы одной группы
        /// </summary>
        /// <returns></returns>
        public bool GroupIsHere()
        {
            return IsElementPresent(By.Name("selected[]"));
        }

        /// <summary>
        /// Кэширование списка групп
        /// Тут хранится запомненный сохраненный список групп
        /// </summary>
        /// <returns></returns>
        private List<GroupData> groupCache = null;

        /// <summary>
        /// Cписок групп
        /// </summary>
        /// <returns>Возвращаем заполненный список</returns>
        public List<GroupData> GetGroupList()
        {
            /// Если кэш еще не заполнен
            if (groupCache == null)
            {
                /// Заполняем кэш
                groupCache = new List<GroupData>();
                manager.Navigator.GoToGroupsPage();
                /// Поиск элементов с тегом span и классом group
                /// Для сохранения списка используется переменная elements
                /// Добавляем коллекцию типа ICollection
                /// IWebElement потому что при наведении на FindElements есть IWebElement
                ICollection<IWebElement> elements = driver.FindElements(By.CssSelector("span.group"));
                /// Превратить объекты типа IWebElement в объекты типа GroupData
                /// Для каждого элемента в такой-то коллекции выполнить такое-то действие
                foreach (IWebElement element in elements)
                {                    
                    /// element.Text передать в качестве параметра в конструкторе GroupData
                    /// После создания объекта его необходимо поместить в groupCache
                    /// В данном случае извлекается Id элемента
                    groupCache.Add(new GroupData(element.Text)
                    {
                        Id = element.FindElement(By.TagName("input")).GetAttribute("value")
                    });
                }
            }
            /// Возвращаем запомненный кэш. Новый список, построенный из старого
            /// Защита от добавления\удаления\ новых элементов в кэш
            return new List<GroupData>(groupCache);
        }

        /// <summary>
        /// Считаем количество групп для удобства хэширования
        /// </summary>
        /// <returns></returns>
        public int GetGroupCount()
        {
            return driver.FindElements(By.CssSelector("span.group")).Count;
        }
    }
}
