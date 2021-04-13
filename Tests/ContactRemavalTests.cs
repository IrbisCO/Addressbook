using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAddressbookTests.Tests
{
        [TestFixture]
        public class ContactRemovalTests : AuthTestBase
        {

            [Test]
            public void ContactRemovalTest()
            {
                //логин и переход на главную сидят в TestBase
                //оставшийся метод состоит из кучи методов и сидит в GroupHelper
                app.Contacts.Remove(1);
            }
        }
    }
