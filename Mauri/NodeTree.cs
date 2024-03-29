namespace Compiler
{
    public abstract class InstructionNode
    {
        public abstract void Run(Card mine, Card adv, Player P1, Player P2);
    }
    public abstract class ExpressionNode
    {

        public abstract int Valuate(Dictionary<string, int> stats, Dictionary<string, int> statsadv, Player P1, Player P2);

    }

    public class Error
    {
        string dialog;
        int line;

        public Error(string _dialog, int _line)
        {
            dialog = _dialog;
            line = 1 + _line - File.ReadAllLines("./default/defaultactions.txt").Length;
        }
        public void _Print()
        {
            PBTout.PBTPrint($" - error en la linea ({line}) : {dialog}", 40, "red");
        }
    }
    public class BinExp : ExpressionNode
    {
        ExpressionNode A;
        string OP;
        ExpressionNode B;

        public BinExp(ExpressionNode _A, ExpressionNode _B, string _mod)
        {
            A = _A;
            B = _B;
            OP = _mod;
        }
        public override int Valuate(Dictionary<string, int> stats, Dictionary<string, int> statsadv, Player P1, Player P2)
        {
            switch (OP)
            {
                case "+":
                    return A.Valuate(stats, statsadv, P1, P2) + B.Valuate(stats, statsadv, P1, P2);
                case "-":
                    return A.Valuate(stats, statsadv, P1, P2) - B.Valuate(stats, statsadv, P1, P2);
                case "*":
                    return A.Valuate(stats, statsadv, P1, P2) * B.Valuate(stats, statsadv, P1, P2);
                case "/":
                    return A.Valuate(stats, statsadv, P1, P2) / B.Valuate(stats, statsadv, P1, P2);
                case "<":
                    return B.Valuate(stats, statsadv, P1, P2) - A.Valuate(stats, statsadv, P1, P2);
                case ">":
                    return A.Valuate(stats, statsadv, P1, P2) - B.Valuate(stats, statsadv, P1, P2);
                case "=":
                    return Math.Min((A.Valuate(stats, statsadv, P1, P2) - B.Valuate(stats, statsadv, P1, P2)), (B.Valuate(stats, statsadv, P1, P2) - A.Valuate(stats, statsadv, P1, P2)));
                case "|":
                    return Math.Max(A.Valuate(stats, statsadv, P1, P2), B.Valuate(stats, statsadv, P1, P2));
                case "&":
                    return Math.Min(A.Valuate(stats, statsadv, P1, P2), B.Valuate(stats, statsadv, P1, P2));
                default:
                    return 1;
            }
        }
    }
    public class NumberExp : ExpressionNode
    {
        int value;

        public NumberExp(string _A)
        {
            value = Convert.ToInt32(_A);
        }
        public override int Valuate(Dictionary<string, int> stats, Dictionary<string, int> statsadv, Player P1, Player P2)
        {
            return value;
        }
    }
    public class VarExp : ExpressionNode
    {
        string name;
        bool self;
        public VarExp(string _A, bool _self)
        {
            name = _A;
            self = _self;
        }
        public override int Valuate(Dictionary<string, int> stats, Dictionary<string, int> statsadv, Player P1, Player P2)
        {
            if (self)
            {
                if (stats.ContainsKey(name))
                    return stats[name];
                else return -1;
            }
            else
            {
                if (statsadv.ContainsKey(name))
                    return statsadv[name];
                else return -1;
            };
        }
    }
    public class AllSelfExp : ExpressionNode
    {
        ExpressionNode E;

        public AllSelfExp(ExpressionNode _e)
        {
            E = _e;
        }
        public override int Valuate(Dictionary<string, int> stats, Dictionary<string, int> statsadv, Player P1, Player P2)
        {
            var list = new List<Dictionary<string, int>>();

            foreach (var item in P1.Field)
                if (!(item == null))
                    list.Add(item.Stats);

            foreach (var item in list)
            {
                if (E.Valuate(item, item, P1, P2) < 0)
                {
                    return -1;
                }
            }
            return 1;
        }
    }
    public class ExistSelfExp : ExpressionNode
    {
        ExpressionNode E;

        public ExistSelfExp(ExpressionNode _e)
        {
            E = _e;
        }
        public override int Valuate(Dictionary<string, int> stats, Dictionary<string, int> statsadv, Player P1, Player P2)
        {
            var list = new List<Dictionary<string, int>>();
            foreach (var item in P1.Field)
                if (!(item == null))
                    list.Add(item.Stats);

            foreach (var item in list)
            {
                if (E.Valuate(item, item, P1, P2) >= 0)
                {
                    return 1;
                }
            }
            return -1;
        }
    }

    public class AllAdvExp : ExpressionNode
    {
        ExpressionNode E;

        public AllAdvExp(ExpressionNode _e)
        {
            E = _e;
        }
        public override int Valuate(Dictionary<string, int> stats, Dictionary<string, int> statsadv, Player P1, Player P2)
        {
            var list = new List<Dictionary<string, int>>();

            foreach (var item in P2.Field)
                if (!(item == null))
                    list.Add(item.Stats);

            foreach (var item in list)
            {
                if (E.Valuate(item, item, P1, P2) < 0)
                {
                    return -1;
                }
            }
            return 1;
        }
    }
    public class ExistAdvExp : ExpressionNode
    {
        ExpressionNode E;

        public ExistAdvExp(ExpressionNode _e)
        {
            E = _e;
        }
        public override int Valuate(Dictionary<string, int> stats, Dictionary<string, int> statsadv, Player P1, Player P2)
        {
            var list = new List<Dictionary<string, int>>();

            foreach (var item in P2.Field)
                if (!(item == null))
                    list.Add(item.Stats);

            foreach (var item in list)
            {
                if (E.Valuate(item, item, P1, P2) >= 0)
                {
                    return 1;
                }
            }
            return -1;
        }
    }

    public class SetInstruction : InstructionNode
    {
        List<string> ToSet;
        ExpressionNode A;
        List<bool> self;

        public SetInstruction(List<string> _ToSet, ExpressionNode _A, List<bool> _self)
        {
            ToSet = _ToSet;
            A = _A;
            self = _self;
        }
        public override void Run(Card mine, Card adv, Player P1, Player P2)
        {
            int a = A.Valuate(mine.Stats, adv.Stats, P1, P2);

            for (int i = 0; i < ToSet.Count; i++)
            {
                if (self[i])
                {
                    if (mine.Stats.ContainsKey(ToSet[i]))
                        mine.Stats[ToSet[i]] = a;
                    else throw new Exception($"no se puede asignar {a} a {ToSet[i]} pq no existe en el contexto");
                }

                else
                {
                    if (adv.Stats.ContainsKey(ToSet[i]))
                        adv.Stats[ToSet[i]] = a;
                    else throw new Exception($"no se puede asignar {a} a {ToSet[i]} pq no existe en el contexto");
                }
            }
        }
    }

    public class AddInstruction : InstructionNode
    {
        Accion A;
        public AddInstruction(Accion _A)
        {
            A = _A;
        }
        public override void Run(Card mine, Card adv, Player P1, Player P2)
        {
            for (int i = 0; i < adv.Addings.Count; i++)
            {
                if (adv.Addings[i].Name == A.Name)
                {
                    adv.Addings[i].MCount(A.count);
                    return;
                }
            }
            adv.Addings.Add(A.NEW());
        }
    }
    public class IfInstruction : InstructionNode
    {
        ExpressionNode A;
        List<InstructionNode> If;
        List<InstructionNode> Else;

        public IfInstruction(ExpressionNode _A, List<InstructionNode> _If, List<InstructionNode> _Else)
        {
            A = _A;
            If = _If;
            Else = _Else;
        }

        public override void Run(Card mine, Card adv, Player P1, Player P2)
        {

            var thiscase = A.Valuate(mine.Stats, adv.Stats, P1, P2);
            if (thiscase >= 0)
            {
                foreach (var item in If)
                    item.Run(mine, adv, P1, P2);
            }
            else
            {
                foreach (var item in Else)
                    item.Run(mine, adv, P1, P2);
            }

        }
    }

    public class WhileInstruction : InstructionNode
    {
        ExpressionNode A;
        ExpressionNode B;
        List<InstructionNode> I;
        int whileLimit = 1000;
        int whileCount = 0;
        public WhileInstruction(ExpressionNode _A, List<InstructionNode> _I)
        {
            A = _A;
            B = _A;
            I = _I;
        }

        public override void Run(Card mine, Card adv, Player P1, Player P2)
        {
            whileCount++;
            if (whileCount > whileLimit)
            {
                whileCount = 0;
                //System.Console.WriteLine("Se llego al top del while");
                return;
            }
            if (A.Valuate(mine.Stats, adv.Stats, P1, P2) >= 0)
            {
                foreach (var item in I)
                    item.Run(mine, adv, P1, P2);
                Run(mine, adv, P1, P2);
            }
        }
    }
    public class ForInstruction : InstructionNode
    {
        ExpressionNode A;
        ExpressionNode B;
        List<InstructionNode> I;

        public ForInstruction(ExpressionNode _A, ExpressionNode _B, List<InstructionNode> _I)
        {
            A = _A;
            B = _B;
            I = _I;
        }

        public override void Run(Card mine, Card adv, Player P1, Player P2)
        {
            int a = A.Valuate(mine.Stats, adv.Stats, P1, P2);
            int b = B.Valuate(mine.Stats, adv.Stats, P1, P2);
            for (int i = Math.Min(a, b); i <= Math.Max(a, b); i++)
            {
                foreach (var item in I)
                    item.Run(mine, adv, P1, P2);
            }
        }
    }
}