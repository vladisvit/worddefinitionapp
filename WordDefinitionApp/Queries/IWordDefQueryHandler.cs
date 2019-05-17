namespace WordDefinitionApp.Queries
{
    public interface IWordDefQueryHandler<in TQuery, out TResult> 
    {
        TResult Execute(TQuery query);
    }
}
