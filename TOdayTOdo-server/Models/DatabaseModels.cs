using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Globalization;
using System.Web.Security;

namespace TOdayTOdo_server.Models
{
    public class DatabaseContext : DbContext
    { 
        // Konstruktor 
        public DatabaseContext()
        {
            
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Vytvorenie databazy ak neexistuje
            Database.SetInitializer<DatabaseContext>(new CreateDatabaseIfNotExists<DatabaseContext>());
        }

        // zoznam tabuliek ktore sa vygeneruju
        public DbSet<Task> Tasks { get; set; }
        public DbSet<MyError> Errors { get; set; }
    }
       
    //trieda reprezentujuca tabulku pre uchovanie Taskov
    [Table("Task")]
    public class Task
    {
        [Key]
        public int UUID { get; set; }
        [Required]
        public String Content { get; set; }
        public DateTime Date { get; set; }

    }
         
    //trieda reprezent tabulka pre ukladanie chýb
    [Table("MyErrors")]
    public class MyError
    {
        [Key]
        public int UUID { get; set; }
        public String Description { get; set; }
        public DateTime Date { get; set; }
    }

}
