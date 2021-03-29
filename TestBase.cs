using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace WebAddressbookTests
{
    //перенесли это + SetUp + TearDown из основных тестов, так как это повторяется
    public class TestBase
    {
        //тут ссылки на вспомогательные классы-помощники

        //поле driver. Используется в помощниках
        protected IWebDriver driver;
        //тут хз
        private StringBuilder verificationErrors;
        //добавляем поле loginHelper, которое используется в методе SetUp для логина (имя должно отличаться (L и l)
        protected LoginHelper loginHelper;
        //добавляем поле navigator, которое используется в методе SetUp для навигации (имя должно отличаться (N и n)
        protected NavigationHelper navigator;
        //добавляем поле groupHelper, которое используется в методе SetUp для всего по группам (имя должно отличаться (G и g)
        protected GroupHelper groupHelper;
        //поле baseURL, задается ниже
        protected string baseURL;

        [SetUp]
        //метод для инициализации (драйвер, ссылка на главную, помощники)
        public void SetupTest()
        {
            driver = new ChromeDriver();
            baseURL = "http://localhost";
            verificationErrors = new StringBuilder();

            //подключаем помощника по логинам из LoginHelper
            loginHelper = new LoginHelper(driver);
            //подключаем помощника по навигации и NavgationHelper
            navigator = new NavigationHelper(driver, baseURL);
            //подключаем помощника по всему остальному из GroupHelper
            groupHelper = new GroupHelper(driver);
        }

        [TearDown]
        //останавливает драйвер в конце
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
    }
}
