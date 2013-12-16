module Fowin.Abbrevs

open System
open System.Collections.Generic
open System.Threading.Tasks

/// Standard OWIN environment dictionary
type OwinEnv = IDictionary<string, obj>

/// Standard OWIN "app", terminating the chain by having no downstream
type AppFunc = System.Func<OwinEnv, Task>

/// Standard OWIN "middleware", expected to await the results of its first argument
type MiddlewareFunc = AppFunc -> AppFunc

/// Used to adapt a "C# Async" method of type Task<'a> to F# Async<'a>
let await = Async.AwaitTask

/// Used to adapt a "C# Async" method of type Task to F# Async<unit>
let await0 (t:Task) = async {
  let! flag = Async.AwaitIAsyncResult t
  ignore flag
  }

/// Turns an F# Async<'a> into a bare C# Task.
/// Used to return the non-generic "Task" type
/// specified in the standard
let start a : Task = upcast (Async.StartAsTask a)

/// Used to start the `next` functions passed in to OWIN
/// middleware as F# Async's
let invoke (next:Func<_,Task>) arg = await0 <| next.Invoke(arg)

type Task with
  static member Null = Task.FromResult(())
