using Microsoft.AspNetCore.Mvc;
using News_Manager.Services;
using News_Manager.ViewModels;

namespace News_Manager.Controllers;

public class NewsController : Controller {
    private readonly INewsService _service;

    public NewsController(INewsService service) {
        _service = service;
    }

    // GET: /News
    public IActionResult Index(string search) {
        var news = _service.ListarTodas(search);
        ViewData["Search"] = search;
        return View(news);
    }

    // GET: /News/Details/5
    public IActionResult Details(int id) {
        var news = _service.ObterPorId(id);
        if (news == null) return NotFound();
        return View(news);
    }

    // GET: /News/Create
    public IActionResult Create() {
        return View(new NewsCreateViewModel());
    }

    // POST: /News/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(NewsCreateViewModel model) {
        if (!ModelState.IsValid) return View(model);

        _service.Criar(model);
        return RedirectToAction(nameof(Index));
    }

    // GET: /News/Edit/5
    public IActionResult Edit(int id) {
        var news = _service.ObterPorId(id);
        if (news == null) return NotFound();

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
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(int id, NewsEditViewModel model) {
        if (id != model.Id) return BadRequest();

        if (!ModelState.IsValid) return View(model);

        _service.Atualizar(model);
        return RedirectToAction(nameof(Index));
    }

    // GET: /News/Delete/5
    public IActionResult Delete(int id) {
        var news = _service.ObterPorId(id);
        if (news == null) return NotFound();
        return View(news);
    }

    // POST: /News/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmed(int id) {
        _service.Excluir(id);
        return RedirectToAction(nameof(Index));
    }
}