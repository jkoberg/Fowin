﻿module Fowin.Apps.BodyApp
open System
open System.Collections.Generic
open System.Threading.Tasks

open Fowin.Abbrevs
open Fowin.Apps.BodyBuilder

let BodyApp (next:AppFunc) (env:OwinEnv) =
    writeBody env """<h1>hello world</h1>"""
    Task.Null
