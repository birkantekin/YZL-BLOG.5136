using Framework.Application.Common.Interfaces;
using Framework.Domain.Entities;
using Framework.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Framework.Infrastructure.Persistence;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    // Veritabanı connectionstring için oluşturduğumuz constracture ler.
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        Database.EnsureCreated();
    }

    // Veritabanı tablolarımızı oluşturduk entiti olarak
    public DbSet<Article> Articles => Set<Article>();

    public DbSet<ArticleLike> ArticleLikes => Set<ArticleLike>();
    public DbSet<Category> Categories => Set<Category>();
    public DbSet<CategoryWithUser> CategoryWithUsers => Set<CategoryWithUser>();
    public DbSet<Comment> Comments => Set<Comment>();
    public DbSet<Role> Roles => Set<Role>();
    public DbSet<User> Users => Set<User>();
    public DbSet<UserPasswordHistory> UserPasswordHistories => Set<UserPasswordHistory>();

    // Configuratsyon ayarları yaptım. Bu yüzden tanımlamalarımızı sağladım
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {


        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        modelBuilder.Entity<Role>().HasData(new Role() { Id = 1, Name = "Admin" });
        modelBuilder.Entity<Role>().HasData(new Role() { Id = 2, Name = "Member" });

        modelBuilder.Entity<User>().HasData(new User()
        {
            Id = 1,
            FirstName = "Admin",
            LastName = "",
            ProfilePhoto = "/Uploads/4e0805c2-31cf-4a80-b7b4-102bd3d6cf4e_about_img.png",
            Email = "admin@hotmail.com",
            IsActive = EnumType.Active,
            Password = "123456",
            RoleId = 1,
            UserName = "admin"
        });

        modelBuilder.Entity<User>().HasData(new User()
        {
            Id =2,
            FirstName = "Bob",
            LastName = "Marley",
            ProfilePhoto = "/Uploads/4e0805c2-31cf-4a80-b7b4-102bd3d6cf4e_about_img.png",
            Email = "user@hotmail.com.tr",
            IsActive = EnumType.Active,
            Password = "123456",
            RoleId = 2,
            UserName = "user"
        });

        List<Category> categoryData = new List<Category>
{
    new Category
    {
        Id = 1,
        Name = "Teknoloji",
        Status = EnumType.Active
    },
    new Category
    {
        Id = 2,
        Name = "Sağlık ve Tıp",
        Status = EnumType.Active
    },
    new Category
    {
        Id = 3,
        Name = "Eğitim",
        Status = EnumType.Active
    },
    new Category
    {
        Id = 4,
        Name = "Sanat ve Kültür",
        Status = EnumType.Active
    },
    new Category
    {
        Id = 5,
        Name = "Bilim",
        Status = EnumType.Active
    },
    new Category
    {
        Id = 6,
        Name = "Spor ve Rekreasyon",
        Status = EnumType.Active
    },
    new Category
    {
        Id = 7,
        Name = "Seyahat ve Turizm",
        Status = EnumType.Active
    },
    new Category
    {
        Id = 8,
        Name = "Moda ve Giyim",
        Status = EnumType.Active
    },
    new Category
    {
        Id = 9,
        Name = "Otomotiv",
        Status = EnumType.Active
    },
    new Category
    {
        Id = 10,
        Name = "İş ve Kariyer",
        Status = EnumType.Active
    },
    new Category
    {
        Id = 11,
        Name = "Finans ve Yatırım",
        Status = EnumType.Active
    },
    new Category
    {
        Id = 12,
        Name = "Gıda ve Beslenme",
        Status = EnumType.Active
    },
    new Category
    {
        Id = 13,
        Name = "Ev ve Bahçe",
        Status = EnumType.Active
    },
    new Category
    {
        Id = 14,
        Name = "Hobi ve El Sanatları",
        Status = EnumType.Active
    },
    new Category
    {
        Id = 15,
        Name = "Aile ve Ebeveynlik",
        Status = EnumType.Active
    },
    new Category
    {
        Id = 16,
        Name = "İnsan İlişkileri",
        Status = EnumType.Active
    },
    new Category
    {
        Id = 17,
        Name = "Doğa ve Çevre",
        Status = EnumType.Active
    },
    new Category
    {
        Id = 18,
        Name = "Siyaset ve Hükümet",
        Status = EnumType.Active
    },
    new Category
    {
        Id = 19,
        Name = "Eğlence ve Film",
        Status = EnumType.Active
    },
    new Category
    {
        Id = 20,
        Name = "Müzik",
        Status = EnumType.Active
    },
};

        // Kategori verilerini kullanarak bir veritabanına kaydetme işlemi veya başka bir işlem gerçekleştirilebilir.

        modelBuilder.Entity<Category>().HasData(categoryData);
        // Kategori verilerini kullanarak bir veritabanına kaydetme işlemi veya başka bir işlem gerçekleştirebilirsiniz.

        long articleId = 0;
        foreach (var item in categoryData)
        {
            for (int i = 1; i <= 20; i++)
            {
                articleId++;    

                var article = new Article
                {
                    Id = articleId,
                    Title = item.Name+" Makale Başlığı " + i,
                    Content = "Makale içeriği "+i+ " Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.",
                    Photo = "/img/tm-img-310x180-1.jpg",
                    CreatedDate = DateTime.Now,
                    Duration = 10,
                    Counter = 0,
                    UserId = 1, // Kullanıcı kimliği
                    CategoryId = item.Id // Kategori kimliği
                };

                modelBuilder.Entity<Article>().HasData(article);

            }


        }

        base.OnModelCreating(modelBuilder);

    }
}