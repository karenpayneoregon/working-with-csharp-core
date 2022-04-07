using System.Collections.Generic;
using System.Linq;
using OracleNorthWindLibrary.Models;

namespace OracleEntityFrameworkUnitTest.Base
{
    public class EmploymentEngineTestBase : CustomerTestBase
    {
        protected Employee SingleEmployee() 
            => AddSandboxEntity(CustomerTestBase.CreateSingleEmployee());


    }
}
