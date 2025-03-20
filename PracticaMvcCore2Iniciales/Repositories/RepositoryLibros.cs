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

        #endregion

        #region Usuarios
        public async Task<List<Usuario>> GetUsuariosAsync()
        {
            return await context.Usuarios.ToListAsync();
        }

        public async Task<Usuario?> FindUsuarioAsync(int idusuario)
        {
            return await this.context.Usuarios.FirstOrDefaultAsync(c => c.IdUsuario == idusuario);
        }

        public async Task<List<VistaPedidos>> GetComprasByUsuarioAsync(int idusuario)
        {
            return await this.context.VistasPedidos.Where(c => c.IdUsuario == idusuario).ToListAsync();
        }

        #endregion

        #region Login
        public async Task<Usuario> LogInAsync(string email, string password)
        {
            return await context.Usuarios
                .FirstOrDefaultAsync(u => u.Email == email && u.Pass == password);
        }

        public async Task<Usuario> FindUsuarioByIdAsync(int id)
        {
            return await context.Usuarios
                .FirstOrDefaultAsync(u => u.IdUsuario == id);
        }
        #endregion

        #region Pedidos
        public async Task<int> GetUltimoIdPedido()
        {
            var ultimoId = await this.context.Pedidos
                                            .MaxAsync(p => (int?)p.IdPedido);

            return ultimoId ?? 1;
        }

        public async Task<int> GetUltimaFactura()
        {
            var ultimaFactura = await this.context.Pedidos
                                            .MaxAsync(f => (int?)f.IdFactura);

            return ultimaFactura ?? 1;
        }
        //GUARDAR LA Compra DEL Usuario
        public async Task ComprarProducto(Pedido pedido)
        {
            context.Pedidos.Add(pedido);
            await context.SaveChangesAsync();
        }
        //MOSTRAR LA Compra DEL Usuario
        public async Task<List<VistaPedidos>> GetPedidosUsuario(int iduser)
        {

            return context.VistasPedidos.Where(c => c.IdUsuario == iduser).ToList();
        }
        public async Task<List<Libro>> GetCarritoAsync(List<int> carrito)
        {
            if (carrito == null || carrito.Count == 0)
            {
                return new List<Libro>();
            }

            return await context.Libros
                .Where(c => carrito.Contains(c.IdLibro))
                .ToListAsync();
        }

        #endregion
    }
}
