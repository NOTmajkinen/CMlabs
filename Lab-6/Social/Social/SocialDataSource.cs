namespace Social
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text.Json;
    using Social.Extensions;
    using Social.Models;

    public class SocialDataSource
    {
        private List<User> _users;

        private List<Friend> _friends;

        private List<Message> _messages;

        public SocialDataSource(string pathUsers, string pathFriends, string pathMessages)
        {
            GetUsers(pathUsers);
            GetFriends(pathFriends);
            GetMessages(pathMessages);
        }

        public UserContext GetUserContext(string userName)
        {
            var userContext = new UserContext();

            // a local function that return User object by its name
            User GetUser(string userName)
            {
                var user =
                    from person in _users
                    where person.Name == userName
                    select person;

                var userList = user.ToList();

                if (userList.Count != 0)
                {
                    return userList[0];
                }

                throw new Exception();
            }

            // a local function that return User object by its id
            User GetUserById(int id)
            {
                var user =
                    from person in _users
                    where person.UserId == id
                    select person;

                var userList = user.ToList();

                if (userList.Count != 0)
                {
                    return userList[0];
                }

                throw new Exception();
            }

            // a local function that return List<UserInformation> object by User object
            List<UserInformation> GetUserFriends(User user)
            {
                var tempList =
                    from person in _friends
                    where (person.FromUserId == user.UserId || person.ToUserId == user.UserId) && ((person.Status == 2) || (person.Status == 1) || (person.Status == 0))
                    select person;

                var theFirstFriendCase =
                    from person in tempList
                    where person.Status == 2 && person.FromUserId == user.UserId
                    select GetUserById(person.ToUserId);

                var theSecondFriendCase =
                    from person in tempList
                    where ((((person.ToUserId == user.UserId) || (person.FromUserId == user.UserId)) && (person.Status != 2)) &&
                    tempList.Any(item => item.FromUserId == user.UserId && item.ToUserId == person.FromUserId)) ||
                    (person.Status == 2 && person.FromUserId != user.UserId)
                    select GetUserById(person.FromUserId);

                var friends = theFirstFriendCase.Concat(theSecondFriendCase);

                var output = RemoveMistakenlyAddedFriends(friends.Select(user => new UserInformation(user.Name, user.Online, user.UserId)).ToList());

                return output;
            }

            List<UserInformation> RemoveMistakenlyAddedFriends(List<UserInformation> friends)
            {
                var user = friends[0];

                var output = new List<UserInformation>();

                output.Add(user);

                if (friends.Count != 1)
                {
                    for (int i = 1; i < friends.Count; i++)
                    {
                        if (friends[i].UserId == user.UserId)
                        {
                            continue;
                        }
                        else
                        {
                            output.Add(friends[i]);
                            user = friends[i];
                        }
                    }

                    return output;
                }

                return friends;
            }

            List<UserInformation> GetUserFriendshipOffers(User user)
            {
                List<UserInformation> friendshipOffers = new List<UserInformation>();

                var tempList = _friends.Where(friend => ((friend.ToUserId == user.UserId) && (friend.Status == 0 || friend.Status == 1) && friend.SendDate > user.LastVisit));

                var userFriendshipOffers = tempList.Select(person => GetUserById(person.FromUserId)).ToList();

                friendshipOffers = userFriendshipOffers.Select(user => new UserInformation(user.Name, user.Online, user.UserId)).ToList();

                return friendshipOffers;
            }

            List<UserInformation> GetUserSubscribers(User user)
            {
                List<UserInformation> subscribers = new List<UserInformation>();

                var tempList =
                    from person in _friends
                    where ((person.ToUserId == user.UserId) && ((person.Status == 3) || (person.Status == 1))) && !_friends.Any(friend => friend.FromUserId == user.UserId)
                    select person;

                var userSubscribers = tempList.Select(person => GetUserById(person.FromUserId));

                subscribers = userSubscribers.Select(user => new UserInformation(user.Name, user.Online, user.UserId)).ToList();

                return subscribers;
            }

            List<UserInformation> GetUserOnlineFriends(List<UserInformation> friends)
            {
                var onlineFriends =
                    from person in friends
                    where person.Online
                    select person;

                return onlineFriends.ToList();
            }

            List<News> GetUserNews(User user)
            {
                var userFriends = GetUserFriends(user);

                var userMessages =
                    from message in _messages
                    where message.SendDate > user.LastVisit
                    select message;

                var userNews =
                    from message in userMessages
                    where message.IsMessageWasMadeByFriend(userFriends)
                    select new News(message.AuthorId, GetUserById(message.AuthorId).Name, message.Likes, message.Text);

                return userNews.ToList();
            }

            userContext.User = GetUser(userName);

            userContext.Friends = GetUserFriends(userContext.User);

            userContext.FriendshipOffers = GetUserFriendshipOffers(userContext.User);

            userContext.Subscribers = GetUserSubscribers(userContext.User);

            userContext.OnlineFriends = GetUserOnlineFriends(userContext.Friends);

            userContext.News = GetUserNews(userContext.User);

            return userContext;
        }

        private void GetUsers(string path)
        {
            string userFile = File.ReadAllText(path);

            _users = JsonSerializer.Deserialize<List<User>>(userFile);
        }

        private void GetFriends(string path)
        {
            string userFile = File.ReadAllText(path);

            _friends = JsonSerializer.Deserialize<List<Friend>>(userFile);
        }

        private void GetMessages(string path)
        {
            string userFile = File.ReadAllText(path);

            _messages = JsonSerializer.Deserialize<List<Message>>(userFile);
        }
    }
}
