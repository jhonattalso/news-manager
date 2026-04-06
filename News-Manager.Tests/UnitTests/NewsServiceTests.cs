using Moq;
using News_Manager.Models;
using News_Manager.Repositories;
using News_Manager.Services;
using News_Manager.ViewModels;
using Xunit;

namespace News_Manager.Tests.UnitTests;

public class NewsServiceTests {
    private readonly Mock<INewsRepository> _repositoryMock;
    private readonly NewsService _newsService;

    public NewsServiceTests() {
        // Arrange (Geral) - Criando o "falso" repositório
        _repositoryMock = new Mock<INewsRepository>();
        _newsService = new NewsService(_repositoryMock.Object);
    }

    [Fact]
    public void ObterPorId_IdExistente_RetornaNoticiaEsperada() {
        // Arrange
        var noticiaEsperada = new News { Id = 1, Title = "Notícia Teste" };
        _repositoryMock.Setup(repo => repo.GetById(1)).Returns(noticiaEsperada);

        // Act
        var resultado = _newsService.ObterPorId(1);

        // Assert
        Assert.NotNull(resultado);
        Assert.Equal("Notícia Teste", resultado.Title);
        _repositoryMock.Verify(repo => repo.GetById(1), Times.Once);
    }

    [Fact]
    public void ObterPorId_IdInexistente_RetornaNulo() {
        // Arrange
        _repositoryMock.Setup(repo => repo.GetById(99)).Returns((News)null!);

        // Act
        var resultado = _newsService.ObterPorId(99);

        // Assert
        Assert.Null(resultado);
    }

    [Fact]
    public void Criar_ModeloValido_ChamaMetodoAddNoRepositorio() {
        // Arrange
        var model = new NewsCreateViewModel {
            Title = "Nova Notícia",
            Author = "Autor Teste",
            Content = "Conteúdo longo aqui..."
        };

        // Act
        _newsService.Criar(model);

        // Assert
        // Verifica se o método Add do repositório foi chamado com QUALQUER objeto do tipo News
        _repositoryMock.Verify(repo => repo.Add(It.IsAny<News>()), Times.Once);
    }
}