﻿//------------------------------------------------------------------------------
// <auto-generated>
//    Этот код был создан из шаблона.
//
//    Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//    Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace _13_EntityFrameworkDemo.Orm
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Objects;
    using System.Data.Objects.DataClasses;
    using System.Linq;
    
    public partial class BookEntities : DbContext
    {
        public BookEntities()
            : base("name=BookEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<Books> Books { get; set; }
    
        public virtual ObjectResult<GetSortedBooks_Result> GetSortedBooks()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetSortedBooks_Result>("GetSortedBooks");
        }
    }
}
