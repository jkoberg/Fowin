module Fowin.Apps.Dummy

open System
open System.Collections.Generic
open System.Threading.Tasks
open Fowin.Types

let DummyApp (next:AppFunc) =
  Func<_,_>(fun (env:OwinEnv) ->
      match env.["owin.ResponseBody"] with
      | :? IO.Stream as s ->
        let b = "<p>Hello, World!</p>"B
        s.WriteAsync(b, 0, b.Length)
      | _ -> failwith "didn't find owin.ResponseBody in environment!"
    )


