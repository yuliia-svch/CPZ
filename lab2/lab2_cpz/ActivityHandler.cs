using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace lab2_cpz
{
    class ActivityHandler
    {
        public static void ShowMock(EntityFiller entityFiller)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("All clients: ");
            Console.ResetColor();
            foreach (string s in (entityFiller.Clients.Select((c) => c.ToString()).ToList()))
                Console.WriteLine(s);
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("\nAll dresses");
            Console.ResetColor();
            foreach (string s in (entityFiller.Dresses.Select((e) => e.ToString()).ToList()))
                Console.WriteLine(s);
        }
        public static void GroupDressesByOwner(EntityFiller entityFiller)
        {
            var objectGroups = from dress in entityFiller.Dresses
                               group dress by dress.OwnerId;
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("\nDresses by owners:");
            Console.ResetColor();
            foreach (IGrouping<int, Dress> g in objectGroups)
            {
                Console.WriteLine("Owner with ID: " + g.Key);
                foreach (var t in g)
                    Console.WriteLine("  " + t.ToString());
            }
        }
        public static void OrderDressesByColor(EntityFiller entityFiller, bool orderWay)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("\nDresses ordered by color:");
            Console.ResetColor();
            if (orderWay == true)
            {
                var dresses = entityFiller.Dresses.OrderBy(o => o.Color);
                foreach (var v in dresses)
                    Console.WriteLine(v.ToString());
            }
            else
            {
                var dresses = entityFiller.Dresses.OrderByDescending(o => o.Color);
                foreach (var v in dresses)
                    Console.WriteLine(v.ToString());
            }
        }
        public static void ClientsAndDressColors(EntityFiller entityFiller)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("\nClients and dress colors:");
            Console.ResetColor();
            var result = from c in entityFiller.Clients
                         join o in entityFiller.Dresses on c.Id equals o.OwnerId
                         select new { OwnerName = c.Name, OwnerSurname = c.Surname, o.Color };
            foreach (var item in result)
                Console.WriteLine($"{item.OwnerName} {item.OwnerSurname} has dress of color {item.Color}");
        }
        public static void CategoriesAndAvaragePrice(EntityFiller entityFiller)
        {
            var result = entityFiller.Dresses.GroupBy(e => e.Category)
                        .Select(g => new { Name = g.Key, Avrg = g.Average(e => e.Price) });
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("\nCategories and average prices: ");
            Console.ResetColor();
            foreach (var item in result)
                Console.WriteLine($"{item.Name}  ~{item.Avrg}");
        }
        public static void CheckSize(EntityFiller entityFiller, Size size)
        {
            bool result = entityFiller.Dresses.Any(o => o.DressSize.Equals(size));
            Console.ForegroundColor = ConsoleColor.Blue;
            if (result == false)
                Console.WriteLine("\nNo dresses with such size " + size);
            else
                Console.WriteLine("\nDresses with " + size + " size were found");
            Console.ResetColor();
        }
        public static void CountSumForDresses(EntityFiller entityFiller)
        {
            int sum = entityFiller.Dresses.Sum(o => o.Price);
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"\nAll dresses cost: {sum}");
            Console.ResetColor();
        }
        public static void MinAndMaxPriceForCategory(EntityFiller entityFiller, string category)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            if (!entityFiller.Dresses.Any(o => o.Category.Equals(category)))
            {
                Console.WriteLine("\nThere isn`t such category");
                Console.ResetColor();
                return;
            }
            int min = entityFiller.Dresses.Where(o => o.Category.Equals(category)).Min(o => o.Price);
            int max = entityFiller.Dresses.Max(o => o.Price);
            Console.WriteLine($"\nDress prices are in such range: {min} - {max}");
            Console.ResetColor();
        }
        public static void ClientByCriteria(EntityFiller entityFiller, Func<Client, bool> criteria)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("\nClients by criteria: ");
            Console.ResetColor();
            var result = entityFiller.Clients.Where(criteria);
            foreach (string s in (result.Select((c) => c.ToString()).ToList()))
                Console.WriteLine(s);
        }
        public static void TopExpensive(EntityFiller entityFiller, int limit)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"\nTop {limit} expensive dresses: ");
            Console.ResetColor();
            var result = entityFiller.Dresses.OrderByDescending(o => o.Price).Take(limit);
            foreach (string s in (result.Select((c) => c.ToString()).ToList()))
                Console.WriteLine(s);
        }

    }
}
