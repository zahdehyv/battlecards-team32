using YUGIOH;
using Compiler;
using PBT;


namespace YUGIOH
{
    public static class Game
    {
        public static void Start()
        {
            var DECKS = MazeCreator._Recopilatory();//Cogiendo la lista de Decks

            System.Console.WriteLine("WELLCOME");
            System.Console.WriteLine();
            Console.ReadLine();
            // Dando la Bienvenida


            foreach (var deck in DECKS)//Showing Decks to choose
            {

                deck.ShowDeck();
            }

            var Deck1 = DECKS[GetSelection(DECKS.Count, "Deck for Player1")];//Getting Deck for Player1



            foreach (var deck in DECKS)//Showing Decks to choose
            {
                deck.ShowDeck();
            }

            var Deck2 = DECKS[GetSelection(DECKS.Count, "Deck for Player2")];//Getting Deck for Player2



            if (Deck1 == Deck2)//Caso base en que ambos escogieron el mismo Deck
            {
                Deck2 = (Deck)Deck1.Clone();
            }


            bool IsP1Virtual = false;
            System.Console.WriteLine("Es Player1 un Virtual Player? (Escribe algo si si)");//Se pregunta si es Virtal el Player1
            if (Console.ReadLine() != null) IsP1Virtual = true;


            bool IsP2Virtual = false;
            System.Console.WriteLine("Es Player2 un Virtual Player? (Escribe algo si si)");//Se pregunta si es Virtal el Player2
            if (Console.ReadLine() != null) IsP2Virtual = true;


            if(IsP1Virtual && IsP2Virtual) new Board(new VirtualPlayer(Deck1), new VirtualPlayer(Deck2)).Play();// Seleccionar si van a ser virtual player o no

            else if(!IsP1Virtual && IsP2Virtual) new Board(new Player(Deck1), new VirtualPlayer(Deck2)).Play();

            else if(IsP1Virtual && !IsP2Virtual) new Board(new VirtualPlayer(Deck1), new Player(Deck2)).Play();

            else if(!IsP1Virtual && !IsP2Virtual) new Board(new Player(Deck1), new Player(Deck2)).Play();
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
            if (index > size || index <= 0)
                return false;
            return true;
        }

    }
}