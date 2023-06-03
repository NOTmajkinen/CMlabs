namespace Social.Models
{
    using System;

    public struct User
    {
        public User(DateTime dateOfBirth, int gender, DateTime lastVisit, string name, bool online, int userId)
        {
            DateOfBirth = dateOfBirth;
            Gender = gender;
            LastVisit = lastVisit;
            Name = name;
            Online = online;
            UserId = userId;
        }

        public DateTime DateOfBirth { get; set; }

        public int Gender { get; set; }

        public DateTime LastVisit { get; set; }

        public string Name { get; set; }

        public bool Online { get; set; }

        public int UserId { get; set; }
    }
}
