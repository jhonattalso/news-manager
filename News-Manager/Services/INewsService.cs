using News_Manager.Models;
using News_Manager.ViewModels;

namespace News_Manager.Services;

public interface INewsService {
    IEnumerable<News> ListarTodas(string busca);
    News ObterPorId(int id);
    void Criar(NewsCreateViewModel model);
    void Atualizar(NewsEditViewModel model);
    void Excluir(int id);
}