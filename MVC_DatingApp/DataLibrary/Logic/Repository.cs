using DataLibrary.Logic;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;

namespace DataLibrary.Logic
{
    public abstract class Repository<TValue, TKey> where TValue : class, IIdentifiable<TKey>
    {
        private ApplicationDbContext context = new ApplicationDbContext();

        public Repository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public DbSet<TValue> Items => context.Set<TValue>();

        public TValue Get(TKey Id)
        {
            return Items.Find(Id);
        }

        public List<TValue> GetAll()
        {
            return Items.ToList();
        }

        public void Add(TValue item)
        {
            Items.Add(item);
        }

        public void Save()
        {
            context.SaveChanges();
        }
        public void Remove (TKey Id)
        {
            var item = Get(Id);
            Items.Remove(item);
        }

        public void Edit(TValue item) {
            context.Set<TValue>().AddOrUpdate(item);
        }
    }
}