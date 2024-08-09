namespace Async.Shared.SearchEngine;

internal interface ISearchEngine
{
   Task<IEnumerable<string>> SearchAsync(string term);
}