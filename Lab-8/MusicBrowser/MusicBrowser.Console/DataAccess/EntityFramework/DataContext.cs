namespace MusicBrowser.Console.DataAccess.EntityFramework
{
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;
    using MusicBrowser.Console.Domain;

    public class DataContext : DbContext
    {
        public DataContext()
            : this("DefaultConnectionString")
        {
        }

        public DataContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {
        }

        public DbSet<Album> Albums { get; set; }

        public DbSet<Song> Songs { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            var albumsModel = modelBuilder.Entity<Album>();

            albumsModel
                .ToTable("albums")
                .HasKey(x => x.Id)
                .Property(x => x.Id)
                .HasColumnName("albumid")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            albumsModel.Property(x => x.Title).HasColumnName("title").HasMaxLength(255).IsRequired();
            albumsModel.Property(x => x.Date).HasColumnName("date");

            var songsModel = modelBuilder.Entity<Song>();

            songsModel
                .ToTable("songs")
                .HasKey(x => x.Id)
                .Property(x => x.Id)
                .HasColumnName("songid")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            songsModel.Property(x => x.Title).HasColumnName("title").HasMaxLength(255).IsRequired();
            songsModel.Property(x => x.Duration).HasColumnName("duration").IsRequired();

            songsModel
                .HasRequired(s => s.Album)
                .WithMany()
                .Map(x => x.MapKey("albumid"))
                .WillCascadeOnDelete(true);
        }
    }
}
