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
using BlogManager;

class Program
{
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
                DataManager.AddBlogSite();
                break;
            case 2:
                DataManager.GetAllBlogs();
                break;
            case 3:
                DataManager.AddPost();
                break;
            case 4:
                DataManager.GetPostsByBlogId();
                break;
            case 5:
                DataManager.UpdatePost();
                break;
            case 6:
                DataManager.DeletePost();
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
            Console.WriteLine("Would you like to continue?");
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
}