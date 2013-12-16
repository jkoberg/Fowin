module Fowin.Main

open Owin
open Microsoft.Owin
open Microsoft.Owin.Security
open Microsoft.Owin.Security.OAuth
open Fowin.Abbrevs
open System.Web

/// Create the OWIN Application by configuring the IAppBuilder with
/// the OWIN middleware we'd like to use.
let setup (app:Owin.IAppBuilder) =
  ignore <| app.Use( Fowin.Apps.BodyBuilder.BodyBuilderMiddleware )
               .Use( Fowin.Apps.ErrorApp.ErrorApp )

/// Command-line entrypoint.
/// Implicitly starts a new HttpListener-based server and runs the configured app.
/// Optionally takes the URL to listen on as the first argument.
[<EntryPoint>]
let main args =
  let uri = match args with | [| |] -> "http://localhost:9678/"
                            | [|u|] -> u
                            | _     -> failwith "invalid command line args"
  log "Starting."
  use a = Microsoft.Owin.Hosting.WebApp.Start(uri, setup)
  log <| sprintf "Started on %s .  Press any key to exit." uri
  ignore <| System.Console.ReadKey()
  0
