/// Тест на проверку логина

using NUnit.Framework;
using WebAddressbookTests.Model;

namespace WebAddressbookTests.Tests
{
    [TestFixture]

    public class LoginTests : TestBase
    {
        [Test]
        public void LoginWithValidCredentials()
        {
            ///Подготовка тестовой ситуации
            app.Auth.Logout();
            ///Действие
            AccountData account = new AccountData("admin", "secret");
            app.Auth.Login(account);
            ///Проверка
            Assert.IsTrue(app.Auth.IsLoggedIn());
        }

        [Test]
        public void LoginWithInvalidCredentials()
        {
            ///Подготовка тестовой ситуации
            app.Auth.Logout();
            ///Действие
            AccountData account = new AccountData("admin", "123456");
            app.Auth.Login(account);
            ///Проверка
            Assert.IsFalse(app.Auth.IsLoggedIn());
        }
    }
}
