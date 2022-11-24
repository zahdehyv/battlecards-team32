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
            return new Deck(Path.GetDirectoryName(deckpath), card);
        }

        Character _MakeCard(string cardpath)
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
                        Array.Copy(toks, 0, presub, 0, presub.Length);
                        Array.Copy(toks, i + 1, postsub, 0, postsub.Length);
                        _Asignate(presub, postsub.ToString(), stats);
                        break;
                    }
                }
            }

            return new Character(Path.GetFileNameWithoutExtension(cardpath), "def", 10, 10);
        }

        void _Asignate(string[] asignto, string expression, Dictionary<string, int> stats)
        {
            var a = _Arithmethic(expression, stats);
        }
        public double _Arithmethic(string expression, Dictionary<string, int> stats)
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
                System.Console.WriteLine(item);
            }
            for (int i = 0; i < ToCalc.Count; i++)
            {
                if (valid.Contains(ToCalc[i]))
                {
                    double a = _GetValue(ToCalc[i - 1], stats);
                    double b = _GetValue(ToCalc[i + 1], stats);
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
                return Convert.ToInt32(ToCalc[0]);
        }

        double _GetValue(string value, Dictionary<string, int> stats)
        {
            if (stats.ContainsKey(value))
            {
                return stats[value];
            }
            else
                return Convert.ToInt32(value);
        }

        double _Evaluate(double a, double b, string mod)
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
            var decksfolder = Directory.GetDirectories("../../decks");
            List<string[]> cardsbydeck = new List<string[]>();
            foreach (var item in decksfolder)
            {
                cardsbydeck.Add(Directory.GetFiles(item));
            }
            return decksfolder;
        }
    }

}