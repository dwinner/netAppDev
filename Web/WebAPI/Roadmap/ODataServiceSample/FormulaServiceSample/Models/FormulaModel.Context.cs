﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FormulaServiceSample.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class FormulaEntities : DbContext
    {
        public FormulaEntities()
            : base("name=FormulaEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Circuit> Circuits { get; set; }
        public virtual DbSet<RaceResult> RaceResults { get; set; }
        public virtual DbSet<Racer> Racers { get; set; }
        public virtual DbSet<Race> Races { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<Team> Teams { get; set; }
        public virtual DbSet<TeamsAndMotor> TeamsAndMotors { get; set; }
        public virtual DbSet<YearResult> YearResults { get; set; }
        public virtual DbSet<YearResultsTeam> YearResultsTeams { get; set; }
    
        public virtual int GetRacers()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("GetRacers");
        }
    }
}
