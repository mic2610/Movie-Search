namespace Movies.Business
{
    public partial class Settings
    {
        public class OmdbApi
        {
            public string Key { get; set; }

            public string BaseUrl { get; set; }
        }
    }
}