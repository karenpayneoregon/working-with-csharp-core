using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationDataLibrary.Comparers;

namespace ApplicationDataLibrary.ComparerHelpers
{
    public class Wrappers
    {
        public static IEqualityComparer<Person> FirstNamEqualityComparer => 
            Equality<Person>.CreateComparer(p => p.FirstName);
        
        public static IEqualityComparer<Person> LastNamEqualityComparer => 
            Equality<Person>.CreateComparer(p => p.LastName);
        
        public static LambdaEqualityComparer<Person> FirstNameLambda => 
            new((p1, p2) => 
            p1.FirstName == p2.FirstName, x => x.FirstName.GetHashCode());
        
        public static LambdaEqualityComparer<Person> LastNameLambda => 
            new((p1, p2) => 
            p1.LastName == p2.LastName, x => x.LastName.GetHashCode());


    }
}
