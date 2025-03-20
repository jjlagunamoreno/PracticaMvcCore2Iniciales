using Microsoft.AspNetCore.Mvc;
using PracticaMvcCore2Iniciales.Data;
using PracticaMvcCore2Iniciales.Extensions;
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
        public IActionResult Carrito(int? idLibroEliminar)
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
                if (idLibroEliminar != null)
                {
                    carrito.Remove(idLibroEliminar.Value);
                    HttpContext.Session.SetObject("CARRITO", carrito);
                }
                List<Libro> peliculas = this.repo.GetLibrosCarrito(carrito);
                return View(peliculas);
            }
        }
    }
}
