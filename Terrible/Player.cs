namespace YUGIOH
{
    // HAND ATTRIBUTE NOT AVAILABLE
    public class Player : ICloneable
    {
        public Card[] Field;

        public Deck Deck;

        public Player(Deck aDeck)
        {
            Deck = aDeck;
            // Hand = new List<Card>();
            Field = new Card[4];
        }


        public Player(Player player)
        {
            Field = new Card[4];
            for (int i = 0; i < player.Field.Count(); i++)
            {
                if (player.Field[i] != null)
                    Field[i] = (Card)player.Field[i].Clone();
            }
            Deck = new Deck(player.Deck.Deckname, player.Deck.Cards);

        }


        public Player(Deck aDeck, Card[] Field)
        {
            this.Deck = aDeck;
            this.Field = Field;
            // Hand = new List<Card>();
        }


        public Object Clone()
        {
            return new Player(this);
        }


        public int GetFieldValue()
        {
            int val = 0;
            foreach (Card c in Field)
            {
                if (c != null) { val += c.GetCardValue(); }
            }
            return val;
        }


        public string WritePlayer(Board board)
        {
            if (this == board.P1)
                return ("Player1");
            else return ("Player2");
        }


        virtual public AccionIndex[] GetActions(Player oP, int oPint, Board board)
        {
            AccionIndex[] ans = new AccionIndex[4];//Es 4 porque el terreno es de tamanno fijo 4

            if(this == board.P1)System.Console.WriteLine("Player1");
            else System.Console.WriteLine("Player2");

            for (int i = 0; i < 4; i++)
            {
                var CurrentCard = Field[i];

                if (CurrentCard != null)
                {

                    ShowAllTheField(oP);// Esto es para imprimir el tablero y las opciones
                    Console.ReadLine();
                    CurrentCard.WriteCard();
                    CurrentCard.WriteAccions();

                    int PlayerIndex = GetSelection(2, "Player to affect");//Here Iget the player that the accion is directed to


                    ShowAllTheField(oP);// Esto es para imprimir el tablero y las opciones
                    Console.ReadLine();
                    CurrentCard.WriteCard();
                    CurrentCard.WriteAccions();

                    int AccionIndex = GetSelection(5, "accion to take");// Here I get the accion to be executed


                    ShowAllTheField(oP);// Esto es para imprimir el tablero y las opciones
                    Console.ReadLine();

                    int CardIndex = GetSelection(4, "targeted card");//Here I get the Index of the card that will be affected


                    ans[i] = new AccionIndex(PlayerIndex, CardIndex, AccionIndex);//Here are created the AccionIndexes (PlayerIndex, CardIndex, AccionIndex)
                }
            }

            return ans;
        }


        public void ExecuteAction(int CardIndex,int AccionIndex, int TargetIndex, Player OppossingPlayer)
        {
            Field[CardIndex].ExecuteAction(AccionIndex,TargetIndex,OppossingPlayer,this);
        }


        public Player GetPlayerFromInt(int p, Player oP)
        // Recibe un entero y devuelve el jugador al que esta haciendo referencia (0 se utiliza para player1 y cualquier otro numero para player2)
        {
            if (p == 0) return this;
            return oP;
        }


        public void ShowAllTheField(Player oP)
        {

            System.Console.WriteLine();
            System.Console.WriteLine("My Field");
            System.Console.WriteLine();
            ShowField();

            System.Console.WriteLine("Opponent Field");
            System.Console.WriteLine();
            oP.ShowField();
        }


        public void ShowField()
        {
            for (int i = 0; i < Field.Count(); i++)
            {
                System.Console.Write(i + 1 + " ");
                if (Field[i] != null) Field[i].WriteCard();
                else System.Console.WriteLine();
                System.Console.WriteLine();
            }
        }


        public virtual void PlayCard()
        {
            if (IsDeckEmpty() || IsFieldFull()) { return; }
            ShowDeck();
            int s = GetSelection(Deck.Cards.Count, " card to play");
            PlayCardFromDeck(s, GetFreeSpace());
            PlayCard();
            return;
        }


        public int GetSelection(int size, string w2s)
        {
            System.Console.WriteLine("Choose a " + w2s);
            var Entry = Console.ReadLine();
            int choice = 0;

            if (Entry != "")
            {
                choice = Convert.ToInt32(Entry);
            }

            while (!IsIndexOk(choice, size))
            {
                System.Console.WriteLine("Indexa bien comunista >=(");
                choice = Convert.ToInt32(Console.ReadLine());
            }

            return choice - 1;
        }


        private bool IsIndexOk(int index, int size)
        {
            if (index > size || index <= 0)
                return false;
            return true;
        }


        public void ShowDeck()
        {
            foreach (Card c in Deck.Cards)
            {
                c.WriteCard();
            }
        }


        public bool IsDeckEmpty() { return !(Deck.Cards.Count() > 0); }


        public bool IsFieldFull()
        {
            foreach (Card c in Field)
                if (c == null)
                    return false;
            return true;
        }


        public int GetFreeSpace()
        {
            for (int i = 0; i < Field.Count(); i++)
            {
                if (Field[i] == null) { return i; }
            }
            System.Console.WriteLine("Entro a GetFreeSpace con el campo lleno y no deberia, retorno 0");
            return 0;
        }


        public bool IsFieldEmpty()
        {
            foreach (Card c in Field)
                if (c != null)
                    return false;
            return true;
        }


        public void PlayCardFromDeck(int iDeck, int iField)
        {
            Field[iField] = Deck.Cards[iDeck];
            Deck.Cards.RemoveAt(iDeck);
        }


    }
}