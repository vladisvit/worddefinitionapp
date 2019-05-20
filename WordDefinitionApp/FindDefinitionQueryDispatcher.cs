using Ninject;
using System;
using System.Threading.Tasks;

namespace WordDefinitionApp
{
    using DictServiceReference;
    using Queries;
    public class FindDefinitionQueryDispatcher : IWordDefQueryDispatcher<FindDefinitionQuery, Task<WordDefinition>>
    {
        private const string EndpointConfigurationName = "DictServiceSoap";
        private readonly IKernel _kernel;
        public FindDefinitionQueryDispatcher(IKernel kernel)
        {
            _kernel = kernel;
        }
        public async Task<WordDefinition> Execute(FindDefinitionQuery query)
        {
            if (query == null)
            {
                throw new ArgumentNullException("query");
            }

            var construtorArgument = new Ninject.Parameters.ConstructorArgument("endpointConfigurationName", EndpointConfigurationName);
            var handler = _kernel.Get<IWordDefQueryHandler<FindDefinitionQuery, Task<WordDefinition>>>(construtorArgument);
            if (handler == null)
            {
                throw new WordDefQueryHandlerNotFoundException(typeof(FindDefinitionQueryHandler));
            }

            var result = await handler.Execute(query);
            return result;
        }
    }
}
