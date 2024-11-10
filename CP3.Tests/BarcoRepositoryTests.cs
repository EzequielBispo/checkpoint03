using CP3.Data.AppData;
using CP3.Data.Repositories;
using CP3.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CP3.Tests
{
    public class BarcoRepositoryTests
    {
        private readonly DbContextOptions<ApplicationContext> _options;
        private readonly ApplicationContext _context;
        private readonly BarcoRepository _barcoRepository;

        public BarcoRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase(databaseName: "TestDb")
                .Options;

            _context = new ApplicationContext(options);
            _barcoRepository = new BarcoRepository(_context);
        }

        [Fact]
        public void Adicionar_DeveAdicionarBarcoERetornarBarcoEntity()
        {
            // Arrange
            var barco = new BarcoEntity
            {
                Nome = "Barco Teste",
                Modelo = "Modelo X",
                Ano = 2022,
                Tamanho = 30.0
            };

            // Act
            var resultado = _barcoRepository.Adicionar(barco);

            // Assert
            var barcoNoDb = _context.Barco.FirstOrDefault(b => b.Id == resultado.Id);
            Assert.NotNull(barcoNoDb);
            Assert.Equal(barco.Nome, barcoNoDb.Nome);
            Assert.Equal(barco.Modelo, barcoNoDb.Modelo);
            Assert.Equal(barco.Ano, barcoNoDb.Ano);
            Assert.Equal(barco.Tamanho, barcoNoDb.Tamanho);
        }
    }
}
    }
}
