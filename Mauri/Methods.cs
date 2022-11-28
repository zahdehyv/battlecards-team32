using System;
using System.IO;
using System.Collections.Generic;
using YUGIOH;
using System.Text;

namespace Compiler
{
    class MazeCreator
    {
        Deck _MakeDeck(string deckpath)
        {
            var card = new List<YUGIOH.Card>();
            foreach (var item in Directory.GetFiles(deckpath))
            {
                card.Add(_MakeCard(item));
            }
            return new Deck(Path.GetFileName(deckpath), card);
        }
        public List<Deck> _Recopilatory()
        {
            List<Deck> list = new List<Deck>();
            foreach (var item in _CargarIndices())
            {
                list.Add(_MakeDeck(item));
            }
            return list;
        }
        Card _MakeCard(string cardpath)
        {
            var texto = File.ReadAllLines(cardpath);
            var stats = new Dictionary<string, int>{
                {"Life",1},
                {"Attack",1},
                {"Defense",1},
                {"Speed",1}
            };
            foreach (var item in texto)
            {
                var toks = item.Split();
                for (int i = 0; i < toks.Length; i++)
                {
                    if (toks[i] == "=")
                    {
                        string[] presub = new string[i];
                        string[] postsub = new string[toks.Length - (i + 1)];
                        StringBuilder possub = new StringBuilder();

                        Array.Copy(toks, 0, presub, 0, presub.Length);
                        Array.Copy(toks, i + 1, postsub, 0, postsub.Length);
                        foreach (var itemi in postsub)
                        {
                            possub.Append(itemi);
                        }
                        _Asignate(presub, possub.ToString(), stats);
                        break;
                    }
                }
            }

            return new Card(Path.GetFileNameWithoutExtension(cardpath), "asd", stats);
        }

        void _Asignate(string[] asignto, string expression, Dictionary<string, int> stats)
        {
            var a = _Arithmethic(expression, stats);

            foreach (var item in asignto)
            {
                stats[item] = Convert.ToInt32(a);
            }
        }
        public int _Arithmethic(string expression, Dictionary<string, int> stats)
        {
            StringBuilder expc = new StringBuilder(expression);
            string valid = "+-*/";
            // var mods=new List<string>();
            for (int i = 0; i < expc.Length; i++)
            {
                if (valid.Contains(expc[i]))
                {
                    expc.Insert(i, ' ');
                    expc.Insert(i + 2, ' ');
                    i = i + 2;
                }
            }
            var ToCalc = new List<string>();
            ToCalc.AddRange(expc.ToString().Split());
            foreach (var item in ToCalc)
            {
                // System.Console.WriteLine(item);
            }
            for (int i = 0; i < ToCalc.Count; i++)
            {
                if (valid.Contains(ToCalc[i]))
                {
                    int a = _GetValue(ToCalc[i - 1], stats);
                    int b = _GetValue(ToCalc[i + 1], stats);
                    ToCalc[i] = _Evaluate(a, b, ToCalc[i]).ToString();
                    ToCalc.RemoveAt(i + 1);
                    ToCalc.RemoveAt(i - 1);
                    i = i - 2;
                }
            }
            if (ToCalc.Count > 1)
            {
                return 0;
            }
            else
                return _GetValue(ToCalc[0], stats);
        }

        int _GetValue(string value, Dictionary<string, int> stats)
        {
            if (stats.ContainsKey(value))
            {
                return stats[value];
            }
            else
                return Convert.ToInt32(value);
        }

        int _Evaluate(int a, int b, string mod)
        {
            switch (mod)
            {
                case "+":
                    return a + b;
                case "-":
                    return a - b;
                case "/":
                    return a / b;
                case "*":
                    return a * b;
                default:
                    return 0;
            }
        }
        string[] _CargarIndices()
        {
            var decksfolder = Directory.GetDirectories("./decks");
            List<string[]> cardsbydeck = new List<string[]>();
            foreach (var item in decksfolder)
            {
                cardsbydeck.Add(Directory.GetFiles(item));
            }
            return decksfolder;
        }
    }

}