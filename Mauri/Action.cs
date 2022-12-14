using YUGIOH;

namespace Compiler
{
    // abstract class  Action
    // {
    //     virtual public void ActionExecution(int fil_target, int col_target){}
    // }    

    public class Accion//:Action
    {
        // public SimpleAttack(Card aEmisor, Board tablero){Emisor=aEmisor; Tablero=tablero;}
        public string Name;
        public List<InstructionNode> Effects;
        public int toSelect;
        public Accion(string aName, int _toselect, List<InstructionNode> aEffects)
        {
            Name = aName;
            toSelect = _toselect;
            Effects = aEffects;
        }
        public void DoAct(Card Self, Card Target, Player P1, Player P2)
        {
            foreach (var item in Effects)
            {
                item.Run(Self.Stats, Target.Stats,P1, P2);
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