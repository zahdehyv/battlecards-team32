namespace PBT
{


    public static class PBTout
    {
        public static void PBTPrint(string toprint, long ms, string color)
        {
            var start = DateTime.Now.Ticks;
            foreach (var item in toprint)
            {
                while ((DateTime.Now.Ticks - start) < ms * 1000) { }
                AnsiConsole.Markup($"[{color}]{item}[/]"); //System.Console.Write();
                start = DateTime.Now.Ticks;
            }
            System.Console.WriteLine();
        }


        public static void PrintField(params Player[] fields)
        {
            Console.Clear();
            var field = new Table();
            field.AddColumn("FIELD");
            //field.AddRow(cardsgrid);
            foreach (var player in fields)
            {
                var currentdeck = new Table();
                currentdeck.AddColumn($"{player.Name}'s field");
                var cardsgrid = new Grid();
                cardsgrid.AddColumn();
                cardsgrid.AddColumn();
                cardsgrid.AddColumn();
                cardsgrid.AddColumn();

                currentdeck.AddRow(cardsgrid);


                List<Table> cardsthere = new List<Table>();
                foreach (var card in player.Field)
                {
                    var carta = new Table();
                    if (card == null)
                    {
                        TableColumn wide = new TableColumn(new Panel("_EMPTY_"));
                        wide.Width = 21;
                        carta.AddColumn(wide);
                        cardsthere.Add(carta);
                        continue;
                    }


                    if (!card.Name.Contains('['))
                    {
                        TableColumn wide = new TableColumn(new Panel(card.Name));
                        wide.Width = 21;
                        carta.AddColumn(wide);
                    }
                    else
                    {
                        TableColumn wide = new TableColumn(new Panel("ERROR"));
                        wide.Width = 21;
                        carta.AddColumn(wide);
                    }


                    carta.AddRow($"A: {card.Stats["Attack"]} D: {card.Stats["Defense"]} S: {card.Stats["Speed"]}");
                    carta.AddRow(new List<Table>());
                    var result = GetWidthCard(card, 20);
                    carta.AddRow(new BarChart().Width(result.Item1).AddItem("LP", card.Stats["Life"], result.Item2));
                    cardsthere.Add(carta);
                }
                cardsgrid.AddRow(cardsthere.ToArray());
                field.AddRow(currentdeck);
            }
            // var aaaaa = new TableColumn(new BarChart().Width(50).Label("aaaa").AddItem("Life", 2, Color.Aqua));

            AnsiConsole.Write(field);
        }
        static (int, Color) GetWidthCard(Card card, int widdth)
        {
            card.MaxLife = Math.Max(card.MaxLife, card.Stats["Life"]);
            int value = (widdth * card.Stats["Life"]) / card.MaxLife;

            if (value >= 18)
                return (value, Color.Green);
            else if (value >= 12)
                return (value, Color.Yellow);
            else if (value >= 10)
                return (value, Color.Red);
            else
                return (9, Color.DarkRed);
        }


        public static T GamePrompt<T>(string title, Func<T, string> converter, params T[] options) where T : notnull
        {
            return AnsiConsole.Prompt(new SelectionPrompt<T>()
            .Title(title)
            .UseConverter(converter)
            .AddChoices(options));
        }

        public static string AskString(string title)
        {
            string name = AnsiConsole.Ask<string>(title);
            switch (name)
            {
                case null:
                    return "Pancracio";
                case "":
                    return "Pancracio";
                default:
                    return name;
            }
        }
        public static void ActualValues(params string[] values)
        {
            Console.Clear();
            foreach (var item in values)
            {
                Console.WriteLine($"{item}");
            }
            System.Console.WriteLine();
        }
        public static void ShowSummonable(Deck deck)
        {
            var table = new Table();
            foreach (var item in deck.Cards)
            {
                var grid = new Grid();
                grid.AddColumn();
                grid.AddRow(new Markup($"[red]{item.Name}[/]"));
                foreach (var itemi in item.Stats.Keys)
                    grid.AddRow($"{itemi}: {item.Stats[itemi]}");
                table.AddColumn(new TableColumn(grid));
            }
            AnsiConsole.Write(table);
        }
    }
}