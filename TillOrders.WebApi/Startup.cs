using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using TillOrders.Data;
using System.Web.Http;
using Autofac.Integration.WebApi;
using Autofac;
using TillOrders.WebApi.Infrastructure;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System;
using Autofac.Extensions.DependencyInjection;
using TillOrders.Services;
using TillOrders.Domain.Model;
using TillOrders.Domain.Data;

namespace TillOrders.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        
        public IContainer ApplicationContainer { get; private set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddDbContext<TillOrdersObjectContext>(options => options.UseSqlServer(Configuration.GetConnectionString("TillOrdersObjectContext")));

            RegisterDependency(services);

            // Create the IServiceProvider based on the container.
            return new AutofacServiceProvider(this.ApplicationContainer);
        }

        private void RegisterDependency(IServiceCollection services) {
           
            var builder = new ContainerBuilder();
            builder.Populate(services);

            builder.RegisterType<TillOrdersObjectContext>().As<IDbContext>().InstancePerLifetimeScope();
            builder.RegisterType<OrderService>().As<IOrderService>().InstancePerLifetimeScope();
            builder.RegisterType<EfRepository<Order>>().As<IRepository<Order>>().InstancePerLifetimeScope();
            builder.RegisterType<EfRepository<OrderItem>>().As<IRepository<OrderItem>>().InstancePerLifetimeScope();


            this.ApplicationContainer = builder.Build();
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
            
        } 
    }
}
