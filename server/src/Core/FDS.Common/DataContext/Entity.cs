namespace FDS.Common.DataContext
{
    public class Entity
    {
        public Entity(int id)
        {
            Id = id;
        }

        public int Id { get; private set; }
    }
}
