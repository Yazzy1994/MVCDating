using System;
using System.Data.Entity;
namespace DataLibrary.Logic
{
    public class ApplicationDbContextInitializer : DropCreateDatabaseAlways<ApplicationDbContext>
    { // Re-create database with example data every time you boot the project.
        protected override void Seed(ApplicationDbContext context)
        {
            base.Seed(context);
        }

    }
}