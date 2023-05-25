using KittATM.Models;
using Microsoft.EntityFrameworkCore;

namespace KittATM.Migrations;

public class DataDbContext : DbContext
{
    public DbSet<User> User { get; set; }

    public string DbPath { get; }

    public DataDbContext()
    {
        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
        DbPath = System.IO.Path.Join(path, "kittatm.db");
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options) => 
        options.UseSqlite($"Data Source={DbPath}");
}