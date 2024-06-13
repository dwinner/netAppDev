module FunctionalDesignPatternDemos.FunctionalDecorator

open System.Diagnostics

let doWork () = printfn "Doing some work"

let logger aWork aName =
   let sw = Stopwatch.StartNew()
   printfn "%s %s" "Entering method" aName
   aWork ()
   sw.Stop()
   printfn $"Exiting method %s{aName}; %f{sw.Elapsed.TotalSeconds}s elapsed"

[<EntryPoint>]
let main argv =
   let loggedWork () = logger doWork "doWork"
   loggedWork ()
   0
