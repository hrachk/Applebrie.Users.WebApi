using Applebrie.Users.WebApi.Entity;
using Applebrie.Users.WebApi.Entity.Models;
using Applebrie.Users.WebApi.Query.Model;

namespace Applebrie.Users.WebApi.Commands.Users
{
    public class UpdateUserCommand : IUserCommand
    {
        private readonly AppDbContext context;

        public UpdateUserCommand(AppDbContext context)
        {
            this.context = context;
        }
        public async Task Execute(UserInputModel userInputModel)
        {
            var users = new User
            { 
                
                FirstName = userInputModel.FirstName, 
                LastName = userInputModel.LastName, 
                BirthDate = userInputModel.BirthDate 
            };

            context.Update(users);
            await context.SaveChangesAsync(true);
        }
    }
}
