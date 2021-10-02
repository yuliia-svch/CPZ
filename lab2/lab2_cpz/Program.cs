using System;

namespace lab2_cpz
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("TASK #1");
            Console.ResetColor();
            Client c1 = new Client(1, "Yuliia", "Savchenko");
            Client c2 = new Client(2, "Yuliia", "Vanchytska");
            Client c3 = new Client(3, "Nastya", "Adermakh");
            Salon a1 = new Salon(1, "Your wedding", "Lviv, pr.Svobody 4");
            Salon a2 = new Salon(2, "Cute dresses", "Lyiv, Doroshenka st. 34");
            Dress o1 = new Dress(1, 1, "Sleeveless", "Sandy", 380, Size.L);
            Dress o2 = new Dress(2, 3, "Train", "Nude", 780, Size.XS);
            Dress o3 = new Dress(3, 5, "Embroidered", "Ivory", 740, Size.S);
            Rentals rentals = new Rentals();
            rentals.AddClient(c1, c2, c3);
            rentals.AddDress(o1, o2, o3);
            rentals.AddSalon(a1, a2);
            rentals.FormOrder(c1, a1, o1, DateTime.Now, 4);
            rentals.FormOrder(c1, a1, o2, DateTime.Now, 5);
            rentals.FormOrder(c2, a2, o1, DateTime.Now, 7);
            rentals.FormOrder(c1, a1, o3, DateTime.Now, 4);
            rentals.FormOrder(c1, a1, o1, DateTime.Now, 4);
            Console.WriteLine(rentals.GetClientsWithOrders());
            Console.WriteLine(rentals.GetAvailableColors());
            Console.WriteLine(rentals.GetClientsSortedByName());
            Console.WriteLine(rentals.GetDressesByOrders());

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("TASK #2");
            Console.ResetColor();
            EntityFiller entityFiller = new EntityFiller();
            ActivityHandler.ShowMock(entityFiller);
            ActivityHandler.GroupDressesByOwner(entityFiller);
            ActivityHandler.OrderDressesByColor(entityFiller, true);
            ActivityHandler.ClientsAndDressColors(entityFiller);
            ActivityHandler.CategoriesAndAvaragePrice(entityFiller);
            ActivityHandler.CheckSize(entityFiller, Size.L);
            ActivityHandler.CountSumForDresses(entityFiller);
            ActivityHandler.MinAndMaxPriceForCategory(entityFiller, "Fluffy");
            ActivityHandler.ClientByCriteria(entityFiller, (c) => c.Name.Equals("Nastya"));
            ActivityHandler.TopExpensive(entityFiller, 3);
            Console.ReadLine();

        }
    }
}
