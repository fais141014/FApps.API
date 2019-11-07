using System;
using FApps.Core.Domain;
using Microsoft.EntityFrameworkCore;

namespace FApps.Core.Context
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options) { }

        DbSet<User> Users { get; set; }
        DbSet<Employee> Employees { get; set; }
        DbSet<Contact> Contacts { get; set; }
        DbSet<Student> Students { get; set; }
        DbSet<LogEntry> logEntries { get; set; }
    }
}
