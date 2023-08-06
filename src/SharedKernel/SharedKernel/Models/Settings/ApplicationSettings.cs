namespace SharedKernel.Models.Settings
{
    public class ApplicationSettings
    {
        public bool EnableSwagger { get; set; }

        public string[] ClientLocations { get; set; }

        public string LocalDevelopmentLocation { get; set; }

        public int DatabaseTimeout { get; set; }

    }
}
