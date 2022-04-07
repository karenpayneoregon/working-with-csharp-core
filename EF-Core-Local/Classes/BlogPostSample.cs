using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Saving.Data;
using Saving.Models;
using Saving.Utilities;

namespace Saving.Classes
{
    public class BlogPostSample
    {
        /// <summary>
        /// Remove existing database, recreate according to models
        /// </summary>
        public static async Task FreshStart()
        {
            await using var context = new BloggingContext();

            await context.Database.EnsureDeletedAsync();
            await context.Database.EnsureCreatedAsync();
        }

        public static async Task UpdateExisting()
        {
            ConsoleColors.WriteHeader($"Running {nameof(UpdateExisting)}");
            await FreshStart();
            
            await using var context = new BloggingContext();

            var blog1 = new Blog
            {
                Url = "http://blogs.msdn.com/dotnet/csharp",
                Description = "Developer blog",
                Posts = new List<Post>
                {
                    new() { Title = "Intro to C#", Content = "Basic C#"},
                    new() { Title = "Working with classes", Content = "Understanding classes"}
                }
            };

            var blog2 = new Blog
            {
                Url = "http://blogs.msdn.com/dotnet/vbnet",
                Description = "Developer blog",
                Posts = new List<Post> { new()
                {
                    Title = "Intro to VB", 
                    Content = "Basic VB.NET"
                } }
            };

            var blog3 = new Blog
            {
                Url = "http://blogs.msdn.com/dotnet/fsharp",
                Description = "Developer blog",
                Posts = new List<Post> { new()
                {
                    Title = "Intro to F#", 
                    Content = "Learn F#"
                } }
            };

            /*
             * Add the three blogs to our DbContext
             */
            context.AddRange(blog1, blog2, blog3);

            /*
             * Save the three blogs to the database
             */
            await context.SaveChangesAsync();


            /*
             * Show the blogs from the database
             */
            foreach (var blog in context.Blogs)
            {
                Debug.WriteLine($"{blog.BlogId,-5}{blog.Description} {blog.Url}");
            }

            /*
             * Start
             *  1. Create a new blog, assign an existing BlogId currently in the database
             *  2. Find the existing blog using FindAsync
             *  3. Attach the existing blog from FindAsync to the current DbContext
             *  4. Set values of inComingBlog to existing blog
             *  5. Save changes
             *  6. Display back all blogs
             */

            int primaryKey = 2;
            
            var incomingBlog = new Blog
            {
                BlogId = primaryKey,
                Url = "http://blogs.msdn.com/dotnet/efcore-net",
                Description = "Karen's blog",
                Posts = new List<Post> { new()
                {
                    Title = "Intro to EF Core", 
                    Content = "Basics"
                } }
            };

            var existingBlog = await context.Blogs.FindAsync(primaryKey);
            var entry = context.Entry(existingBlog);
            entry.CurrentValues.SetValues(incomingBlog);

            await context.SaveChangesAsync();
            
            Debug.WriteLine("");

            foreach (var blog in context.Blogs)
            {
                Debug.WriteLine($"{blog.BlogId,-5}{blog.Description} {blog.Url}");
            }

            /*
             * End
             */

        }

        public static async Task CreateNewPopulateRead()
        {
            // Local method to CreateNewPopulateRead()
            static void ShowBlogs(BloggingContext context)
            {

                foreach (var blog in context.Blogs)
                {
                    Console.WriteLine($"{blog.BlogId,-3}{blog.Description,-30}{blog.Url}");

                    // never assume there are children
                    if (blog.Posts is not null)
                    {
                        foreach (var post in blog.Posts)
                        {
                            Console.WriteLine($"\t{post.PostId,-3}{post.Title}");
                            Console.WriteLine($"\t\t{post.Content}");
                        }
                    }
                    else
                    {
                        Console.WriteLine("No post");
                    }


                    Console.WriteLine();

                }
            }

            ConsoleColors.WriteHeader($"Running {nameof(CreateNewPopulateRead)}");
            await FreshStart();

            // create blogs and post
            await using var context = new BloggingContext();

            var blog1 = new Blog
            {
                Url = "http://blogs.msdn.com/dotnet/csharp",
                Description = "Developer blog",
                Posts = new List<Post>
                {
                    new() { Title = "Intro to C#", Content = "Basic C#"},
                    new() { Title = "Working with classes", Content = "Understanding classes"}
                }
            };

            var blog2 = new Blog
            {
                Url = "http://blogs.msdn.com/dotnet/vbnet",
                Description = "Developer blog",
                Posts = new List<Post> { new() { Title = "Intro to VB", Content = "Basic VB.NET" } }
            };

            var blog3 = new Blog
            {
                Url = "http://blogs.msdn.com/dotnet/fsharp",
                Description = "Developer blog",
                Posts = new List<Post> { new() { Title = "Intro to F#", Content = "Learn F#" } }
            };

            /*
             * Add the three blogs to our DbContext
             */
            context.AddRange(blog1, blog2, blog3);

            /*
             * Save blogs to the database
             */
            await context.SaveChangesAsync();

            var blogListings = await context.Blogs.ToListAsync();

            //foreach (var blog in blogListings)
            //{
            //    Debug.WriteLine($"{blog.BlogId, -5}{blog.Description}");
            //    foreach (var blogPost in blog.Posts)
            //    {
            //        Debug.WriteLine($"\t{blogPost.PostId, -5}{blogPost.Title}");
            //    }
            //}

            ShowBlogs(context);

        }
        /// <summary>
        /// Important to keep the DbContext scoped properly for each operation
        /// and to understand how caching and change tracking works.
        /// 
        /// - Create three blogs with posts
        /// - Delete one blog
        /// - Edit one blog
        ///     - inspect
        ///     - force reload
        /// </summary>
        public static async Task DeleteAndModifyRecordIndividualContexts()
        {

            ConsoleColors.WriteHeader($"Running {nameof(DeleteAndModifyRecordIndividualContexts)}");

            /* - local method
             * Iterate all blogs/post
             */
            static void ShowBlogs(BloggingContext context)
            {

                foreach (var blog in context.Blogs)
                {
                    Console.WriteLine($"{blog.BlogId, -3}{blog.Description,-30}{blog.Url}");

                    // never assume there are children
                    if (blog.Posts is not null)
                    {
                        foreach (var post in blog.Posts)
                        {
                            Console.WriteLine($"\t{post.PostId,-3}{post.Title}");
                            Console.WriteLine($"\t\t{post.Content}");
                        }
                    }
                    else
                    {
                        Console.WriteLine("No post");
                    }


                    Console.WriteLine();

                }
            }

            /* - local method
             * Update a single blog url which the caller will
             * not know about see in this case the caller reloads
             * said blog entry.
             */
            static void UpdateBlogTitle(int blogIdentifier)
            {
                using var context = new BloggingContext();
                var blog = context.Blogs.FirstOrDefault(b => b.BlogId == blogIdentifier);
                blog.Url = "https://csharpforums.net/";


                var queryString = context.Blogs.Where(blog => blog.BlogId == 2).ToQueryString();
                
                ConsoleColors.Line();
                ConsoleColors.WriteSectionYellow($"context.Blogs.Where(x => x.BlogId == 2) {queryString}");
                ConsoleColors.EmptyLine();

                context.SaveChanges();
            }

            // start fresh
            await using (var context = new BloggingContext())
            {
                await context.Database.EnsureDeletedAsync();
                await context.Database.EnsureCreatedAsync();
            }

            // create blogs and post
            await using (var context = new BloggingContext())
            {
                var blog1 = new Blog
                {
                    Url = "http://blogs.msdn.com/dotnet/csharp", 
                    Description = "Developer blog",
                    Posts = new List<Post>
                    {
                        new() { Title = "Intro to C#", Content = "Basic C#"},
                        new() { Title = "Working with classes", Content = "Understanding classes"}
                    }
                };

                var blog2 = new Blog
                {
                    Url = "http://blogs.msdn.com/dotnet/vbnet",
                    Description = "Developer blog",
                    Posts = new List<Post> { new() { Title = "Intro to VB", Content = "Basic VB.NET"} }
                };

                var blog3 = new Blog
                {
                    Url = "http://blogs.msdn.com/dotnet/fsharp",
                    Description = "Developer blog",
                    Posts = new List<Post> { new() { Title = "Intro to F#", Content = "Learn F#"} }
                };

                context.AddRange(blog1, blog2, blog3);

                await context.SaveChangesAsync();
                ShowBlogs(context);

                Console.WriteLine($"Blog count after add {context.Blogs.Count()}");

                context.Blogs.Remove(blog2);
                await context.SaveChangesAsync();
                Console.WriteLine($"Blog count after remove one blog {context.Blogs.Count()}");

                int blogIdentifier = 1;

                // this update will not be seen for the current DbContext being tracked
                UpdateBlogTitle(blogIdentifier);

                var changedBlog = context.Blogs.FirstOrDefault(b => b.BlogId == blogIdentifier);
                Console.WriteLine($"After change: '{changedBlog.Url}'");

                /*
                 * to see the change
                 * Reloads the entity from the database overwriting any property values
                 * with values from the database.
                 *
                 * https://docs.microsoft.com/en-us/dotnet/api/microsoft.entityframeworkcore.changetracking.entityentry.reloadasync?view=efcore-6.0
                 *
                 */
                await context.Entry(changedBlog).ReloadAsync();

                Console.WriteLine($"After change reloaded: '{changedBlog.Url}' State: {context.Entry(changedBlog).State}");
                /*
                 * this is debatable dependent on business logic
                 */
                context.Entry(changedBlog).State = EntityState.Modified;
                Console.WriteLine($"After change reloaded: '{changedBlog.Url}' State: {context.Entry(changedBlog).State}");

            }

            // simple inspection
            await using (var context = new BloggingContext())
            {
                Console.WriteLine($"Blog identifiers {string.Join(",", context.Blogs.Select(blog => blog.BlogId))}");
                Console.WriteLine($"Blog count {context.Blogs.Count()}");
            }

            Console.WriteLine();

        }

    }
}