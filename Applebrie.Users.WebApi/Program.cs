using Applebrie.Domain;
using Applebrie.Users.WebApi.Commands.Users;
using Applebrie.Users.WebApi.Queries;
using Applebrie.Users.WebApi.Query.Users;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//AppDbContext
builder.Services.AddDbContext<AppDbContext>(options =>
               options.UseSqlServer(
                   builder.Configuration.GetConnectionString("SqlConnectionString"),
                   b => b.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName)));

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddScoped<IUserCommand, CreateUserCommand>();
builder.Services.AddScoped<UpdateUserCommand, UpdateUserCommand>();
builder.Services.AddSingleton<GetUserByIdQuery, GetUserByIdQuery>();

//Autofac
#region Autofac

var containerBuilder = new ContainerBuilder();

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
{
    containerBuilder
    .RegisterAssemblyTypes(Assembly.GetAssembly(typeof(GetUserByIdQuery)))
    .AsImplementedInterfaces()
    .InstancePerLifetimeScope();

    // репозитории
    containerBuilder
    .RegisterGeneric(typeof(Repository<>))
    .As(typeof(IRepository<>))
    .InstancePerDependency();


    //Automapper
    MapperConfiguration mapperConfiguration =
        new MapperConfiguration(cfg =>
        {
            cfg.ShouldMapProperty = p => p.GetMethod.IsPublic || p.GetMethod.IsAssembly;
            cfg.AddMaps(
                typeof(GetUserByIdQuery).Assembly 
                );
        });
    containerBuilder.Register(context => mapperConfiguration);

});

containerBuilder.Populate(builder.Services);

#endregion Autofac

var app = builder.Build();



// Configure the HTTP request pipeline.
app.UseCors();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

#region Auto Migrate Database
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<AppDbContext>();
        context.Database.Migrate();
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while seeding the atabase.");
    }
}
#endregion

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
