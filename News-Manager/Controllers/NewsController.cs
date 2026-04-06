using Microsoft.AspNetCore.Mvc;
using News_Manager.Services;
using News_Manager.ViewModels;

namespace News_Manager.Controllers;

public class NewsController : Controller {
    private readonly INewsService _service;
    private readonly ILogger<NewsController> _logger;

    public NewsController(INewsService service, ILogger<NewsController> logger) {
        _service = service;
        _logger = logger;
    }

    // GET: /News
    [Route("")]
    [Route("lista")]
    public IActionResult Index(string search) {
        _logger.LogInformation("Listando notícias. Filtro de busca: {Search}", search ?? "Nenhum");
        var news = _service.ListarTodas(search!);
        ViewData["Search"] = search;
        return View(news);
    }

    // GET: /News/Details/5
    [Route("detalhes/{id:int}")]
    public IActionResult Details(int id) {
        _logger.LogInformation("Visualizando detalhes da notícia ID: {Id}", id);
        var news = _service.ObterPorId(id);
        if (news == null) {
            _logger.LogWarning("Notícia com ID {Id} não foi encontrada.", id);
            return NotFound();
        }
        return View(news);
    }

    // GET: /News/Create
    [Route("cadastrar")]
    public IActionResult Create() {
        return View(new NewsCreateViewModel());
    }

    // POST: /News/Create
    [HttpPost("cadastrar")]
    [ValidateAntiForgeryToken]
    public IActionResult Create(NewsCreateViewModel model) {
        if (!ModelState.IsValid) {
            _logger.LogWarning("Tentativa de criação de notícia com dados inválidos.");
            return View(model);
        }

        try {
            _service.Criar(model);
            _logger.LogInformation("Notícia '{Title}' criada com sucesso por {Author}.", model.Title, model.Author);
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex) {
            _logger.LogError(ex, "Erro ao criar notícia: {Message}", ex.Message);
            return View(model);
        }
    }

    // GET: /News/Edit/5
    [Route("editar/{id:int}")]
    public IActionResult Edit(int id) {
        var news = _service.ObterPorId(id);
        if (news == null) {
            _logger.LogWarning("Tentativa de editar notícia inexistente ID: {Id}", id);
            return NotFound();
        }

        var model = new NewsEditViewModel {
            Id = news.Id,
            Title = news.Title,
            Author = news.Author,
            PublishDate = news.PublishDate,
            Category = news.Category,
            Content = news.Content,
            IsPublished = news.IsPublished
        };

        return View(model);
    }

    // POST: /News/Edit/5
    [HttpPost("editar/{id:int}")]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(int id, NewsEditViewModel model) {
        if (id != model.Id) {
            _logger.LogError("Inconsistência de ID na edição: Rota {IdRoute} vs Model {IdModel}", id, model.Id);
            return BadRequest();
        }

        if (!ModelState.IsValid) return View(model);

        try {
            _service.Atualizar(model);
            _logger.LogInformation("Notícia ID {Id} atualizada com sucesso.", id);
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex) {
            _logger.LogError(ex, "Erro ao atualizar notícia ID {Id}.", id);
            return View(model);
        }
    }

    // GET: /News/Delete/5
    [Route("excluir/{id:int}")]
    public IActionResult Delete(int id) {
        var news = _service.ObterPorId(id);
        if (news == null) return NotFound();
        return View(news);
    }

    // POST: /News/Delete/5
    [HttpPost("excluir/{id:int}"), ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmed(int id) {
        try {
            _service.Excluir(id);
            _logger.LogInformation("Notícia ID {Id} excluída permanentemente.", id);
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex) {
            _logger.LogError(ex, "Erro crítico ao excluir notícia ID {Id}.", id);
            return RedirectToAction(nameof(Index));
        }
    }
}