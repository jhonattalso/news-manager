using News_Manager.Data;
using News_Manager.Models;

namespace News_Manager.Repositories;

public class NewsRepository : INewsRepository {
    private readonly NewsDbContext _context;

    public NewsRepository(NewsDbContext context) {
        _context = context;
    }

    public IEnumerable<News> GetAll() {
        return _context.News.ToList();
    }

    public News GetById(int id) {
        return _context.News.Find(id)!;
    }

    public void Add(News news) {
        _context.News.Add(news);
        _context.SaveChanges();
    }

    public void Update(News news) {
        _context.News.Update(news);
        _context.SaveChanges();
    }

    public void Remove(int id) {
        var news = GetById(id);
        if (news != null) {
            _context.News.Remove(news);
            _context.SaveChanges();
        }
    }

    public IEnumerable<News> Search(string query) {
        if (string.IsNullOrEmpty(query)) return GetAll();

        return _context.News
            .Where(n => n.Title.Contains(query) || n.Author.Contains(query))
            .ToList();
    }
}