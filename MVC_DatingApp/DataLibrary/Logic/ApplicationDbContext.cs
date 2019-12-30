using DataLibrary.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Diagnostics;

namespace DataLibrary.Logic
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
            Database.Log = s => Debug.WriteLine(s);
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>(); // Enable cascade delete
            base.OnModelCreating(modelBuilder);
        }

        //DbSets

        public DbSet<UserSignUpModel> Userss { get; set; }
        public DbSet<FriendListModel> Friends { get; set; }
        public DbSet<PostModel> Posts { get; set; }
        public DbSet<RequestModel> Requests { get; set; }
    }
}