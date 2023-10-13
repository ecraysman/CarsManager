
using Api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PetaPoco;
using System.Reflection;
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
        [HttpGet()]
        public  IActionResult Get()
   
        {
            int Limit = 100;
            if(Request.Query.ContainsKey("limit"))
            {
                Limit = Convert.ToInt32(Request.Query["limit"]);
            }
            var model = _microORM.EzeORM.Fetch<CarsDBModel>("SELECT * FROM Cars LIMIT " + Limit);
            if (model.Count == 0)
            {
                return NotFound();
            }
            //ToDo: Transform the results to show the date of the last maintenance only
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

                DateOnly internalLastMaintenance; 
          

                if (!DateOnly.TryParse(value.LastMaintenance, out internalLastMaintenance)) internalLastMaintenance = DateOnly.FromDateTime(DateTime.Now);
             
                //Validations that are not "Already there" ?

                CarsDBModel new_value = new CarsDBModel(value.Model, value.Mechanic, value.LastOdometer, value.Driver, value.Year, value.Trim.ToString(), value.Maker.ToString(), internalLastMaintenance.Year.ToString() + "-" + internalLastMaintenance.Month.ToString() + "-" + internalLastMaintenance.Day.ToString());
                _microORM.EzeORM.Save(new_value );
                return Ok(new_value);
                
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
                
            }
        }

        // PUT api/<CarsController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] CarsModel value)
        {
            //Aca tendriamos que validar que actualizar, y en base a eso vemos

            var model = _microORM.EzeORM.SingleOrDefault<CarsDBModel>(id);

            if (model == null)
            {
                return NotFound();
            }

            //Avoid editing fields that are not "requested"

            foreach(PropertyInfo prop in model.GetType().GetProperties())
            {
                if (prop.Name != "Id" && prop.Name != "LastMaintenance" && prop.Name != "LastOdometer" && prop.Name != "Year")
                {
                    if (prop.GetValue(model) != null)
                    {
                        if (prop.GetValue(model).ToString() != prop.GetValue(value).ToString())
                        {
                            prop.SetValue(model, prop.GetValue(value));
                        }
                    }
                }

                else
                {
                    switch (prop.Name)
                    {
                        case "LastOdometer":
                            if (prop.GetValue(model) != null)
                            {
                                if (Convert.ToInt64(prop.GetValue(model)) != value.LastOdometer)
                                {
                                    prop.SetValue(model, value.LastOdometer);
                                }
                            }
                            break;
                        case "Year":
                            if (prop.GetValue(model) != null)
                            {
                                if (Convert.ToInt32(prop.GetValue(model)) != value.Year)
                                {
                                    prop.SetValue(model, value.Year);
                                }
                            }
                            break;

                        case "LastMaintenance":
                            if (prop.GetValue(model) != null)
                            {
                                if (prop.GetValue(model).ToString() != value.LastMaintenance)
                                {
                                    DateOnly internalLastMaintenance;


                                    if (!DateOnly.TryParse(value.LastMaintenance, out internalLastMaintenance)) internalLastMaintenance = DateOnly.FromDateTime(DateTime.Now);
                                    prop.SetValue(model, internalLastMaintenance.Year.ToString() + "-" + internalLastMaintenance.Month.ToString() + "-" + internalLastMaintenance.Day.ToString());
                                }
                            }
                            break;
                        default: //DoNothing
                            break;
                    }
                    


                }
            }
            _microORM.EzeORM.Save(model);
           

            return Ok(model);

      
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
