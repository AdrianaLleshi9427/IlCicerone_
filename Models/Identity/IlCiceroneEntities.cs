using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IlCicerone.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;


namespace IlCicerone.Models
{
    public class IlCiceroneEntities : DbContext
    {
        public System.Data.Entity.DbSet<IlCicerone.Models.Article> Articles { get; set; }
        //    public IlCiceroneEntities() : base("IlCiceroneEntities")
        //    {
        //    }

        //    public DbSet<Guide> Guides { get; set; }

        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //    {
        //        //Database.SetInitializer<demoEntities>(null);
        //        modelBuilder.Entity<Guide>().ToTable("Guides");
        //        modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

        //        base.OnModelCreating(modelBuilder);


        //    }
    }
}