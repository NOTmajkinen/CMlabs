namespace Social.Models
{
    using System;
    using System.Collections.Generic;

    public struct Message
    {
        public Message(int authorId, List<int> likes, int messageId, DateTime sendDate, string text)
        {
            AuthorId = authorId;
            Likes = likes;
            MessageId = messageId;
            SendDate = sendDate;
            Text = text;
        }

        public int AuthorId { get; set; }

        public List<int> Likes { get; set; }

        public int MessageId { get; set; }

        public DateTime SendDate { get; set; }

        public string Text { get; set; }
    }
}
