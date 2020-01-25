using System;
using System.Collections.Generic;
using System.Text;

namespace Sagua.Table.Abstractions
{
    public interface IPager
    {
        void GoToPage(int index);
        void SetLimit(int limit);

        void Update(IPaging paging);
    }
}
