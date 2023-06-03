namespace Social.Models
{
    using System;

    public struct User
    {
        public User(DateTime dateOfBirth, bool gender, DateTime lastVisit, string name, bool online, int userId)
        {
            this.DateOfBirth = dateOfBirth;
            this.Gender = gender;
            this.LastVisit = lastVisit;
            this.Name = name;
            this.Online = online;
            this.UserId = userId;
        }

        public DateTime DateOfBirth { get; set; }

        public bool Gender { get; set; }

        public DateTime LastVisit { get; set; }

        public string Name { get; set; }

        public bool Online { get; set; }

        public int UserId { get; set; }
    }
}
