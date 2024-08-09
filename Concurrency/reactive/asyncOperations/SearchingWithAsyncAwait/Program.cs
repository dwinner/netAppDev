using Async.Shared.SearchEngine;
using RxHelpers;

Demo.DisplayHeader("Creating async observable with async-await");

var results = SearchEngineExample.Search_WithAsyncAwait("Rx");
results.RunExample("search async-await");