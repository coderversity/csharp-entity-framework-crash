// using System;
// using System.Linq;

// using var db = new BloggingContext();

// // Note: This sample requires the database to be created before running.
// Console.WriteLine($"Database path: {db.DbPath}.");

// // Create
// Console.WriteLine("Inserting a new blog");
// db.Add(new Blog { Url = "http://blogs.msdn.com/adonet" });
// db.SaveChanges();

// // Read
// Console.WriteLine("Querying for a blog");
// var blog = db.Blogs
//     .OrderBy(b => b.BlogId)




//     .First();

// // Update
// Console.WriteLine("Updating the blog and adding a post");
// blog.Url = "https://devblogs.microsoft.com/dotnet";
// blog.Posts.Add(
//     new Post { Title = "Hello World", Content = "I wrote an app using EF Core!" });
// db.SaveChanges();

// // Delete
// Console.WriteLine("Delete the blog");
// db.Remove(blog);
// db.SaveChanges();

using System;
using TestApp;

class Program
{
    private static Blog _blogInstance = new Blog();
    private static Post _postInstance = new Post();
    
    static void Main()
    {
        int choice = 0;

        do
        {
            ShowIntro();
            
            bool isValidChoice = int.TryParse(Console.ReadLine(), out choice);
            Console.WriteLine();

            if (!isValidChoice)
            {
                Console.WriteLine("Invalid choice. Try again.");
            }
            else
            {
                RunSelection(choice);
            }
        } while (!HasQuit());
    }

    private static void ShowIntro()
    {
        Console.Clear();
        Console.WriteLine("Welcome to the Blog Manager! What would you like to do? \n");
        Console.WriteLine("1. Add a blog site");
        Console.WriteLine("2. Get all blogs");
        Console.WriteLine("3. Add a post");
        Console.WriteLine("4. Get all posts");
        Console.WriteLine("5. Update a post");
        Console.WriteLine("6. Delete a post");
        
        Console.Write("Select a number: ");
    }

    private static void RunSelection(int choice)
    {
        Console.WriteLine($"You selected: {choice}\n");

        switch (choice)
        {
            case 1:
                Console.WriteLine("Enter blog website name: ");
                string name = Console.ReadLine() ?? "";

                Console.WriteLine("Enter URL: ");
                string url = Console.ReadLine() ?? "";
                _blogInstance.Add(name, url);
                break;
            case 2:
                GetAllBlogs();
                break;
            case 3:
                Console.WriteLine("Enter post title: ");
                string title = Console.ReadLine() ?? "";

                Console.WriteLine("Enter post content: ");
                string content = Console.ReadLine() ?? "";

                Console.WriteLine("Select a number for the blog you would like to add this post to: ");
                GetAllBlogs();
                int blogId = 0;

                if (int.TryParse(Console.ReadLine(), out blogId))
                {
                    _postInstance.Add(blogId, title, content);
                }
                else
                {
                    Console.WriteLine("Invalid input. Try again.");
                }
                break;
            case 4:
                Console.Write("Enter the blog ID: ");
                
                if (int.TryParse(Console.ReadLine(), out blogId))
                {
                    List<Post> posts = _postInstance.GetPostsFor(blogId);

                    if (posts.Count > 0)
                    {
                        Console.WriteLine($"Here are the posts for {posts[0].Blog.Name}\n");

                        foreach (var item in posts)
                        {
                            Console.WriteLine($"Post ID: {item.PostId}");
                            Console.WriteLine($"Related Blog ID: {item.BlogId}");
                            Console.WriteLine($"Content: {item.Content}");
                            Console.WriteLine();
                        }
                    }
                    else
                    {
                        Console.WriteLine("No posts found");
                    }
                }
                break;
            case 5:
                break;
            case 6:
                break;
            default:
                Console.WriteLine("Please select a choice between 1-6.");
                break;
        }
    }

    private static bool HasQuit()
    {
        bool isValidInput = false;
        bool hasQuit = false;

        do
        {
            Console.WriteLine("What would you like to continue?");
            Console.Write("Press Y to continue, and N to exit: ");
            string userInput = Console.ReadLine() ?? "";
            userInput = userInput.ToLower();

            if (userInput == "y")
            {
                isValidInput = true;
            }
            else if (userInput == "n")
            {
                Console.WriteLine("Thank you for using Blog Manager.");
                Environment.Exit(0);
                isValidInput = true;
                hasQuit = true;
            }
            else
            {
                Console.WriteLine("Invalid input. Try again.");
            }
        } while (!isValidInput);

        return hasQuit;
    }

    private static void GetAllBlogs()
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
}