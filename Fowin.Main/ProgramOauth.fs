/// A simple app that gets its user identity from some OAuth2 provider
module Fowin.MainWithOAuth2

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

