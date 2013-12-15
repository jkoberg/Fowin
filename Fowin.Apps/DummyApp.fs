module Fowin.Apps.Dummy

open System
open System.Collections.Generic
open System.Threading.Tasks
open Fowin.Types

let DummyApp (next:AppFunc) =
  Func<_,_>(fun (env:OwinEnv) ->
    start <| async {

      match env.["owin.ResponseBody"] with
      | :? IO.Stream as s ->
        let b = "<p>yo</p>"B
        s.Write(b, 0, b.Length)
      | _ -> failwith "didn't find owin.ResponseBody in environment!"

    }
  )


