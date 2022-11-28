namespace YUGIOH
{
    // abstract public class Card
    // {
    //     public string card_name;
    //     public string description;
    //     public int cost;
    //     // public effect effect;

    //     virtual public bool IsDead(){System.Console.WriteLine("Abstract is dead"); return true;}
    //     virtual public void TakeDamage(int damage){System.Console.WriteLine("Abstract class can't take damage");}
    //     virtual public int Damage()
    //     {
    //         System.Console.WriteLine("Abstract method can't cause damage");
    //         return 0;
    //     }

    //     virtual public void WriteCard()
    //     {
    //         System.Console.WriteLine("Calling abstract WriteCard");
    //     }
    // }
    public class Card
    {
        public string Name;
        public Dictionary<string,int> Stats;
        public List<Action> Actions;
        // public int Life;
        // public int Attack;
        // public int Defense;
        // public int Speed;


        // mana/def

        public Card(string aName, int aLife, int aAttack, int aDefense, int aSpeed, List<Action> actions )
        {
            Name = aName;
            Stats = new Dictionary<string, int>
            {
                {"Life",aLife},
                {"Attack",aAttack},
                {"Defense",aDefense},
                {"Speed",aSpeed}
            };
            this.Actions = actions;
        }
        //  public Card(string aName, int aLife, int aAttack, int aDefense, int aSpeed )
        // {
        //     Name = aName;
        //     Stats = new Dictionary<string, int>
        //     {
        //         {"Life",aLife},
        //         {"Attack",aAttack},
        //         {"Defense",aDefense},
        //         {"Speed",aSpeed}
        //     };
        // }
        public Card(string aName, Dictionary<string,int> aStats, List<Action> actions)
        {
            Name = aName;
            Stats = aStats;
            this.Actions = actions;
        }
        // Dictionary<"string","int">

        // public void AddAction(Action a){ Actions.Add(a); }

        public bool IsDead(){ return Stats["Life"]<=0; }

        public void WriteCard()
        {
            System.Console.Write(Name + " ");
            string a = ": ";
            foreach(string s in Stats.Keys)
            {
                System.Console.Write(s + a + Stats[s] + " ");
            }
            System.Console.WriteLine();
        }

         public int Damage(){ return Stats["Attack"]; }

        public  void TakeDamage(int damage)
        {
            Stats["Life"] -= damage;
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
    // public class Card
    // {
    //     public string Name;
    //     public int Life;
    //     public int Atack;
    //     public int Defense;
    //     public int Speed;


    //     // mana/def

    //     public Card(string aName, string aDescription, int aStrength, int aEnergy)
    //     {
    //         Name = aName;
    //         description = aDescription;
    //         cost = aStrength + aEnergy;
    //         Life = aStrength;
    //         energy = aEnergy;
    //     }

    //     public override bool IsDead(){ return Life<=0; }

    //     override public void WriteCard()
    //     {
    //         System.Console.WriteLine(card_name +" Cost: " + cost + " Strength: " + Life + " Energy: " + energy + " " + description);
    //     }

    //     override public int Damage(){ return Life; }

    //     public override void TakeDamage(int damage)
    //     {
    //         Life -= damage;
    //     }

    // }

}