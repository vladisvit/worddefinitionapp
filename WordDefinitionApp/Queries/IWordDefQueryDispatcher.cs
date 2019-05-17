namespace WordDefinitionApp.Queries
{
    public interface IWordDefQueryDispatcher<in TQuery, out TResult> where TQuery : IWordDefQuery<TResult>
    {
        TResult Execute(TQuery query) ;
    }
}