namespace FDS.Common.Entities
{
    using FDS.Common.DataContext;
    using FDS.Common.DataContext.Enums;

    public class Settings : Entity
    {
        protected Settings(int id) : base(id) { }

        public Settings(
            int id,
            ImportState state)
            : base(id)
        {
            State = state;
        }

        public ImportState State { get; private set; }
    }
}
