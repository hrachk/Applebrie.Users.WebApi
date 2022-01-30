using Applebrie.Users.WebApi.Commands.Users;
using Applebrie.Users.WebApi.Entity;
using Applebrie.Users.WebApi.Entity.Models;
using Applebrie.Users.WebApi.Query.Model;
using Microsoft.EntityFrameworkCore;

namespace Applebrie.Users.WebApi.Query.Users
{
    public class CreateUserCommand : IUserCommand
    {
        private readonly AppDbContext context;
 
        public CreateUserCommand(AppDbContext context)
        {
            this.context = context;
        }
        public async Task Execute(UserInputModel userInputModel)
          {
            User user = new User { 
                Id = userInputModel.Id,
            LastName = userInputModel.LastName,
            FirstName = userInputModel.FirstName,
            BirthDate = userInputModel.BirthDate
            };

            context.Add(user);

            // сохраняем           
            await context.SaveChangesAsync();
 
        }
    }
}
