using OpenQA.Selenium;
using System;

namespace WebAddressbookTests
{
    public class GroupHelper : HelperBase
    {
        //создается поле типа IWebDriver, но теперь это в HelperBase

        //чтобы было видно driver. надо создать конструктор, в качестве параметра передается driver
        //так как есть БАЗОВЫЙ класс, то обращаемся к ЕГО конструктору и передается в качесве параметра ссылка на driver
        //теперь передаем ссылку на manager, так как в ApplicationManager 
        //вместо ссылки на драйвер (в скобках) передается ссылка на ApplicationManager
        public GroupHelper(ApplicationManager manager) : base(manager)
        {
        }

        //этот метод описыват основные и одинаковые действия при создании групп. Его перенес из тестов GroupCreationTests
        public GroupHelper Create (GroupData group)
        {
            manager.Navigator.GoToGroupsPage();
            InitGroupCreation();
            FillGroupForm(group);
            SubmitGroupCreation();
            ReturnToGroupsPage();
            return this;
        }

        ////этот метод описыват основные и одинаковые действия при удалении групп. Его перенес из тестов GroupRemovalTests
        internal GroupHelper Remove(int p)
        {
            manager.Navigator.GoToGroupsPage();            
            SelectGroup(1);
            RemoveGroup();
            ReturnToGroupsPage();
            return this;
        }

        //клик по "new group"
        //Когда вызывается метод в результате возвращается ссылка на него самого
        //поэтому после public идет не void, а сам GroupHelper
        //ну и retutn this в конце
        public GroupHelper InitGroupCreation()
        {
            driver.FindElement(By.Name("new")).Click();
            return this;
        }

        //заполняем форму групп
        public GroupHelper FillGroupForm(GroupData group)
        {
            driver.FindElement(By.Name("group_name")).Click();
            driver.FindElement(By.Name("group_name")).Clear();
            driver.FindElement(By.Name("group_name")).SendKeys(group.Name);
            driver.FindElement(By.Name("group_header")).Clear();
            driver.FindElement(By.Name("group_header")).SendKeys(group.Header);
            driver.FindElement(By.Name("group_footer")).Clear();
            driver.FindElement(By.Name("group_footer")).SendKeys(group.Footer);
            return this;
        }

        //подтверждаем создание группы
        public GroupHelper SubmitGroupCreation()
        {
            driver.FindElement(By.Name("submit")).Click();
            return this;
        }

        //возврат на страницу с группами
        public GroupHelper ReturnToGroupsPage()
        {
            driver.FindElement(By.LinkText("group page")).Click();
            return this;
        }

        //выбор группы (на стрнице групп). Добавили int index и указали в тесте какой именно номер надо брать
        public GroupHelper SelectGroup(int index)
        {
            driver.FindElement(By.XPath("(//input[@name='selected[]'])[" + index + "]")).Click();
            return this;
        }

        //удаление группы
        public GroupHelper RemoveGroup()
        {
            driver.FindElement(By.Name("delete")).Click();
            return this;
        }
    }
}
