namespace DataGenerator
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using System.Text;
    using Social.Extensions;
    using Social.Models;

    public class InputToDataBase
    {
        public static void AddUsers(List<User> users, string connectionString)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                foreach (var user in users)
                {
                    var command = new SqlCommand(
                        @"
                            INSERT INTO [Social].[dbo].[Users] (gender, dateOfBirth, lastVisit, isOnline, name) VALUES (@Gender, @DateOfBirth, @LastVisit, @IsOnline, @Name)", connection);

                    command.Parameters.AddWithValue("@Gender", user.Gender);
                    command.Parameters.AddWithValue("@DateOfBirth", user.DateOfBirth);
                    command.Parameters.AddWithValue("@LastVisit", user.LastVisit);
                    command.Parameters.AddWithValue("@IsOnline", user.Online);
                    command.Parameters.AddWithValue("@Name", user.Name);

                    command.BeginExecuteNonQuery();
                }
            }
        }

        public static void AddFriends(List<Friend> friends, string connectionString)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                foreach (var friend in friends)
                {
                    var command = new SqlCommand(
                        @"
                            INSERT INTO [Social].[dbo].[Friends] (userFrom, userTo, friendStatus, sendDate) VALUES (@UserFrom, @UserTo, @FriendStatus, @SendDate)", connection);

                    command.Parameters.AddWithValue("@UserFrom", friend.FromUserId);
                    command.Parameters.AddWithValue("@UserTo", friend.ToUserId);
                    command.Parameters.AddWithValue("@FriendStatus", friend.Status);
                    command.Parameters.AddWithValue("@SendDate", friend.SendDate);

                    command.ExecuteNonQuery();
                }
            }
        }


        public static void AddNews(List<Message> news, List<User> users, string connectionString)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                foreach (var message in news)
                {
                    var command = new SqlCommand(
                        @"
                            INSERT INTO [Social].[dbo].[Messages] (authorId, sendDate, messageText) VALUES (@AuthorId, @SendDate, @MessageText)", connection);

                    command.Parameters.AddWithValue("@AuthorId", message.AuthorId);
                    command.Parameters.AddWithValue("@SendDate", message.SendDate);
                    command.Parameters.AddWithValue("@MessageText", message.Text);

                    command.BeginExecuteNonQuery();

                    AddLikes(message, connectionString);
                }
            }
        }

        public static void AddLikes(Message message, string connectionString)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                foreach (var like in message.Likes)
                {
                    var command = new SqlCommand(
                        @"
                            INSERT INTO [Social].[dbo].[Likes] (userId, messageId) VALUES (@UserId, @MessageId)", connection);

                    command.Parameters.AddWithValue("@UserId", like);
                    command.Parameters.AddWithValue("@MessageId", message.MessageId);

                    command.BeginExecuteNonQuery();
                }
            }
        }
    }
}
