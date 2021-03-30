using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorochinMCWPF.Entites
{
    public partial class Component
    {
        public string ArchiveInfo
        {
            get
            {
                if (IsArchived)
                    return "Да";
                else
                    return "Нет";
            }
        }
    }
}
