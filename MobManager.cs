using System.Collections.Generic;
using System.Linq;

namespace simpleRPG
{
    class MobManager
    {
        public Mob selectedMob { get; private set; }
        private List<Mob> Mobs = new List<Mob>();
        private Mob Create(string name, int hp, int cp, int level)
        {
            Mob m = new Mob(name, hp, cp, level);
            Mobs.Add(m);
            return m;
        }
        private int CalculateMobAmount()
        {
            //implement
            return 2;
        }
        public void ConstructMobs()
        {
            int mobAmount = CalculateMobAmount();
            int HighestFighterLevel = Program.FighterManager.GetHighestLevelFighter();
            int AverageFighterCP = Program.FighterManager.GetFighterAverageCP();
            int AverageFighterHP = Program.FighterManager.GetFighterAverageHP();

            for (int i = 0; i < mobAmount; i++)
            {
                string[] namePool = { "yes", "no" };
                string name = namePool[Program.Rnd.Next(0, namePool.Length - 1)];

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

        private void Select(Mob m) => selectedMob = m;
        public bool MobsAlive() => Mobs.All(x => x.IsAlive());
    }
}
