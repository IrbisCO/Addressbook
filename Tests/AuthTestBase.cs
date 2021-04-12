///Базовый класс для всех тестов, которые требуют входа в систему 

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
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
