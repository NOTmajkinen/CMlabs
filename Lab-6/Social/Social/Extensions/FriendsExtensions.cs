namespace Social.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Social.Models;

    public static class FriendsExtensions
    {
        public static bool IsMessageWasMadeByFriend(this Message message, List<UserInformation> friends)
        {
            return friends.Any(friend => friend.UserId == message.AuthorId);
        }
    }
}
