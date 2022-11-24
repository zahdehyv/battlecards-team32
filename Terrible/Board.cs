using System;
using System.Collections.Generic;
namespace YUGIOH
{

    class Board
    {
        public Player P1;
        public Player P2;
        public string TextBox;

        public Board(Player P1, Player P2)
        {
            this.P1 = P1;
            this.P2 = P2;
            TextBox = "START";
        }

        // public void PlayRound()
        // {
        //     ShowBoard(true, false, "Player1 Turn");
        //     P1.ShowHand();
        //     System.Console.WriteLine("Select a card pressing a number");
        //     string s = System.Console.ReadLine();
        //     int selection = Convert.ToInt32(s);
        //     // if(Options(P1).Contains(selection))
        //     System.Console.WriteLine("Donde lo pongo");
        //     System.Console.WriteLine("Fila");
        //     int dim0 = Convert.ToInt32(System.Console.ReadLine());
        //     System.Console.WriteLine("Columna");
        //     int dim1 = Convert.ToInt32(System.Console.ReadLine());
        //     P1.PlayCard(selection,dim0,dim1);
        //     ShowBoard(true,false,"");


        // }

        // public void PlayRound(int player)
        // {
        //     bool bp = Convert.ToBoolean(player);

        //     var cPlayer = P1;
        //     if(bp == true)
        //         cPlayer = P2;

        //     int selected_card = GetSelection(cPlayer.Hand.Count(), "card", bp);
            
            
        // }

        private int GetSelection(int size, string w2s, bool bp)
        {
            ShowBoard(!bp,bp,"Choose a " + w2s);

            int choice = Convert.ToInt32(Console.ReadLine());

            while(!IsIndexOk(choice,size))
            {
                System.Console.WriteLine("Indexa bien comunista! >=(");
                choice = Convert.ToInt32(Console.Read());
            }

            return choice;
        }

        public List<int> Options(Player p)
        {
            List<int> options = new List<int>();
            for (int i = 1; i <= p.Hand.Count; i++)
            {
                options.Add(i);
            }
            return options;
        }

        private bool IsIndexOk(int index, int size)
        {
            if(index>size)
                return false;
            return true;
        }
        public bool End() { return (P1.Life <= 0 || P2.Life <= 0); }

        public void ShowHand(Player player, bool CanBeShown)
        {
            foreach (Card c in player.Hand)
            {
                string a = " | ";
                System.Console.Write(c.card_name + a);
            }
            System.Console.WriteLine();
        }

        public void ShowBoard(bool b1, bool b2, string text)
        {
            ShowHand(P1, b1);
            System.Console.WriteLine("-----------------------------------");
            ShowField();
            System.Console.WriteLine("-----------------------------------");
            ShowHand(P2, b2);
            System.Console.WriteLine();
            System.Console.WriteLine(text);

        }

        public void ShowField()
        {
            ShowPlayerField(P1);
            ShowPlayerField(P2);
        }

        public void ShowPlayerField(Player P)
        {
            var Field = P.Field;

            for (int i = 0; i < Field.GetLength(0); i++)
            {
                for (int j = 0; j < Field.GetLength(1); j++)
                {
                    if(!(Field[i, j] == null))
                        System.Console.Write(Field[i, j].card_name + " | ");
                    else
                        System.Console.Write("Empty");
                }
                System.Console.WriteLine();
            }
        }
    }
}