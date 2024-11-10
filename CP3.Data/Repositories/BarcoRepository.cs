using CP3.Data.AppData;
using CP3.Domain.Entities;
using CP3.Domain.Interfaces;

namespace CP3.Data.Repositories
{
    public class BarcoRepository : IBarcoRepository
    {
        private readonly ApplicationContext _context;

        public BarcoRepository(ApplicationContext context)
        {
            _context = context;
        }

        public BarcoEntity? Adicionar(BarcoEntity cliente)
        {
            _context.Barco.Add(cliente);
            _context.SaveChanges();
            return cliente;
        }

        public BarcoEntity? Editar(BarcoEntity cliente)
        {
            _context.Barco.Update(cliente);
            _context.SaveChanges();
            return cliente;
        }

        public BarcoEntity? ObterPorId(int id)
        {
            return _context.Barco.Find(id);
        }

        public IEnumerable<BarcoEntity>? ObterTodos()
        {
            return _context.Barco.ToList();
        }

        public BarcoEntity? Remover(int id)
        {
            throw new NotImplementedException();
        }
    }
}
