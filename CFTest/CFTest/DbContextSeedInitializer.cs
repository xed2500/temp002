using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeFirstTest
{
    class DbContextSeedInitializer : DropCreateDatabaseAlways<BloggingContext>//DropCreateDatabaseWhenModelChanges, CreateDatabaseIfNotExists
    {
        protected override void Seed(BloggingContext context)
        {
            context.Categories.Add( new Category { Id = Guid.NewGuid(), Name = "Cat 1" } );
            context.Categories.Add( new Category { Id = Guid.NewGuid(), Name = "Cat 2 Lorem ipsum" } );
            context.Categories.Add( new Category { Id = Guid.NewGuid(), Name = "Cat 3 Test" } );

            context.Labels.Add(new Label { Title = "label t1", Description = "label d1", Id = Guid.NewGuid() });
            context.Labels.Add(new Label { Title = "label t2", Description = "label d2", Id = Guid.NewGuid() });
            
            context.SaveChanges();
        }
    }
}