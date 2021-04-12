//этот класс создан, чтобы одинаковый код из помощников переместить сюда
//Базовый класс для помощников 

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace WebAddressbookTests
{
    public class HelperBase
    {
        //вот это самое поле driver для хелперов
        protected IWebDriver driver;
        protected ApplicationManager manager;

        //конструктор. На вход принимает ссылку на Application manager
        public HelperBase(ApplicationManager manager)
        {
            //сохраняем ссылку на manager, чтобы каждый помощник про него знал
            this.manager = manager;
            //получает ссылку на Driver у менеджера и все этой ссылкой могут пользоваться
            driver = manager.Driver;
        }

        /// <summary>
        /// Перенесен из GroupHelper
        /// Метод делается из обычного Clear-SendKeys выносом By.Name("group_name") и group.Name в локальные переменные,
        /// заменой ими в основной части и рефакторингом
        /// Делается один раз и используется для всех форм
        /// было так:
        /// driver.FindElement(By.Name("group_name")).Clear();
        /// driver.FindElement(By.Name("group_name")).SendKeys(group.Name);
        /// </summary>
        /// <param name="locator">бывший By.Name("group_name"), по которому искалось поле, куда потом записывалось значение</param>
        /// <param name="text">бывший group.Name, где передавалось значение для хедера\футера</param>
        public void Type(By locator, string text)
        {
            ///Если поле text (значение футера\хедера не NULL, то его надо очистить и запонить, иначе не трогать
            if (text != null)
            {
                driver.FindElement(locator).Clear();
                driver.FindElement(locator).SendKeys(text);
            }
        }
        /// <summary>
        /// Проверка наличия элемента
        /// </summary>
        /// <param name="by"></param>
        /// <returns></returns>
        public bool IsElementPresent(By by)
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
    }
}