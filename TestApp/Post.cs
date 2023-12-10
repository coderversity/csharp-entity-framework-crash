namespace TestApp;
using System;
using System.Linq;
public class Post
{
    public int PostId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;

    public int BlogId { get; set; }
    public Blog Blog { get; set; } = new();

    public List<Post> GetAll()
    {
        List<Post> posts = new List<Post>();

        using (var dbContext = new BloggingContext())
        {
            try
            {
                posts = dbContext.Posts.ToList(); 
            }
            catch (System.Exception ex)
            {
                Console.WriteLine($"Error while fetching posts: {ex.Message}");
                
            }
        }

        return posts;
    }

    public List<Post> GetPostsFor(int blogId)
    {
        List<Post> posts = new List<Post>();

        using (var dbContext = new BloggingContext())
        {
            try
            {
                Console.WriteLine("BLOG ID: " + blogId);
                posts = dbContext.Posts.Where(p => p.BlogId == blogId).ToList();
                Console.WriteLine("POSTS COUNT: " + posts.Count);
            }
            catch (System.Exception ex)
            {
                Console.WriteLine($"Error while fetching posts for blog ID {blogId}: {ex.Message}");
                
            }
        }

        return posts;
    }

    public void Add(int blogId, string title, string content)
    {
        using (var dbContext = new BloggingContext())
        {
            try
            {
                var newPost = new Post
                {
                    BlogId = blogId,
                    Title = title,
                    Content = content
                };

                dbContext.Posts.Add(newPost);
                dbContext.SaveChanges();
                Console.WriteLine("Post added!");
            }
            catch (System.Exception ex)
            {
                Console.WriteLine($"An error occurred while adding a post: {ex.Message}");
                throw;
            }
        }
    }
}