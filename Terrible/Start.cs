using YUGIOH;
using Compiler;
using PBT;


namespace YUGIOH
{
    public static class Game
    {
        public static void Start()
        {
            var DECKS=MazeCreator._Recopilatory();

            System.Console.WriteLine("WELLCOME");
            System.Console.WriteLine();
            Console.ReadLine();
            System.Console.WriteLine();

            foreach (var deck in DECKS)
            {
                deck.ShowDeck();
            }

            var Deck1 = DECKS[GetSelection(DECKS.Count, "Deck for Player1")];


            foreach (var deck in DECKS)
            {
                deck.ShowDeck();
            }

            var Deck2 = DECKS[GetSelection(DECKS.Count, "Deck for Player2")];

// Seleccionar si van a ser virtual player o no
// Chequear las empty cards
            new Board(new Player(Deck1),new Player(Deck2)).Play();


        }


        
        public static int GetSelection(int size, string w2s)
        {

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