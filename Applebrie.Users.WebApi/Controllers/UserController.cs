using Applebrie.Domain;
using Applebrie.Users.WebApi.Commands.Users;
using Applebrie.Users.WebApi.Queries;
using Applebrie.Users.WebApi.Queries.Models;
using Applebrie.Users.WebApi.Query.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Applebrie.Users.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        #region Fields
        private readonly AppDbContext context;
        private readonly IUserCommand userCommand;
        private readonly UpdateUserCommand updateUserCommand;
        private readonly GetUserByIdQuery getUserByIdQuery;

        #endregion

        #region Constructor
        public UserController(AppDbContext context, 
            IUserCommand userCommand, 
            UpdateUserCommand updateUserCommand,
            GetUserByIdQuery getUserByIdQuery)
        {
            this.context = context;
            this.userCommand = userCommand;
            this.updateUserCommand = updateUserCommand;
            this.getUserByIdQuery = getUserByIdQuery;
        }

        #endregion

        #region Methods
        //GetAllUsers
        [HttpGet("allUsers")]
        public async Task<ActionResult<List<UserInputModel>>> GetUsers()
        {
            return Ok(await context.Users.ToListAsync());
        }

        //Get User By Id
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserModel))]
        public async Task<ActionResult> GetUserById(string id)
        {
            var currentCustomer = await getUserByIdQuery.Execute(Guid.Parse(id));
             return Ok(currentCustomer);
        }

        //Create new user
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

        // Update User
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

        // Delete User by Id
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

        #endregion
    }
}
