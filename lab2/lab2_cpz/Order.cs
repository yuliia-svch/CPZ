using System;
using System.Collections.Generic;
using System.Linq;

namespace lab2_cpz
{
    public class Order : IComparable<Order>
    {
        public int Id { get; private set; }
        public int ClientId { get; private set; }
        public int SalonId { get; private set; }
        public int DressId { get; private set; }
        public int DayDuration { get; private set; }
        public DateTime RentStart { get; private set; }
        public int Price { get; private set; }
        public Order(int id, int clientId, int estateAgentId, int estateObjectId, int dayDuration, DateTime rentStart, int pricePerDay)
        {
            Id = id;
            ClientId = clientId;
            SalonId = estateAgentId;
            DressId = estateObjectId;
            DayDuration = dayDuration;
            RentStart = rentStart;
            Price = dayDuration * pricePerDay;
        }
        public override string ToString()
        {
            return string.Format($"{Id} {ClientId} {SalonId} {DressId} {DayDuration} {RentStart} {Price}");
        }
        public override bool Equals(object obj)
        {
            Order order = obj as Order;
            if (order == null)
                return false;
            if (order.Id == Id)
                return true;
            return false;
        }

        public int CompareTo(Order other)
        {
            if (other.Id == Id)
                return 0;
            else if (other.Id < Id)
                return 1;
            else
                return -1;
        }
    }
    public static class IEnumerableExtension
    {
        public static Dictionary<int, int> OrdersByObjects(this SortedSet<Order> orders)
        {
            var contractGroups = from order in orders
                                 group order by order.DressId into g
                                 select new { Id = g.Key, Count = g.Count() };
            return contractGroups.ToDictionary(k => k.Id, v => v.Count);
        }
    }

}
