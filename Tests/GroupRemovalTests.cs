using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;

namespace WebAddressbookTests.Tests
{
    [TestFixture]
    public class GroupRemovalTests : AuthTestBase
    {
        
        [Test]
        public void GroupRemovalTest()
        {
            GroupData group = new GroupData("aaa");
            //логин и переход на главную сидят в TestBase
            //оставшийся метод состоит из кучи методов и сидит в GroupHelper
            app.Groups.Remove(1, group);
        }
    }
}
