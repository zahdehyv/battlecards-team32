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
Game.MainMenu();

// var deccc = MazeCreator._Recopilatory();
// System.Console.WriteLine();