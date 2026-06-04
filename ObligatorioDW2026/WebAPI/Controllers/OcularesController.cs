using Aplicacion.CasosDeUso;
using CasosUso.DTOs;
using CasosUso.InterfacesCU;
using Excepciones;
using Excepciones.ExcepcionesPropias;
using Microsoft.AspNetCore.Authorization;
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

        /// <summary>
        /// Alta de ocular
        /// </summary>
        /// <remarks>
        /// Permite crear un nuevo ocular.
        /// </remarks>
        /// <param name="nuevo">Objeto DTO que contiene la información del nuevo ocular.</param>
        [ProducesResponseType(typeof(OcularDTO), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        // POST api/<OcularesController>
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult CrearOcular([FromBody] OcularDTO nuevo)
        {
            try
            {
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

        /// <summary>
        /// Modificación de ocular
        /// </summary>
        /// <remarks>
        /// Permite modificar los datos de un ocular existente.
        /// </remarks>
        /// <param name="id">Identificador del ocular a modificar.</param>
        /// <param name="aModificar">Objeto DTO con los datos actualizados del ocular.</param>
        [ProducesResponseType(typeof(OcularDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        // PUT api/<OcularesController>/5
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public IActionResult Modificar(int? id, [FromBody] OcularDTO? aModificar)
        {
            try
            {
                if (id <= 0) return BadRequest("El id debe ser mayor a cero.");
                if (id == null) return BadRequest("No se proporciona el id del ocular a modificar");
                if (aModificar == null) return BadRequest("No se proporcionan datos para la modificación");
                if (aModificar.Id != id) return BadRequest("No coinciden los id del ocular");

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
