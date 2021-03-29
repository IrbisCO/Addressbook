//7 видео 1 урока 13 минута

using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class GroupCreationTests : TestBase
    {
        
        [Test]
        public void GroupCreationTest()
        {
            navigator.GoToHomePage();
            //передается один объект AccountData
            //говорим тесту, что у него есть помощник
            loginHelper.Login(new AccountData("admin", "secret"));
            navigator.GoToGroupsPage();
            groupHelper.InitGroupCreation();
            //передаем значения в зависимости от конструктора. Если выбран первый (где только name, то передаем только "aaa"
            //а если выбран второй, то надо передать все  три значения (теперь в нем нет смысла)
            //FillGroupForm(new GroupData("aaa", "sss", "ddd"));

            //HO если знаений много, то можно обойтись без конструктора и передать параметры следующим образом:
            //создается группа с неким именем ("ааа")
            //теперь конструктор из GroupData со всеми параметрами не нужен, так как он слишком громоздкий
            GroupData group = new GroupData("aaa");
            //поля Header\Footer, если они не нужны, можно в любой момент убрать, они будут заполнен дефолтными значениями
            group.Header = "sss";
            group.Footer = "ddd";
            //в качестве параметра передаем объект group
            groupHelper.FillGroupForm(group);
            groupHelper.SubmitGroupCreation();
            groupHelper.ReturnToGroupsPage();
        }
    }
}
