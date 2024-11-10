using CP3.Application.Services;
using CP3.Data.AppData;
using CP3.Data.Repositories;
using CP3.Domain.Entities;
using CP3.Domain.Interfaces;
using CP3.Domain.Interfaces.Dtos;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace CP3.Tests
{
    public class BarcoApplicationServiceTests
    {
        private readonly Mock<IBarcoRepository> _repositoryMock;
        private readonly Mock<IBarcoApplicationService> _barcoServiceMock;
        private readonly ApplicationContext _context;
        private readonly BarcoRepository _repository;
        private readonly BarcoApplicationService _barcoService;

        public BarcoApplicationServiceTests()
        {
            _repositoryMock = new Mock<IBarcoRepository>();
            _barcoServiceMock = new Mock<IBarcoApplicationService>();
            _barcoService = new BarcoApplicationService(_repositoryMock.Object);

            var options = new DbContextOptionsBuilder<ApplicationContext>()
            .UseInMemoryDatabase(databaseName: "TesteDb")
            .Options;

            _context = new ApplicationContext(options);
            _repository = new BarcoRepository(_context);

        }

        [Fact]
        public void AdicionarBarco_DeveRetornarBarcoEntity_QuandoAdicionarComSucesso()
        {
            // Arrange
            var barcoDtoMock = new Mock<IBarcoDto>();
            barcoDtoMock.Setup(b => b.Id).Returns(1);
            barcoDtoMock.Setup(b => b.Nome).Returns("Barco Teste");
            barcoDtoMock.Setup(b => b.Modelo).Returns("Modelo A");
            barcoDtoMock.Setup(b => b.Ano).Returns(2022);
            barcoDtoMock.Setup(b => b.Tamanho).Returns(30.5);
            barcoDtoMock.Setup(b => b.Validate());

            var barcoEsperado = new BarcoEntity
            {
                Id = 1,
                Nome = "Barco Teste",
                Modelo = "Modelo A",
                Ano = 2022,
                Tamanho = 30.5
            };

            _repositoryMock.Setup(r => r.Adicionar(It.IsAny<BarcoEntity>())).Returns(barcoEsperado);

            // Act
            var resultado = _barcoService.AdicionarBarco(barcoEsperado);

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal(barcoEsperado.Id, resultado.Id);
            Assert.Equal(barcoEsperado.Nome, resultado.Nome);
            Assert.Equal(barcoEsperado.Modelo, resultado.Modelo);
            Assert.Equal(barcoEsperado.Ano, resultado.Ano);
            Assert.Equal(barcoEsperado.Tamanho, resultado.Tamanho);
            barcoDtoMock.Verify(b => b.Validate(), Times.Once);
        }

        [Fact]
        public void EditarBarco_DeveRetornarBarcoEntity_QuandoEditarComSucesso()
        {
            // Arrange
            var barcoParaEditar = new BarcoEntity
            {
                Id = 1,
                Nome = "Barco Original",
                Modelo = "Modelo X",
                Ano = 2021,
                Tamanho = 25.0
            };

            var barcoEditado = new BarcoEntity
            {
                Id = 1,
                Nome = "Barco Editado",
                Modelo = "Modelo Y",
                Ano = 2022,
                Tamanho = 30.5
            };

            _repositoryMock.Setup(r => r.Editar(It.IsAny<BarcoEntity>())).Returns(barcoEditado);

            // Act
            var resultado = _barcoService.EditarBarco(barcoParaEditar);

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal(barcoEditado.Id, resultado.Id);
            Assert.Equal(barcoEditado.Nome, resultado.Nome);
            Assert.Equal(barcoEditado.Modelo, resultado.Modelo);
            Assert.Equal(barcoEditado.Ano, resultado.Ano);
            Assert.Equal(barcoEditado.Tamanho, resultado.Tamanho);
            _repositoryMock.Verify(r => r.Editar(barcoParaEditar), Times.Once);
        }

        [Fact]
        public void ObterBarcoPorId_DeveRetornarBarcoEntity_QuandoIdExistente()
        {
            // Arrange
            int barcoId = 1;
            var barcoEsperado = new BarcoEntity
            {
                Id = barcoId,
                Nome = "Barco Exemplo",
                Modelo = "Modelo Z",
                Ano = 2020,
                Tamanho = 27.5
            };

            _repositoryMock.Setup(r => r.ObterPorId(barcoId)).Returns(barcoEsperado);

            // Act
            var resultado = _barcoService.ObterBarcoPorId(barcoId);

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal(barcoEsperado.Id, resultado.Id);
            Assert.Equal(barcoEsperado.Nome, resultado.Nome);
            Assert.Equal(barcoEsperado.Modelo, resultado.Modelo);
            Assert.Equal(barcoEsperado.Ano, resultado.Ano);
            Assert.Equal(barcoEsperado.Tamanho, resultado.Tamanho);
            _repositoryMock.Verify(r => r.ObterPorId(barcoId), Times.Once);
        }

        [Fact]
        public void ObterTodosBarcos_DeveRetornarListaDeBarcoEntities_QuandoExistemBarcos()
        {
            // Arrange
            var listaDeBarcos = new List<BarcoEntity>
            {
                new BarcoEntity { Id = 1, Nome = "Barco 1", Modelo = "Modelo A", Ano = 2021, Tamanho = 25.5 },
                new BarcoEntity { Id = 2, Nome = "Barco 2", Modelo = "Modelo B", Ano = 2020, Tamanho = 30.0 }
            };

            _repositoryMock.Setup(r => r.ObterTodos()).Returns(listaDeBarcos);

            // Act
            var resultado = _barcoService.ObterTodosBarcos();

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal(2, resultado.Count());
            Assert.Contains(resultado, b => b.Nome == "Barco 1");
            Assert.Contains(resultado, b => b.Nome == "Barco 2");
            _repositoryMock.Verify(r => r.ObterTodos(), Times.Once);
        }

        [Fact]
        public void RemoverBarco_DeveRetornarBarcoEntity_QuandoRemoverComSucesso()
        {
            // Arrange
            int barcoId = 1;
            var barcoParaRemover = new BarcoEntity
            {
                Id = barcoId,
                Nome = "Barco Removido",
                Modelo = "Modelo X",
                Ano = 2020,
                Tamanho = 25.0
            };

            _repositoryMock.Setup(r => r.Remover(barcoId)).Returns(barcoParaRemover);

            // Act
            var resultado = _barcoService.RemoverBarco(barcoId);

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal(barcoParaRemover.Id, resultado.Id);
            Assert.Equal(barcoParaRemover.Nome, resultado.Nome);
            Assert.Equal(barcoParaRemover.Modelo, resultado.Modelo);
            Assert.Equal(barcoParaRemover.Ano, resultado.Ano);
            Assert.Equal(barcoParaRemover.Tamanho, resultado.Tamanho);
            _repositoryMock.Verify(r => r.Remover(barcoId), Times.Once);
        }

        [Fact]
        public void ObterTodos_DeveRetornarListaDeBarcosQuandoExistemBarcosNoBanco()
        {
            // Arrange
            var barco1 = new BarcoEntity
            {
                Nome = "Barco Teste 1",
                Modelo = "Modelo A",
                Ano = 2022,
                Tamanho = 30.0
            };

            var barco2 = new BarcoEntity
            {
                Nome = "Barco Teste 2",
                Modelo = "Modelo B",
                Ano = 2023,
                Tamanho = 35.0
            };

            _context.Barco.Add(barco1);
            _context.Barco.Add(barco2);
            _context.SaveChanges();

            // Act
            var resultado = _repository.ObterTodos();

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal(2, resultado.Count());
            Assert.Contains(resultado, b => b.Nome == barco1.Nome);
            Assert.Contains(resultado, b => b.Nome == barco2.Nome);
        }

        [Fact]
        public void Remover_DeveRemoverBarcoQuandoIdExistente()
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
            var resultado = _repository.Remover(barcoExistente.Id);

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal(barcoExistente.Id, resultado.Id);

            var barcoNoDb = _context.Barco.FirstOrDefault(b => b.Id == barcoExistente.Id);
            Assert.Null(barcoNoDb);
        }

    }
}