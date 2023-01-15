namespace Compiler
{
    static class MazeCreator
    {
        static string DeckDir = "./decks";
        static string[] DefActions = File.ReadAllLines("./default/defaultactions.txt");

        public static List<Deck> _Recopilatory()
        {
            List<Deck> decks = new List<Deck>();
            var indexes = Directory.GetDirectories(DeckDir);
            foreach (var item in indexes)
            {
                Deck deck = _MakeDeck(item);
                decks.Add(deck);
            }
            System.Console.WriteLine();
            AnsiConsole.Write(new Markup("[underline red]continue[/]"));
            Console.ReadKey(true);
            return decks;
        }
        static Deck _MakeDeck(string deckpath)
        {
            PBTout.PBTPrint($"Creando deck {Path.GetFileName(deckpath)}", 40, "cyan");
            var card = new List<Card>();
            foreach (var item in Directory.GetFiles(deckpath))
                card.Add(_MakeCard(item));
            return new Deck(Path.GetFileName(deckpath), card);
        }

        static Card _MakeCard(string cardpath)
        {
            PBTout.PBTPrint($" Creando carta {Path.GetFileNameWithoutExtension(cardpath)}", 30, "green");

            var texto = File.ReadAllLines(cardpath).ToList();

            for (int i = 0; i < DefActions.Length; i++)
                texto.Insert(i, DefActions[i]);
            var stats = new Dictionary<string, int>{
                    {"Life",1},
                    {"Attack",1},
                    {"Defense",1},
                };
            List<Error> errors = new List<Error>();
            var a = Parser.ParsearInst(texto, stats, errors, 0);
            var emptyplayer = new Player(new Deck("", new List<Card>()), "");
            var card = new Card(Path.GetFileNameWithoutExtension(cardpath), stats, a.Item2);

            foreach (var item in a.Item1)
                item.Run(card, card, emptyplayer, emptyplayer);

            if (errors.Count != 0)
            {
                foreach (var item in stats.Keys)
                    stats[item] = 1;
                foreach (var item in errors)
                    item._Print();
                PBTout.PBTPrint($"* se ha creado la carta {Path.GetFileNameWithoutExtension(cardpath)} como carta de error", 80, "gray");
                return new Card($"|X| {Path.GetFileNameWithoutExtension(cardpath)}", stats, new List<Accion>());
            }
            return card;
        }
    }

}
