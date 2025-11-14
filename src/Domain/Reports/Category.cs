using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedKernel;

namespace Domain.Todos;

public class Category : Entity
{
    public Guid Id { get; set; }
    public string Name { get; set; }
}
