namespace YUGIOH
{
    // abstract class  Action
    // {
    //     virtual public void ActionExecution(int fil_target, int col_target){}
    // }    

    public class Action//:Action
    {
        // public SimpleAttack(Card aEmisor, Board tablero){Emisor=aEmisor; Tablero=tablero;}
        public string Name;
        public List<Effect> Effects;

        public Action(string aName,List<Effect> aEffects)
        {
            Name = aName;
            Effects = aEffects;
        }

        public void ShowAction()
        {
            System.Console.WriteLine(Name);
            foreach (Effect e in Effects)
            {
                System.Console.WriteLine(e.Stat + " " + e.Cant);
            }
        }
        public void SimpleAttack(Card emisor, Card receptor, Player cPlayer)
        {
            // bool bp = board.GetBooleanPlayer(cp);

            receptor.Stats["Life"] -= (emisor.Stats["Attack"] - receptor.Stats["Defense"]);
            System.Console.WriteLine(receptor.Stats["Life"]);
            System.Console.WriteLine(emisor.Stats["Attack"]);
            System.Console.WriteLine(receptor.Stats["Defense"]);
            receptor.WriteCard();
            // board.ShowBoard(bp, "SimpleAttack efectuado sobre" + receptor.Name);
            // if(rMana<eStrength)
            //     return rStrength -(eStrength-rMana) ;
            // return rStrength;
        }

        // public void DirectHit(int damage)
    }
}

//  class SimpleAttack//:Action
//     {
//         // public SimpleAttack(Card aEmisor, Board tablero){Emisor=aEmisor; Tablero=tablero;}
//         public void SimpleAttackCharacter(Board board, Card emisor, Card receptor, int cp)
//         {
//             bool bp = board.GetBooleanPlayer(cp);

//             receptor.strength -= emisor.strength;

//             receptor.WriteCard();
//             board.ShowBoard(!bp,bp,"SimpleAttack efectuado sobre" + receptor.card_name);
//             // if(rMana<eStrength)
//             //     return rStrength -(eStrength-rMana) ;
//             // return rStrength;
//         }

//         // public void DirectHit(int damage)
//     }
// }