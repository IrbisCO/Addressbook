﻿using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class GroupRemovalTests : TestBase
    {
        
        [Test]
        public void GroupRemovalTest()
        {
            //логин и переход на главную сидят в TestBase
            //оставшийся метод состоит из кучи методов и сидит в GroupHelper
            app.Groups.Remove(1);
        }
    }
}
