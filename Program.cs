using Compiler;
// See https://aka.ms/new-console-template for more information
MazeCreator asd = new MazeCreator();
var decks = asd._Recopilatory();
System.Console.WriteLine();
foreach (var item in decks)
{
   item.ShowDeck(true);
    System.Console.WriteLine();
}