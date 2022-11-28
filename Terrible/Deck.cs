namespace YUGIOH
{
    public class Deck
    {
        public string Deckname;
        public List<Card> Cards;

        public Deck(string name,List<Card> aCards)
        {
            Deckname = name;//SEXO
            Cards = aCards;
        }

        public bool IsEmpty(){return !(Cards.Count() > 0);}

        public void Add(Card card)
        {
            Cards.Add(card);
        }

        public void Erase(string name)
        {
            for (int i = 0; i < Cards.Count; i++)
            {
                if (Cards[i].Name == name)
                {
                    Cards.RemoveAt(i);
                    return;
                }
            }
            System.Console.WriteLine("No existe una carta con ese nombre");
        }

        public void Erase(int index)
        {
            Cards.RemoveAt(index);
        }

        public void ShowDeck(bool AllInfo)
        {
            if (AllInfo)
            {
                foreach (Card c in Cards)
                {
                    c.WriteCard();
                }
            }
            else
            {
                foreach (Card c in Cards)
                {
                    System.Console.WriteLine(c.Name);
                }
            }
        }
    }
}