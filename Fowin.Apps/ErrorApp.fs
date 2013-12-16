module Fowin.Apps.ErrorApp

open Fowin.Abbrevs
open Fowin.Apps.BodyBuilder

let ErrorApp (next:AppFunc) (env:OwinEnv) =
    for key in env.Keys do
      writeBody env (sprintf "%s: %A\n" key env.[key])
    NullTask
