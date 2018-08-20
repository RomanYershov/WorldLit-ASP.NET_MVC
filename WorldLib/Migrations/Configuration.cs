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

            //context.Categories.AddOrUpdate(new Models.Category { Name = "Классика" }, new Models.Category { Name = "Фантастика" });

            //context.Discussions.AddOrUpdate(
            //    new Models.Discussion
            //    {
            //        Name = "Мир классической литературы",
            //        Category = context.Categories.Find(1),
            //        Description = "Какой то текст",
            //        DateTime = DateTime.Now,
            //        Status = Models.DiscussionStatusEnum.Publish
            //    },
            //      new Models.Discussion
            //      {
            //          Name = "В мире фантастики",
            //          Category = context.Categories.Find(2),
            //          Description = "Какой то текст",
            //          DateTime = DateTime.Now,
            //          Status = Models.DiscussionStatusEnum.Publish
            //    }
            //    );

            //context.Comments.AddOrUpdate(
            //    new Models.Comment
            //    {
            //        CreationDateTime = DateTime.Now,
            //        Text = "Первый комментарий для теста",
            //        Discussion = context.Discussions.Find(1),
            //        User = context.Users.FirstOrDefault()
            //    },
            //     new Models.Comment
            //     {
            //         CreationDateTime = DateTime.Now,
            //         Text = "Второй комментарий для теста",
            //         Discussion = context.Discussions.Find(1),
            //         User = context.Users.FirstOrDefault()
            //     },
            //      new Models.Comment
            //      {
            //          CreationDateTime = DateTime.Now,
            //          Text = "Третий комментарий для теста",
            //          Discussion = context.Discussions.Find(2),
            //          User = context.Users.FirstOrDefault()
            //      }
            //    );
            //context.SaveChanges();
        }
    }
}
