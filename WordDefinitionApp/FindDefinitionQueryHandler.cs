using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordDefinitionApp
{
    using DictServiceReference;
    using Queries;
    public class FindDefinitionQueryHandler : IWordDefQueryHandler<FindDefinitionQuery, Task<WordDefinition>>
    {
        private readonly DictServiceSoapClient dictServiceClient;
        
        public FindDefinitionQueryHandler(string endpointConfigurationName)
        {
            dictServiceClient = new DictServiceSoapClient(endpointConfigurationName);
            dictServiceClient.Open();
        }
        public async Task<WordDefinition> Execute(FindDefinitionQuery query)
        {
            return  await dictServiceClient.DefineAsync(query.SearchWord);
        }
    }
}
