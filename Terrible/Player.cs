namespace YUGIOH
{
    public class Player
    {
        public List<Card> Hand;
        public Card[,] Field;
        public Deck Deck;
        public int Life;

        public Player(Deck aDeck, int aLife, int FirstDraw, int FieldDim0, int FieldDim1)
        {
            Deck = aDeck;

            Life = aLife;

            Hand = new List<Card>();
            for (int i = 0; i < FirstDraw; i++)
            {
               Draw(0); 
            }

            Field = new Card[FieldDim0,FieldDim1];
        }

        public bool IsDeckEmpty(){return Deck.IsEmpty();}

        // public bool IsEmpty( int iDim0, int iDim1) { return (Field[iDim0, iDim1] == null); }


        public bool IsFieldEmpty()
        {
            foreach(Card c in Field)
                if(c != null)
                    return false;
            return true;
        }


        public void ShowHand()
        {
            foreach(Card c in Hand)
                c.WriteCard();
        }

        // This Method Plays A card in the board it assumes the space is empty
        public void PlayCard(int iHand, int iField0, int iField1)
        {
            Field[iField0,iField1] = Hand[iHand];
            Hand.RemoveAt(iHand);
        }

        public void Draw(int index)
        {
            Hand.Add(Deck.Cards[index]);
            Deck.Erase(index);
        }

    }
}