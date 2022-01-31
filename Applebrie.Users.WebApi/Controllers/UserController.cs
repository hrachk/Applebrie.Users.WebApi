using Applebrie.Domain;
using Applebrie.Users.WebApi.Commands;
using Applebrie.Users.WebApi.Commands.Users;
using Applebrie.Users.WebApi.Query.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Applebrie.Users.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly AppDbContext context;
        private readonly IUserCommand userCommand;
        private readonly UpdateUserCommand updateUserCommand; 

        #region Fields

        #endregion

        #region Constructor
        public UserController(AppDbContext context, IUserCommand userCommand, UpdateUserCommand updateUserCommand)
        {
            this.context = context;
            this.userCommand = userCommand;
            this.updateUserCommand = updateUserCommand; 
        }

        #endregion

        [HttpGet("users")]
        public async Task<ActionResult<List<UserInputModel>>> GetUsers()
        {
            return Ok(await context.Users.ToListAsync());
        }

        [HttpPost("createUser")]
        public async Task<ActionResult> CreateUser(UserInputModel userInputModel)
        {
            try
            {
                userInputModel.Id = Guid.NewGuid();
                await userCommand.Execute(userInputModel);
            }
            catch (Exception exeption)
            {

                throw new Exception(exeption.Message);
            }

            return Ok(await context.Users.ToListAsync());
        }



        [HttpPut("updateUser")]
        public async Task<ActionResult> UpdateUser(UserInputModel userInputModel)
        {

            try
            {
                await updateUserCommand.Execute(userInputModel);
            }
            catch (Exception exeption)
            {
                throw new Exception(exeption.Message);
            }


            return Ok(await context.Users.ToListAsync());
        }


      

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUser(Guid id)
        {
            var dbUser = await context.Users.FindAsync(id);
            if (dbUser == null)
                return NotFound("User not found");

            context.Users.Remove(dbUser);
            await context.SaveChangesAsync();

            return Ok(await context.Users.ToListAsync());
        }

       
    }
}
