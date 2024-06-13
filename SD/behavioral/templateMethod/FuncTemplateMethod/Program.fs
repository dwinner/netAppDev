module FunctionalDesignPatternDemos.TemplateMethod

type GameState =
   { mutable CurrentPlayer: int
     mutable NumberOfPlayers: int
     mutable WinningPlayer: int }

let runGame initialState startAction takeTurnAction haveWinnerAction =
   let state = initialState
   startAction state

   while not (haveWinnerAction state) do
      takeTurnAction state

   printfn $"Player %i{state.WinningPlayer} wins."

let chess () =
   let mutable turn = 0
   let mutable maxTurns = 10

   let state =
      { NumberOfPlayers = 2
        CurrentPlayer = 0
        WinningPlayer = -1 }

   let start state =
      printfn $"Starting a game of chess with %i{state.NumberOfPlayers} players"

   let takeTurn state =
      printfn $"Turn %i{turn} taken by player %i{state.CurrentPlayer}."
      state.CurrentPlayer <- (state.CurrentPlayer + 1) % state.NumberOfPlayers
      turn <- turn + 1
      state.WinningPlayer <- state.CurrentPlayer

   let haveWinner state = turn = maxTurns

   runGame state start takeTurn haveWinner

[<EntryPoint>]
let main argv =

   chess ()

   0
