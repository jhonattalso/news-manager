using News_Manager.Models;
using News_Manager.Repositories;
using News_Manager.ViewModels;

namespace News_Manager.Services;

public class NewsService : INewsService {
    private readonly INewsRepository _repo;

    public NewsService(INewsRepository repo) {
        _repo = repo;
    }

    public IEnumerable<News> ListarTodas(string busca) {
        return _repo.Search(busca);
    }

    public News ObterPorId(int id) {
        return _repo.GetById(id);
    }

    public void Criar(NewsCreateViewModel model) {
        var news = new News {
            Title = model.Title,
            Author = model.Author,
            PublishDate = model.PublishDate,
            Category = model.Category,
            Content = model.Content,
            IsPublished = false
        };
        _repo.Add(news);
    }

    public void Atualizar(NewsEditViewModel model) {
        var news = _repo.GetById(model.Id);
        if (news != null) {
            news.Title = model.Title;
            news.Author = model.Author;
            news.Category = model.Category;
            news.Content = model.Content;
            news.IsPublished = model.IsPublished;
            _repo.Update(news);
        }
    }

    public void Excluir(int id) {
        _repo.Remove(id);
    }
}