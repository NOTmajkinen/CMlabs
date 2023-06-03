namespace DataGenerator
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    using System.Text.Json;
    using Social.Models;

    public class DataSerialize
    {
        private List<User> _users;

        private List<Friend> _friends;

        private List<Message> _messages;

        public DataSerialize(string pathUsers, string pathFriends, string pathMessages, List<User> users, List<Friend> friends, List<Message> messages)
        {
            _users = users;
            _friends = friends;
            _messages = messages;

            SetUsers(pathUsers);
            SetFriends(pathFriends);
            SetMessages(pathMessages);
        }

        private void SetUsers(string path)
        {
            string jsonString = JsonSerializer.Serialize(_users);

            File.WriteAllText(path, jsonString);
        }

        private void SetFriends(string path)
        {
            string jsonString = JsonSerializer.Serialize(_friends);

            File.WriteAllText(path, jsonString);
        }

        private void SetMessages(string path)
        {
            string jsonString = JsonSerializer.Serialize(_messages);

            File.WriteAllText(path, jsonString);
        }
    }
}
