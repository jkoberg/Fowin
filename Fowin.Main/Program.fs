module Fowin.Main

let setup (app:Owin.IAppBuilder) = 
  ignore <| app.Use( Fowin.Apps.BodyBuilder.BodyBuilderMiddleware )
               .Use( Fowin.Apps.BodyApp.BodyApp )

[<EntryPoint>]
let main args =
  let uri = "http://localhost:9678/"
  use a = Microsoft.Owin.Hosting.WebApp.Start(uri, setup)
  printfn "Started on %s. Press any key to exit."  uri
  ignore <| System.Console.ReadKey()
  0
