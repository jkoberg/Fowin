module Fowin.Main

open Owin
open Microsoft.Owin
open Microsoft.Owin.Security
open Microsoft.Owin.Security.OAuth
open Fowin.Abbrevs
open System.Web

//let newSetup = 
//  let ExternalIdentityMiddleware (next:AppFunc) (env:OwinEnv) = 
//    let authentication = System.Web.HttpContext.Current.GetOwinContext().Authentication
//    let result = Async.AwaitTask <| authentication.AuthenticateAsync("External")
//    let externalIdentity = result.Identity
//    env.["id.external"] = externalIdentity
//
//
//  let setup (app:Owin.IAppBuilder) = 
//    let no = """
//    let authOpts =
//      OAuthBearerAuthenticationOptions(
//        AuthenticationMode = AuthenticationMode.Active,
//        Provider = OAuthBearerAuthenticationProvider(
//          OnValidateIdentity = (
//            fun ctx -> start <| async {
//              ctx.Rejected()
//            })
//        )
//      )
//    app.SetDefaultSignInAsAuthenticationType("Google")
//    """
//
//    ignore <| app.UseCookieAuthentication(
//                    CookieAuthenticationOptions(
//                      AuthenticationType = "External",
//                      AuthenticationMode = AuthenticationMode.Passive,
//                      CookieName = CookieAuthenticationDefaults.CookiePrefix + "External",
//                      ExpireTimeSpan = TimeSpan.FromMinutes(5)
//                    ))
//                 .UseGoogleAuthentication() //clientId="854752695711-2jqjuitjjjdu8qghppuclnvhuiormn4j.apps.googleusercontent.com", clientSecret="rHghirl-3FZg6Otl6Lf2kIhY")
//                 .Use( Fowin.Apps.BodyBuilder.BodyBuilderMiddleware )
//                 .Use( Fowin.Apps.ErrorApp.ErrorApp )
//  0



/// Create the OWIN Application by configuring the IAppBuilder with
/// the OWIN middleware we'd like to use.
let setup (app:Owin.IAppBuilder) =
  ignore <| app.Use( Fowin.Apps.BodyBuilder.BodyBuilderMiddleware )
               .Use( Fowin.Apps.ErrorApp.ErrorApp )


/// print a string to STDERR, prefixed with a timestamp
let log msg = eprintfn "%s: %s" (System.DateTimeOffset.Now.ToString("o")) msg
let alog msg = await0 <| System.Console.Error.WriteAsync(sprintf "%s: %s\n" (System.DateTimeOffset.Now.ToString("o")) msg)

/// Command-line entrypoint.
/// Implicitly starts a new HttpListener-based server and runs the configured app.
/// Optionally takes the URL to listen on as the first argument.
[<EntryPoint>]
let main args =
  let uri =
    match args with
    | [|     |] -> "http://localhost:9678/"
    | [| uri |] -> uri
    | _         -> failwith "invalid command line params"
    
  log "Starting."
  use a = Microsoft.Owin.Hosting.WebApp.Start(uri, setup)
  log <| sprintf "Started on %s .  Press any key to exit." uri
  ignore <| System.Console.ReadKey()
  0
