# Tizzani.ValueObject.EntityFrameworkCore

A simple value object framework easily configurable for use with Entity Framework Core.

![GitHub Workflow Status](https://img.shields.io/github/workflow/status/erinnmclaughlin/Tizzani.ValueObject/.NET)
[![Nuget](https://img.shields.io/nuget/v/Tizzani.ValueObject.EntityFrameworkCore)](https://www.nuget.org/packages/Tizzani.ValueObject.EntityFrameworkCore/0.1.0)

## Sample Usage

##### BlogTitle.cs
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
```

##### Blog.cs
```csharp
public sealed class Blog
{
    public int Id { get; set; }
    public BlogTitle Title { get; set; } = null!;
}
```

##### BlogContext.cs
```csharp
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

## Json Serialization
In some cases it might be useful to serialize/deserialize to/from the underlying value type. For these cases, you can include `ValueObjectConverter` in your json serialization options:

```csharp
var blog = new Blog(1, new BlogTitle("My Sick Blog"));

// Without custom converter:
var defaultJson = JsonSerializer.Serialze(blog);            // { "Id": 1, "Title": { "Value": "My Sick Blog" } }

// With custom converter:
var options = new JsonSerializerOptions();
options.Converters.Add(new ValueObjectConverter());
var customJson = JsonSerializer.Serialize(blog, options);   // { "Id": 1, "Title": "My Sick Blog" }
```
