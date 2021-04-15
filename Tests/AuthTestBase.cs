///Базовый класс для всех тестов, которые требуют входа в систему 

using NUnit.Framework;
using WebAddressbookTests.Model;

namespace WebAddressbookTests.Tests
{
    public class AuthTestBase : TestBase
    {
        [SetUp]
        ///метод для логина
        public void SetupLogin()
        {
            ///передается один объект AccountData
            ///говорим тесту, что у него есть помощник
            app.Auth.Login(new AccountData("admin", "secret"));
        }
    }
}
