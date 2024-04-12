using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using FoodUserAuth.BusinessLogic.Abstractions;
using FoodUserAuth.WebApi.Extensions;
using FoodUserAuth.WebApi.Models;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace FoodUserAuth.WebApi.Controllers
{
    [Authorize(Roles = UserRoleExtension.AdministrationRole)]
    [Route("api/v1/[controller]")]
    [Produces("application/json")]
    public class UsersController : ControllerBase
    {
        private readonly ILogger<UsersController> _logger;
        private readonly IUsersService _usersService;

        public UsersController(ILogger<UsersController> logger,
            IUsersService usersService) 
        {
            _usersService = usersService;
            _logger = logger;
        }

        /// <summary>
        /// Return list of all user
        /// </summary>
        /// <param name="item"></param>
        /// <returns>List of users</returns>
        /// <response code="200">Success</response>
        /// <response code="400">If error</response>
        [HttpGet]
        public ActionResult<IEnumerable<UserModel>> GetAll()
        {
            return Ok(new UserModel[0]);
        }

        /// <summary>
        /// Create a new user in list of users
        /// </summary>
        /// <param name="item">User</param>
        /// <returns>List of users</returns>
        /// <response code="200">Success</response>
        /// <response code="400">If error</response>
        [HttpPut]
        public ActionResult Create(UserCreateModel item)
        {
            return Ok();
        }

        /// <summary>
        /// Return list of all user
        /// </summary>
        /// <param name="item"></param>
        /// <returns>List of users</returns>
        /// <response code="200">Success</response>
        /// <response code="400">If error</response>
        [HttpPost]
        public ActionResult Update(UserModel item)
        {
            return Ok();
        }

        /// <summary>
        /// Return list of all user
        /// </summary>
        /// <param name="item"></param>
        /// <returns>List of users</returns>
        /// <response code="200">Success</response>
        /// <response code="400">If error</response>
        [HttpDelete]
        public ActionResult Delete(UserDeleteModel item)
        {
            return Ok();
        }
    }
}
