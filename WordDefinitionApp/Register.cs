using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject;

namespace WordDefinitionApp
{
    using DictServiceReference;
    using Queries;
    public class Register
    {
        public static void RegisterDependencies(IKernel kernel)
        {
            kernel.Bind(typeof(IWordDefQueryHandler<FindDefinitionQuery, Task<WordDefinition>>)).To(typeof(FindDefinitionQueryHandler));
        }
    }
}
