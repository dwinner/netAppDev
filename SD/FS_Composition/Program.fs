module FunctionalDesignPatternDemos.Program

let addFive x = x + 5
let timesTwo x = x * 2

//let timesTwoAddFive x = x |> timesTwo |> addFive
let timesTwoAddFive = timesTwo >> addFive

[<EntryPoint>]
let main _ =
   printfn $"%i{addFive (timesTwo 3)}" // 11
   printfn $"%i{3 |> timesTwo |> addFive}"
   printfn $"%i{addFive <| (timesTwo <| 3)}"
   printfn $"%i{timesTwoAddFive 3}"
   0
