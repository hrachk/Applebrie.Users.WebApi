using Applebrie.Users.WebApi.Query.Model;

namespace Applebrie.Users.WebApi.Commands.Users
{
    public interface IUserCommand
    {
        public Task Execute(UserInputModel userInputModel);
    }
}
