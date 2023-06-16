namespace Wini.Admin.Models
{
    public class AppSetting
    {
        /// <summary>
        /// Gets or sets key for encrypt jwt.
        /// </summary>
        public string JwtKey { get; set; }

        public List<int> RoldAdminIds { get; set; }

    }
}