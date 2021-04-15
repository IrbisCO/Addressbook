/// Создание контакта

using NUnit.Framework;
using WebAddressbookTests.Model;

namespace WebAddressbookTests.Tests
{
    [TestFixture]
    public class ContactCreationTests : AuthTestBase
    {

        [Test]
        public void ContactCreationTest()
        {
            /// передаем значения в зависимости от конструктора. Если выбран только name\surname, то передаем только "Name", "Surname"
            /// поля Name\Surname, если они не нужны, можно в любой момент убрать, они будут заполнены дефолтными значениями (при указании Name = "")
            /// если написано NULL, то с полем не выполняется каких-либо действий
            ContactData contact = new ContactData("Name", "Surname");
            /// логин и переход на главную сидят в TestBase (там же их описание), а так как он от него наследуется, то сам значет что делать
            /// через ApplicationManager взываем к помощникам (app.Navigator, app.Auth, app.Contacts)
            /// Единсвенное действие:
            /// все одинаковые методы были пересены в ContactHelper и теперь вызывается один метод, в котором вызываются другие методы
            app.Contacts.Create(contact);
        }

        [Test]
        public void EmptyContactCreationTest()
        {
            /// аналогично ContactCreationTest
            ContactData contact = new ContactData("", "");
            app.Contacts.Create(contact);
        }
    }
}

