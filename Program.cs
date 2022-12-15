using Compiler;
using YUGIOH;
using PBT;
// See https://aka.ms/new-console-template for more information
var decks = MazeCreator._Recopilatory();
System.Console.WriteLine();
foreach (var item in decks)
{
    item.ShowDeck();
    System.Console.WriteLine();
}
var emptyplayer = new Player(new Deck("a", new List<Card>()));
decks[0].Cards[1].Actions[0].DoAct(decks[0].Cards[1], decks[0].Cards[0], emptyplayer, emptyplayer);

System.Console.WriteLine();
foreach (var item in decks)
{
    item.ShowDeck();
    System.Console.WriteLine();
}
System.Console.WriteLine();
PBTout.PBTPrint("me cago en todos",350);




//Console.Beep(1000,1000);
//Parser ast= new Parser();
//System.Console.WriteLine(ast.ParsearExps("3<3+1").Valuate());
