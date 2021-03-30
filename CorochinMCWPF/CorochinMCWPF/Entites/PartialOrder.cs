using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorochinMCWPF.Entites
{
    public partial class Order
    {
        public List<Component> ListOfComponents
        {

            get
            {
                List<Component> buffList = new List<Component>();
                foreach (var item in ComponentOfOrder.ToList())
                {
                    buffList.Add(item.Component);
                }

                return buffList;
            }
        }
    }
}
