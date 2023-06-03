namespace Social.Models
{
    using System.Collections.Generic;

    public struct News
    {
        public News(int authorId, string authorName, List<int> likes, string text)
        {
            AuthorId = authorId;
            AuthorName = authorName;
            Likes = likes;
            Text = text;
        }

        public int AuthorId { get; set; }

        public string AuthorName { get; set; }

        public List<int> Likes { get; set; }

        public string Text { get; set; }
    }
}
