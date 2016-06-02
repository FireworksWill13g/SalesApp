using SalesApp.Interfaces;
using SalesApp.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesApp.Data
{
    class SalesContext : DbContext
    //SalesContext inherits from DbContext which is apart of the Entity Framework 
    {
        //Define some Db sets for each of the models

        public DbSet<Sale> Sales { get; set; }

        public DbSet<SalesPerson> People { get; set; }

        public DbSet<SalesRegion> Regions { get; set; }

        //whenever a DBcontect is created, a method called OnModelCreating is called by Entity Framework
        // If you want to make any changes to the model you creat an override

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // We do not want a cascade delete, which means when one item is deleted the related items are as well

            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

        }

        public override int SaveChanges()
        {
            ChangeTracker.DetectChanges();

            //soft deletion - changing the active flag to flase instead of deleting all
            var stateManager = ((IObjectContextAdapter)this).ObjectContext.ObjectStateManager;

            var deleteEntities = stateManager               // grab the deleted entities by saying
                .GetObjectStateEntries(EntityState.Deleted) // give me all entities that have been deleted
                .Select(e => e.Entity)                      //Only want the actual Entity to deal with
                .OfType<IActive>()                          // This selects only the ones in IActive
                .ToArray();                                 // puts these into an array

            //loop through array of deleted entities and change their status in Active to Inactive so its not actually deleted , it is just set to not active
            foreach (var deletedEntity in deleteEntities)
            {
                if (deletedEntity == null) continue;
                stateManager.ChangeObjectState(deletedEntity, EntityState.Modified);
                deletedEntity.Active = false;   // sets active flag to false
            }

            //auditing feature

            var createdEntities = stateManager
                .GetObjectStateEntries(EntityState.Added)
                .Select(e => e.Entity)
                .OfType<BaseModel>()
                .ToArray();

            foreach (var createdEntity in createdEntities)
            {
                createdEntity.CreatedDate = DateTime.Now;
                createdEntity.CreatedBy = Environment.UserName;
                createdEntity.UpdatedDate = DateTime.Now;
                createdEntity.UpdatedBy = Environment.UserName;
            }


            var updatedEntities = stateManager
                .GetObjectStateEntries(EntityState.Modified)
                .Select(e => e.Entity)
                .OfType<BaseModel>()
                .ToArray();

            foreach (var updatedEntity in updatedEntities)
            {
                updatedEntity.UpdatedDate = DateTime.Now;
                updatedEntity.UpdatedBy = Environment.UserName;
            }
            return base.SaveChanges();
        }
    }
}
