namespace FDS.Common.Models
{
    using FDS.Common.DataContext.Enums;

    public class Settings
    {
        public int Id { get; set; }

        public ImportState State { get; set; }
    }
}
