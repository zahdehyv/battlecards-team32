namespace YUGIOH
{
    public class Deck : ICloneable
    {
        public string Deckname;
        public List<Card> Cards;

        public Deck(string name, List<Card> aCards)
        {
            Deckname = name;
            Cards = aCards;
        }

        public Deck(Deck deck)
        {
            Deckname = deck.Deckname;
            Cards = new List<Card>();
            foreach (var item in deck.Cards)
            {
                Cards.Add((Card)item.Clone());
            }
        }

        public Object Clone()
        {
            return new Deck(this);
        }

        public bool IsEmpty() { return !(Cards.Count() > 0); }

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

        public void ShowDeck()
        {
            System.Console.WriteLine(Deckname);
            

            for (int i = 0; i < Cards.Count; i++)
            {
                if(Cards[i].Name.StartsWith("|X|"))
                {
                    Cards.RemoveAt(i);
                }
                
                if(i < Cards.Count)
                    Cards[i].WriteCard();
            }
        }
    }
}
