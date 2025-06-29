using FeatureTracker.Domain.Model.Categories;
using FeatureTracker.Domain.Model.Companies;
using FeatureTracker.Domain.Model.Persons;
using FeatureTracker.Domain.Model.Requests;
using FeatureTracker.Domain.Model.Teams;
using FeatureTracker.Domain.Model.Users;
using Microsoft.EntityFrameworkCore;

namespace FeatureTracker.Infrastructure;

public class Context : DbContext
{
    #region Constructors
    public Context() => Database.SetCommandTimeout(12000);

    public Context(DbContextOptions<Context> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(Context).Assembly);
        base.OnModelCreating(modelBuilder);
    }
    #endregion

    #region Models

    #region Users
    public DbSet<User> Users { get; set; }
    public DbSet<UserInvite> UserInvite { get; set; }
    #endregion

    #region Persons
    public DbSet<Person> Person { get; set; }
    public DbSet<PersonType> PersonType { get; set; }
    public DbSet<PersonAddress> PersonAddress { get; set; }
    #endregion

    #region Requests
    public DbSet<Request> Request { get; set; }
    #endregion

    #region Companies
    public DbSet<Company> Company { get; set; }
    #endregion

    #region Categories
    public DbSet<Category> Category { get; set; }
    #endregion

    #region Teams
    public DbSet<Team> Team { get; set; }
    #endregion

    #endregion
}
