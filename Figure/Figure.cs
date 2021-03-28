using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Addressbook
{
    class Figure
    {
        //можно не указывать true\false, так как работает в любом случае. Не забудь про bool, так как всего два остояния
        private bool colored = false;

        public bool Colored
        {
            get
            {
                return colored;
            }
            set
            {
                colored = value;
            }
        }
    }
}
