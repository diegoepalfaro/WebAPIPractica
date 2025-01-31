using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPIPractica.Models;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

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

        ///<sumary>
        ///Endpoint que retorna los registros de una tabla filtrada por su ID
        ///</sumary>
        ///<param name="id"></param>
        ///<returns></returns>

        [HttpGet]
        [Route("GetById/{id}")]
        public IActionResult Get(int id)
        {
            equipos? equipo = (from e in _equiposContexto.equipos
                               where e.id_equipos== id
                               select e).FirstOrDefault();
            if (equipo == null)
            {
                return NotFound();
            }
            return Ok(equipo);
        }
        ///<sumary>
        ///Endpoint que retorna los registros de una tabla filtrada por descripcion
        ///</sumary>
        ///<param name="id"></param>
        ///<returns></returns>

        [HttpGet]
        [Route("Find/{filtro}")]
        public IActionResult FindByDescription(string filtro)
        {
            equipos? equipo = (from e in _equiposContexto.equipos
                               where e.descripcion.Contains(filtro)
                               select e).FirstOrDefault();
            if (equipo == null)
            {
                return NotFound();
            }
            return Ok(equipo);
        }


        [HttpPost]
        [Route("Add")]
        public IActionResult GuardarEquipo([FromBody] equipos equipo)
        {
            try
            {
                _equiposContexto.equipos.Add(equipo);
                _equiposContexto.SaveChanges();
                return Ok(equipo);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("actualizar/{id}")]

        public IActionResult ActualizarEquipo(int id, [FromBody] equipos equipoModificar)
        {
            //Para actualizar un registro primero se accede a el desde la base
            equipos? equipoActual = (from e in _equiposContexto.equipos
                                     where e.id_equipos==id
                                     select e).FirstOrDefault();
            //Se verifica que exista segun su ID
            if(equipoActual == null)
            { return NotFound(); }

            //Si se ecuentra se altera
            equipoActual.nombre = equipoModificar.nombre;
            equipoActual.descripcion = equipoModificar.descripcion;
            equipoActual.marca_id = equipoModificar.marca_id;
            equipoActual.tipo_equipo_id = equipoModificar.tipo_equipo_id;
            equipoActual.anio_compra = equipoModificar.anio_compra;
            equipoActual.costo = equipoModificar.costo;
            
            //Se marca como modificado y se envía
            _equiposContexto.Entry(equipoActual).State=EntityState.Modified;
            _equiposContexto.SaveChanges();
            return Ok(equipoModificar);
        }

        [HttpDelete]
        [Route("eliminar/{id}")]
        public IActionResult EliminarEquipo(int id)
        {
            //Se obtiene el original de la base
            equipos? equipo = (from e in _equiposContexto.equipos
                               where e.id_equipos==id
                               select e).FirstOrDefault();
            //Verificar si existe
            if (equipo == null)  
                return NotFound();

            //Se elimina el registro
            _equiposContexto.equipos.Attach(equipo);
            _equiposContexto.equipos.Remove(equipo);
            _equiposContexto.SaveChanges();
            return Ok(equipo);
        }

    }
}

