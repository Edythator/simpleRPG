namespace simpleRPG
{
    class Mob : Entity
    {
        // enkel OOP klass för en ond
        public int Level;
        public Mob(string name, int hp, int cp, int level) : base(name, hp, cp)
        {
            Name = name;
            HP = hp;
            CP = cp;
            Level = level;
        }
    }
}