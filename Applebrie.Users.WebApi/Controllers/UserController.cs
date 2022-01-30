using Applebrie.Users.WebApi.Commands.Users;
using Applebrie.Users.WebApi.Entity;
using Applebrie.Users.WebApi.Entity.Models;
using Applebrie.Users.WebApi.Query.Model;
using Applebrie.Users.WebApi.Query.Users;
using Microsoft.AspNetCore.Http;
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
        public UserController(AppDbContext context, IUserCommand userCommand, UpdateUserCommand updateUserCommand )
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
                await userCommand.Execute(userInputModel);
            }
            catch (Exception exeption)
            {

                throw  new Exception(exeption.Message);
            }

            return Ok(await context.Users.ToListAsync());
       }



        [HttpPut("updateUser")]
        public async Task<ActionResult> UpdateUser(UserInputModel  userInputModel)
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

      //  [HttpDelete]

    }
}
