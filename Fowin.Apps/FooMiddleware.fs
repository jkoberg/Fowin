module Fowin.Apps.Foo

open System
open System.Threading.Tasks
open Fowin.Types

let FooMiddleware (next:AppFunc) =
  Func<_,_>(fun (env:OwinEnv) ->
    env.["FooToken"] <- "foo!"
    start <| async {
      let! flag = next.Invoke(env) |> Async.AwaitIAsyncResult
      ignore flag
    }
  )

