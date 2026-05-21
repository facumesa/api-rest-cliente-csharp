using Aplicacion.CasosDeUso;
using CasosUso.DTOs;
using CasosUso.InterfacesCU;
using Excepciones;
using Excepciones.ExcepcionesPropias;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OcularesController : ControllerBase
    {
        public IAltaOcular CUAltaOcular { get; set; }
        public IEditarOcular CUEditarOcular { get; set; }

        public OcularesController(IAltaOcular cUAltaOcular, IEditarOcular cUEditarOcular)
        {
            CUAltaOcular = cUAltaOcular;
            CUEditarOcular = cUEditarOcular;
        }

        // POST api/<OcularesController>
        [HttpPost]
        public IActionResult CrearOcular([FromBody] OcularDTO nuevo)
        {
            // if (!UsuarioEsAdmin(HttpContext)) return Unauthorized();
            try
            {
                //if (!ModelState.IsValid) return BadRequest(ModelState);
                if (nuevo == null) return BadRequest("No se proporcionan datos para el alta");
                if (nuevo.Id != 0) return BadRequest("No se debe proporcionar id para el alta");

                CUAltaOcular.Ejecutar(nuevo);

                return CreatedAtRoute("ObtenerEquipoPorId", new { id = nuevo.Id }, nuevo);
            }
            catch (DatosInvalidosException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Ocurrió un problema y no se pudo realizar el alta.");
            }
        }

        // PUT api/<OcularesController>/5
        [HttpPut("{id}")]
        public IActionResult Modificar(int? id, [FromBody] OcularDTO? aModificar)
        {
            try
            {
                if (id == null) return BadRequest("No se proporciona el id del tema a modificar");
                if (aModificar == null) return BadRequest("No se proporcionan datos para la modificación");
                if (aModificar.Id != id) return BadRequest("No coinciden los id del tema");

                CUEditarOcular.Ejecutar(aModificar);
                return Ok(aModificar);
            }
            catch (DatosInvalidosException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (OperacionInvalidaException ex)
            {
                return NotFound(ex.Message);
            }
            catch
            {
                return StatusCode(500, "Ocurrió un problema y no se pudo realizar la modificación.");
            }
        }

    }
}
