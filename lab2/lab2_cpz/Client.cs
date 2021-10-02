using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab2_cpz
{
    class Client : IComparable<Client>
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Surname { get; private set; }
        public Client(int id, string name, string surname)
        {
            Id = id;
            Name = name;
            Surname = surname;
        }
        public override string ToString()
        {
            return string.Format($"{Id} {Name} {Surname}");
        }
        public override bool Equals(object obj)
        {
            Client client = obj as Client;
            if (client == null)
                return false;
            if (client.Id == Id)
                return true;
            return false;
        }
        public int CompareTo(Client other)
        {
            if (other.Id == Id)
                return 0;
            else if (other.Id < Id)
                return 1;
            else
                return -1;
        }
    }
    class ClientsByNameComparer : IComparer<Client>
    {
        public int Compare(Client x, Client y)
        {
            return x.Name.CompareTo(y.Name);
        }
    }

}
