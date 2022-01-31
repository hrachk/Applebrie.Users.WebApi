using Applebrie.Domain;
using Applebrie.Users.WebApi.Commands.Users;
using Applebrie.Users.WebApi.Query.Users;
using Microsoft.EntityFrameworkCore;

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


var app = builder.Build();



// Configure the HTTP request pipeline.
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
app.UseCors();
app.UseAuthorization();

app.MapControllers();

app.Run();
