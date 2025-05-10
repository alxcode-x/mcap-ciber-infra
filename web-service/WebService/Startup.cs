using Microsoft.EntityFrameworkCore;
using WebService.DataAccess;

namespace WebService
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddDbContext<PostgresSqlContext>(options =>
                options.UseNpgsql(Configuration.GetConnectionString("PostgreSqlConnectionString")));
            services.AddEndpointsApiExplorer();
            services.AddScoped<IDataAccessProvider, DataAccessProvider>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
   
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}