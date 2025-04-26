using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Uniterm.Models;

namespace Uniterm
{
    public interface IUnitermCanvas
    {
        void AddVerticalOperation(AbstractOperation op);
        void AddHorizontalOperation(AbstractOperation op);
        void Refresh();
    }
}
