/// Хелпер по навигации. Для одинаковых перемещений по странице

using OpenQA.Selenium;

namespace WebAddressbookTests
{
    public class NavigationHelper : HelperBase
    {
        /// <summary>
        /// создаем поле типа string для baseURL
        /// </summary>
        private string baseURL;

        /// <summary>
        /// чтобы было видно driver. надо создать конструктор, в качестве параметра передается driver, только назвать его соотвественно
        /// и теперь, в отличии от LoginHelper, тут передается два параметра
        /// так как есть БАЗОВЫЙ класс, то обращаемся к ЕГО конструктору и передается в качесве параметра ссылка на driver
        /// </summary>
        /// <param name="manager"></param>
        /// <param name="baseURL"></param>
        public NavigationHelper(ApplicationManager manager, string baseURL) : base(manager)
        {
            //в поле присваиваем значение, которое передано как параметр
            this.baseURL = baseURL;
        }

        /// <summary>
        /// страница со входом
        /// </summary>
        public void GoToHomePage()
        {
            ///Если есть нужный адрес
            if (driver.Url == baseURL + "/addressbook/")
            {
                ///...то ничего делать не надо
                return;
            }
            ///иначе переходим на главную
            driver.Navigate().GoToUrl(baseURL + "/addressbook/");
        }

        /// <summary>
        /// Страница групп
        /// </summary>
        public void GoToGroupsPage()
        {
            ///Если есть нужный адрес + кнопка new...
            if (driver.Url == baseURL + "/addressbook/group.php" && IsElementPresent(By.Name("new")))
            {
                ///...то ничего делать не надо
                return;
            }
            ///иначе клик по groups
            driver.FindElement(By.LinkText("groups")).Click();
        }

        /// <summary>
        /// Страница Home (со списком контактов)
        /// </summary>
        public void GoToHome()
        {
            ///Если есть нужный адрес + текст Last name...
            if (driver.Url == baseURL + "addressbook/" && IsElementPresent(By.LinkText("Last name")))
            {
                ///...то ничего делать не надо
                return;
            }
            ///иначе клик по Home
            driver.FindElement(By.LinkText("home")).Click();
        }
    }
}
