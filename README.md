# FSharpXAML
This tutorial demonstrates how to use WPF and the XAML-language with F#.

## Walkthrough
**Summary:**
1. Create an F# console application
2. Install the nuget-package [FsXaml.Wpf](https://fsprojects.github.io/FsXaml/)
3. Create a .xaml-file in your project and define your window.
4. Import the .xaml-file into F# and instantiate the window defined in the file.

### The .xaml-file
You add this file to your project. Make sure it has a *.xaml* file-ending. Then in Visual Studio you edit the files properties to copy to output directory *always* or *if newer* and make sure the build action is *resource*. It should automatically set itself as a *resource*.

Next add this to the .xaml-file. This defines an emtpy window. The first attribute is needed for the .NET window elements like labels, buttons, etc. The second attribute defines a recursive reference to the window itself. It's used mostly to name elements in the window so that they might be accessed from F#. We will look at this more a little later.
```xml
<Window xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml">

</Window>
```

### F#

You want to give the main function the STAThread attribute so that it may spawn a window.
```fsharp
open System

[<STAThread; EntryPoint>]
let main _ =
    0 // Integer return code
```

Next you use the XAML type provider to load the .xaml-file and autogenerate a type we can instantiate.
```fsharp
open FsXaml

type MyWindow = XAML<"MyWindow.xaml">
```

To spawn a window you first have to instantiate the type you've defined in your .xaml-file. Next you need to instantiate the `Application`-type found in the *System.Windows*-namespace.
```fsharp
open System
open System.Windows

open FsXaml

type MyWindow = XAML<"MyWindow.xaml">

[<STAThread; EntryPoint>]
let main _ =
    let myWindow = MyWindow()
    let application = Application()
    0 // Integer return code
```

The last step is to start the application with the window by calling `application.Run myWindow`. The `Run`-method returns an appropriate integer return code so you can replace the hardcoded value in the main function with the returned value given by `Run`.
```fsharp
open System
open System.Windows

open FsXaml

type MyWindow = XAML<"MyWindow.xaml">

[<STAThread; EntryPoint>]
let main _ =
    let myWindow = MyWindow()
    let application = Application()
    application.Run myWindow
```

That's it! You've created a basic XAML window in F#. Let's take a look at how to define some basic interactions between F# and your window.
___

## Interaction between F# and XAML
We'll create a button in XAML and refer to it in F# to define its behaviour.

Add this button-tag to your window. You can probably figure out what most of the button attributes do. The interesting one we want to focus on is `x:Name="MyButton"`. This attribute gets this button an explicit member reference in the `MyWindow`-type. This means that we can now access this button from our F# code.
```xml
<Window xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml">
    <Button x:Name="MyButton"
            Content="click!"
            Width="100"
            Height="25" />
</Window>
```

Let's take a look at how we access our window from F#.
```fsharp
let myWindow = MyWindow()

myWindow.MyButton // MyWindow now has a member referencing the button
```

We can give our button behaviour by adding a function to the `Click`-event.
```fsharp
myWindow.MyButton.Click.Add (fun _ -> printfn "Clicked!")
```
Now when you run the program and click the button `Clicked!` should appear in the console window.

And that's it for now. Good luck with F# and Wpf! If you want to learn more, you can google how to, for example, do a checkbox by googling something along the lines of "checkbox wpf" or "checkbox xaml". There aren't that many good resources out there on how to work with XAML and Wpf in F# but plenty in C# so if you know a bit of OOP you can probably translate how to solve a problem related to windows in C# into F#.
