using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace lab2_cpz
{
    class Rentals
    {
        private SortedSet<Client> clients = new SortedSet<Client>();
        private SortedSet<Salon> salons = new SortedSet<Salon>();
        private SortedSet<Dress> dresses = new SortedSet<Dress>();
        private SortedSet<Order> orders = new SortedSet<Order>();
        private Dictionary<int, List<Order>> clientsWithOrders = new Dictionary<int, List<Order>>();
        public void AddClient(Client client) => clients.Add(client);
        public void AddClient(params Client[] _clients)
        {
            foreach (Client client in _clients)
                clients.Add(client);
        }
        public void AddSalon(Salon salon) => salons.Add(salon);
        public void AddSalon(params Salon[] _salons)
        {
            foreach (Salon salon in _salons)
                salons.Add(salon);
        }
        public void AddDress(Dress dress) => dresses.Add(dress);
        public void AddDress(params Dress[] _dresses)
        {
            foreach (Dress dress in _dresses)
                dresses.Add(dress);
        }
        public void FormOrder(Client _client, Salon _salon, Dress _dress, DateTime rentStart, int dayDuration)
        {
            Client client = clients.FirstOrDefault((c) => c.Equals(_client));
            Salon salon = salons.FirstOrDefault((a) => a.Equals(_salon));
            Dress dress = dresses.FirstOrDefault((o) => o.Equals(_dress));
            if (client != null && dress != null && salon != null)
            {
                Order contract =
                    new Order
                    (orders.Count + 1, client.Id, salon.Id, dress.Id, dayDuration, rentStart, dress.Price);
                orders.Add(contract);
                RefreshRentals();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("There is no such client, dress or salon");
                Console.ResetColor();
            }
        }
        private void RefreshRentals()
        {
            clientsWithOrders.Clear();
            foreach (Client client in clients)
            {
                int clientId = client.Id;
                var clientOrders = orders.Where((c) => c.ClientId == clientId).ToList();
                clientsWithOrders.Add(clientId, clientOrders);
            }
        }
        public string GetClientsWithOrders()
        {
            Console.WriteLine("Every client has such orders:");
            StringBuilder stringBuilder = new StringBuilder("");
            int[] keys = clientsWithOrders.Keys.ToArray();
            for (int i = 0; i < keys.Length; i++)
            {
                stringBuilder.Append("Id = " + keys[i] + "\n");
                List<Order> clientOrders = clientsWithOrders[keys[i]];
                foreach (Order order in clientOrders)
                    stringBuilder.Append(order + "\n");
            }
            return stringBuilder.ToString();
        }
        public string GetAvailableColors()
        {
            Console.WriteLine("Available colors:");
            StringBuilder stringBuilder = new StringBuilder("");
            var colors = dresses.Select((o) => new { o.Color }).Distinct().ToList();
            foreach (var color in colors)
                stringBuilder.Append(color.Color + " ");
            return stringBuilder.ToString() + "\n";
        }
        public string GetClientsSortedByName()
        {
            Console.WriteLine("Sorted clients by name:");
            StringBuilder stringBuilder = new StringBuilder("");
            List<Client> sortedClients = clients.ToList();
            sortedClients.Sort(new ClientsByNameComparer());
            foreach (var client in sortedClients)
                stringBuilder.Append(client + "\n");
            return stringBuilder.ToString();
        }
        public string GetDressesByOrders()
        {
            Console.WriteLine("Dresses and count of orders:");
            Dictionary<int, int> dressesByContracts = orders.OrdersByObjects();
            StringBuilder stringBuilder = new StringBuilder("");
            foreach (KeyValuePair<int, int> keyValue in dressesByContracts)
            {
                stringBuilder.Append(keyValue.Key + " - " + keyValue.Value + " times\n");
            }
            return stringBuilder.ToString();
        }
        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder("We have clients:\n");
            foreach (Client client in clients)
                stringBuilder.Append(client + "\n");
            stringBuilder.Append("\nSalons:\n");
            foreach (Salon salon in salons)
                stringBuilder.Append(salon + "\n");
            stringBuilder.Append("\nDresses:\n");
            foreach (Dress dress in dresses)
                stringBuilder.Append(dress + "\n");
            stringBuilder.Append("\nOrders:\n");
            foreach (Order order in orders)
                stringBuilder.Append(order + "\n");
            return stringBuilder.ToString();
        }

    }
}
