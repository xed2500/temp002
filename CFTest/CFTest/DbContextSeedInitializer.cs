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

            context.WLists.Add(new WList { Id = Guid.NewGuid(), Name = "WList 1", Description = "Long description" });

            WList parentList = new WList { Id = Guid.NewGuid(), Name = "WList 2", Description = "Short description" };
            context.WLists.Add(parentList);
            context.WListItems.Add(new WListItem { Id = Guid.NewGuid(), Name = "Item 1", Details="Item 1 details", WListId = parentList.Id, WList = parentList });
            context.WListItems.Add(new WListItem { Id = Guid.NewGuid(), Name = "Item 2", Details = "Item 2 details", WListId = parentList.Id, WList = parentList });
            
            context.SaveChanges();
        }
    }
}