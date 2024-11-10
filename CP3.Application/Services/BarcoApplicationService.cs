using CP3.Domain.Entities;
using CP3.Domain.Interfaces;
using CP3.Domain.Interfaces.Dtos;

namespace CP3.Application.Services
{
    public class BarcoApplicationService : IBarcoApplicationService
    {
        private readonly IBarcoRepository _repository;

        public BarcoApplicationService(IBarcoRepository repository)
        {
            _repository = repository;
        }

        public BarcoEntity AdicionarBarco(BarcoEntity entity)
        {
            return _repository.Adicionar(entity);
        }

        public BarcoEntity EditarBarco(int id, IBarcoDto entity)
        {
            throw new NotImplementedException();
        }

        public BarcoEntity ObterBarcoPorId(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<BarcoEntity> ObterTodosBarcos()
        {
            throw new NotImplementedException();
        }

        public BarcoEntity RemoverBarco(int id)
        {
            throw new NotImplementedException();
        }
    }
}
