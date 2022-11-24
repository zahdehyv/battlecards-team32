using System;
using System.Collections.Generic;
namespace YUGIOH
{
    class Player
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

        public void ShowHand()
        {
            foreach(Card c in Hand)
                c.WriteCard();
        }

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