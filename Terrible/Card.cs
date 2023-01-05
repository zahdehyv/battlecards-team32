namespace YUGIOH
{

    public class Card : ICloneable
    {
        //ATRIBUTOS
        public string Name;
        public Dictionary<string, int> Stats;
        public List<Accion> Actions;

        public int MaxLife;


        public Card(string aName, int aLife, int aAttack, int aDefense, int aSpeed, List<Accion> actions)//Constructor con parametros
        {
            Name = aName;
            Stats = new Dictionary<string, int>
            {
                {"Life",aLife},
                {"Attack",aAttack},
                {"Defense",aDefense},
                {"Speed",aSpeed}
            };
            Actions = actions;
        }


        public Card(string aName, Dictionary<string, int> aStats, List<Accion> _actions)//Constructor con dictionary
        {
            Name = aName;
            Stats = aStats;
            Actions = _actions;
            MaxLife = aStats["Life"];
        }


        public Card(Card card)//Constructor del clon
        {
            Name = card.Name + " ";
            Stats = new Dictionary<string, int>();
            foreach (var k in card.Stats.Keys)
            {
                Stats[k] = card.Stats[k];
            }
            Actions = new List<Accion>();
            foreach (var item in card.Actions)
                Actions.Add(item);
        }


        public Object Clone()//Metodo para clonar
        {
            return new Card(this);
        }


        public void ExecuteAction(int AIndex, int Target, Player Oppossing, Player Current)
        {
            if (this != null && Oppossing.Field[Target]!=null)
                Actions[AIndex].DoAct(this, Oppossing.Field[Target], Oppossing, Current);
        }


        public void ExecuteAction(int AIndex, int Target, Player TargetPlayer, int OppossingPlayer, int CurrentPlayer, Board board)
        {
            if (AIndex >= 0)
            {
                if (this != null && TargetPlayer.Field[Target] != null)
                    Actions[AIndex].DoAct(this, TargetPlayer.Field[Target], board.GetPlayerFromInt(CurrentPlayer), board.GetPlayerFromInt(OppossingPlayer));
            }
        }



        public bool IsDead() { return Stats["Life"] <= 0; }
        //Comprueba si la carta se puede retirar del campo se usa en el metodo Board.UpdateBoard()


        public int GetCardValue()//Obtiene el valor de la carta sumando todas las estadisticas(mayores que 0)
        {
            int val = 0;
            if (Stats["Life"] > 0)
            {
                foreach (int i in Stats.Values)
                {
                    if (i > 0) val += i;
                }
            }
            return val;
        }


        public void WriteCard()//Se usa para imprimir la carta en consola
        {
            System.Console.Write("- " + Name + " ");
            if (!Name.StartsWith("|X|"))
            {
                foreach (string item in Stats.Keys)
                    System.Console.Write($"  {item}: {Stats[item]}  ");
                System.Console.WriteLine();
            }
        }


        public void WriteAccions()//Se usa para imprimir las acciones de la carta en consola
        {

            if (Actions.Count > 0)
            {
                System.Console.Write(" * Acciones: ");
                foreach (var item in Actions)
                    System.Console.Write($"{item.Name}, ");
                System.Console.WriteLine();
            }
        }


        public void SimpleAttack(Card c)//METODO OBSOLETO OJO
        {
            int damage = Stats["Attack"] - c.Stats["Defense"];
            if (damage > 0)
            {
                c.Stats["Life"] -= damage;
            }
        }


    }

}