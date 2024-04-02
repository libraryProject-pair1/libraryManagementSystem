using NArchitecture.Core.Persistence.Repositories;


namespace Domain.Entities;
public class Book : Entity<Guid>
{
    public string ISBN { get; set; }
    public string Name { get; set; }
    public ICollection<AuthorBook>? AuthorBooks { get; set; }
    public int Pages { get; set; }
    public virtual Publisher? Publisher { get; set; }
    public Guid PublisherId { get; set; }
    public string Language {  get; set; }
    public Guid CategoryId { get; set; }
    public ICollection<CategoryBook>? CategoryBooks { get; set; }
    public string Description { get; set; }
    public int UnitsInStock { get; set; }
}
