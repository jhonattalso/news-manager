using News_Manager.Models;

namespace News_Manager.Repositories;

public interface INewsRepository
{
    IEnumerable<News> GetAll();
    News GetById(int id);
    void Add(News news);
    void Update(News news);
    void Remove(int id);
    IEnumerable<News> Search(string query);
}
