/// Помощник для авторицзации 

using OpenQA.Selenium;
using System;
using WebAddressbookTests.Model;

namespace WebAddressbookTests.AppManager
{
    public class LoginHelper : HelperBase
    {
        /// <summary>
        /// чтобы было видно driver. надо создать конструктор, в качестве параметра передается driver
        /// так как есть БАЗОВЫЙ класс, то обращаемся к ЕГО конструктору и передается в качесве параметра ссылка на driver
        /// </summary>
        /// <param name="manager">Обращение к конструктору базового класса</param>
        public LoginHelper(ApplicationManager manager) : base(manager)
        {
        }

        /// <summary>
        /// метод принимает один параметр типа AccountData
        /// </summary>
        /// <param name="account"></param>
        public void Login(AccountData account)
        {
            ///Если залогинен 
            if (IsLoggedIn())
            {
                ///Если залогинен под учеткой, переданной в качестве параметра
                if (IsLoggedIn(account))
                {
                    ///то все ок
                    return;
                }
                ///Если неправильная учетка - Logout
                Logout();
            }
            /// в поля вносятся значения свойств объекта (account.Username). Передаем параметр как объект
            Type(By.Name("user"), account.Username);
            /// в поля вносятся значения свойств объекта (account.Password)
            Type(By.Name("pass"), account.Password);
            driver.FindElement(By.XPath("//input[@value='Login']")).Click();
        }

        /// <summary>
        /// Выход
        /// </summary>
        public void Logout()
        {
            ///Проверка залогинен ли
            if (IsLoggedIn())
            {
                driver.FindElement(By.LinkText("Logout")).Click();
            }
        }

        /// <summary>
        /// Проверка залогинен ли
        /// </summary>
        /// <returns></returns>
        public bool IsLoggedIn()
        {
            ///поиск кнопки logout с помощью IsElementPresent
            return IsElementPresent(By.Name("logout"));
        }

        /// <summary>
        /// Проверка залогинен ли под правильной учеткой
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public bool IsLoggedIn(AccountData account)
        {
            /// Проверка, что залогинен и имя указанное в скобках равно данным из account 
           // return IsLoggedIn()
           //     && driver.FindElement(By.Name("logout")).FindElement(By.TagName("b")).Text == "(" + account.Username + ")";

            /// Проверка, что залогинен и имя указанное в скобках равно данным из account 
            /// GetLoggetUserName - Извлекает имя пользователя, который залогинен
            return IsLoggedIn() && GetLoggetUserName() == account.Username;
                
        }

        public string GetLoggetUserName()
        {
            string text = driver.FindElement(By.Name("logout")).FindElement(By.TagName("b")).Text;
                
            /// Substring - берем кусочек текста. Обрезаем первый и последний символ
            return text.Substring(1, text.Length - 2);
        }
    }
}
