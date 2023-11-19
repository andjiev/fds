namespace FDS.Common.Entities
{
    using System;
    using FDS.Common.DataContext;
    using FDS.Common.DataContext.Enums;

    public class Package : Entity
    {
        protected Package(int id) : base(id) { }

        public Package(
            int id,
            string name,
            string currentVersion,
            string latestVersion,
            int? score,
            string url,
            string description,
            DateTime? updatedOn,
            PackageStatus status)
            : base(id)
        {
            Name = name;
            CurrentVersion = currentVersion;
            LatestVersion = latestVersion;
            Score = score;
            Url = url;
            Description = description;
            UpdatedOn = updatedOn;
            Status = status;
        }

        public string Name { get; private set; }

        public string CurrentVersion { get; private set; }

        public string LatestVersion { get; private set; }

        public int? Score { get; private set; }

        public string Url { get; private set; }

        public string Description { get; private set; }

        public DateTime? UpdatedOn { get; private set; }

        public PackageStatus Status { get; private set; }

        public void UpdateStatus(PackageStatus status)
        {
            Status = status;
        }
    }
}
