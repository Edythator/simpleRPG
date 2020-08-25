using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace simpleRPG
{
    class Program
    {
        //TODO: implement some kind of function to calculate amount of money loss&gain scaled to level
        //TODO: implement randomization and management of mobs/fighters
        //TODO: implement functions to scale mobs to fighter levels

        public static Random Rnd = new Random();
        static void Main(string[] args)
        {
            bool turn = true;
            int money = 100;
            Fighter baseFighter = new Fighter("Green", 10, 2, 1, "Sandwich");
            Mob mob = new Mob("Devil of Hell", 5, 1, 2);

            Console.WriteLine("Hello. Welcome to the dungeon. Let's continue, shall we?");
            while (baseFighter.HP > 0 && mob.HP > 0)
            {
                if (turn)
                {
                    mob.HP -= Rnd.Next(baseFighter.CP - 2, baseFighter.HP + 2);
                    turn = false;
                }
                else
                {
                    baseFighter.HP -= Rnd.Next(mob.CP - 2, mob.HP + 2);
                    turn = true;
                }
            }

            if (baseFighter.IsAlive()) {
                Console.WriteLine("\nYou lost! :(");
                int moneyLoss = 6;
            } else if (mob.IsAlive()) {
                Console.WriteLine("\nYou won! :D");
                int moneyGain = 5;
            }
        }
    }
}
