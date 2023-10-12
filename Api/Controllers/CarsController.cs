
using Api.Models;
using Microsoft.AspNetCore.Mvc;
using static ORM.Cars;


namespace Api.Controllers
{
    [Route("/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        private readonly ILogger<CarsController> _logger;
        private ORM.MicroORM _microORM;

        public CarsController(ILogger<CarsController> logger)
        {
            _logger = logger;
            _microORM = new ORM.MicroORM();
        }

        // GET: api/<CarsController>

        [HttpGet("{limit}")]
        //[HttpGet()]
        public IActionResult Get()
        //public IEnumerable<CarsDBModel> Get([FromQuery] OptionalAttribute int Limit = 100)
        {
            var model = _microORM.EzeORM.Fetch<CarsDBModel>("SELECT * FROM Cars LIMIT " + 100);
            if (model.Count == 0)
            {
                return NotFound();
            }

            return Ok(model.ToList());
        }

        // GET api/<CarsController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)

        {
            var model = _microORM.EzeORM.SingleOrDefault<CarsDBModel>(id);

            if (model == null)
            {
                return NotFound();
            }

            return Ok(model);
        }

        // POST api/<CarsController>
        [HttpPost]
        public IActionResult Post([FromBody] CarsModel value)
        {
            
            try
            {   //Transform to db object.
                CarsDBModel new_value = new CarsDBModel(value.Model, value.Mechanic, value.LastOdometer, value.Driver, value.Year, value.Trim.ToString(), value.Maker.ToString(), value.LastMaintenance);
                _microORM.EzeORM.Save(new_value );
                return CreatedAtAction("Created OK", new_value.Id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
                
            }
        }

        // PUT api/<CarsController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] string value)
        {
            //Aca tendriamos que validar que actualizar, y en base a eso vemos
            return Ok();
        }

        // DELETE api/<CarsController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _microORM.EzeORM.Delete(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
