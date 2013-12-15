module Fowin.Main

open System
open System.Collections.Generic
open System.Threading.Tasks
open Owin
open Microsoft.Owin
open Microsoft.Owin.Hosting

open Fowin.Types
open Fowin.Apps

let buildApp (app:IAppBuilder) =
    ignore <| app.Use(Foo.FooMiddleware)
                 .Use(Dummy.DummyApp)
  
[<EntryPoint>]
let Main args = 
  let uri = "http://localhost:9678/"
  try
    printfn "Starting."
    use a = WebApp.Start("http://localhost:9678/", buildApp)
    printfn "Started."
    ignore <| Console.ReadKey()
    printfn "Stopping."
    0
  with
  | :? System.Reflection.TargetInvocationException as ex ->
      eprintfn "Problem starting. Maybe another process bound to that port??"
      eprintfn "%s" (ex.ToString())
      1
 