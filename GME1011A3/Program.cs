using System.Collections.Generic;
using HeroInheritance;

namespace GME1011A3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.White;

            //Epic battle goes here :)
            Random rng = new Random();

            Console.Write("How much health does your hero have?... : ");
            int heroHealth = int.Parse(Console.ReadLine()); // Get hero health from the user

            Console.Write("What is your hero's name?... : ");
            string heroName = Console.ReadLine(); // Get hero name from the user

            Console.Write("What is your hero's strength?... : ");
            int heroStrength = int.Parse(Console.ReadLine()); // Get hero strength from the user

            Fighter hero = new Fighter(heroHealth, heroName, heroStrength); //TODO: Get these arguments from the user - health, name, strength
            Console.WriteLine("Here is our heroic hero: " + hero + "\n\n");
            int specialChance = 100; //the chances of using a special

            Console.Write("How many baddies are there... : ");
            int numBaddies = int.Parse(Console.ReadLine()); // Get number of baddies from the user
            int numAliveBaddies = numBaddies;
            int baddieChance = 3; // the admount of types of minion's incase i add a ROBOT later
            int typeBaddie = 1; 


            //TODO: change this so that it can contain goblins and skellies! Just change the type of the list!!
            List<Minion> baddies = new List<Minion>();

            for (int i = 0; i < numBaddies; i++)
            {
                //TODO: each baddie should have 50% chance of being a goblin, 50% chance of
                //being a skellie. A skellie should have random health between 25 and 30, and 0 armour (remember
                //skellie armour is 0 anyway)
                typeBaddie = rng.Next(1, baddieChance+1);
                if (typeBaddie == 1)
                    baddies.Add(new Goblin(rng.Next(30, 35), rng.Next(1, 5), rng.Next(1, 10)));
                if (typeBaddie == 2)
                    baddies.Add(new Skellie(rng.Next(25, 31), 0));
                if (typeBaddie == 3)
                    baddies.Add(new Robot(rng.Next(20, 25), rng.Next(1, 5))); //if you add a robot, this is how you do it
            }

            //this should work even after you make the changes above
            Console.WriteLine("Here are the baddies!!!");
            for(int i = 0; i < baddies.Count; i++)
            {
                Console.WriteLine(baddies[i]);
            }
            Console.WriteLine("\n\n");
            Console.WriteLine("Let the EPIC battle begin!!!");
            Console.WriteLine("----------------------------");


            //loop runs as long as there are baddies still alive and the hero is still alive!!
            while (numAliveBaddies > 0 && !hero.isDead())
            {
                //figure out which enemy we are going to battle - the first one that isn't dead
                int indexOfEnemy = 0;
                while (baddies[indexOfEnemy].isDead())
                {
                    indexOfEnemy++;
                }

                //hero deals damage first
                Console.WriteLine(hero.GetName() + " is attacking enemy #" + (indexOfEnemy+1) + " of " + numBaddies + ". Eek, it's a " + baddies[indexOfEnemy].GetType().Name);

                specialChance = rng.Next(1, 101);
                int heroDamage = hero.DealDamage();             //how much damage?
                int heroSpecial = hero.DealDamage() + 1;        //special if no type found

                if (hero.GetType()==typeof(Fighter)) // Check for type of hero
                    heroSpecial = ((Fighter)hero).Berserk(); //if the hero is a fighter, use their special attack

                if (specialChance <= 67)
                {
                    Console.WriteLine("Hero deals " + heroDamage + " heroic damage.");
                    baddies[indexOfEnemy].TakeDamage(heroDamage);
                } //baddie takes the damage 

                else if (heroSpecial == 0)
                {
                    Console.WriteLine("unable to use special hero deals " + heroDamage + " heroic damage.");
                    baddies[indexOfEnemy].TakeDamage(heroDamage);
                } //baddie takes the damage 

                else
                {
                    Console.WriteLine("Hero is using their special attack! it deals " + heroSpecial); //hero uses their special attack
                    baddies[indexOfEnemy].TakeDamage(heroSpecial);
                }

                //TODO: The hero doesn't ever use their special attack - but they should. Change the above to 
                //have a 33% chance that the hero uses their special, and 67% that they use their regular attack.
                //If the hero doesn't have enough special power to use their special attack, they do their regular 
                //attack instead - but make a note of it in the output. There's no way for the hero to get more special
                //power points, but if you want to craft a way for that to happen, that's fine.




                //NOTE to coders - armour affects how much damage goblins take, and skellies take
                //half damage - remember that when reviewing the output

                //did we vanquish the baddie we were battling?
                if (baddies[indexOfEnemy].isDead())
                {
                    numAliveBaddies--; //one less baddie to worry about.
                    Console.WriteLine("Enemy #" + (indexOfEnemy+1) + " has been dispatched to void.");
                }
                else //baddie survived, now attacks the hero
                {
                    int baddieDamage = baddies[indexOfEnemy].DealDamage();  //how much damage?
                    int baddieSpecial = baddies[indexOfEnemy].DealDamage() + 1; //special if no type found

                    if (baddies[indexOfEnemy].GetType() == typeof(Goblin)) // Check for type of baddie
                        baddieSpecial = ((Goblin)baddies[indexOfEnemy]).GoblinBite(); //if the baddie is a goblin, use their special attack

                    if (baddies[indexOfEnemy].GetType() == typeof(Skellie)) // Check for type of baddie
                        baddieSpecial = ((Skellie)baddies[indexOfEnemy]).SkellieRattle(); //if the baddie is a skellie, use their special attack

                    if (baddies[indexOfEnemy].GetType() == typeof(Robot)) // Check for type of baddie
                        baddieSpecial = ((Robot)baddies[indexOfEnemy]).RoboBlast(); //if the baddie is a robot, use their special attack

                    specialChance = rng.Next(1, 101); //chance of special attack
                    if (specialChance <= 67) 
                    {
                        Console.WriteLine("Enemy #" + (indexOfEnemy + 1) + " deals " + baddieDamage + " damage!");
                        hero.TakeDamage(baddieDamage); 
                    } //hero takes damage

                    else
                    {
                        Console.WriteLine("Enemy #" + (indexOfEnemy +1) + " is going in for a Devastating blow and deals " + baddieSpecial + " damage!");
                        hero.TakeDamage(baddieSpecial);
                    } //hero takes damage

                    //TODO: The baddie doesn't ever use their special attack - but they should. Change the above to 
                    //have a 33% chance that the baddie uses their special, and 67% that they use their regular attack.




                    //let's look in on our hero.
                    Console.WriteLine(hero.GetName() + " has " + hero.GetHealth() + " health remaining.");
                    if (hero.isDead()) //did the hero die
                    {
                        Console.WriteLine(hero.GetName() + " has died. All hope is lost.");
                    }

                }
                Console.WriteLine("-----------------------------------------");
            }
            //if we made it this far, the hero is victorious! (that's what the message says.
            if(!hero.isDead())
                Console.WriteLine("\nAll enemies have been dispatched!! " + hero.GetName() + " is victorious!");
        }

    }
}