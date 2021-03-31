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

        public string FIOfClient
        {
            get
            {
                return $"{LastNameClient} {FirstNameClient}";
            }
        }

        public decimal TotalPrice
        {
            get
            {
                decimal price = 0;
                foreach (var item in AppData.Context.ComponentOfOrder.ToList().Where(p => p.OrderId == Id).ToList())
                {
                    price += item.Count * item.Component.Price;
                }
                return price;
            }
        }

        public string FullStatus
        {
            get
            {
                var returnedValue = "";
                switch (OrderStatusId)
                {
                    case 2:
                        returnedValue = "Выполняется...";
                        break;
                    case 3:
                        returnedValue = "Выполнен...";
                        break;
                    case 4:
                        returnedValue = "Отменен...";
                        break;
                    default:
                        returnedValue = "Произошла ошибка...";
                        break;
                }
                return returnedValue;
            }
        }
    }
}
