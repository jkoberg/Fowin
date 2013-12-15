module Fowin.Main

let setup (app:Owin.IAppBuilder) = 
  ignore <| app.Use( Fowin.Apps.BodyBuilder.BodyBuilderMiddleware )
               .Use( Fowin.Apps.BodyApp.BodyApp )

[<EntryPoint>]
let main args =
  let uri =
    match args with
    | [|     |] -> "http://localhost:9678/"
    | [| uri |] -> uri
    | _         -> failwith "invalid command line params"
  use a = Microsoft.Owin.Hosting.WebApp.Start(uri, setup)
  printfn "%s: Started on %s. Press any key to exit."  (System.DateTimeOffset.Now.ToString("o")) uri
  ignore <| System.Console.ReadKey()
  0
