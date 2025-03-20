using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Core.Types;
using PracticaMvcCore2Iniciales.Data;
using PracticaMvcCore2Iniciales.Extensions;
using PracticaMvcCore2Iniciales.Filters;
using PracticaMvcCore2Iniciales.Models;
using PracticaMvcCore2Iniciales.Repositories;

namespace PracticaMvcCore2Iniciales.Controllers
{
    public class LibrosController : Controller
    {
        private RepositoryLibros repo;
        public LibrosController(RepositoryLibros repo)
        {
            this.repo = repo;
        }

        //MOSTRAMOS TODOS LOS LIBROS
        public async Task<IActionResult> Index()
        {
            List<Libro> libros = await this.repo.GetLibrosAsync();
            return View(libros);
        }
        //MOSTRAMOS LOS DETALLES DE UN LIBRO
        public async Task<IActionResult> DetallesLibro(int idlibro)
        {
            Libro libro = await this.repo.FindLibroAsync(idlibro);
            return View(libro);
        }
        //CARGAMOS LOS GENEROS DE LOS LIBROS
        public async Task<IActionResult> LibrosGenero(int idgenero)
        {
            Genero genero = await this.repo.FindGeneroAsync(idgenero);

            ViewData["GENERO"] = genero;

            List<Libro> libros = await this.repo.GetLibrosGeneroAsync(idgenero);
            return View(libros);
        }

        //GUARDAR LIBROS EN EL CARRITO
        public IActionResult GuardarLibroCarrito(int idLibro, int? idGenero)
        {
            if (idLibro != null)
            {
                List<int> carrito;
                if (HttpContext.Session.GetObject<List<int>>("CARRITO") == null)
                {
                    carrito = new List<int>();
                }
                else
                {
                    carrito = HttpContext.Session.GetObject<List<int>>("CARRITO");
                }
                carrito.Add(idLibro);
                HttpContext.Session.SetObject("CARRITO", carrito);
            }
            if (idGenero != null)
            {
                return RedirectToAction("LibrosGenero", "Libros", new { idgenero = idGenero });

            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        // MOSTRAR EL CARRITO
        public async Task<IActionResult> Carrito(int? idEliminar)
        {
            //Le pasamos el carrito
            List<int> carrito = HttpContext.Session.GetObject<List<int>>("CARRITO");

            //Tienes que crear para añadir datos al carrito
            if (carrito == null)
            {
                return View();
            }
            else
            {
                if (idEliminar != null)
                {
                    carrito.Remove(idEliminar.Value);
                    HttpContext.Session.SetObject("CARRITO", carrito);
                }
                List<Libro> libros = await this.repo.GetCarritoAsync(carrito);
                return View(libros);
            }
        }

        [AuthorizeLibros]
        [HttpPost]
        public async Task<IActionResult> Compra(List<int> carrito)
        {
            var idUsuarioClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (idUsuarioClaim == null)
            {
                return RedirectToAction("Login");
            }
            int idUsuario = int.Parse(idUsuarioClaim.Value);

            int idpedido = await repo.GetUltimoIdPedido();
            int idfactura = await repo.GetUltimaFactura() + 1;
            for (int i = 0; i < carrito.Count; i++)
            {
                int idLibro = carrito[i];
                idpedido++;
                Pedido nuevoPedido = new Pedido()
                {
                    IdPedido = idpedido,
                    IdFactura = idfactura,
                    Fecha = DateTime.Now,
                    IdLibro = idLibro,
                    IdUsuario = idUsuario,
                    Cantidad = 1
                };

                await repo.ComprarProducto(nuevoPedido);

            }
            HttpContext.Session.Remove("CARRITO");
            return RedirectToAction("PerfilUsuario", "Managed", new { iduser = idUsuario });
        }

        [AuthorizeLibros]
        public async Task<IActionResult> ComprasUsuario(int iduser)
        {
            List<VistaPedidos> pedidos = await this.repo.GetPedidosUsuario(iduser);

            return View(pedidos);
        }
    }
}
