module Fowin.Apps.Dummy

open System
open System.Collections.Generic
open System.Threading.Tasks
open Fowin.Types

let writeHello(body:IO.Stream) =
  let b = "<p>Hello, World!</p>"B
  body.Write(b, 0, b.Length)

let DummyApp (next:AppFunc) =
  Func<_,_>(fun (env:OwinEnv) ->
    start <| async {

      match env.["owin.ResponseBody"] with
      | :? IO.Stream as s -> writeHello s
      | _ -> failwith "didn't find owin.ResponseBody in environment!"

    }
  )


