using News_Manager.Models;
using System.ComponentModel.DataAnnotations;

namespace News_Manager.ViewModels;
public class NewsEditViewModel : NewsCreateViewModel {
    public int Id { get; set; }
    public bool IsPublished { get; set; }
}