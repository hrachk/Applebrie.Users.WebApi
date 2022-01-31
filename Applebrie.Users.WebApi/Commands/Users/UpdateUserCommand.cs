using Applebrie.Users.WebApi.Entity;
using Applebrie.Users.WebApi.Query.Model;

namespace Applebrie.Users.WebApi.Commands.Users
{
    public class UpdateUserCommand : IUserCommand
    {
        #region field
        private readonly AppDbContext context;
        #endregion

        #region Constructor
        public UpdateUserCommand(AppDbContext context)
        {
            this.context = context;
        }
        #endregion

        #region Method
        public async Task Execute(UserInputModel userInputModel)
        {
            var dbUser = await context.Users.FindAsync(userInputModel.Id);
            if (dbUser == null)
                return;


            dbUser.FirstName = userInputModel.FirstName;
            dbUser.LastName = userInputModel.LastName;
            dbUser.BirthDate = userInputModel.BirthDate;

            await context.SaveChangesAsync();
        }
        #endregion
    }
}
