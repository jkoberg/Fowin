module Fowin.Apps.BodyBuilder

open System
open System.Collections.Generic
open System.Threading.Tasks
open Fowin.Abbrevs

/// The list of parts to be written to the body after executing the downstream app.
type Bodylist = List<Task<string>>

/// use writeBody during processing to add a bare string the to-be-built body
let writeBody (env:OwinEnv) s =
  let elems : Bodylist = downcast env.["body.parts"]
  elems.Add(Task.FromResult(s))

/// This middleware will add a List<Task<string>> to the environment,
/// then run the downstream app `next`, and then will await
/// and write each string to the body of the response
/// on the way out. Downstream middleware may write bare strings with
/// `writeBody env "foo"`, or add Task<string> objects to
/// `env.["body.parts"]` directly.
let BodyBuilderMiddleware (next:AppFunc) (env:OwinEnv) =
  start <| async {
    env.["body.parts"] <- Bodylist()
    do! env |> invoke next // call downstream middleware
    use body = new IO.StreamWriter(stream = downcast env.["owin.ResponseBody"]) 
    for task in downcast env.["body.parts"] do
      let! (elemtxt:string) = await task
      do! await0 <| body.WriteAsync elemtxt
  }
