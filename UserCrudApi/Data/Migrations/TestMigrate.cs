using FluentMigrator;

namespace UserCrudApi.Data.Migrations
{
    [Migration(21032024404)]
    public class TestMigrate : Migration
    {
        public override void Down()
        {
            
        }

        public override void Up()
        {
            Execute.Script(@"./Data/Scripts/data.sql");
        }


    }
}
