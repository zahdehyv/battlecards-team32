namespace YUGIOH
{
    // HAND ATTRIBUTE NOT AVAILABLE
    public class Player
    {
        // public Deck Hand;
        public Card[] Field;
        public Deck Deck;

        public Player(Deck aDeck)
        {
            Deck = aDeck;
            // Hand = new Deck();
            Field = new Card[4];
        }

        public Player(Deck aDeck, Card[] Field)
        {
            this.Deck = aDeck;
            this.Field = Field;
            // Hand = new Deck();
        }

        public Player Copy()
        {
            return new Player(Deck,Field);
        }

        public int GetFieldValue()
        {
            int val = 0;
            foreach (Card c in Field)
            {
                if(c != null){ val += c.GetCardValue(); }
            }
            return val;
        }

        virtual public AccionIndex[] GetActions(Player oP)
        {
            AccionIndex[] ans = new AccionIndex[4];//Es 4 porque el terrno es de tamanno fijo 4

            for (int i = 0; i < 4; i++)
            {
                // System.Console.WriteLine("Escoge una accion para la carta " + i);

                System.Console.WriteLine("Escoge a quien atacara la carta " + (i+1));
                System.Console.WriteLine();
                System.Console.WriteLine("My Field");
                System.Console.WriteLine();
                ShowField();

                // int PlayerAffectedint = GetSelection(2, "Player to be affected");
                // // int Accion = GetSelection(2,"Accion to take") AQUI EL TIPO DEBE ESCOGER LA ACCION A EJECUTAR

                // Player PlayerAffected = P1;
                // if (PlayerAffectedint == 1) { PlayerAffected = P2; }

                System.Console.WriteLine("Opponent Field");
                System.Console.WriteLine();
                oP.ShowField();
                int CardIndex = GetSelection(4, "targeted card");

                ans[i] = new AccionIndex(oP,CardIndex);
            }

            return ans;
        }



        public void ShowField()
        {
            for (int i = 0; i < Field.Count(); i++)
            {
                System.Console.Write(i + 1 + " ");
                if(Field[i] != null) Field[i].WriteCard();
                else System.Console.WriteLine();
                System.Console.WriteLine();
            }
        }

        public void PlayCard()
        {
            if (IsDeckEmpty() || IsFieldFull()) { return; }
            ShowDeck();
            int s = GetSelection(Deck.Cards.Count, "a card to play");
            PlayCardFromDeck(s,GetFreeSpace());
            PlayCard();
            return;
        }

        public int GetSelection(int size, string w2s)
        {
            var Entry = Console.ReadLine();
            int choice = 0;
            
            if(Entry!=null){choice = Convert.ToInt32(Entry);}

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


        // public void ShowHand()
        // {
        //     foreach (Card c in Hand)
        //         c.WriteCard();
        // }

        // This Method Plays A card in the board it assumes the space is empty
        // public void PlayCardFromHand(int iHand, int iField0, int iField1)
        // {
        //     Field[iField0, iField1] = Hand[iHand];
        //     Hand.RemoveAt(iHand);
        // }

        public void PlayCardFromDeck(int iDeck, int iField)
        {
            Field[iField] = Deck.Cards[iDeck];
            Deck.Cards.RemoveAt(iDeck);
        }
        // public void Draw(int index)
        // {
        //     Hand.Add(Deck.Cards[index]);
        //     Deck.Erase(index);
        // }


        // public bool IsDeckEmpty(){return Deck.IsEmpty();}

        // public bool IsEmpty( int iDim0, int iDim1) { return (Field[iDim0, iDim1] == null); }

    }
}