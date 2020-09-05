namespace simpleRPG
{
    public abstract class Entity
    {
        public string Name;
        public int HP;
        public Entity(string name, int hp)
        {
            Name = name;
            HP = hp;
        }
        public bool IsAlive()
        {
            return HP > 0 ? true : false;
        }
    }
}