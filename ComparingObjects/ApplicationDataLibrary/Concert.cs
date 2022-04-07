using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationDataLibrary
{
    public class Concert
    {
        
        public int PersonId { get; set; }
        public int ConcertCount { get; set; }
        public int Year { get; set; }

        public override string ToString() 
            => $"{nameof(PersonId)}: {PersonId}, {nameof(ConcertCount)}: {ConcertCount}";
    }
}
