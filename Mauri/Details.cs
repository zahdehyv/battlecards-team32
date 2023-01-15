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
                        var gridup = new Grid();
                        gridup.AddColumn();
                        List<Panel> panels = new List<Panel>(){
                            new Panel(card.Name)
                        };
                        for (int i = 0; i < card.Addings.Count; i++)
                        {
                            gridup.AddColumn();
                            panels.Add(new Panel(card.Addings[i].Name[0].ToString()));
                        }
                        gridup.AddRow(panels.ToArray());

                        TableColumn wide = new TableColumn(gridup);
                        wide.Width = 21;
                        carta.AddColumn(wide);
                    }
                    else
                    {
                        TableColumn wide = new TableColumn(new Panel("ERROR"));
                        wide.Width = 21;
                        carta.AddColumn(wide);
                    }


                    //     carta.AddRow(new Panel(card.Stats["Attack"].ToString()+Emoji.Known.Dagger));
                    //   carta.AddRow($"{card.Stats["Defense"]}:shield:");
                    // carta.AddRow(new Markup($"{card.Stats["Attack"]}:dagger: {card.Stats["Defense"]}:shield:"));
                    int pos = 0;
                    string[] statist = new string[3];
                    statist[0] = "---";
                    statist[1] = "---";
                    statist[2] = "---";
                    var griid = new Grid();
                    griid.AddColumn();
                    griid.AddColumn();
                    griid.AddColumn();
                    foreach (var item in card.Stats.Keys)
                    {
                        if (item != "Life")
                        {
                            var currstt = $"{item[0]}{item[1]}{item[2]}:{card.Stats[item]}";
                            switch (pos)
                            {
                                case 0:
                                    statist[0] = currstt;
                                    pos++;
                                    if (item == card.Stats.Keys.ToArray()[card.Stats.Keys.ToArray().Length - 1])
                                    {
                                        statist[1] = "---";
                                        statist[2] = "---";
                                        griid.AddRow((string[])statist.Clone());
                                    }
                                    break;
                                case 1:
                                    statist[1] = currstt;
                                    pos++;
                                    if (item == card.Stats.Keys.ToArray()[card.Stats.Keys.ToArray().Length - 1])
                                    {
                                        statist[2] = "---";
                                        griid.AddRow((string[])statist.Clone());
                                    }
                                    break;
                                case 2:
                                    statist[2] = currstt;
                                    griid.AddRow((string[])statist.Clone());
                                    pos=0;
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                    carta.AddRow(griid);
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
                case " ":
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