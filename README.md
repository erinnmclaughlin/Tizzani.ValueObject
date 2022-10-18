# Tizzani.ValueObject.EntityFrameworkCore

A simple value object framework easily configurable for use with entity framework.

## Sample Usage

```csharp
public sealed record BlogTitle : ValueObject<string>
{
    public BlogTitle(string value) : base(value) { }
    
    public override void Validate(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Blog title is required.");
          
        if (value.Length > 50)
            throw new ArgumentException("Blog title cannot exceed 50 characters.");
    }
}

public sealed class Blog
{
    public int Id { get; set; }
    public BlogTitle Title { get; set; } = null!;
}

public sealed class BlogContext : DbContext
{
    public DbSet<Blog>() => Set<Blog>();
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        modelBuilder.MapValueObjects();
        
        // for additional configuration (e.g., max length):
        modelBuilder.Entity<Blog>().HasValueObject(x => x.Title).HasMaxLength(50);
    }
}
```

