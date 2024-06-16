using System.Reflection;
using UsersApiClass = Users.Api.Apis.UsersApi;
using UsersApplicationDependencyInjectionClass = Users.Application.DependencyInjection;
using AuthApiClass = Auth.Api.Apis.AuthApi;
using Travels.Domain;

namespace Core.ArchitectureTests.Utils;

public abstract class SolutionUtils
{
    public static Assembly[] GetSolutionAssemblies()
    {
        return new[]
        {
            #region Infrastructure

            typeof(Infrastructure.Persistence.DependencyInjection).Assembly,       

            #endregion

            #region Core

            typeof(Core.Users.Domain.ApplicationUser).Assembly,
            typeof(Core.Application.DependencyInjection).Assembly,
            typeof(Core.Api.DependencyInjection).Assembly,

            typeof(Core.Auth.Application.DependencyInjection).Assembly,
            typeof(Core.Auth.Api.DependencyInjection).Assembly,
            
            #endregion

            #region Applications
            
            #region Users
            
            typeof(UsersApplicationDependencyInjectionClass).Assembly,
            typeof(UsersApiClass).Assembly,



            #endregion
            #region Tours

            typeof(Tours.Application.DependencyInjection).Assembly,
            typeof(TravelAroundBelarusProj.Api.Apis.ToursController).Assembly, 

            #endregion

            #region Routes

            typeof(Routes.Application.DependencyInjection).Assembly,
            typeof(TravelAroundBelarusProj.Api.Apis.RoutesController).Assembly, 

            #endregion

            #region Attractions

            typeof(Attractions.Application.DependencyInjection).Assembly,
            typeof(TravelAroundBelarusProj.Api.Apis.AttractionsController).Assembly, 

            #endregion

            #region Auth

            typeof(Auth.Application.DependencyInjection).Assembly,
            typeof(AuthApiClass).Assembly,

            #endregion

            #endregion
        };
    }
}