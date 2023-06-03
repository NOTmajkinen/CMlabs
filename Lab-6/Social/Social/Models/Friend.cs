namespace Social.Models
{
    using System;

    public struct Friend
    {
        public Friend(int fromUserId, DateTime sendDate, int status, int toUserId)
        {
            FromUserId = fromUserId;
            SendDate = sendDate;
            Status = status;
            ToUserId = toUserId;
        }

        public int FromUserId { get; set; }

        public DateTime SendDate { get; set; }

        public int Status { get; set; }

        public int ToUserId { get; set; }
    }
}
