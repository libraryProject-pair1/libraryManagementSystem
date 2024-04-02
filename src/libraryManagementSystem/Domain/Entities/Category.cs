using NArchitecture.Core.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities;
public class Category : Entity<Guid>
{
    public string CategoryName { get; set; }
    public string CategoryDescription { get; set; }
    public ICollection<CategoryBook>? CategoryBooks { get; set; }
}
