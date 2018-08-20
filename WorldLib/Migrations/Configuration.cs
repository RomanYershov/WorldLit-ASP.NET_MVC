namespace WorldLib.Migrations
{
    using Microsoft.AspNet.Identity;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<WorldLib.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(WorldLib.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            //context.Users.AddOrUpdate(
            //    new Models.ApplicationUser
            //    {
            //        Email = "adminsupport@mail.ru",
            //        PasswordHash = new PasswordHasher().HashPassword("123qwe")
            //    }
            //    );

            //context.Categories.AddOrUpdate(new Models.Category { Name = "��������" }, new Models.Category { Name = "����������" });

            //context.Discussions.AddOrUpdate(
            //    new Models.Discussion
            //    {
            //        Name = "��� ������������ ����������",
            //        Category = context.Categories.Find(1),
            //        Description = "����� �� �����",
            //        DateTime = DateTime.Now,
            //        Status = Models.DiscussionStatusEnum.Publish
            //    },
            //      new Models.Discussion
            //      {
            //          Name = "� ���� ����������",
            //          Category = context.Categories.Find(2),
            //          Description = "����� �� �����",
            //          DateTime = DateTime.Now,
            //          Status = Models.DiscussionStatusEnum.Publish
            //    }
            //    );

            //context.Comments.AddOrUpdate(
            //    new Models.Comment
            //    {
            //        CreationDateTime = DateTime.Now,
            //        Text = "������ ����������� ��� �����",
            //        Discussion = context.Discussions.Find(1),
            //        User = context.Users.FirstOrDefault()
            //    },
            //     new Models.Comment
            //     {
            //         CreationDateTime = DateTime.Now,
            //         Text = "������ ����������� ��� �����",
            //         Discussion = context.Discussions.Find(1),
            //         User = context.Users.FirstOrDefault()
            //     },
            //      new Models.Comment
            //      {
            //          CreationDateTime = DateTime.Now,
            //          Text = "������ ����������� ��� �����",
            //          Discussion = context.Discussions.Find(2),
            //          User = context.Users.FirstOrDefault()
            //      }
            //    );
            //context.SaveChanges();
        }
    }
}
