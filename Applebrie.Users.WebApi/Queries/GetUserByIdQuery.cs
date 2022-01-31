using Applebrie.Domain;
using Applebrie.Users.WebApi.Queries.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace Applebrie.Users.WebApi.Queries
{
    public class GetUserByIdQuery
    {
        #region Fields
        private readonly MapperConfiguration mapperConfiguration;
        private readonly IRepository<User> userRepository;

        #endregion Fields

        #region Construcotor
        public GetUserByIdQuery(IRepository<User> userRepository, MapperConfiguration mapperConfiguration)
        {
            this.userRepository = userRepository;
            this.mapperConfiguration = mapperConfiguration;
        }

        #endregion Constructor

        #region Methods

        /// <summary>
        /// Возвращает клиента по Id
        /// </summary>
        public async Task<UserModel> Execute(Guid id)
        {
            return await userRepository.Where(c => c.Id == id).ProjectTo<UserModel>(mapperConfiguration).FirstOrDefaultAsync();
        }

        #endregion Methods
    }
}
