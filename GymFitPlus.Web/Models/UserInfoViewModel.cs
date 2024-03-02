namespace GymFitPlus.Web.Models
{
    public class UserInfoViewModel
    {
        public string FullName { get; set; } = string.Empty;
        public int Age { get; set; }
        public string Gander { get; set; } = string.Empty;
        public byte[]? Image { get; set; }
        public string? FacebookUrl { get; set; }
        public string? InstagramUrl { get; set; }
        public string? YouTubeUrl { get; set; }
    }
}
