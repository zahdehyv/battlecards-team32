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

        public void Play()
        {
            int cRound = 1;
            while (!End())
            {
                System.Console.WriteLine();
                System.Console.WriteLine("ROUND " + cRound);
                System.Console.WriteLine();
                if (!P1.IsDeckEmpty()) { P1.Draw(0); }
                PlayCard(0);
                if (!P2.IsDeckEmpty()) { P2.Draw(0); }
                PlayCard(1);

                if (cRound != 1)
                {
                    ActionPhase(0);
                    ActionPhase(1);
                }
                cRound += 1;
            }
        }
        // public void PlayTurn(int player)
        // {
        //     PlayCard(player);
        // }

        public void PlayCard(int player)
        {
            bool bp = GetBooleanPlayer(player);

            var cPlayer = P1;
            if (bp) { cPlayer = P2; }

            ShowBoard(bp, "Write Something if you want to play a card");

            if (Console.ReadLine() != "")
            {


                // //Esta pincha no se porque la puse
                // foreach (Card c in cPlayer.Field)
                // {
                //     if (c == null)
                //         break;
                //     return;
                // }

                int selected_card = GetSelection(cPlayer.Hand.Count(), "card", bp);
                int selected_fil = 0;//GetSelection(cPlayer.Field.GetLength(0), "fila", bp);
                int selected_col = GetSelection(cPlayer.Field.GetLength(1), "columna", bp);

                while (!IsEmpty(cPlayer, selected_fil, selected_col))
                {
                    System.Console.WriteLine("Ese lugar esta ocupado");
                    selected_card = GetSelection(cPlayer.Hand.Count(), "card", bp);
                    selected_fil = GetSelection(cPlayer.Field.GetLength(0), "fila", bp);
                    selected_col = GetSelection(cPlayer.Field.GetLength(1), "columna", bp);
                }

                string text = "Carta " + cPlayer.Hand[selected_card].Name + " jugada a posicion: " + "[" + selected_fil + ", " + selected_col + "]";

                cPlayer.PlayCard(selected_card, selected_fil, selected_col);
                ShowBoard(bp, text);
            }

        }

        public void ActionPhase(int player)
        {
            var bp = GetBooleanPlayer(player);
            var Players = GetPlayers(bp);

            var cPlayer = Players.Item1;
            var oPlayer = Players.Item2;

            var bool_mask = GetFieldBooleanMask(cPlayer);

            while (!IsMaskComplete(bool_mask))
            {
                ShowBoard(bp, "Write something if you are going to take action Write nothing if you dont");

                if (Console.ReadLine() == "") return;

                // int selected_card = GetSelection(cPlayer.Hand.Count(), "attacking card", bp);
                int attackin_fil = 0;//GetSelection(cPlayer.Field.GetLength(0), "attacking card fila", bp);
                int attackin_col = GetSelection(cPlayer.Field.GetLength(1), "attacking card columna", bp);

                while (cPlayer.Field[attackin_fil, attackin_col] == null)
                {
                    attackin_fil = 0;//GetSelection(cPlayer.Field.GetLength(0), "attacking card fila", bp);
                    attackin_col = GetSelection(cPlayer.Field.GetLength(1), "attacking card columna", bp);
                }

                Card attackin_card = cPlayer.Field[attackin_fil, attackin_col];
                attackin_card.WriteCard();

                if (oPlayer.IsFieldEmpty()) oPlayer.Life -= cPlayer.Field[attackin_fil, attackin_col].Damage();

                int attacked_fil = 0;//GetSelection(cPlayer.Field.GetLength(0), "attacked card fila", !bp);
                int attacked_col = GetSelection(oPlayer.Field.GetLength(1), "attacked card columna", bp);

                while (oPlayer.Field[attacked_fil, attacked_col] == null)
                {
                    attacked_fil = 0;//GetSelection(cPlayer.Field.GetLength(0), "attacked card fila", bp);
                    attacked_col = GetSelection(cPlayer.Field.GetLength(1), "attacked card columna", bp);
                }

                Card attacked_card = oPlayer.Field[attacked_fil, attacked_col];

                // attacked_card.TakeDamage(attackin_card.Damage());
                // Action.SimpleAttack(attackin_card,attacked_card,cPlayer);

                attacked_card.WriteCard();

                UpdateBoard();
            }
        }

        public void UpdateBoard()
        {
            for (int i = 0; i < P1.Field.GetLength(0); i++)
            {
                for (int j = 0; j < P1.Field.GetLength(1); j++)
                {
                    if (P1.Field[i, j] != null)
                    {
                        if (P1.Field[i, j].IsDead())
                        {
                            P1.Field[i, j] = null;
                        }
                    }
                }
            }
            for (int i = 0; i < P2.Field.GetLength(0); i++)
            {
                for (int j = 0; j < P2.Field.GetLength(1); j++)
                {
                    if (P1.Field[i, j] != null)
                    {
                        if (P1.Field[i, j].IsDead())
                        {
                            P1.Field[i, j] = null;
                        }
                    }
                }
            }

            // foreach(Card c in P1.Field)
        }

        public bool IsMaskComplete(bool[,] mask)
        {
            foreach (bool b in mask)
                if (!b) { return b; }
            return true;
        }

        public bool[,] GetFieldBooleanMask(Player player)
        {
            var Mask = new bool[player.Field.GetLength(0), player.Field.GetLength(1)];

            for (int i = 0; i < player.Field.GetLength(0); i++)
            {
                for (int j = 0; j < player.Field.GetLength(1); j++)
                {
                    Mask[i, j] = player.Field[i, j] == null;
                }
            }
            return Mask;
        }
        public (Player, Player) GetPlayers(bool bp)
        {
            var cPlayer = P1;
            var oPlayer = P2;
            if (bp) { cPlayer = P2; oPlayer = P1; }

            return (cPlayer, oPlayer);
        }

        public bool GetBooleanPlayer(int player) { return Convert.ToBoolean(player); }

        public bool IsEmpty(Player P, int iDim0, int iDim1) { return (P.Field[iDim0, iDim1] == null); }

        public int GetSelection(int size, string w2s, bool bp)
        {
            ShowBoard(bp, "Choose a " + w2s);

            int choice = Convert.ToInt32(Console.ReadLine());

            while (!IsIndexOk(choice, size))
            {
                System.Console.WriteLine("Indexa bien comunista >=(");
                choice = Convert.ToInt32(Console.ReadLine());
            }

            return choice - 1;
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
            if (index > size)
                return false;
            return true;
        }

        public bool End() { return (P1.Life <= 0 || P2.Life <= 0); }



        public void ShowBoard(bool bp, string text)
        {
            Player cPlayer = P1;
            Player oPlayer = P2;
            if (bp) { cPlayer = P2; oPlayer = P1;}

            System.Console.WriteLine("----------------------------------------------------------------------");
            // ShowHand(P1, b1);
            System.Console.WriteLine("--------------------------------------------");
            ShowField();
            System.Console.WriteLine("--------------------------------------------");

            if (cPlayer == P1) { System.Console.WriteLine("Player1"); }
            else { System.Console.WriteLine("Player2"); }

            ShowHand(cPlayer);

            ShowFieldData(cPlayer,oPlayer);
            // ShowHand(P2, b2);
            System.Console.WriteLine(text);
        }
        public void ShowHand(Player player)
        {
            System.Console.WriteLine();
            System.Console.WriteLine("Hand:");
            foreach (Card c in player.Hand)
            {
                string a = " | ";
                System.Console.Write(a + c.Name + a);
            }
            System.Console.WriteLine();
            System.Console.WriteLine();
            foreach (Card c in player.Hand)
            {
                // string a = " | ";
                // System.Console.Write(a + c.Name + a);
                c.WriteCard();
            }
            System.Console.WriteLine();
        }

        public void ShowField()
        {
            ShowPlayerField(P1);
            System.Console.WriteLine("--------------------------------------------");
            ShowPlayerField(P2);
        }

        public void ShowFieldData(Player cPlayer, Player oPlayer)
        {
            System.Console.WriteLine("My Field:");
            for (int i = 0; i < cPlayer.Field.GetLength(0); i++)
            {
                for (int j = 0; j < cPlayer.Field.GetLength(1); j++)
                {
                    if (cPlayer.Field[i, j] != null)
                    {
                        System.Console.Write("[" + i + ", " + j + "] ");
                        cPlayer.Field[i, j].WriteCard();
                    }
                }
            }
            System.Console.WriteLine();
            System.Console.WriteLine("Opponent Field:");
            for (int i = 0; i < oPlayer.Field.GetLength(0); i++)
            {
                for (int j = 0; j < oPlayer.Field.GetLength(1); j++)
                {
                    if (oPlayer.Field[i, j] != null)
                    {
                        System.Console.Write("[" + i + ", " + j + "] ");
                        oPlayer.Field[i, j].WriteCard();
                    }
                }
            }
            System.Console.WriteLine();
        }

        public void ShowPlayerField(Player P)
        {
            var Field = P.Field;

            for (int i = 0; i < Field.GetLength(0); i++)
            {
                for (int j = 0; j < Field.GetLength(1); j++)
                {
                    if (!(Field[i, j] == null))
                        System.Console.Write(" | " + Field[i, j].Name + " | ");
                    else
                        System.Console.Write(" | " + "Empty" + " | ");
                }
                System.Console.WriteLine();
            }
        }
    }
}