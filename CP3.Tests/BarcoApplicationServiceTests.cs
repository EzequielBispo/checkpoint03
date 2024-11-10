using CP3.Application.Services;
using CP3.Domain.Entities;
using CP3.Domain.Interfaces;
using CP3.Domain.Interfaces.Dtos;
using Moq;

namespace CP3.Tests
{
    public class BarcoApplicationServiceTests
    {
        private readonly Mock<IBarcoRepository> _repositoryMock;
        private readonly Mock<IBarcoApplicationService> _barcoServiceMock;
        private readonly BarcoApplicationService _barcoService;

        public BarcoApplicationServiceTests()
        {
            _repositoryMock = new Mock<IBarcoRepository>();
            _barcoServiceMock = new Mock<IBarcoApplicationService>();
            _barcoService = new BarcoApplicationService(_repositoryMock.Object);

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
            barcoDtoMock.Verify(b => b.Validate(), Times.Once); // Verifica se o método Validate foi chamado
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
    }
}