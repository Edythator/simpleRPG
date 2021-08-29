using System.Collections.Generic;
using System.Linq;

namespace simpleRPG
{
    internal sealed class MobManager
    {
        // samma princip som i FighterManager
        public Mob SelectedMob { get; private set; }
        private readonly List<Mob> _mobs = new List<Mob>();
        private Mob Create(string name, int hp, int cp, int level)
        {
            Mob m = new Mob(name, hp, cp, level);
            _mobs.Add(m);
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
            int highestFighterLevel = f.GetHighestLevelFighter();
            int highestFighterCP = f.GetFighterAverageCP();
            int highestFighterHP = f.GetFighterAverageHP();

            for (int i = 0; i < mobAmount; i++)
            {
                string[] namePool = { "yes", "no" };
                string name = namePool[Program.Rnd.Next(0, namePool.Length - 1)];

                //scale const 20
                int hp = Program.Rnd.Next(highestFighterHP - 20, highestFighterHP + 20);
                if (hp < 0)
                    hp = 1;
                int cp = Program.Rnd.Next(highestFighterCP - 20, highestFighterCP + 20);
                if (cp < 0)
                    cp = 1;
                int level = Program.Rnd.Next(highestFighterLevel - 3, highestFighterLevel + 2);
                if (level < 0)
                    level = 1;

                if (i == 0)
                    Select(Create(name, hp, cp, level));
                else
                    Create(name, hp, cp, level);
            }
        }

        // väljer ett main monster
        private void Select(Mob m) => SelectedMob = m;

        // checkar ifall alla lever
        public bool MobsAlive() => _mobs.All(x => x.IsAlive());

        public int GetAverageLevel()
        {
            int avg = 0;
            _mobs.ForEach(x => avg += x.HP);
            return avg;
        }
    }
}
