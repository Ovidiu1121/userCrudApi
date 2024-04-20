using FluentMigrator;

namespace UserCrudApi.Data.Migrations
{
    [Migration(21032924)]
    public class CreateSchema : Migration
    {
        public override void Down()
        {
            throw new NotImplementedException();
        }

        public override void Up()
        {
            Create.Table("users")
                 .WithColumn("id").AsInt32().PrimaryKey().Identity()
                 .WithColumn("name").AsString(128).NotNullable()
                 .WithColumn("email").AsString(128).NotNullable()
                 .WithColumn("role").AsString(128).NotNullable();
        }
    }
}
