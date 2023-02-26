namespace FDS.Package.Domain.Entities
{
    using FDS.Common.DataContext;

    public class Version : Entity
    {
        public Version(int id, string name)
            : base(id)
        {
            Name = name;
        }

        public string Name { get; private set; }
    }
}
