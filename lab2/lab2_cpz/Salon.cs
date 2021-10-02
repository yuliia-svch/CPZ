using System;
using System.Collections.Generic;
using System.Text;

namespace lab2_cpz
{
    class Salon : IComparable<Salon>
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Address { get; private set; }
        public Salon(int id, string name, string address)
        {
            Id = id;
            Name = name;
            Address = address;
        }
        public override string ToString()
        {
            return string.Format($"{Id} {Name} {Address}");
        }
        public override bool Equals(object obj)
        {
            Salon salon = obj as Salon;
            if (salon == null)
                return false;
            if (salon.Id == Id)
                return true;
            return false;
        }

        public int CompareTo(Salon other)
        {
            if (other.Id == Id)
                return 0;
            else if (other.Id < Id)
                return 1;
            else
                return -1;
        }
    }

}
