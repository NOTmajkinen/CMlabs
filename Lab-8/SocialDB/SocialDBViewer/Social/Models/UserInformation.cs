namespace Social.Models
{
    public struct UserInformation
    {
        public UserInformation(string name, bool online, int userId)
        {
            Name = name;
            Online = online;
            UserId = userId;
        }

        public string Name { get; set; }

        public bool Online { get; set; }

        public int UserId { get; set; }
    }
}
