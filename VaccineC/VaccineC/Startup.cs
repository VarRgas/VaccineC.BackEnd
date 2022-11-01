using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Newtonsoft.Json.Serialization;
using VaccineC.Authentication.Extensions;
using VaccineC.Authentication.Interfaces;
using VaccineC.Authentication.Services;
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
                options.AddPolicy(name: FrontendOrigins,
                                  builder =>
                                  {
                                      builder.AllowAnyOrigin()
                                      .AllowAnyHeader()
                                      .AllowAnyMethod();
                                  });
            });

            services.AddControllers()
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.ContractResolver = new DefaultContractResolver();
                });

            services.AddMvcCore(o =>
            {
                //#if !DEBUG
                var policy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .Build();
                o.Filters.Add(new AuthorizeFilter(policy));
                //#endif

                //o.Filters.Add<NotificationFilter>();
                o.EnableEndpointRouting = false;
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
            services.AddScoped<IPersonPhoneRepository, PersonPhoneRepository>();
            services.AddScoped<IPersonAddressRepository, PersonAddressRepository>();
            services.AddScoped<IPersonPhysicalRepository, PersonPhysicalRepository>();
            services.AddScoped<IPersonJuridicalRepository, PersonJuridicalRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductDosesRepository, ProductDosesRepository>();
            services.AddScoped<IProductSummaryBatchRepository, ProductSummaryBatchRepository>();
            services.AddScoped<IMovementRepository, MovementRepository>();
            services.AddScoped<IMovementProductRepository, MovementProductRepository>();
            services.AddScoped<INotificationRepository, NotificationRepository>();
            services.AddScoped<IBudgetRepository, BudgetRepository>();
            services.AddScoped<IBudgetNegotiationRepository, BudgetNegotiationRepository>();
            services.AddScoped<IBudgetProductRepository, BudgetProductRepository>();
            services.AddScoped<IDiscardRepository, DiscardRepository>();
            services.AddScoped<IBudgetHistoricRepository, BudgetHistoricRepository>();
            services.AddScoped<IEventRepository, EventRepository>();
            services.AddScoped<IAuthorizationRepository, AuthorizationRepository>();
            services.AddScoped<IAuthorizationNotificationRepository, AuthorizationNotificationRepository>();
            services.AddScoped<IApplicationRepository, ApplicationRepository>();

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
            services.AddScoped<IPersonPhoneAppService, PersonPhoneAppService>();
            services.AddScoped<IPersonAddressAppService, PersonAddressAppService>();
            services.AddScoped<IPersonPhysicalAppService, PersonPhysicalAppService>();
            services.AddScoped<IPersonJuridicalAppService, PersonJuridicalAppService>();
            services.AddScoped<IProductAppService, ProductAppService>();
            services.AddScoped<IProductDosesAppService, ProductDosesAppService>();
            services.AddScoped<IProductSummaryBatchAppService, ProductSummaryBatchAppService>();
            services.AddScoped<IMovementAppService, MovementAppService>();
            services.AddScoped<IMovementProductAppService, MovementProductAppService>();
            services.AddScoped<IBudgetProductAppService, BudgetProductAppService>();
            services.AddScoped<IBudgetNegotiationAppService, BudgetNegotiationAppService>();
            services.AddScoped<IBudgetAppService, BudgetAppService>();
            services.AddScoped<IAuthorizationAppService, AuthorizationAppService>();
            services.AddScoped<INotificationAppService, NotificationAppService>();
            services.AddScoped<IDiscardAppService, DiscardAppService>();
            services.AddScoped<IBudgetHistoricAppService, BudgetHistoricAppService>();
            services.AddScoped<IEventAppService, EventAppService>();
            services.AddScoped<IAuthorizationNotificationAppService, AuthorizationNotificationAppService>();
            services.AddScoped<IApplicationAppService, ApplicationAppService>();
            services.AddScoped<IJwtService, JwtService>();

            services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());
            services.AddMediatR(AppDomain.CurrentDomain.Load("VaccineC.Command.Application"));

            services.AddAutoMapper(typeof(QueryModelMapper).Assembly);
            services.AddScoped<IQueryContext, QueryContext>();

            //Conexão Amanda
            //services.AddDbContext<VaccineCCommandContext>(options => options.UseSqlServer("Data Source=DESKTOP-LDCPPUG\\SQLEXPRESS;Initial Catalog=vaccinec;persist security info=True;Integrated Security=SSPI;"));
            //services.AddDbContext<VaccineCContext>(options => options.UseSqlServer("Data Source=DESKTOP-LDCPPUG\\SQLEXPRESS;Initial Catalog=vaccinec;persist security info=True;Integrated Security=SSPI;"));

            //Conexão Guilherme
            services.AddDbContext<VaccineCCommandContext>(options => options.UseSqlServer("data source=CXJ0975;initial catalog=vaccinecdb;user id=sa;password=PromobSQL2021"));
            services.AddDbContext<VaccineCContext>(options => options.UseSqlServer("data source=CXJ0975;initial catalog=vaccinecdb;user id=sa;password=PromobSQL2021"));

            services.Configure<FormOptions>(o =>
            {
                o.ValueLengthLimit = int.MaxValue;
                o.MultipartBodyLengthLimit = int.MaxValue;
                o.MemoryBufferThreshold = int.MaxValue;

            });
            services.AddJwtAuthentication();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(FrontendOrigins);

            app.UseStaticFiles();
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"Resources")),
                RequestPath = new PathString("/Resources")
            });

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
