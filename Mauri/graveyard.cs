
   // public static int _GetValue(string value, Dictionary<string, int> stats)
        // {
        //     if (stats.ContainsKey(value))
        //     {
        //         return stats[value];
        //     }
        //     else
        //         return Convert.ToInt32(value);
        // }

        // Card _MakeCard(string cardpath)
        // {
        //     

        //     bool searchend = false;
        //     foreach (var item in texto)
        //     {
        //         var toks = item.Split();
        //         if (searchend)
        //         {
        //             if (item == "end")
        //             {
        //                 searchend = false;
        //             }
        //             continue;
        //         }

        //         if (toks[0] == "if")
        //         {
        //             string[] sub = new string[toks.Length - 1];
        //             Array.Copy(toks, 1, sub, 0, sub.Length);
        //             if (_If(sub, stats))
        //             {

        //             }else
        //             {
        //                 searchend = true;
        //             }

        //         }
        //         for (int i = 0; i < toks.Length; i++)
        //         {
        //             if (toks[i] == "=")
        //             {
        //                 string[] presub = new string[i];
        //                 string[] postsub = new string[toks.Length - (i + 1)];
        //                 StringBuilder possub = new StringBuilder();

        //                 Array.Copy(toks, 0, presub, 0, presub.Length);
        //                 Array.Copy(toks, i + 1, postsub, 0, postsub.Length);
        //                 foreach (var itemi in postsub)
        //                 {
        //                     possub.Append(itemi);
        //                 }
        //                 _Asignate(presub, possub.ToString(), stats);
        //                 break;
        //             }
        //         }
        //     }

        //     
        // }

        // bool _If(string[] tovaluate, Dictionary<string, int> stats)
        // {
        //     List<List<string>> eachboolexp = new List<List<string>>();
        //     eachboolexp.Add(new List<string>());
        //     List<string> bolians = new List<string>();
        //     string[] bol = { "or", "and" };
        //     int i = 0;
        //     foreach (var item in tovaluate)
        //     {
        //         if (bol.Contains(item))
        //         {
        //             bolians.Add(item);
        //             eachboolexp.Add(new List<string>());
        //             i++;
        //         }
        //         else
        //             eachboolexp[i].Add(item);
        //     }
        //     return _EvaluateBool(eachboolexp, bolians, stats);
        // }

        // bool _EvaluateBool(List<List<string>> exps, List<string> bolian, Dictionary<string, int> stats)
        // {
        //     if (bolian.Count == 0)
        //     {
        //         return _GetBool(exps[0], stats);
        //     }
        //     else
        //     {

        //         switch (bolian[0])
        //         {
        //             case "or":
        //                 List<string> retor = new List<string> { (_GetBool(exps[0], stats) || _GetBool(exps[1], stats)).ToString() };
        //                 bolian.RemoveAt(0); exps.RemoveAt(0); exps.RemoveAt(0);
        //                 exps.Insert(0, retor);
        //                 return _EvaluateBool(exps, bolian, stats);
        //             case "and":
        //                 List<string> retand = new List<string> { (_GetBool(exps[0], stats) && _GetBool(exps[1], stats)).ToString() };
        //                 bolian.RemoveAt(0); exps.RemoveAt(0); exps.RemoveAt(0);
        //                 exps.Insert(0, retand);
        //                 return _EvaluateBool(exps, bolian, stats);
        //             default:
        //                 return false;
        //         }
        //     }
        // }

        // bool _GetBool(List<string> toevaluate, Dictionary<string, int> stats)
        // {
        //     if (toevaluate.Count == 1)
        //     {
        //         switch (toevaluate[0])
        //         {
        //             case "True":
        //                 return true;
        //             case "False":
        //                 return false;
        //             default:
        //                 return false;
        //         }
        //     }
        //     else
        //     {
        //         int i = 0;
        //         string[] bol = { "is", "<", ">" };
        //         StringBuilder[] xps = new StringBuilder[2];
        //         xps[0] = new StringBuilder();
        //         xps[1] = new StringBuilder();
        //         string mod = " ";
        //         foreach (var item in toevaluate)
        //         {
        //             if (i > 1)
        //             {
        //                 System.Console.WriteLine("Te pasaste de operadores primo");
        //                 return false;
        //             }
        //             if (bol.Contains(item))
        //             {
        //                 mod = item;
        //                 i++;
        //             }
        //             else
        //                 xps[i].Append(item);
        //         }
        //         if (mod == " ")
        //         {
        //             return false;
        //         }
        //         return _BinBool(xps[0].ToString(), xps[1].ToString(), mod, stats);
        //     }
        // }
        // bool _BinBool(string a, string b, string mod, Dictionary<string, int> stats)
        // {
        //     switch (mod)
        //     {
        //         case "is":
        //             return _Arithmethic(a, stats) == _Arithmethic(b, stats);
        //         case ">":
        //             return _Arithmethic(a, stats) > _Arithmethic(b, stats);
        //         case "<":
        //             return _Arithmethic(a, stats) < _Arithmethic(b, stats);
        //         default:
        //             return false;
        //     }

        // }

        // void _Asignate(string[] asignto, string expression, Dictionary<string, int> stats)
        // {
        //     var a = _Arithmethic(expression, stats);

        //     foreach (var item in asignto)
        //     {
        //         stats[item] = Convert.ToInt32(a);
        //     }
        // }

        // int _Arithmethic(string expression, Dictionary<string, int> stats)
        // {
        //     StringBuilder expc = new StringBuilder(expression);
        //     string valid = "+-*/";
        //     string order1 = "*/";
        //     string order2 = "+-";
        //     // var mods=new List<string>();
        //     for (int i = 0; i < expc.Length; i++)
        //     {
        //         if (valid.Contains(expc[i]))
        //         {
        //             expc.Insert(i, ' ');
        //             expc.Insert(i + 2, ' ');
        //             i = i + 2;
        //         }
        //     }
        //     var ToCalc = new List<string>();
        //     ToCalc.AddRange(expc.ToString().Split());
        //     // foreach (var item in ToCalc)
        //     // {
        //     //     // System.Console.WriteLine(item);
        //     // }
        //     for (int i = 0; i < ToCalc.Count; i++)
        //     {
        //         if (order1.Contains(ToCalc[i]))
        //         {
        //             int a = _GetValue(ToCalc[i - 1], stats);
        //             int b = _GetValue(ToCalc[i + 1], stats);
        //             ToCalc[i] = _EvaluateAritmethic(a, b, ToCalc[i]).ToString();
        //             ToCalc.RemoveAt(i + 1);
        //             ToCalc.RemoveAt(i - 1);
        //             i = i - 2;
        //         }
        //     }
        //     for (int i = 0; i < ToCalc.Count; i++)
        //     {
        //         if (order2.Contains(ToCalc[i]))
        //         {
        //             int a = _GetValue(ToCalc[i - 1], stats);
        //             int b = _GetValue(ToCalc[i + 1], stats);
        //             ToCalc[i] = _EvaluateAritmethic(a, b, ToCalc[i]).ToString();
        //             ToCalc.RemoveAt(i + 1);
        //             ToCalc.RemoveAt(i - 1);
        //             i = i - 2;
        //         }
        //     }
        //     if (ToCalc.Count > 1)
        //     {
        //         return 0;
        //     }
        //     else
        //         return _GetValue(ToCalc[0], stats);
        // }



        // int _EvaluateAritmethic(int a, int b, string mod)
        // {
        //     switch (mod)
        //     {
        //         case "+":
        //             return a + b;
        //         case "-":
        //             return a - b;
        //         case "/":
        //             return a / b;
        //         case "*":
        //             return a * b;
        //         default:
        //             return 0;
        //     }
        // }


        

//Console.Beep(1000,1000);
//Parser ast= new Parser();
//System.Console.WriteLine(ast.ParsearExps("3<3+1").Valuate());


    // abstract class  Action
    // {
    //     virtual public void ActionExecution(int fil_target, int col_target){}
    // }    

 // public SimpleAttack(Card aEmisor, Board tablero){Emisor=aEmisor; Tablero=tablero;}