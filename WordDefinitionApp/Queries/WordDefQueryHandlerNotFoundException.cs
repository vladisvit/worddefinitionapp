using System;

namespace WordDefinitionApp.Queries
{
    public class WordDefQueryHandlerNotFoundException : Exception      
    {
        private string message;
        public WordDefQueryHandlerNotFoundException(object param) 
        {
            message = param.ToString();
        }

        public override string ToString()
        {
            return message;
        }
    }
}
