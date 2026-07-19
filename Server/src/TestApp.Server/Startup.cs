using TestApp.ToDoList.Application.Common;
using TestApp.ToDoList.Application.Services;
using TestApp.ToDoList.Infrastructure.Repositories;
using TestApp.ToDoList.Infrastructure.Store;

namespace TestApp.Server
{
    public class Startup
    {
        IConfiguration configuration;

        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(configuration);

            // Add DB
            services.AddDbContext<ToDoListDbContext>();

            // Add controllers
            services.AddControllers();

            // Configure app services
            services.AddScoped<IToDoListTracker, ToDoListTracker>();
            services.AddScoped<IToDoItemsRepository, ToDoItemsRepository>();
            //services.AddScoped<ToDoListEntityModel>();

            services.AddCors(options =>
            {
                options.AddDefaultPolicy(p =>
                {
                    p.AllowAnyOrigin()
                      .AllowAnyHeader();
                });
            });

            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider svcProv)
        {
            // Enable Swagger in all environments
            app.UseSwagger();
            app.UseSwaggerUI();

            var appLifetime = svcProv.GetRequiredService<IHostApplicationLifetime>();
            appLifetime.ApplicationStarted.Register(OnApplicationStarted);

            app.UseRouting();
            app.UseAuthorization();

            app.UseCors();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private void OnApplicationStarted()
        {
            // Do nothing
        }

    }
}