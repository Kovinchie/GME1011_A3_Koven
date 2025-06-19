using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GME1011A3;

namespace HeroInheritance
{
    internal class Robot : Minion
    {
            
    public Robot(int health, int armour) : base(health, armour)
        {
            _armour = armour + 1; // robots are made of metal so have higher armor class
        }
        public override void TakeDamage(int damage)
        {
            _health -= (damage - _armour);
        }
        public override int DealDamage()
        {
            Random rng = new Random();
            return rng.Next(3, 6);
        }

        //Robot special.
        public int RoboBlast()
        {
            Console.WriteLine("**KaChooM**");
            Random rng = new Random();
            return rng.Next(9, 19);
        }

        public override string ToString()
        {
        return "Robot[" + base.ToString() + "]";
        }
    }
}

