namespace YUGIOH
{
    public class Card
    {
        //por el momento no va a ser abstracta
        public string card_name;
        public string description;
        public Dictionary<string, int> stats;
        // public effect effect;

        public Card(string aName, string aDescription, Dictionary<string, int> aStats)
        {
            card_name = aName;
            description = aDescription;
            stats = aStats;
        }

        public void WriteCard()
        {
            System.Console.WriteLine(card_name);
            System.Console.WriteLine(" Life: " + stats["Life"]+" Attack: " + stats["Attack"] + " Defense: " + stats["Defense"] + " Speed: " + stats["Speed"]);
        }

    }

    // class EmptySpace:Card
    // {
    //     public EmptySpace()
    //     {
    //         card_name = "Empty Space";
    //         description = "Empty Space";

    //     }
    //     public override void WriteCard()
    //     {
    //         System.Console.WriteLine("Empty Space");
    //     }
    // }

}