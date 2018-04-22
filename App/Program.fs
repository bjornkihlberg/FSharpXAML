open System
open System.Windows

open FsXaml

type MyWindow = XAML<"MyWindow.xaml">

let MyButton_onClick _ = printfn "click!"

[<STAThread; EntryPoint>]
let main _ =
    let myWindow = MyWindow()
    myWindow.MyButton.Click.Add MyButton_onClick

    Application().Run myWindow |> ignore
    0 // return an integer exit code
