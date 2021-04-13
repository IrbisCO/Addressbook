using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests.Tests
{
    class ContactModificationTests : AuthTestBase
    {
        [Test]
        public void ContactModificationTest()
        {
            ///новые данные для модификации. Взяли из GroupCreationTests и заменили название на newData
            ContactData newData = new ContactData("Name1", "Surname1");
            //логин и переход на главную сидят в TestBase
            //оставшийся метод состоит из кучи методов и сидит в GroupHelper
            //модификация нужного элемента + новые данные
            app.Contacts.Modify(1, newData);
        }
    }
}
