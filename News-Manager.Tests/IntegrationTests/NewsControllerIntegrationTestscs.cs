using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using Xunit;

namespace News_Manager.Tests.IntegrationTests;

// IClassFixture serve para subir o servidor uma única vez para todos os testes desta classe
public class NewsControllerIntegrationTests : IClassFixture<WebApplicationFactory<Program>> {
    private readonly HttpClient _client;

    public NewsControllerIntegrationTests(WebApplicationFactory<Program> factory) {
        // Cria um cliente HTTP que sabe como "conversar" com o servidor em memória
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task Index_AcessarPaginaInicial_RetornaSucessoComHtml() {
        // Arrange (Organizar)
        var url = "/lista"; // Uma das rotas definidas no seu NewsController

        // Act (Agir)
        var response = await _client.GetAsync(url);

        // Assert (Afirmar)
        // Verifica se o status code é 200-299 (Sucesso) 
        response.EnsureSuccessStatusCode();
        Assert.Equal("text/html; charset=utf-8", response.Content.Headers.ContentType!.ToString());
    }

    [Fact]
    public async Task Details_IdInexistente_RetornaNotFound() {
        // Arrange
        var url = "/detalhes/99999"; // ID que provavelmente não existe

        // Act
        var response = await _client.GetAsync(url);

        // Assert
        // Valida o tratamento de erro conforme solicitado no PDF 
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }
}