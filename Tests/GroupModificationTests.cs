using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    class GroupModificationTests : TestBase
    {
        [Test]
        public void GroupModificationTest()
        {
            ///новые данные для модификации. Взяли из GroupCreationTests и заменили название на newData
            GroupData newData = new GroupData("www");
            ///поля Header\Footer, если они не нужны, можно в любой момент убрать, они будут заполнены дефолтными значениями
            ///если написано NULL, то с полем не выполняется каких-либо действий
            newData.Header = null;
            newData.Footer = null;
            //логин и переход на главную сидят в TestBase
            //оставшийся метод состоит из кучи методов и сидит в GroupHelper
            //модификация нужного элемента + новые данные
            app.Groups.Modify(1, newData);
        }
    }
}
