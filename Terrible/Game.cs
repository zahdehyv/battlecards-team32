using YUGIOH;
using Compiler;
using PBT;
using AI;
using Spectre.Console;

namespace YUGIOH
{
    public static class Game
    {
        static List<Deck> DECKS = new List<Deck>();
        static string[] textW = File.ReadAllLines("./default/weights.txt");
        static (double, double)[] weights = new (double, double)[]{
                    (Convert.ToDouble(textW[0].Split()[0]),Convert.ToDouble(textW[0].Split()[1])),
                    (Convert.ToDouble(textW[1].Split()[0]),Convert.ToDouble(textW[1].Split()[1])),
                    (Convert.ToDouble(textW[2].Split()[0]),Convert.ToDouble(textW[2].Split()[1])),
                    (Convert.ToDouble(textW[3].Split()[0]),Convert.ToDouble(textW[3].Split()[1]))
                };
        public static void MainMenu()
        {
            Console.Clear();
            string[] options = new[] { "Build Decks", "Exit Game" }; //{ "Start Game", "Build Decks","Exit Game"}
            if (DECKS.Count > 0)
                options = new[] { "Start Game", "Build Decks", "Exit Game" };

            switch (PBTout.GamePrompt<string>("", (x => x), options))
            {
                case "Start Game":
                    Start();
                    break;
                case "Training Room":
                    var tempw = StartTraining();
                    weights = new (double, double)[]{
                    (tempw[7],tempw[6]),
                    (tempw[5],tempw[4]),
                    (tempw[3],tempw[2]),
                    (tempw[1],tempw[0])
                };
                    MainMenu();
                    break;
                case "Build Decks":
                    DECKS = MazeCreator._Recopilatory();
                    MainMenu();
                    break;
                case "Exit Game":
                    return;
                default:
                    break;
            }


            //var DECKS = MazeCreator._Recopilatory();//Cogiendo la lista de Decks
        }
        public static void Start()
        {
            Console.Clear();

            Player[] players = new Player[2];
            for (int i = 0; i < players.Length; i++)
            {
                Console.Clear();
                PBTout.PBTPrint($"Player {i + 1}: ", 250, "white");
                string name = PBTout.AskString("Escriba su nombre: ");
                PBTout.ActualValues(name);
                PBTout.PBTPrint("Seleccione su Deck: ", 250, "white");
                var SelectedDeck = PBTout.GamePrompt<Deck>("Decks: ", (x => x.Deckname), DECKS.ToArray());
                PBTout.ActualValues(name, SelectedDeck.Deckname);
                PBTout.PBTPrint("Es un jugador virtual : ", 250, "white");
                bool virt = PBTout.GamePrompt<bool>("Virtual? ", (x => x.ToString()), new[] { false, true });
                PBTout.ActualValues(name, SelectedDeck.Deckname, virt.ToString());
                if (virt)
                    players[i] = new VirtualPlayer((Deck)SelectedDeck.Clone(), name, weights);
                else
                    players[i] = new Player((Deck)SelectedDeck.Clone(), name);

                PBTout.ActualValues(name, SelectedDeck.Deckname, virt.ToString());
                if (!PBTout.GamePrompt<bool>("All Right? ", (x => x.ToString()), new[] { true, false }))
                    i--;
            }
            Console.Clear();


            AnsiConsole.Write(new FigletText(players[0].Name)
            .LeftAligned()
            .Color(Color.Red));
            AnsiConsole.Write(new FigletText("vs")
            .Centered()
            .Color(Color.White));
            AnsiConsole.Write(new FigletText(players[1].Name)
            .RightAligned()
            .Color(Color.Blue));
            System.Console.WriteLine();
            AnsiConsole.Write(new Markup("[blue]( PRESS [/][underline red]ANY KEY[/][blue] TO START )[/]").Centered());
            Console.ReadKey(true);
            new Board(players[0], players[1]).FirstTurn(players[0], players[1]);
        }
        public static double[] StartTraining()
        {
            Console.Clear();
            Deck[] decks = new Deck[2];
            for (int i = 0; i < 2; i++)
            {
                decks[i] = PBTout.GamePrompt<Deck>($"Seleccione deck de entrenamiento {i}", (x => x.Deckname), DECKS.ToArray());
            }
            int iter = Convert.ToInt32(PBTout.AskString("Introduzca el numero de iteraciones: "));
            int turns = Convert.ToInt32(PBTout.AskString("Introduzca el numero maximo de turnos: "));
            var trainer = new Trainer(decks[0], decks[1], iter, turns);
            var result = trainer.Train();
            return result;
        }


        public static int GetSelection(int size, string w2s)
        {
            System.Console.WriteLine("Choose a" + w2s);
            int choice = Convert.ToInt32(Console.ReadLine());

            while (!IsIndexOk(choice, size))
            {
                System.Console.WriteLine("Indexa bien comunista >=(");
                choice = Convert.ToInt32(Console.ReadLine());
            }

            return choice - 1;
        }
        private static bool IsIndexOk(int index, int size)
        {
            return !(index > size || index <= 0);
        }

    }
}