namespace BlogManager;

using System;
using System.Data.Common;
using System.Linq;

public class Blog
{
    public int BlogId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;

    public List<Blog> GetAll()
    {
        List<Blog> blogs = new List<Blog>();

        using (var dbContext = new BloggingContext())
        {
            try
            {
                blogs = dbContext.Blogs.ToList(); 
            }
            catch (System.Exception ex)
            {
                Console.WriteLine($"Error while fetching blogs: {ex.Message}");
                
            }
        }

        return blogs;
    }

    public void Add(string name, string url)
    {
        using (var dbContext = new BloggingContext())
        {
            try
            {
                var newBlog = new Blog
                {
                    Name = name,
                    Url = url
                };

                dbContext.Blogs.Add(newBlog);
                dbContext.SaveChanges();
                Console.WriteLine("Blog added!");
            }
            catch (System.Exception ex)
            {
                Console.WriteLine($"An error occurred while adding a blog site: {ex.Message}");
                throw;
            }
        }
    }
}