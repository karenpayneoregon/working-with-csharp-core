using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationDataLibrary.Comparers
{
    public class PersonIdentifierGroupComparer : IEqualityComparer<int>
    {
        private int _limit = 3;

        public bool Equals(int x, int y) => IsInLimit(x) == IsInLimit(y);

        public int GetHashCode(int obj) => IsInLimit(obj) ? 1 : 2;

        public bool IsInLimit(int id) => id < _limit;
    }
}
