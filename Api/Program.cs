using System.Reflection;
using System.Text.Json.Serialization;
using Asp.Versioning;
using Asp.Versioning.ApiExplorer;
using Juliapos.AspNetCore.Authorization;
using Juliapos.Authorization.Swagger;
using Juliapos.Authorization.Swagger.Options;
using Juliapos.Patterns.DtoMapping;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using Serilog.Settings.Configuration;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Text.Json;
using Juliapos.Portal.ProductApi.Api;
using Juliapos.Portal.ProductApi.Db;
using Juliapos.Portal.ProductApi.Db.DataQueries;
using Juliapos.Portal.ProductApi.Db.DataQueries.Implementation;
using Juliapos.Portal.ProductApi.Api.Models.Dto;
using Juliapos.Portal.ProductApi.Db.Models;
using Juliapos.Portal.ProductApi.Api.Mappers;
using Juliapos.Portal.ProductApi.Services;
using Juliapos.Portal.ProductApi.Services.Implementation;
using Juliapos.Patterns.CQRS.Queries;
using System.Data;
using Juliapos.Portal.ProductApi.Queries;
using Juliapos.Portal.ProductApi.Queries.Handlers;
using Juliapos.Patterns.CQRS.Commands;
using Juliapos.Portal.ProductApi.Commands;
using Juliapos.Portal.ProductApi.Commands.Handlers;

var builder = WebApplication.CreateBuilder(args);

builder.Host
    .UseSerilog((hostingContext, loggerConfiguration)
        => loggerConfiguration.ReadFrom.Configuration(hostingContext.Configuration,
        new ConfigurationReaderOptions { }));

var configuration = new ConfigurationBuilder()
    .SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true)
    .AddEnvironmentVariables()
    .Build();

builder.Configuration.AddConfiguration(configuration);


builder.Services.Configure<AuthSwaggerOptions>(builder.Configuration.GetSection("swagger"));


// Add services to the container.

builder.Services
    .AddRouting(options => options.LowercaseUrls = true)
    .AddControllers(options =>
    {
        options.Filters.Add<ApiResultExceptionFilter>();
    })
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    });


ConfigureDb(builder.Services);
ConfigureAuthorization(builder.Services);
ConfigureServices(builder.Services);
ConfigureSwagger(builder.Services);


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(o =>
{
    o.EnableAnnotations();
    //o.DocumentFilter<DescribePatchDocumentFilter>();
    //o.OperationFilter<DescribePatchOperationFilter>();
});

var connectionString = builder.Configuration.GetConnectionString("Postgres");
builder.Services
    .AddHealthChecks()
    .AddNpgSql(connectionString);

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    var versionProvider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();
    var optionsAccessor = app.Services.GetRequiredService<IOptions<AuthSwaggerOptions>>();

    foreach (var description in versionProvider.ApiVersionDescriptions.OrderByDescending(x => x.ApiVersion))
    {
        options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", $"Tracking Api v{description.ApiVersion}");
        //options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", $"{optionsAccessor.Value.ApiName} v{description.ApiVersion}");
    }

    options.OAuthClientId(optionsAccessor.Value.ClientId);
    options.OAuthUsePkce();
});

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.MapHealthChecks("/health", new HealthCheckOptions()
{
    ResponseWriter = HealthCheckExtensions.WriteResponse
});

app.Run();



void ConfigureDb(IServiceCollection services)
{
    void InitDebugLogging(DbContextOptionsBuilder options, bool debugLogging)
    {
        if (debugLogging)
        {
            options.LogTo(Console.WriteLine, LogLevel.Debug)
                .EnableDetailedErrors()
                .EnableSensitiveDataLogging();
        }
    }

    var debugLogging = false;
    var provider = builder.Configuration.GetValue("DbProvider", "sqlserver");
    var connectionString = builder.Configuration.GetConnectionString(provider);

    if (!Assembly.GetEntryAssembly().FullName.StartsWith("dotnet-swagger")
        && !Assembly.GetEntryAssembly().FullName.StartsWith("dotnet-ef"))
    {
        ApiDbContext.EnsureDbCreated(connectionString, provider, false);
    }


    services.AddDbContext<ApiDbContext>(options =>
    {
        options.UseNpgsql(connectionString, options
            => options.MigrationsAssembly(
                typeof(Juliapos.Portal.ProductApi.Db.Postgres.ApiDbDesignTimeContext).Assembly.FullName));
        InitDebugLogging(options, debugLogging);

    });

    services.AddDatabaseDeveloperPageExceptionFilter();
    services.AddScoped<IApiDbContext, ApiDbContext>((sp) => sp.GetRequiredService<ApiDbContext>());
    services.AddTransient<IApiDbDataStore, ApiDbDataStore>();
}

void ConfigureAuthorization(IServiceCollection services)
{
    //services.AddApiAuthentication(builder.Configuration.GetSection("swagger").Get<AuthSwaggerOptions>());
    services.AddAuthorization(options =>
    {
    });

    // Alleen de authority is nodig!
    var swaggerOptions = builder.Configuration.GetSection("swagger").Get<AuthSwaggerOptions>();
    services
        .AddAuthentication()
        .AddJwtBearer(options =>
         {
             options.Authority = swaggerOptions.Authority;
             options.TokenValidationParameters = new TokenValidationParameters
             {
                 ValidIssuers = [ swaggerOptions.Authority ],
                 ValidAudiences =[ swaggerOptions.Authority + "/resources" ],
                 
             };
             options.Events = new JwtBearerEvents
             {
                 OnTokenValidated = async ctxt =>
                 {
                     await Task.Yield();
                 }
             };
         });
}


void ConfigureServices(IServiceCollection services)
{
    services
        .AddHttpContextAccessor();

    services
        .AddScoped<IAuthorizationContext, AuthorizationContext>()
        .AddScoped<IEndpointArgumentValidator, EndpointArgumentValidator>()
        ;

    // singleton?
    services.AddTransient<IDtoMapper, DtoMapper>();
    services.AddTransient<IDtoMapper<Product, ProductDto>, ProductDtoMapper>();
    services.AddTransient<IDtoMapper<ProductCategory, ProductCategoryDto>, ProductCategoryDtoMapper>();
    services.AddTransient<IDtoMapper<DustCategory, DustCategoryDto>, DustCategoryDtoMapper>();
    services.AddTransient<IDtoMapper<MenuCategory, MenuCategoryDto>, MenuCategoryDtoMapper>();
    services.AddTransient<IDtoMapper<Property, PropertyDto>, PropertyDtoMapper>();
    services.AddTransient<IDtoMapper<SelectionPage, SelectionPageDto>, SelectionPageDtoMapper>();

    services.AddScoped<IProductsService, ProductsService>();
    services.AddScoped<IProductCategoriesService, ProductCategoriesService>();

    // queries
    services.AddTransient<IQueryHandler, QueryHandler>();
    services.AddTransient<IHandleQuery<DustCategoriesQuery, IEnumerable<DustCategory>>, DustCategoriesQueryHandler>();
    services.AddTransient<IHandleQuery<DustCategoryQuery, DustCategory>, DustCategoryQueryHandler>();
    services.AddTransient<IHandleQuery<MenuCategoriesQuery, IEnumerable<MenuCategory>>, MenuCategoriesQueryHandler>();
    services.AddTransient<IHandleQuery<MenuCategoryQuery, MenuCategory>, MenuCategoryQueryHandler>();
    services.AddTransient<IHandleQuery<PropertiesQuery, IEnumerable<Property>>, PropertiesQueryHandler>();
    services.AddTransient<IHandleQuery<PropertyQuery, Property>, PropertyQueryHandler>();
    services.AddTransient<IHandleQuery<SelectionPagesQuery, IEnumerable<SelectionPage>>, SelectionPagesQueryHandler>();
    services.AddTransient<IHandleQuery<SelectionPageQuery, SelectionPage>, SelectionPageQueryHandler>();

    // commands
    services.AddTransient<ICommandHandler, CommandHandler>();
    services.AddTransient<IHandleCommand<DustCategoryCreateCommand, DustCategory>, DustCategoryCreateCommandHandler>();
    services.AddTransient<IHandleCommand<DustCategoryUpdateCommand, DustCategory>, DustCategoryUpdateCommandHandler>();
    services.AddTransient<IHandleCommand<DustCategoryDeleteCommand, DustCategory>, DustCategoryDeleteCommandHandler>();
    services.AddTransient<IHandleCommand<MenuCategoryCreateCommand, MenuCategory>, MenuCategoryCreateCommandHandler>();
    services.AddTransient<IHandleCommand<MenuCategoryUpdateCommand, MenuCategory>, MenuCategoryUpdateCommandHandler>();
    services.AddTransient<IHandleCommand<MenuCategoryDeleteCommand, MenuCategory>, MenuCategoryDeleteCommandHandler>();
    services.AddTransient<IHandleCommand<PropertyCreateCommand, Property>, PropertyCreateCommandHandler>();
    services.AddTransient<IHandleCommand<PropertyUpdateCommand, Property>, PropertyUpdateCommandHandler>();
    services.AddTransient<IHandleCommand<PropertyDeleteCommand, Property>, PropertyDeleteCommandHandler>();
    services.AddTransient<IHandleCommand<SelectionPageCreateCommand, SelectionPage>, SelectionPageCreateCommandHandler>();
    services.AddTransient<IHandleCommand<SelectionPageUpdateCommand, SelectionPage>, SelectionPageUpdateCommandHandler>();
    services.AddTransient<IHandleCommand<SelectionPageDeleteCommand, SelectionPage>, SelectionPageDeleteCommandHandler>();

}

void ConfigureSwagger(IServiceCollection services)
{
    services.AddSingleton<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerGenOptions>();

    services.AddEndpointsApiExplorer();
    services.AddApiVersioning(options =>
    {
        options.DefaultApiVersion = new ApiVersion(1, 0);
        options.AssumeDefaultVersionWhenUnspecified = true;
        options.ReportApiVersions = true;
    })
    .AddApiExplorer(options =>
    {
        options.GroupNameFormat = "'v'VVV";
        options.SubstituteApiVersionInUrl = true;
        options.AssumeDefaultVersionWhenUnspecified = true;
    });

    services.AddSwaggerGen(options =>
    {
        var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
        options.IncludeXmlComments(xmlPath);

        //options.CustomSchemaIds(x => 
        //    x.IsClass && x.Name.EndsWith("Dto") ? x.Name.Substring(0,x.Name.Length-3) : x.Name
        //);
        //options.DocumentFilter<HideEndpointPrefixDocumentFilter>();
        //options.OperationFilter<PermissionInformationEnrichmentOperationFilter>();
    });

}

class DummyAuthorizationContext : IAuthorizationContext
{
    public Guid? UserId => Guid.Parse("cabc70b6-bc1b-402e-a3e5-bbe929508752");
    public Guid? OrganizationId => Guid.Parse("f76232f6-e78f-4a59-9d28-fd1638f055d8");
    public Guid? PrincipalId => Guid.Empty;
    public string[] Permissions => [];
}
