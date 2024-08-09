using Async.Shared.SearchEngine;
using RxHelpers;

Demo.DisplayHeader("Converting Tasks to observables");

var results = SearchEngineExample.Search_ConcatingTasks("Rx");

results
   .RunExample("tasks to observables");