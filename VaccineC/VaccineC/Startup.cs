using MediatR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Serialization;
using VaccineC.Command.Data.Context;
using VaccineC.Command.Data.Repositories;
using VaccineC.Command.Domain.Abstractions.Repositories;
using VaccineC.Query.Application.Abstractions;
using VaccineC.Query.Application.Mapper;
using VaccineC.Query.Application.Services;
using VaccineC.Query.Data.Context;
using VaccineC.Query.Data.QueryContext;
using VaccineC.Query.Model.Abstractions;

namespace VaccineC
{
    public class Startup
    {
        readonly string FrontendOrigins = "_frontendOrigins";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(name: FrontendOrigins, builder =>
                {
                    builder
                        .WithOrigins("http://localhost:4200")
                        .WithHeaders("Content-Type")
                        .WithMethods("GET", "POST", "PUT", "DELETE", "OPTIONS");
                });
            });
            services.AddControllers()
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.ContractResolver = new DefaultContractResolver();
                });




            //AppServices
            services.AddScoped<IExampleAppService, ExampleAppService>();

            services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());
            services.AddMediatR(AppDomain.CurrentDomain.Load("VaccineC.Command.Application"));

            services.AddAutoMapper(typeof(QueryModelMapper).Assembly);
            services.AddScoped<IQueryContext, QueryContext>();

            services.AddDbContext<VaccineCCommandContext>(options => options.UseSqlServer("data source=CXJ0975;initial catalog=vaccinecdb;user id=sa;password=PromobSQL2021"));
            services.AddDbContext<VaccineCContext>(options => options.UseSqlServer("data source=CXJ0975;initial catalog=vaccinecdb;user id=sa;password=PromobSQL2021"));

            //Repositories
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IPersonRepository, PersonRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(FrontendOrigins);

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
