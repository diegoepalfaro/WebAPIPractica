using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPIPractica.Models;
using Microsoft.EntityFrameworkCore;

namespace WebAPIPractica.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class equiposController : ControllerBase
    {
        private readonly equiposContext _equiposContexto;
        public equiposController(equiposContext equiposContexto)
        {
            _equiposContexto = equiposContexto;
        }
       
        ///<sumary>
        ///Endpoint que retorna el listado de todos los equipos existentes
        ///</sumary>
        ///<returns></returns>
        [HttpGet]
        [Route("GetAll")]

        public IActionResult Get()
        {

            List<equipos> listadoEquipo = (from e in _equiposContexto.equipos select e).ToList();
            if (listadoEquipo.Count() == 0)
            {
                return NotFound();
            }
            return Ok(listadoEquipo);
        }
    }
}
