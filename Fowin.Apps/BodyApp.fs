module Fowin.Apps.BodyApp

open Fowin.Abbrevs
open Fowin.Apps.BodyBuilder

/// This app writes a string to the body.
/// It does not do any processing that requires an async or Task, so it
/// just mutates the environment and returns an empty task using
/// the `Task.Null` convenience extension from Fowin.Abbrevs
let BodyApp (next:AppFunc) (env:OwinEnv) =
    writeBody env """<h1>hello world</h1>"""
    Task.Null
