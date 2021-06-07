/// Тест проверяет, что для некоторого отдельно взятого контакта информация на главной странице (в таблице) 
/// соответствует информации, представленной в форме редактирования контакта (где задаются все его свойства).

using NUnit.Framework;
using System;
using System.Collections.Generic;
using WebAddressbookTests.Model;

namespace WebAddressbookTests.Tests
{
    [TestFixture]
    public class ContactInformationTests : AuthTestBase
    {
        /// <summary>
        /// Проверка данных из формы и из таблицы
        /// </summary>
        [Test]
        public void TestContactInformation()
        {
            /// Получение информации из таблицы контактов
            ContactData fromTable = app.Contacts.GetContactInformationFromTable(0);
            /// Получение информации из формы редактирования контакта
            ContactData fromForm = app.Contacts.GetContactInformationFromEditForm(0);

            Console.Out.WriteLine(fromForm.SecondaryHomePhone + "\n" + fromForm.SecondaryAddress +"\n" + fromForm.Notes);

            /// Verification
            /// 
            /// Сравнивается имя и фамилия, так как в ContactData только они и сравниваются в методе Equals
            Assert.AreEqual(fromTable, fromForm);
            /// Сравниваем адрес
            Assert.AreEqual(fromTable.Address, fromForm.Address);
            /// Сравниваем телефоны
            Assert.AreEqual(fromTable.AllPhones, fromForm.AllPhones);
        }

        [Test]
        public void TestDetailsInformation()
        {
            /// Получение информации из формы редактирования контакта
            ContactData fromForm = app.Contacts.GetContactInformationFromEditForm(0);
            /// Получение информации на странице просмотра свойств контакта 
            ContactData fromDetails = app.Contacts.GetContactInformationFromDetails(0);

            Console.Out.Write("Information from EditForm:" + "\n\n" + fromForm.ContactDetails + "\n\n" + "Information from Details:" + "\n\n" + fromDetails.ContactDetails);

            /// Verification
            Assert.AreEqual(fromDetails.ContactDetails, fromForm.ContactDetails);
        }
    }
}