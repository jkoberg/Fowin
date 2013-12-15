Fowin
=====

A Simple Hello World App for OWIN in F#.

This app is a basic demonstration of the app function delegate of OWIN.

The example implements middleware that keeps a list of Task<strings>'s to render to the body,
and simply awaits them and writes them all out when the "app" has completed.

The example "app" just adds "hello world" to the list of things to write to the body.

This example uses the Microsoft Http Listener to run as a standalone web server. Please
make sure the port defined in Fowin.Main/Program.fs is not in use!

Cheers

Joe Koberg
jkoberg@gmail.com
