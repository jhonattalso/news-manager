using System.ComponentModel.DataAnnotations;

namespace News_Manager.Models;

public class News
{
    public int Id { get; set; }

    [Required]
    [StringLength(150)]
    public string Title { get; set; } = string.Empty;

    [Required]
    [StringLength(100)]
    public string Author { get; set; } = string.Empty;

    [DataType(DataType.Date)]
    public DateTime PublishDate { get; set; }

    [Required]
    public Category Category { get; set; }

    [Required]
    [DataType(DataType.MultilineText)]
    public string Content { get; set; } = string.Empty;

    public bool IsPublished { get; set; }
}

