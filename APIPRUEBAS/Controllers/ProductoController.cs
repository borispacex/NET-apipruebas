using APIPRUEBAS.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Microsoft.AspNetCore.Cors;

namespace APIPRUEBAS.Controllers
{
    [EnableCors("ReglasCors")]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        public readonly DBAPIContext _dbContext;
        public ProductoController(DBAPIContext dbContext)
        {
            _dbContext = dbContext;
        }

        // Lista de todos los PRODUCTOS
        [HttpGet]
        [Route("Lista")]
        public IActionResult Lista()
        {
            List<Producto> lista = new List<Producto>();
            try
            {
                // lista = _dbContext.Productos.ToList(); // solo producto
                lista = _dbContext.Productos.Include(c => c.oCategoria).ToList(); // incluimos categoria
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", response = lista });
            } catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message, response = lista });

            }
        }
        // Producto por ID
        [HttpGet]
        [Route("Obtener/{idProducto:int}")]
        public IActionResult Obtener(int idProducto)
        {
            Producto oProducto = new Producto();
            oProducto = _dbContext.Productos.Find(idProducto);
            if (oProducto == null) return BadRequest("Producto no encontrado");
            
            try
            {
                oProducto = _dbContext.Productos.Include(c => c.oCategoria).Where(p => p.IdProducto == idProducto).FirstOrDefault();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", response = oProducto });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message, response = oProducto });

            }
        }

        // Guardar Producto
        [HttpPost]
        [Route("Guardar")]
        public IActionResult Guardar([FromBody] Producto objeto)
        {
            try
            {
                _dbContext.Productos.Add(objeto);
                _dbContext.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message });
            }
        }

        // Editar Producto
        [HttpPut]
        [Route("Editar")]
        public IActionResult Editar([FromBody] Producto objeto)
        {
            Producto oProducto = _dbContext.Productos.Find(objeto.IdProducto);
            if (oProducto == null) return BadRequest("Producto no encontrado");
            try
            {
                oProducto.CodigoBarra = objeto.CodigoBarra is null ? oProducto.CodigoBarra : objeto.CodigoBarra;
                oProducto.Descripcion = objeto.Descripcion is null ? oProducto.Descripcion : objeto.Descripcion;
                oProducto.Marca = objeto.Marca is null ? oProducto.Marca : objeto.Marca;
                oProducto.IdCategoria = objeto.IdCategoria is null ? oProducto.IdCategoria : objeto.IdCategoria;
                oProducto.Precio = objeto.Precio is null ? oProducto.Precio : objeto.Precio;

                _dbContext.Productos.Update(oProducto);
                _dbContext.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message });
            }
        }

        // Eliminar producto
        [HttpDelete]
        [Route("Eliminar/{idProducto:int}")]
        public IActionResult Eliminar(int idProducto)
        {
            Producto oProducto = new Producto();
            oProducto = _dbContext.Productos.Find(idProducto);
            if (oProducto == null) return BadRequest("Producto no encontrado");

            try
            {
                _dbContext.Productos.Remove(oProducto);
                _dbContext.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message });

            }
        }
    }
}
