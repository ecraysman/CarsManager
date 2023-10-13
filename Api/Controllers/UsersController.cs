using Api.Models;
using Microsoft.AspNetCore.Mvc;
using static ORM.Users;

namespace Api.Controllers
{
    [Route("/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ILogger<UsersController> _logger;
        private ORM.MicroORM _microORM;

        public UsersController(ILogger<UsersController> logger)
        {
            _logger = logger;
            _microORM = new ORM.MicroORM();
        }

        // GET api/<Users>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var model = _microORM.EzeORM.SingleOrDefault<UsersDBModel>(id);

            if (model == null)
            {
                return NotFound();
            }

            return Ok(model.Sanitize());
        }

        // POST api/<Users>
        [HttpPost]
        public IActionResult Post([FromBody] UsersModel value)
        {
            try
            {
                //Validate we have all fields for creation
                UsersDBModel new_value = new();
                if (new_value.ValidateForCreation(value.UserName, value.UserKey, value.ApiKey))
                {
                    new_value.UpdateLastLogin();
                    _microORM.EzeORM.Save(new_value);
                    return Ok(new_value.Sanitize());
                }
                else return BadRequest();

            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // PUT api/<Users>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] UsersModel value)
        {
            var model = _microORM.EzeORM.SingleOrDefault<UsersDBModel>(id);

            if (model == null)
            {
                return NotFound();
            }

            bool DoSave = false;

            //Only allow to modify the:
            //Password
            //ApiKey
            //Request a new refreshToken
            if (value.ApiKey != null && value.ApiKey != model.ApiKey)
            {
                model.ApiKey = value.ApiKey;
                DoSave = true;
            }

            if (!string.IsNullOrEmpty(value.UserKey))
            {
                model.UserKey = value.UserKey;
                DoSave = true;
            }

            if (!string.IsNullOrEmpty(value.RefreshToken) && value.RefreshToken == "NEW")
            {
                model.RefreshToken = Guid.NewGuid().ToString();
                DoSave = true;
            }
            if (DoSave)
            {
                model.Creation = DateTime.Now.ToString("yyyy-MM-dd");
                model.UpdateLastLogin();
                _microORM.EzeORM.Save(model);
                return Ok(model.Sanitize());
            }
            else
            {
                //Empty because nothing to do...
                return NoContent();
            }
        }

        // DELETE api/<Users>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _microORM.EzeORM.Delete("Users", "Id", id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}