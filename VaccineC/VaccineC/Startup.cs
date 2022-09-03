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

            //Repositories
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IPersonRepository, PersonRepository>();
            services.AddScoped<IPaymentFormRepository, PaymentFormRepository>();
            services.AddScoped<IResourceRepository, ResourceRepository>();
            services.AddScoped<IUserResourceRepository, UserResourceRepository>();
            services.AddScoped<ICompanyRepository, CompanyRepository>();
            services.AddScoped<ICompanyParameterRepository, CompanyParameterRepository>();
            services.AddScoped<ICompanyScheduleRepository, CompanyScheduleRepository>();

            //AppServices
            services.AddScoped<IExampleAppService, ExampleAppService>();
            services.AddScoped<IPaymentFormAppService, PaymentFormAppService>();
            services.AddScoped<IResourceAppService, ResourceAppService>();
            services.AddScoped<ICompanyAppService, CompanyAppService>();
            services.AddScoped<IUserAppService, UserAppService>();
            services.AddScoped<IPersonAppService, PersonAppService>();
            services.AddScoped<IUserResourceAppService, UserResourceAppService>();
            services.AddScoped<ICompanyParameterAppService, CompanyParameterAppService>();
            services.AddScoped<ICompanyScheduleAppService, CompanyScheduleAppService>();


            services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());
            services.AddMediatR(AppDomain.CurrentDomain.Load("VaccineC.Command.Application"));

            services.AddAutoMapper(typeof(QueryModelMapper).Assembly);
            services.AddScoped<IQueryContext, QueryContext>();

            services.AddDbContext<VaccineCCommandContext>(options => options.UseSqlServer("Data Source=DESKTOP-LDCPPUG\\SQLEXPRESS;Initial Catalog=vaccinec;persist security info=True;Integrated Security=SSPI;"));
            services.AddDbContext<VaccineCContext>(options => options.UseSqlServer("Data Source=DESKTOP-LDCPPUG\\SQLEXPRESS;Initial Catalog=vaccinec;persist security info=True;Integrated Security=SSPI;"));
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
