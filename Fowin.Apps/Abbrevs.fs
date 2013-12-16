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

/// Empty task that has "already finished" with Result=(),
/// to be used where a Task value is expected but no
/// async Task needs to be completed
let NullTask = Task.FromResult(())

/// print a string to STDERR, prefixed with a timestamp
let log msg = eprintfn "%s: %s" (System.DateTimeOffset.Now.ToString("o")) msg
let alog msg = await0 <| System.Console.Error.WriteAsync(sprintf "%s: %s\n" (System.DateTimeOffset.Now.ToString("o")) msg)
