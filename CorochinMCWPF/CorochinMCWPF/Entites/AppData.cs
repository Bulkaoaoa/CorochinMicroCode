using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace CorochinMCWPF.Entites
{
    public class AppData
    {
        public static CorochinMCDBEntities Context = new CorochinMCDBEntities();
        public static Frame MainFrame;
        public static User CurrUser;
    }
}
