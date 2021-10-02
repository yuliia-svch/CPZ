using System;
using System.Collections.Generic;
using System.Text;

namespace lab2_cpz
{
    class Dress: IComparable<Dress>
    {
        public int Id { get; private set; }
        public int OwnerId { get; private set; }
        public string Category { get; private set; }
        public string Color { get; private set; }
        public int Price { get; private set; }
        public Size DressSize { get; private set; }
        public Dress(int id, int ownerId, string category, string color, int price, Size dressSize)
        {
            Id = id;
            OwnerId = ownerId;
            Category = category;
            Color = color;
            Price = price;
            DressSize = dressSize;
        }
        public override string ToString()
        {
            return string.Format($"{Id} {OwnerId} {Category} {Color} {Price} {DressSize}");
        }
        public override bool Equals(object obj)
        {
            Dress dress = obj as Dress;
            if (dress == null)
                return false;
            if (dress.Id == Id)
                return true;
            return false;
        }

        public int CompareTo(Dress other)
        {
            if (other.Id == Id)
                return 0;
            else if (other.Id < Id)
                return 1;
            else
                return -1;
        }
    }
    enum Size
    {
        XS,
        S,
        M,
        L,
        XL
    }

}
