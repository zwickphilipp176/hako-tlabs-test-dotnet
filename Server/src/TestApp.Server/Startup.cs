using FluentValidation;
using TestApp.Server.Filters;
using TestApp.ToDoList.Application.Common;
using TestApp.ToDoList.Application.Model.TodoItem.Validation;
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
            services.AddDbContext<IToDoListDbContext, ToDoListDbContext>();

            // Add controllers
            services.AddControllers(options =>
            {
                options.Filters.Add<RequestLoggingFilter>();
                options.Filters.Add<ValidationFilter>();
            })
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter());
            });

            services.AddLogging(cfg =>
            {
                cfg.AddConsole(); ;
            });

            // FluentValidation validators
            services.AddValidatorsFromAssemblyContaining<CreateToDoItemValidator>();

            // Configure app services
            services.AddScoped<IToDoListTracker, ToDoListTracker>();
            services.AddScoped<IToDoItemsRepository, ToDoItemsRepository>();
            services.AddScoped<IItemTagsRepository, ItemTagsRepository>();

            services.AddCors(options =>
            {
                options.AddDefaultPolicy(p =>
                {
                    p.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
                });
            });

            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Enable Swagger in all environments
            app.UseSwagger();
            app.UseSwaggerUI();

            var appLifetime = app.ApplicationServices.GetRequiredService<IHostApplicationLifetime>();
            appLifetime.ApplicationStarted.Register(() => OnApplicationStarted(app.ApplicationServices));

            app.UseRouting();
            app.UseAuthorization();

            app.UseCors();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private void OnApplicationStarted(IServiceProvider svcProv)
        {
            using var scope = svcProv.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<ToDoListDbContext>();
            ToDoListDbSeeder.Seed(dbContext);
        }

    }
}