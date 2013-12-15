module Fowin.Main

let setup (app:Owin.IAppBuilder) = 
  ignore <| app.Use( Fowin.Apps.BodyBuilder.BodyBuilderMiddleware )
               .Use( Fowin.Apps.BodyApp.BodyApp )

[<EntryPoint>]
let main args =
  let uri =
    match List.ofArray args with
    | [] -> "http://localhost:9678/"
    | uri::_ -> uri
  use a = Microsoft.Owin.Hosting.WebApp.Start(uri, setup)
  printfn "%s: Started on %s. Press any key to exit."  (System.DateTimeOffset.Now.ToString("o")) uri
  ignore <| System.Console.ReadKey()
  0
