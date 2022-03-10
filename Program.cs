using Microsoft.EntityFrameworkCore;

public class Users
{
    public int userId { get; set; }
    public string? username { get; set; }
    public string? email { get; set; }
    public string? password { get; set; }
    public Role role { get; set; }
    public ICollection<Courses> courses;
    public ICollection<Modules> modules;
}

public class Role
{
    public int roleId { get; set; }
    public string roleName { get; set; }

}

public class Courses
{
    public int courseId { get; set; }
    public string courseName { get; set; }
    public ICollection<Users> suscriber;
    public ICollection<Modules> module;
}

public class Modules
{
    public int moduleId { get; set; }
    public string? moduleName { get; set; }
    public string? titleModule { get; set; }
    public string? descriptionModule { get; set; }
    public ICollection<Users> suscriber { get; set; }
    public ICollection<Topic> topics { get; set; }
}

public class Topic
{
    public int topicId { get; set; }
    public string? topicTitle { get; set; }
    public string? topicDescription { get; set; }
    public ICollection<Questions> questions { get; set; }
}

public class Questions
{
    public int questionId { get; set; }
    public string? questionTitle { get; set; }
    public string? questionText { get; set; }
    public Users createdBy;
    public ICollection<Likes> likes { get; set; }
    public ICollection<Answers> answers { get; set; } 
}

public class Answers
{
    public int answerId { get; set; }
    public string? answerText { get; set; }
    public Users CreatedBy { get; set; }
    public ICollection<Likes> likes { get; set; }
}

public class Likes
{
    public int likesId { get; set; }
    public bool isLiked { get; set; }
    public List<Answers> answer { get; set; }
    public List<Questions> question { get; set; }
}

public class Notification
{
    public int notificationId { get; set; }
    public Topic topic { get; set; }
    public List<Users> user { get; set; }
}

internal class MyContext : DbContext
{
    public DbSet<Users> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Courses> Courses { get; set; }
    public DbSet<Modules> Modules { get; set; }
    public DbSet<Topic> Topics { get; set; }
    public DbSet<Questions> Questions { get; set; }
    public DbSet<Answers> Answers { get; set; }
    public DbSet<Likes> Likes { get; set; }
    public DbSet<Notification> Notifications { get; set; }

    protected override void OnModelCreating(ModelBuilder model)
    {
        // Relacion User Role
        model.Entity<Users>()
            .HasOne(p => p.role);
        
        // Relacion User Modules
        model.Entity<Users>()
            .HasMany(b => b.modules)
            .WithMany(p => p.suscriber);
        
        // Relacion User Courses
        model.Entity<Users>()
            .HasMany(b => b.courses)
            .WithMany(p => p.suscriber);

        // Relacion Courses Modules
        model.Entity<Courses>()
            .HasMany(p => p.module);

        // Relacion Modules Questions
        model.Entity<Modules>()
            .HasMany(p => p.topics);

        // Relacion Topic Question
        model.Entity<Topic>()
            .HasMany(p => p.questions);

        // Relacion Question Answers
        model.Entity<Questions>()
            .HasMany(p => p.answers);

        // Relacion Question Likes
        model.Entity<Likes>()
            .HasMany(p => p.question);
        
        // Relación Answers Likes
        model.Entity<Likes>()
            .HasMany(p => p.answer);

        // Relación Notification modules
        model.Entity<Notification>()
            .HasMany(p => p.user);

        // Relacion Notification Topic
        model.Entity<Notification>()
            .HasOne(p => p.topic);
    }
}