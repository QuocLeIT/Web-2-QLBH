using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebDoAn.Models;

namespace WebDoAn.Helpers
{
    public class Rquest
    {
        public List<User> reQuestSellItem { get; set; }

        public Rquest()
        {
            this.reQuestSellItem = new List<User>();
        }

        public void AddSellItem(User item)
        {
            var eItem = this.reQuestSellItem
                .Where(i => i.f_ID == item.f_ID)
                .FirstOrDefault();

            if (eItem == null)
            {
                this.reQuestSellItem.Add(item);           
            }            
        }


        public void RemoveSellItem(int userid)
        {

            var toDeleteItem = this.reQuestSellItem
               .Where(i => i.f_ID == userid)
               .FirstOrDefault();
            if (toDeleteItem != null)
            {
                this.reQuestSellItem.Remove(toDeleteItem);
            }
        }
    }
}