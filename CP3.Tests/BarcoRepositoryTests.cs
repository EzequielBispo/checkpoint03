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

        [Fact]
        public void Editar_DeveAtualizarBarcoERetornarBarcoEntity()
        {
            // Arrange
            var barcoExistente = new BarcoEntity
            {
                Nome = "Barco Teste",
                Modelo = "Modelo X",
                Ano = 2022,
                Tamanho = 30.0
            };

            _context.Barco.Add(barcoExistente);
            _context.SaveChanges();

            barcoExistente.Nome = "Barco Atualizado";
            barcoExistente.Modelo = "Modelo Y";
            barcoExistente.Ano = 2023;
            barcoExistente.Tamanho = 35.0;

            // Act
            var resultado = _barcoRepository.Editar(barcoExistente);

            // Assert
            var barcoNoDb = _context.Barco.FirstOrDefault(b => b.Id == resultado.Id);
            Assert.NotNull(barcoNoDb);
            Assert.Equal(barcoExistente.Nome, barcoNoDb.Nome);
            Assert.Equal(barcoExistente.Modelo, barcoNoDb.Modelo);
            Assert.Equal(barcoExistente.Ano, barcoNoDb.Ano);
            Assert.Equal(barcoExistente.Tamanho, barcoNoDb.Tamanho);
        }

        [Fact]
        public void ObterPorId_DeveRetornarBarcoQuandoIdExistente()
        {
            // Arrange
            var barcoExistente = new BarcoEntity
            {
                Nome = "Barco Teste",
                Modelo = "Modelo X",
                Ano = 2022,
                Tamanho = 30.0
            };

            _context.Barco.Add(barcoExistente);
            _context.SaveChanges();

            // Act
            var resultado = _barcoRepository.ObterPorId(barcoExistente.Id);

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal(barcoExistente.Id, resultado.Id);
            Assert.Equal(barcoExistente.Nome, resultado.Nome);
            Assert.Equal(barcoExistente.Modelo, resultado.Modelo);
            Assert.Equal(barcoExistente.Ano, resultado.Ano);
            Assert.Equal(barcoExistente.Tamanho, resultado.Tamanho);
        }

    }
}

