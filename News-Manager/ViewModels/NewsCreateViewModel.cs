using System.ComponentModel.DataAnnotations;
using News_Manager.Models;

namespace News_Manager.ViewModels;

public class NewsCreateViewModel {
    [Required(ErrorMessage = "O título é obrigatório")]
    [StringLength(150, ErrorMessage = "O título não pode exceder 150 caracteres")]
    public string Title { get; set; } = string.Empty;

    [Required(ErrorMessage = "O autor é obrigatório")]
    public string Author { get; set; } = string.Empty;

    [DataType(DataType.Date)]
    public DateTime PublishDate { get; set; } = DateTime.Today;

    [Required(ErrorMessage = "Selecione uma categoria")]
    public Category Category { get; set; }

    [Required(ErrorMessage = "O conteúdo não pode estar vazio")]
    public string Content { get; set; } = string.Empty;
}