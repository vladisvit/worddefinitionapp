using System.Threading.Tasks;

namespace WordDefinitionApp
{
    using DictServiceReference;
    using Queries;
    public class FindDefinitionQuery : IWordDefQuery<Task<WordDefinition>>
    {
        public string SearchWord { get; }

        public FindDefinitionQuery(string inputWord) => SearchWord = inputWord;
    }
}
