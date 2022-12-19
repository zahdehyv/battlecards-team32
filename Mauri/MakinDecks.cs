using System;
using System.IO;
using System.Collections.Generic;
using YUGIOH;
using PBT;

namespace Compiler
{
    static class MazeCreator
    {
        static string DeckDir="./decks";
        static string DefActions="./defaultactions/defaultactions.txt";

        public static List<Deck> _Recopilatory()
        {
            List<Deck> list = new List<Deck>();
            var indexes = Directory.GetDirectories(DeckDir);
            foreach (var item in indexes)
                list.Add(_MakeDeck(item));
            return list;
        }
        static Deck _MakeDeck(string deckpath)
        {
            PBTout.PBTPrint($"Creando deck {Path.GetFileName(deckpath)}", 100,"cyan");
            var card = new List<Card>();
            foreach (var item in Directory.GetFiles(deckpath))
                card.Add(_MakeCard(item));
            return new Deck(Path.GetFileName(deckpath), card);
        }

        static Card _MakeCard(string cardpath)
        {
            PBTout.PBTPrint($" Creando carta {Path.GetFileNameWithoutExtension(cardpath)}", 70,"green");
            var texto = File.ReadAllLines(cardpath).ToList();
            var defaultactions = File.ReadAllLines(DefActions);
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
            var emptyplayer = new Player(new Deck("", new List<Card>()));
            
            foreach (var item in a.Item1)
                item.Run(stats, stats, emptyplayer, emptyplayer);

            if (errors.Count != 0)
            {
                foreach (var item in stats.Keys)
                    stats[item] = 0;
                foreach (var item in errors)
                    item._Print();
                PBTout.PBTPrint($"* se ha creado la carta {Path.GetFileNameWithoutExtension(cardpath)} como carta de error", 80,"gray");
                return new Card($"|X| [{Path.GetFileNameWithoutExtension(cardpath)}]", stats, new List<Accion>());
            }
            return new Card(Path.GetFileNameWithoutExtension(cardpath), stats, a.Item2);
        }
    }

}
