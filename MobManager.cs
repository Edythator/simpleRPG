using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace simpleRPG
{
    class MobManager
    {
        // samma princip som i FighterManager
        public Mob selectedMob { get; private set; }
        private List<Mob> Mobs = new List<Mob>();
        private Mob Create(string name, int hp, int cp, int level)
        {
            Mob m = new Mob(name, hp, cp, level);
            Mobs.Add(m);
            return m;
        }

        // funktion för att kalkylera hur många monster/onda de ska finnas i ett rum/dungeon/whatever
        private int CalculateMobAmount()
        {
            //implement
            return 2;
        }

        // funktion för att konstrurera de onda/monsterna baserat på den mediana leveln, cp och hp i det goda laget
        public void ConstructMobs(FighterManager f)
        {
            int mobAmount = CalculateMobAmount();
            int HighestFighterLevel = f.GetHighestLevelFighter();
            int AverageFighterCP = f.GetFighterAverageCP();
            int AverageFighterHP = f.GetFighterAverageHP();

            for (int i = 0; i < mobAmount; i++)
            {
                string[] namePool = { "yes", "no" };
                string name = namePool[Program.Rnd.Next(0, namePool.Length - 1)];

                //scale const 20
                int hp = Program.Rnd.Next(AverageFighterHP - 20, AverageFighterHP + 20);
                if (hp < 0)
                    hp = 1;
                int cp = Program.Rnd.Next(AverageFighterCP - 20, AverageFighterCP + 20);
                if (cp < 0)
                    cp = 1;
                int level = Program.Rnd.Next(HighestFighterLevel - 3, HighestFighterLevel + 2);
                if (level < 0)
                    level = 1;

                if (i == 0)
                    Select(Create(name, hp, cp, level));
                else
                    Create(name, hp, cp, level);
            }
        }

        // väljer ett main monster
        private void Select(Mob m) => selectedMob = m;

        // checkar ifall alla lever
        public bool MobsAlive() => Mobs.All(x => x.IsAlive());

        public int GetAverageLevel()
        {
            int avg = 0;
            Mobs.ForEach(x => avg += x.HP);
            return avg;
        }
    }
}
