using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdaCredit
{
    public interface IPerson
    {
        string Name { get; }
        string Id { get; }
        bool Status { get; }
    }
}
