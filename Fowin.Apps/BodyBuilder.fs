module Fowin.Apps.BodyBuilder

open System
open System.Threading.Tasks
open Fowin.Abbrevs

type Bodylist = System.Collections.Generic.List<Task<string>>

let writeBody (env:OwinEnv) s =
  let elems : Bodylist = downcast env.["body.parts"]
  elems.Add(Task.FromResult(s))

let finalize (env:OwinEnv) = async {
  let bodystream : IO.Stream = downcast env.["owin.ResponseBody"]
  let elems : Bodylist = downcast env.["body.parts"]
  for task in elems do
    use s = new IO.StreamWriter(bodystream)
    let! elemtxt = await task
    do! await0 <| s.WriteAsync elemtxt
  }

let BodyBuilderMiddleware (next:AppFunc) (env:OwinEnv) =
    env.["body.parts"] <- Bodylist()
    start <| async {
      do! env |> invoke next
      do! finalize env
    }
