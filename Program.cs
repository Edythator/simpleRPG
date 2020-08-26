using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace simpleRPG
{
    class Program
    {
        //TODO: implement some kind of function to calculate amount of money loss&gain scaled to level
        //TODO: implement randomization and management of mobs/fighters
        //TODO: implement functions to scale mobs to fighter levels
        //TODO: implement saving

        public static Random Rnd = new Random();

        // make sure to not forget to initalize the fighters, the program is useless in its current form without this functionality
        public static Fighter[] Fighters = new Fighter[6];
        static void Main(string[] args)
        {
            bool turn = true;
            int money = 100;
            Mob[] mobs = new Mob[3];

            Console.WriteLine("Hello. Welcome to the dungeon. Let's continue, shall we?");
            while (Fighters.All(x => x.HP > 0) && mobs.All(x => x.HP > 0))
            {
                if (turn)
                {
                    // implement scaling amount of crit derived from the total CP the character can do
                    mobs[0].HP -= Rnd.Next(Fighters[0].CP - 2, Fighters[0].CP + 2);
                    Thread.Sleep(1000);
                    turn = false;
                }
                else
                {
                    Fighters[0].HP -= Rnd.Next(mobs[0].CP - 2, mobs[0].CP + 2);
                    Thread.Sleep(1000);
                    turn = true;
                }
            }

            if (Fighters.All(x => x.IsAlive())) {
                Console.WriteLine("\nYou lost! :(");
                int moneyLoss = 6;
                money -= moneyLoss;
            } else if (mobs.All(x => x.IsAlive())) {
                Console.WriteLine("\nYou won! :D");
                int moneyGain = 5;
                money += moneyGain;
            }
        }
    }
}
