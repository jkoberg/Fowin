module Fowin.Apps.Foo

open System
open System.Threading.Tasks
open Fowin.Types

let FooMiddleware (next:AppFunc) =
  Func<_,_>(fun (env:OwinEnv) ->
    env.["FooToken"] <- "foo!"
    next.Invoke(env)
  )

let FooMiddleware2 (next:AppFunc) =
  Func<_,_>(fun (env:OwinEnv) ->
    // do non-async (non-blocking) things here
    env.["FooToken"] <- "foo!"
    start <| async {

      // do async things here like IO
      let! filecontents = Async.AwaitTask <| System.IO.File.OpenText("c:\\test.txt").ReadToEndAsync()

      // Call the callback with the perhaps modified environment
      let! flag = next.Invoke(env) |> Async.AwaitIAsyncResult
      ignore flag
    }
  )