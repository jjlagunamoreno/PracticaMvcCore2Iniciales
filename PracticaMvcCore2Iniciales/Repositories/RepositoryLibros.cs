using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PracticaMvcCore2Iniciales.Data;
using PracticaMvcCore2Iniciales.Models;

namespace PracticaMvcCore2Iniciales.Repositories
{
    public class RepositoryLibros
    {
        private LibrosContext context;
        public RepositoryLibros(LibrosContext context)
        {
            this.context = context;
        }
        /********************************************************************************************************/
        #region Libros
        //MOSTRAMOS TODOS LOS LIBROS
        public async Task<List<Libro>> GetLibrosAsync()
        {
            return await this.context.Libros.ToListAsync();
        }
        //MOSTRAMOS LOS DETALLES DE UN LIBRO
        public async Task<Libro> FindLibroAsync(int idlibro)
        {
            return await this.context.Libros.FirstOrDefaultAsync(x => x.IdLibro == idlibro);
        }
        //CARGAMOS LOS GENEROS DE LOS LIBROS
        public async Task<List<Libro>> GetLibrosGeneroAsync(int idgenero)
        {
            return await this.context.Libros.Where(p => p.IdGenero == idgenero).ToListAsync();
        }

        public async Task<List<Genero>> GetAllGenerosAsync()
        {
            return await this.context.Generos.ToListAsync();
        }

        public async Task<Genero> FindGeneroAsync(int idgenero)
        {
            return await this.context.Generos.FirstOrDefaultAsync(x => x.IdGenero == idgenero);
        }

        //GUARDAR LIBROS EN EL CARRITO
        public List<Libro> GetLibrosCarrito(List<int> idLibros)
        {
            return context.Libros.Where(p => idLibros.Contains(p.IdLibro)).ToList();
        }

        // MOSTRAR EL CARRITO


        #endregion

        #region Usuarios

        #endregion

        #region Pedidos

        #endregion
    }
}
