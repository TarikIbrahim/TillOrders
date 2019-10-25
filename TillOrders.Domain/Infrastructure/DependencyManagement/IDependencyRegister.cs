using Autofac;

namespace TillOrders.Domain.Infrastructure.DependencyManagement
{
    public interface IDependencyRegister
    {
        /// <summary>
        /// Register services and interfaces
        /// </summary>
        /// <param name="builder">Container builder</param>
        /// <param name="typeFinder">Type finder</param>
        
        void Register(ContainerBuilder builder, ITypeFinder typeFinder);

        /// <summary>
        /// Gets order of this dependency registrar implementation
        /// </summary>
        int Order { get; }
    }
}
