﻿using OpenQA.Selenium;

namespace WebAddressbookTests
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
        /// этот метод описыват действия при создании групп
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
        /// этот метод описыват действия для модификации групп
        /// </summary>
        /// <param name="p">Номер группы, который надо модифицировать</param>
        /// <param name="newData">новые данные, которые указываются вместо старых</param>
        /// <param name="group">данные для создания группы</param>
        /// <returns></returns>
        public GroupHelper Modify(int p, GroupData newData, GroupData group)
        {
            ///проверяется есть ли группа, которую можно изменить
            ///если нет, то создается
            manager.Navigator.GoToGroupsPage();
            if (!GroupIsHere())
            {
                InitGroupCreation();
                FillGroupForm(group);
                SubmitGroupCreation();
            }
            ///модификация 
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
        /// <param name="group">данные для создания группы</param>
        /// <returns></returns>
        internal GroupHelper Remove(int p, GroupData group)
        {
            ///проверяется есть ли группа, которую можно изменить
            ///если нет, то создается
            manager.Navigator.GoToGroupsPage();
            if (!GroupIsHere())
            {
                InitGroupCreation();
                FillGroupForm(group);
                SubmitGroupCreation();
            }
            ///удаление
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
            driver.FindElement(By.XPath("(//input[@name='selected[]'])[" + index + "]")).Click();
            return this;
        }

        /// <summary>
        /// Удаление группы
        /// </summary>
        /// <returns></returns>
        public GroupHelper RemoveGroup()
        {
            driver.FindElement(By.Name("delete")).Click();
            return this;
        }

        /// <summary>
        /// Подтверждение модификации группы
        /// </summary>
        public GroupHelper SubmitGroupModification()
        {
            driver.FindElement(By.Name("update")).Click();
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
        private bool GroupIsHere()
        {
            return IsElementPresent(By.Name("selected[]"));
        }
    }
}
