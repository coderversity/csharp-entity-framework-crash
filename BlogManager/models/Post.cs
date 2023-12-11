namespace BlogManager;

using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

public class Post
{
    public int PostId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;

    public int BlogId { get; set; }
    public Blog? Blog { get; set; }

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
                Console.WriteLine($"Blog ID: {blogId}");
                posts = dbContext.Posts
                            .Where(p => p.BlogId == blogId)
                            .Include(p => p.Blog)
                            .ToList();
            }
            catch (System.Exception ex)
            {
                Console.WriteLine($"Error while fetching posts for blog ID {blogId}: {ex.Message}");
                
            }
        }

        return posts;
    }

    public Post GetPost(int postId)
    {
        using (var dbContext = new BloggingContext())
        {
            try
            {
                var post = dbContext.Posts.FirstOrDefault(p => p.PostId == postId);

                if (post != null)
                {
                    return post;
                }
            }
            catch (System.Exception ex)
            {
                Console.WriteLine($"Error while fetching post: {ex.Message}");
            }
        }

        return new Post();
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

    public void Update(int postId, string title, string content)
    {
        using (var dbContext = new BloggingContext())
        {
            try
            {
                var post = dbContext.Posts.FirstOrDefault(p => p.PostId == postId);

                if (post == null)
                {
                    Console.WriteLine("No matching post found");
                }
                else
                {
                    post.Title = title;
                    post.Content = content;
                    
                    dbContext.SaveChanges();
                    Console.WriteLine("Post updated!");
                }
            }
            catch (System.Exception ex)
            {
                Console.WriteLine($"An error occurred while updating a post: {ex.Message}");
                throw;
            }
        }
    }

    public void Delete(Post post)
    {
        using (var dbContext = new BloggingContext())
        {
            try
            {
                dbContext.Posts.Remove(post);
                dbContext.SaveChanges();
                Console.WriteLine("Post deleted!");
            }
            catch (System.Exception ex)
            {
                Console.WriteLine($"An error occurred while deleting a post: {ex.Message}");
                throw;
            }
        }
    }
}