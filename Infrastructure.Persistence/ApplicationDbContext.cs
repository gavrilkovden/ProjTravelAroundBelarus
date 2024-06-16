using System.Reflection;
using Auth.Domain;
using Core.Users.Domain;
using Microsoft.EntityFrameworkCore;
using Travels.Domain;

namespace Infrastructure.Persistence;

public sealed class ApplicationDbContext : DbContext
{
    #region Users

    internal DbSet<ApplicationUser> ApplicationUsers { get; } = default!;

    internal DbSet<ApplicationUserRole> ApplicationUserRoles { get; } = default!;

    internal DbSet<ApplicationUserApplicationUserRole> ApplicationUserApplicationUserRole { get; } = default!;

    #endregion

    #region Auth

    internal DbSet<RefreshToken> RefreshTokens { get; } = default!;

    #endregion

    #region Attractions

    internal DbSet<Attraction> Attractions { get; } = default!;

    #endregion

    #region AttractionInRoutes

    internal DbSet<AttractionInRoute> AttractionInRoutes { get; } = default!;

    #endregion

    #region AttractionFeedback

    internal DbSet<AttractionFeedback> AttractionFeedback { get; } = default!;

    #endregion

    #region Routes

    internal DbSet<Route> Routes { get; } = default!;

    #endregion

    #region Tours

    internal DbSet<Tour> Tours { get; } = default!;

    #endregion

    #region TourFeedback

    internal DbSet<TourFeedback> TourFeedback { get; } = default!;

    #endregion

    #region WorkSchedules

    internal DbSet<WorkSchedule> WorkSchedules { get; } = default!;

    #endregion

    #region Addresses

    internal DbSet<Address> Address { get; } = default!;

    #endregion

    #region GeoLocations

    internal DbSet<GeoLocation> GeoLocations { get; } = default!;

    #endregion

    #region Ef

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(modelBuilder);
    }

    #endregion
}