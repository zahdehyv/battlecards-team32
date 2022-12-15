using System;
using System.IO;
using System.Collections.Generic;
using YUGIOH;
using PBT;

namespace Compiler
{
    static class MazeCreator
    {
        public static List<Deck> _Recopilatory()
        {
            List<Deck> list = new List<Deck>();
            foreach (var item in _CargarIndices())
            {
                list.Add(_MakeDeck(item));
            }
            return list;
        }

        static string[] _CargarIndices()
        {
            // var decksfolder = Directory.GetDirectories("./decks");
            // List<string[]> cardsbydeck = new List<string[]>();
            // foreach (var item in decksfolder)
            // {
            //     cardsbydeck.Add(Directory.GetFiles(item));
            // }
            return Directory.GetDirectories("./decks");
        }
        static Deck _MakeDeck(string deckpath)
        {
            PBTout.PBTPrint($"Creando deck {Path.GetFileName(deckpath)}",100);
            var card = new List<YUGIOH.Card>();
            foreach (var item in Directory.GetFiles(deckpath))
            {
                card.Add(_MakeCard(item));
            }
            return new Deck(Path.GetFileName(deckpath), card);
        }

        static Card _MakeCard(string cardpath)
        {
            PBTout.PBTPrint($" Creando carta {Path.GetFileNameWithoutExtension(cardpath)}",70);
            var texto = File.ReadAllLines(cardpath).ToList();
            var defaultactions = File.ReadAllLines("./defaultactions/defaultactions.txt");
            for (int i = 0; i < defaultactions.Length; i++)
                texto.Insert(i, defaultactions[i]);

            var stats = new Dictionary<string, int>{
                    {"Life",1},
                    {"Attack",1},
                    {"Defense",1},
                    {"Speed",1}
                };
            List<Error> errors = new List<Error>();
            var a = Parser.ParsearInst(texto, stats, errors);
            var emptyplayer = new Player(new Deck("a", new List<Card>()));
            foreach (var item in a.Item1)
            {
                item.Run(stats, stats, emptyplayer, emptyplayer);
            }
            if (errors.Count != 0)
            {
                foreach (var item in stats.Keys)
                {
                    stats[item] = 0;
                }
                foreach (var item in errors)
                {
                    item._Print();
                }
                PBTout.PBTPrint($"* se ha creado la carta {Path.GetFileNameWithoutExtension(cardpath)} como carta de error",80);
                return new Card($"|||carta_error||| [{Path.GetFileNameWithoutExtension(cardpath)}]", stats, new List<Accion>());
            }
            return new Card(Path.GetFileNameWithoutExtension(cardpath), stats, a.Item2);
        }


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

    }

}
