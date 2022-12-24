using Compiler;
using YUGIOH;
using PBT;
using Spectre.Console;

Console.Clear();
Console.WriteLine();
AnsiConsole.Write(new FigletText("WELLCUM")
.Centered()
.Color(Color.Red));
AnsiConsole.Write(new FigletText("PLAYERS!")
.Centered()
.Color(Color.Blue));
System.Console.WriteLine();
AnsiConsole.Write(new Markup("[blue]...press [/][underline red]any key[/][blue] to continue...[/]").Centered());
Console.ReadKey(true);

Game.MainMenu(new List<Deck>());