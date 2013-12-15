module Fowin.Abbrevs

open System
open System.Collections.Generic
open System.Threading.Tasks

type OwinEnv = IDictionary<string, obj>
type AppFunc = System.Func<OwinEnv, Task>
type MiddlewareFunc = AppFunc -> AppFunc

let await = Async.AwaitTask

let start a : Task = upcast (Async.StartAsTask a)

let await0 (t:Task) = async {
  let! flag = Async.AwaitIAsyncResult t
  ignore flag
  }

let invoke (next:Func<_,Task>) arg = await0 <| next.Invoke(arg)

type Task with
  static member Null = Task.FromResult(())
