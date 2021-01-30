using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.CodeFirstDemo._4BAIF.Domain.Model
{
    public record Address
    {
        public string Street { get; init; } = string.Empty;

        public string City { get; init; } = string.Empty;
    }
}
