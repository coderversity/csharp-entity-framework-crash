namespace BlogManager;

public class DataManager
{
    private static Blog _blogInstance = new Blog();
    private static Post _postInstance = new Post();

    public static void AddBlogSite()
    {
        Console.WriteLine("Enter blog website name: ");
        string name = Console.ReadLine() ?? "";

        Console.WriteLine("Enter URL: ");
        string url = Console.ReadLine() ?? "";
        _blogInstance.Add(name, url);
    }

    public static void GetAllBlogs()
    {
        var blogs = _blogInstance.GetAll();

        if (blogs.Count == 0)
        {
            Console.WriteLine("No blogs found");
        }
        else
        {
            foreach (var item in blogs)
            {
                Console.WriteLine($"Blog ID: {item.BlogId}");
                Console.WriteLine($"Blog Name: {item.Name}");
                Console.WriteLine($"URL: {item.Url}\n");
            }
        }
    }

    public static void AddPost()
    {
        Console.WriteLine("Enter post title: ");
        string title = Console.ReadLine() ?? "";

        Console.WriteLine("Enter post content: ");
        string content = Console.ReadLine() ?? "";

        Console.WriteLine("Available blogs");
        GetAllBlogs();

        Console.WriteLine("Select the blog you want to add this post to: ");
        int blogId = 0;

        if (int.TryParse(Console.ReadLine(), out blogId))
        {
            _postInstance.Add(blogId, title, content);
        }
        else
        {
            Console.WriteLine("Invalid input. Try again.");
        }
    }

    public static void GetPostsByBlogId()
    {
        Console.Write("Enter the blog ID: ");
        int blogId = 0;
                
        if (int.TryParse(Console.ReadLine(), out blogId))
        {
            List<Post> posts = _postInstance.GetPostsFor(blogId);

            if (posts.Count > 0)
            {
                foreach (var item in posts)
                {
                    Console.WriteLine($"Post ID: {item.PostId}");
                    Console.WriteLine($"Title: {item.Title}");
                    Console.WriteLine($"Content: {item.Content}");
                    Console.WriteLine();
                }
            }
            else
            {
                Console.WriteLine("No posts found");
            }
        }
    }

    public static void UpdatePost()
    {
        Console.Write("Enter the post ID for the post you want to update: ");
        int postId = 0;

        if (int.TryParse(Console.ReadLine(), out postId))
        {
            Console.WriteLine("Enter new title: ");
            string title = Console.ReadLine() ?? "";

            Console.WriteLine("Enter new content: ");
            string content = Console.ReadLine() ?? "";

            _postInstance.Update(postId, title, content);
        }
        else
        {
            Console.WriteLine("Invali post ID. Try again");
        }
    }

    public static void DeletePost()
    {
        Console.Write("Enter the post ID for the post you want to delete: ");
        int postId = 0;

        if (int.TryParse(Console.ReadLine(), out postId))
        {
            var post = _postInstance.GetPost(postId);
            _postInstance.Delete(post);
        }
        else
        {
            Console.WriteLine("Invali post ID. Try again");
        }
    }
}
