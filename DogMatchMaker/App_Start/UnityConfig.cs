using DogMatchMaker.Data;
using DogMatchMaker.Orchestrator;
using System.Web.Mvc;
using Unity;
using Unity.Injection;
using Unity.Lifetime;
using Unity.Mvc5;

namespace DogMatchMaker
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();
            
            // register all your components with the container here
            // it is NOT necessary to register your controllers
            
            // e.g. container.RegisterType<ITestService, TestService>();
            
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
            container.RegisterType<IHomeOrchestrator, HomeOrchestrator>(new TransientLifetimeManager(), new InjectionConstructor(new DogRepository("DogMatchMaker")));

        }
    }
}