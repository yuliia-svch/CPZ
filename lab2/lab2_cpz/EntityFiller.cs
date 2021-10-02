using System;
using System.Collections.Generic;
using System.Text;

namespace lab2_cpz
{
    class EntityFiller
    {
        public List<Client> Clients
        {
            get
            {
                return new List<Client>
                {
                    new Client(1, "Yuliia", "Savchenko"),
                    new Client(2, "Yuliia", "Vanchytska"),
                    new Client(3, "Nastya", "Adermakh"),
                    new Client(4, "Ariana", "Grande"),
                    new Client(5, "Taylor", "Swift"),
                    new Client(6, "Billie", "Eilish")
                };
            }
        }
        public List<Dress> Dresses
        {
            get
            {
                return new List<Dress>
                {
                    new Dress(1, 3, "Fluffy", "White", 560, Size.L),
                    new Dress(2, 4, "Straight", "Beige", 670, Size.M),
                    new Dress(3, 1, "Sleeveless", "Sandy", 380, Size.L),
                    new Dress(4, 5, "Straight", "Cream", 590, Size.XS),
                    new Dress(5, 6, "Fluffy", "Mocha", 490, Size.M),
                    new Dress(6, 3, "Fluffy", "Milk White", 780, Size.XL),
                    new Dress(7, 5, "Embroidered", "Ivory", 740, Size.S),
                    new Dress(8, 3, "Train", "Nude", 620, Size.XS),
                    new Dress(9, 2, "Sleeveless", "Champagne", 650, Size.L),
                    new Dress(10, 5, "Train", "White", 430, Size.M),
                    new Dress(11, 1, "Sleeveless", "Champagne", 550, Size.XL),
                    new Dress(12, 3, "Train", "Nude", 780, Size.XS),
                    new Dress(13, 4, "Fluffy", "Milk White", 630, Size.XS),
                    new Dress(14, 6, "Embroidered", "Beige", 540, Size.M),
                };
            }
        }

    }
}
