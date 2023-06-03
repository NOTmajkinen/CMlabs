using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace MusicViewer
{
    class SingleQueryAdoNetMusicRepository : IMusicRepository
    {
        private readonly string _connectionString;

        public SingleQueryAdoNetMusicRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<Album> ListAlbums()
        {
            IList<Album> results = new List<Album>();

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var command = new SqlCommand("SELECT * FROM [albums] JOIN [songs] ON albums.albumId = songs.albumId", connection);

                using (var dataReader = command.ExecuteReader())
                {
                    List<Song> songs = new List<Song>();
                    int previousAlbumId = 0;
                    DateTime previousDate = DateTime.Now;
                    string previousTitle = string.Empty;

                    while (dataReader.Read())
                    {
                        var albumId = (int)dataReader["albumId"];

                        if (previousAlbumId == albumId)
                        {
                            songs.Add(new Song(
                                (int)dataReader.GetValue(4),
                                (string)dataReader.GetValue(5),
                                (TimeSpan)dataReader["duration"]));

                            previousDate = (DateTime)dataReader["date"];
                            previousTitle = (string)dataReader["title"];
                        }
                        else if (previousAlbumId != albumId && (songs.Count == 0 || songs.Count == 1))
                        {
                            previousAlbumId = albumId;

                            songs.Add(new Song(
                                (int)dataReader.GetValue(4),
                                (string)dataReader.GetValue(5),
                                (TimeSpan)dataReader["duration"]));
                        }
                        else
                        {
                                results.Add(new Album(
                                    previousAlbumId,
                                    previousDate,
                                    previousTitle,
                                    songs.ToList()
                                    ));

                            songs.Clear();

                            songs.Add(new Song(
                               (int)dataReader.GetValue(4),
                               (string)dataReader.GetValue(5),
                               (TimeSpan)dataReader["duration"]));
                        }
                    }

                    if (songs.Count != 0)
                    {
                        results.Add(new Album(
                            previousAlbumId,
                            previousDate,
                            previousTitle,
                            songs.ToList()
                            ));
                    }
                }
            }

            return results;
        }
    }
}
