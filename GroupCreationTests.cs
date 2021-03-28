using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace WebAddressbookTests
{
    [TestFixture]
    public class GroupCreationTests
    {
        private IWebDriver driver;
        private StringBuilder verificationErrors;
        private string baseURL;
        private bool acceptNextAlert = true;

        [SetUp]
        public void SetupTest()
        {
            driver = new ChromeDriver();
            baseURL = "http://localhost";
            verificationErrors = new StringBuilder();
        }

        [TearDown]
        public void TeardownTest()
        {
            try
            {
                driver.Quit();
            }
            catch (Exception)
            {
                // Ignore errors if unable to close the browser
            }
            Assert.AreEqual("", verificationErrors.ToString());
        }

        [Test]
        public void GroupCreationTest()
        {
            OpenHomePage();
            //передается один объект AccountData
            Login(new AccountData("admin", "secret"));
            GoToGroupsPage();
            InitGroupCreation();
            //передаем значения в зависимости от конструктора. Если выбран первый (где только name, то передаем только "aaa"
            //а если выбран второй, то надо передать все  три значения (теперь в нем нет смысла)
            //FillGroupForm(new GroupData("aaa", "sss", "ddd"));

            //HO если знаений много, то можно обойтись без конструктора и передать параметры следующим образом:
            //создается группа с неким именем ("ааа")
            //теперь конструктор из GroupData со всеми параметрами не нужен, так как он слишком громоздкий
            GroupData group = new GroupData("aaa");
            //поля Header\Footer, если они не нужны, можно в любой момент убрать, они будут заполнен дефолтными значениями
            group.Header = "sss";
            group.Footer = "ddd";
            //в качестве параметра передаем объект group
            FillGroupForm(group);
            SubmitGroupCreation();
            ReturnToGroupsPage();
        }

        private void ReturnToGroupsPage()
        {
            driver.FindElement(By.LinkText("group page")).Click();
        }

        private void SubmitGroupCreation()
        {
            driver.FindElement(By.Name("submit")).Click();
        }

        private void FillGroupForm(GroupData group)
        {
            driver.FindElement(By.Name("group_name")).Click();
            driver.FindElement(By.Name("group_name")).Clear();
            driver.FindElement(By.Name("group_name")).SendKeys(group.Name);
            driver.FindElement(By.Name("group_header")).Clear();
            driver.FindElement(By.Name("group_header")).SendKeys(group.Header);
            driver.FindElement(By.Name("group_footer")).Clear();
            driver.FindElement(By.Name("group_footer")).SendKeys(group.Footer);
        }

        private void InitGroupCreation()
        {
            driver.FindElement(By.Name("new")).Click();
        }

        private void GoToGroupsPage()
        {
            driver.FindElement(By.LinkText("groups")).Click();
        }

        //метод принимает один параметр типа AccountData, назвать можно как угодно (хоть AccountData irbiska)
        private void Login(AccountData account)
        {
            driver.FindElement(By.Name("user")).Clear();
            //в поля вносятся значения свойств объекта (account.Username). Передаем параметр как объект
            driver.FindElement(By.Name("user")).SendKeys(account.Username);
            driver.FindElement(By.Name("pass")).Click();
            driver.FindElement(By.Name("pass")).Clear();
            //в поля вносятся значения свойств объекта (account.Password)
            driver.FindElement(By.Name("pass")).SendKeys(account.Password);
            driver.FindElement(By.XPath("//input[@value='Login']")).Click();
        }

        private void OpenHomePage()
        {
            driver.Navigate().GoToUrl(baseURL + "/addressbook/");
        }

        private bool IsElementPresent(By by)
        {
            try
            {
                driver.FindElement(by);
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        private bool IsAlertPresent()
        {
            try
            {
                driver.SwitchTo().Alert();
                return true;
            }
            catch (NoAlertPresentException)
            {
                return false;
            }
        }

        private string CloseAlertAndGetItsText()
        {
            try
            {
                IAlert alert = driver.SwitchTo().Alert();
                string alertText = alert.Text;
                if (acceptNextAlert)
                {
                    alert.Accept();
                }
                else
                {
                    alert.Dismiss();
                }
                return alertText;
            }
            finally
            {
                acceptNextAlert = true;
            }
        }
    }
}
