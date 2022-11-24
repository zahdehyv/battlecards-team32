namespace YUGIOH
{
    abstract public class Card
    {
        public string card_name;
        public string description;
        public int cost;
        // public effect effect;

        virtual public void WriteCard()
        {
            System.Console.WriteLine("Calling abstract WriteCard");
        }
    }
    public class Character : Card
    {
        public int strength;
        // Atack/Vida
        public int energy;
        // mana/def

        public Character(string aName, string aDescription, int aStrength, int aEnergy)
        {
            card_name = aName;
            description = aDescription;
            cost = aStrength + aEnergy;
            strength = aStrength;
            energy = aEnergy;
        }

        override public void WriteCard()
        {
            System.Console.WriteLine(card_name +" Cost: " + cost + " Strength: " + strength + " Energy: " + energy + " " + description);
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