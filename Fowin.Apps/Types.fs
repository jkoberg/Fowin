module Fowin.Types

open System.Collections.Generic
open System.Threading.Tasks

type OwinEnv = IDictionary<string, obj>
type AppFunc = System.Func<OwinEnv, Task>
type MiddlewareFunc = AppFunc -> AppFunc

let start a : Task = upcast (Async.StartAsTask a)
