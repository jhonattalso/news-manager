using News_Manager.Models;

namespace News_Manager.Repositories;

public class InMemoryNewsRepository : INewsRepository
{
    private readonly List<News> _newsList;
    private int _nextId = 1;

    public InMemoryNewsRepository()
    {
        _newsList = new List<News>
            {
                new News { Id = _nextId++, Title="Primeira Notícia", Author="Admin", PublishDate = DateTime.Today, Category = Category.Outro, Content="Conteúdo de exemplo", IsPublished=true }
               
            };
    }

    public IEnumerable<News> GetAll() => _newsList.OrderByDescending(n => n.PublishDate);

    public News GetById(int id) => _newsList.FirstOrDefault(n => n.Id == id);

    public void Add(News news)
    {
        news.Id = _nextId++;
        _newsList.Add(news);
    }

    public void Update(News news)
    {
        var existing = GetById(news.Id);
        if (existing == null) return;
        existing.Title = news.Title;
        existing.Author = news.Author;
        existing.PublishDate = news.PublishDate;
        existing.Category = news.Category;
        existing.Content = news.Content;
        existing.IsPublished = news.IsPublished;
    }

    public void Remove(int id)
    {
        var news = GetById(id);
        if (news != null) _newsList.Remove(news);
    }

    public IEnumerable<News> Search(string query)
    {
        if (string.IsNullOrWhiteSpace(query))
            return GetAll();

        query = query.Trim().ToLower();

        return _newsList.Where(n =>
            (n.Title != null && n.Title.ToLower().Contains(query)) ||
            (n.Author != null && n.Author.ToLower().Contains(query)) ||
            (n.Category.ToString().ToLower().Contains(query))
        );
    }


}

