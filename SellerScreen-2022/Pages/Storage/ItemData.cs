using System;
using System.Collections.Generic;
using System.Text;

namespace SellerScreen_2022.Pages.Storage
{
    public class ItemData
    {
        public enum StatusTypes
        {
            Ready,
            Warning,
            Error,
            Deactivated
        }

        public struct Item
        {
            public bool Status { get; set; }
            public string Name { get; set; }
            public double Availible { get; set; }
            public double Price { get; set; }

            public StatusTypes BoolToStatus()
            {
                if (Status && Availible <= 10) return StatusTypes.Ready;
                else if (Status && Price == 0) return StatusTypes.Ready;
                else if (Status && (Availible == 0 || string.IsNullOrEmpty(Name))) return StatusTypes.Ready;
                else return StatusTypes.Deactivated;
            }
        }

        public Dictionary<int, Item> itemList = new Dictionary<int, Item>();
    }
}
