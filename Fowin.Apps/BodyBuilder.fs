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

/// finalize is called by the middleware to write out the environment
let finalize (env:OwinEnv) = async {
  let bodystream : IO.Stream = downcast env.["owin.ResponseBody"]
  let elems : Bodylist = downcast env.["body.parts"]
  for task in elems do
    use s = new IO.StreamWriter(bodystream)
    let! elemtxt = await task
    do! await0 <| s.WriteAsync elemtxt
  }

/// This middleware will add a List<Task<string>> to the environment,
/// then run the downstream app `next`, and then will await
/// and write each string to the body of the response
/// on the way out. Downstream middleware may write bare strings with
/// `writeBody env "foo"`, or add Task<string> objects to
/// `env.["body.parts"]` directly.
let BodyBuilderMiddleware (next:AppFunc) (env:OwinEnv) =
    env.["body.parts"] <- Bodylist()
    start <| async {
      do! env |> invoke next
      do! finalize env
    }
