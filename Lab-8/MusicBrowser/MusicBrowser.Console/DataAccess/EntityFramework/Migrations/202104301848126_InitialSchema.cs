namespace MusicBrowser.Console.DataAccess.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class InitialSchema : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.albums",
                c => new
                    {
                        albumid = c.Int(nullable: false, identity: true),
                        title = c.String(nullable: false, maxLength: 255),
                        date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.albumid);

            CreateTable(
                "dbo.songs",
                c => new
                    {
                        songid = c.Int(nullable: false, identity: true),
                        title = c.String(nullable: false, maxLength: 255),
                        duration = c.Time(nullable: false, precision: 7),
                        albumid = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.songid)
                .ForeignKey("dbo.albums", t => t.albumid, cascadeDelete: true)
                .Index(t => t.albumid);
        }

        public override void Down()
        {
            DropForeignKey("dbo.songs", "albumid", "dbo.albums");
            DropIndex("dbo.songs", new[] { "albumid" });
            DropTable("dbo.songs");
            DropTable("dbo.albums");
        }
    }
}
