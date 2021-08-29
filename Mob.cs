namespace simpleRPG
{
    internal class Mob : FightingEntity
    {
        // enkel OOP klass för en ond
        private int _level;
        public Mob(string name, int hp, int cp, int level)
        {
            Name = name;
            HP = hp;
            CP = cp;
            _level = level;
        }
    }
}