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
            var dbUser = await context.Users.FindAsync(userInputModel.Id);
            if (dbUser == null)
                return;

            User user = new User()
            {   
                Id = userInputModel.Id,
                FirstName = userInputModel.FirstName,
                LastName = userInputModel.LastName,
                BirthDate = userInputModel.BirthDate,
            };
            context.Update(user);
            await context.SaveChangesAsync();
        }
    }
}
