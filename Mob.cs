namespace simpleRPG
{
    class Mob : Entity
    {
        public static string name;
        public static int hp;
        public int CP;
        public int Level;
        public Mob() : base(name, hp)
        {
            // add more names
            string[] mobNames = { "yes", "no" };
            Name = mobNames[Program.Rnd.Next(0, mobNames.Length - 1)];

            int averageHP = Program.fighterManager.GetFighterAverageHP();
            int averageCP = Program.fighterManager.GetFighterAverageCP();

            HP = Program.Rnd.Next(averageHP - 20, averageHP + 20);
            CP = Program.Rnd.Next(averageCP - 20, averageCP + 20);

            // base level on fighter lvl
            int highestLvl = Program.fighterManager.GetHighestLevelFighter();
            Level = Program.Rnd.Next(highestLvl - 3, highestLvl + 2);
        }
    }
}
