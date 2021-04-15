///Класс для описания полей при создании контактов

namespace WebAddressbookTests.Model
{
    public class ContactData
    {
        /// <summary>
        /// пара полей для имени\фамилии
        /// </summary>
        private string name;
        private string surname;

        /// <summary>
        /// конструктор. Принимает на вход параметры name\surname
        /// можно сделать несколько конструкторов. Например во втором хотим сделать все поля обязательными
        /// </summary>
        /// <param name="name">Имя</param>
        /// <param name="surname">Фамилия</param>
        public ContactData(string name, string surname)
        {
            this.name = name;
            this.surname = surname;
        }

        /// <summary>
        /// свойства
        /// </summary>
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }

        /// <summary>
        /// свойства
        /// </summary>
        public string Surname
        {
            get
            {
                return surname;
            }
            set
            {
                surname = value;
            }
        }
    }
}
