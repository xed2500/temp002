using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeFirstTest
{
    //sample from: http://msdn.microsoft.com/en-us/data/jj193542.aspx
    //data adnotations: http://blogs.msdn.com/b/efdesign/archive/2010/03/30/data-annotations-in-the-entity-framework-and-code-first.aspx
    //understanding db initializers: http://www.codeguru.com/csharp/article.php/c19999/Understanding-Database-Initializers-in-Entity-Framework-Code-First.htm
    class Program
    {
        static void Main(string[] args)
        {
            TestEF test = new TestEF();
            test.DoTest();         
        }       
    }

    public class TestEF
    {
        public void DoTest()
        {
            Database.SetInitializer(new DbContextSeedInitializer());
            //Database.SetInitializer<BloggingContext>(null);//do not initialize checks

            TestAddNewItem();
            ShowItems();

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();  
        }

        private void ShowItems()
        {
            using (var db = new BloggingContext())
            {
                var query = from b in db.Labels orderby b.Title select b;
                Console.WriteLine("All items in the database:");
                foreach (var item in query)
                {
                    Console.WriteLine(item.Title + " - " + item.Description);
                }
            }
        }

        private void TestAddNewItem()
        {
            using (var db = new BloggingContext())
            {
                db.Database.Initialize(false);//run now, not at first db operation

                Console.Write("Enter a new item title: ");
                var title = Console.ReadLine();

                Console.Write("Enter a new item description: ");
                var description = Console.ReadLine();

                var label = new Label { Title = title, Description = description }; 
                db.Labels.Add(label); 
                db.SaveChanges();

                /*Guid postId = Guid.NewGuid();
                Guid catId = Guid.NewGuid();
                var cat = new Category { Id = catId, Name = "ASP.NET" };
                var post = new BlogPost { Id = postId, Title = "Title1", Content = "Hello World!", PublishDate = new DateTime(2011, 1, 1), Category = cat };
                db.Categories.Add(cat);
                db.BlogPosts.Add(post);
                Console.WriteLine(db.Database.Connection.ConnectionString);
                int i = db.SaveChanges();
                Console.WriteLine("{0} records added...", i);*/
            }
        }
    }

    public class Blog
    {
        public int BlogId { get; set; }
        public string Name { get; set; }

        public virtual List<Post> Posts { get; set; }
    }

    public class Post
    {
        public int PostId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }

        public int BlogId { get; set; }
        public virtual Blog Blog { get; set; }
    }

    //[Table("Labels")]
    public class Label
    {
        [Key]
        public Guid Id { get; set; }
        [MaxLength(35), MinLength(1)]
        public string Title { get; set; }
        [MaxLength(100)]
        public string Description { get; set; }
    }

   // [Table("TEntries")]
    public class TEntry
    {
        [Key]
        public Guid Id { get; set; }
        [MaxLength(250), MinLength(1)]
        public string Description { get; set; }
        [MaxLength(300)]
        public string AdditionalInfo { get; set; }

        [ForeignKey("Category")]
        public Guid CategoryId { get; set; }
        public virtual Category Category { get; set; }

        public virtual List<Label> AttachedLabels { get; set; }
    }

    //[Table("Categories")]
    public class Category
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<TEntry> TEntries { get; set; }
    }

    public class BloggingContext : DbContext
    {
        public BloggingContext(string connStringName = "XDefaultConnection") : base(connStringName) { }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Post> Posts { get; set; }

        public DbSet<Label> Labels { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<TEntry> TEntries { get; set; }
    }

}
