namespace Compiler
{

    public static class Parser
    {
        static (string, bool) GetAsignable(string code)
        {
            if (code.StartsWith("adv."))
            {
                string build = "";
                for (int i = 4; i < code.Length; i++)
                    build += code[i];
                return (build, false);
            }
            else return (code, true);
        }

        static (List<string>, int, List<string>) BuildInst(List<string> code, int i, List<Error> errors, int startline)
        {
            List<string> INST = new List<string>();
            List<string> INSTELSE = new List<string>();
            bool elise = false;
            int balance = 0;
            int balanceifs = 1;
            for (int k = i + 1; k < code.Count; k++)
            {
                if (code[k].StartsWith("else"))
                {
                    balanceifs--;
                    if (balanceifs == 0)
                    {
                        elise = true;
                        continue;
                    }
                    else if (balanceifs < 0)
                        errors.Add(new Error($"desbalanceados los inicios y finales respecto a la instruccion padre en la linea ({i}) [else de mas]", startline + k));
                }
                else if (code[k].StartsWith("if")) balanceifs++;

                if (code[k].StartsWith("start"))
                {
                    balance++;
                    if (balance == 1)
                        continue;
                }
                if (code[k].StartsWith("end"))
                {
                    balance--;
                    if (balance == 0)
                    {
                        i = k;
                        break;
                    }
                    else if (balance < 0)
                        errors.Add(new Error($"desbalanceados los inicios y finales respecto a la instruccion padre en la linea ({i}) [end de mas]", startline + k));
                }
                if (elise)
                    INSTELSE.Add(code[k]);
                else INST.Add(code[k]);

            }

            if (balance > 0)
                errors.Add(new Error("la instruccion iniciada en esta linea llega al final del codigo sin cerrarse", startline + i));

            return (INST, i, INSTELSE);
        }
        public static (List<InstructionNode>, List<Accion>) ParsearInst(List<string> code, Dictionary<string, int> stats, List<Error> errors, int startline)
        {
            List<InstructionNode> _Inst = new List<InstructionNode>();
            List<Accion> _Act = new List<Accion>();
            List<Accion> _Add = new List<Accion>();

            //code.RemoveAll(x => ValidINST(x));

            for (int i = 0; i < code.Count; i++)
            {
                if (code[i] == "")
                    continue;

                //Console.WriteLine(100*Convert.ToDecimal(i)/Convert.ToDecimal(code.Count));
                var decode = code[i].Split();
                switch (decode[0])
                {
                    case "set":
                        List<string> ToSet = new List<string>();
                        List<bool> self = new List<bool>();
                        bool toexp = false;
                        string expA = "";
                        for (int j = 1; j < decode.Length; j++)
                        {
                            if (decode[j] == "to")
                            {
                                toexp = true;
                                continue;
                            }
                            if (toexp)
                            {
                                expA += decode[j];
                            }
                            else
                            {
                                var a = GetAsignable(decode[j]);
                                if (!stats.ContainsKey(a.Item1))
                                    stats.Add(a.Item1, 1);
                                ToSet.Add(a.Item1);
                                self.Add(a.Item2);

                            }
                        }
                        if (toexp)
                            _Inst.Add(new SetInstruction(ToSet, ParsearExps(expA, stats, errors, i), self));
                        else errors.Add(new Error($"no encontrado valor de asignacion en {code[i]}", startline + i));
                        break;
                    case "if":
                        expA = "";

                        for (int j = 1; j < decode.Length; j++)
                        {
                            expA += decode[j];
                        }
                        var BuiltInst = BuildInst(code, i, errors, startline);
                        int tempstart = i + 2;
                        i = BuiltInst.Item2;
                        var instrucciones = ParsearInst(BuiltInst.Item1, stats, errors, tempstart);
                        _Act.AddRange(instrucciones.Item2);
                        _Inst.Add(new IfInstruction(ParsearExps(expA, stats, errors, i), instrucciones.Item1, ParsearInst(BuiltInst.Item3, stats, errors, tempstart).Item1));
                        break;
                    case "while":
                        expA = "";
                        for (int j = 1; j < decode.Length; j++)
                            expA += decode[j];
                        BuiltInst = BuildInst(code, i, errors, startline);
                        tempstart = i + 2;
                        i = BuiltInst.Item2;
                        instrucciones = ParsearInst(BuiltInst.Item1, stats, errors, tempstart);
                        _Act.AddRange(instrucciones.Item2);
                        _Inst.Add(new WhileInstruction(ParsearExps(expA, stats, errors, i), instrucciones.Item1));
                        break;
                    case "for":
                        expA = "";
                        string expB = "";
                        bool second = false;
                        for (int j = 1; j < decode.Length; j++)
                        {
                            if (decode[j] == "to")
                            {
                                second = true;
                                continue;
                            }
                            if (second)
                                expB += decode[j];
                            else
                                expA += decode[j];
                        }

                        BuiltInst = BuildInst(code, i, errors, startline);
                        tempstart = i + 2;
                        i = BuiltInst.Item2;
                        instrucciones = ParsearInst(BuiltInst.Item1, stats, errors, tempstart);
                        _Act.AddRange(instrucciones.Item2);
                        _Inst.Add(new ForInstruction(ParsearExps(expA, stats, errors, i), ParsearExps(expB, stats, errors, i), instrucciones.Item1));

                        break;
                    case "add":
                        int toselect = 0;
                        if (decode[2].Length == decode[2].ToCharArray().ToList().Count(x => Char.IsDigit(x)))
                        {
                            toselect = Convert.ToInt32(decode[2]);
                        }
                        string desc = "";
                        for (int p = 3; p < decode.Length; p++)
                            desc += $"{decode[p]} ";
                        BuiltInst = BuildInst(code, i, errors, startline);
                        tempstart = i + 2;
                        i = BuiltInst.Item2;
                        instrucciones = ParsearInst(BuiltInst.Item1, stats, errors, tempstart);
                        _Inst.Add(new AddInstruction(new Accion(decode[1], toselect, instrucciones.Item1, desc)));
                        break;
                    case "def":
                        toselect = 0;
                        if (decode[2].Length == decode[2].ToCharArray().ToList().Count(x => Char.IsDigit(x)))
                        {
                            toselect = Convert.ToInt32(decode[2]);
                        }
                        desc = "";
                        for (int p = 3; p < decode.Length; p++)
                            desc += $"{decode[p]} ";
                        BuiltInst = BuildInst(code, i, errors, startline);
                        tempstart = i + 2;
                        i = BuiltInst.Item2;
                        instrucciones = ParsearInst(BuiltInst.Item1, stats, errors, tempstart);
                        _Act.Add(new Accion(decode[1], toselect, instrucciones.Item1, desc));
                        break;
                    case " ":
                        continue;
                    default:
                        errors.Add(new Error($"no reconocida la instruccion {code[i]}", startline + i));
                        //throw new Exception($"no reconocida la instruccion {code[i]}");
                        break;
                }
            }
            return (_Inst, _Act);
        }
        static bool CheckLogic(string code)
        {
            string[] valid = { "self.exist(", "self.all(", "adv.all(", "adv.exist(" };
            int a = 0;
            foreach (var item in code)
            {
                if ("()".Contains(item))
                    a++;
            }
            foreach (var item in valid)
            {
                if (code.StartsWith(item) && code.EndsWith(')') && a == 2)
                    return true;
            }
            return false;
        }

        static string GetLogic(string code)
        {
            string a = "";
            foreach (var item in code)
            {
                if (item == '(')
                {
                    return a;
                }
                a += item;
            }
            return a;
        }
        static string GetExpLog(string code)
        {
            string a = "";
            bool started = false;
            foreach (var item in code)
            {
                if (item == ')')
                    break;
                if (item == '(')
                {
                    started = true;
                    continue;
                }
                if (started)
                    a += item;
            }
            return a;
        }
        public static ExpressionNode ParsearExps(string code, Dictionary<string, int> stats, List<Error> errors, int line)
        {
            if (code.Length == code.ToCharArray().ToList().Count(x => Char.IsDigit(x)))
            {
                return new NumberExp(code);
            }
            else if (CheckLogic(code))
            {
                var logic = GetLogic(code);
                var explogic = ParsearExps(GetExpLog(code), stats, errors, line);
                switch (logic)
                {
                    case "self.exist":
                        return new ExistSelfExp(explogic);
                    case "self.all":
                        return new AllSelfExp(explogic);
                    case "adv.exist":
                        return new ExistAdvExp(explogic);
                    case "adv.all":
                        return new AllAdvExp(explogic);
                    default:
                        break;
                }
            }
            else
            {
                string[] MODS = { "|&", "<>=", "+-", "/*" };
                foreach (var item in MODS)
                {
                    bool parjump = false;
                    for (int i = code.Length - 1; i >= 0; i--)
                    {
                        switch (code[i])
                        {
                            case '(':
                                parjump = false;
                                break;
                            case ')':
                                parjump = true;
                                break;
                            default:
                                break;
                        }
                        if (parjump)
                            continue;
                        if (item.Contains(code[i]))
                        {
                            string A = "";
                            string B = "";
                            for (int j = 0; j < code.Length; j++)
                            {
                                if (j < i)
                                    A += code[j];
                                else if (j > i)
                                    B += code[j];
                            }
                            if (A == "")
                            {
                                A = "-1";
                                errors.Add(new Error($"se esperaba algo a la izquierda de {code[i]}", line));
                            }
                            if (B == "")
                            {
                                B = "-1";
                                errors.Add(new Error($"se esperaba algo a la derecha de {code[i]}", line));
                            }
                            return new BinExp(ParsearExps(A, stats, errors, line), ParsearExps(B, stats, errors, line), code[i].ToString());
                        }
                    }
                }
            }
            if (code == "" || code == " ")
            {
                errors.Add(new Error($"expresion vacia", line));
                return new NumberExp("-1");
            }
            else
            {
                var posVAR = GetAsignable(code);
                return new VarExp(posVAR.Item1, posVAR.Item2);
            }
        }
    }


    public static class PARSER
    {
        static (List<InstructionNode>, List<Accion>) Statements(List<string> code, Dictionary<string, int> stats, List<Error> errors)
        {
            for (int i = 0; i < code.Count; i++)
            {

            }
            throw new Exception();
        }
    }
}
