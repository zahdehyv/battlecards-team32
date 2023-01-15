namespace YUGIOH
{
    // HAND ATTRIBUTE NOT AVAILABLE
    public class Player : ICloneable
    {
        public string Name { get; private set; }
        public Card[] Field;

        public Deck Deck;

        public Player(Deck aDeck, string _name)
        {
            Deck = aDeck;
            Name = _name;
            // Hand = new List<Card>();
            Field = new Card[4];
        }


        public Player(Player player)
        {
            Name = player.Name;
            Field = new Card[4];
            for (int i = 0; i < player.Field.Count(); i++)
            {
                if (player.Field[i] != null)
                    Field[i] = (Card)player.Field[i].Clone();
            }
            Deck = new Deck(player.Deck.Deckname, player.Deck.Cards);

        }


        public Player(Deck aDeck, Card[] Field, string _name)
        {
            Name = _name;
            this.Deck = aDeck;
            this.Field = Field;
            // Hand = new List<Card>();
        }


        public Object Clone()
        {
            return new Player(this);
        }

        public virtual string PLAY(Player adversary, Board board)
        {
            board.UpdateBoard();

            PBTout.PrintField(adversary, this);
            PlayCard(adversary);
            List<Card> ATTACKERS = new List<Card>();
            foreach (var item in this.Field)
                if (item != null)
                    ATTACKERS.Add(item);

            while (ATTACKERS.Count > 0)
            {
            Back01:
                var caster = PBTout.GamePrompt<Card>($"{this.Name}, realice su jugada: ", (x => x.Name), ATTACKERS.ToArray());
                List<Accion> actions = new List<Accion>();
                actions.AddRange(caster.Actions);
                actions.Add(new Accion("Pasar", 1, new List<InstructionNode>(), ""));
                actions.Add(new Accion("Volver", 1, new List<InstructionNode>(), ""));
            Back02:
                var action = PBTout.GamePrompt<Accion>($"{this.Name}, seleccione la accion: ", (x => x.Name), actions.ToArray());

                if (action.Name == "Pasar")
                    goto None;
                if (action.Name == "Volver")
                    goto Back01;

                var objetiveplayer = PBTout.GamePrompt<Player>($"{this.Name}, a quien afectara la accion: ", (x => x.Name), new[] { adversary, this });
                List<Card> TARGETS = new List<Card>();

                foreach (var item in objetiveplayer.Field)
                    if (item != null)
                        TARGETS.Add(item);

                TARGETS.Add(new Card("Volver", 1, 1, 1, 1, new List<Accion>()));

                var target = PBTout.GamePrompt<Card>($"{this.Name}, seleccione al objetivo a {action.Name}: ", (x => x.Name), TARGETS.ToArray());
                if (target.Name == "Volver")
                    goto Back02;
                action.DoAct(caster, target, this, adversary);
                PBTout.PBTPrint($"{caster.Name} ha usado {action.Name} en {target.Name}", 200, "white");
                goto Some;
            None:
                PBTout.PBTPrint($"{caster.Name} ha saltado su turno", 200, "white");
            Some:
                Console.ReadKey(true);
                ATTACKERS.Remove(caster);
                board.UpdateBoard();
                PBTout.PrintField(adversary, this);
                if (board.End())
                {
                    Console.Clear();
                    return board.GetWinner();
                }
            }
            foreach (var item in this.Field)
            {
                if (item == null) continue;
                item.AddingsDo(this, adversary);
            }
            board.UpdateBoard();
            PBTout.PrintField(adversary, this);
            System.Console.WriteLine();
            System.Console.WriteLine();
            AnsiConsole.Markup($"[red]TERMINAR TURNO de {Name}[/]");
            Console.ReadKey(true);
            return adversary.PLAY(this, board);
        }


        public int GetFieldValue()
        {
            int val = 0;
            foreach (Card c in Field)
            {
                if (c != null) { val += c.GetCardValue(); }
            }
            return val;
        }


        public string WritePlayer(Board board)
        {
            if (this == board.P1)
                return ("Player1");
            else return ("Player2");
        }

        public void ExecuteAction(int CardIndex, int AccionIndex, int TargetIndex, Player TargetPlayer, Player OppossingPlayer, Board board)
        {
            Field[CardIndex].ExecuteAction(AccionIndex, TargetIndex, TargetPlayer, board.GetIntFromPlayer(OppossingPlayer), board.GetIntFromPlayer(this), board);
            // int AIndex, int Target,Player TargetPlayer, int OppossingPlayer, int CurrentPlayer, Board board
        }



        public void ExecuteAction(int CardIndex, int AccionIndex, int TargetIndex, Player OppossingPlayer)
        {
            Field[CardIndex].ExecuteAction(AccionIndex, TargetIndex, OppossingPlayer, this);
        }


        public Player GetPlayerFromInt(int p, Player oP)
        // Recibe un entero y devuelve el jugador al que esta haciendo referencia (0 se utiliza para player1 y cualquier otro numero para player2)
        {
            if (p == 0) return this;
            return oP;
        }


        public void ShowAllTheField(Player oP)
        {

            System.Console.WriteLine();
            System.Console.WriteLine("My Field");
            System.Console.WriteLine();
            ShowField();

            System.Console.WriteLine("Opponent Field");
            System.Console.WriteLine();
            oP.ShowField();
        }


        public void ShowField()
        {
            for (int i = 0; i < Field.Count(); i++)
            {
                System.Console.Write(i + 1 + " ");
                if (Field[i] != null) Field[i].WriteCard();
                else System.Console.WriteLine();
                System.Console.WriteLine();
            }
        }


        public virtual void PlayCard(Player adversary)
        {
            if (IsDeckEmpty() || IsFieldFull()) { return; }
            PBTout.ShowSummonable(Deck);
            var card = PBTout.GamePrompt<Card>($"{this.Name}, juegue una Carta: ", (x => x.Name), Deck.Cards.ToArray());
            PlayCardFromDeck(card, GetFreeSpace());
            PBTout.PBTPrint($"{Name} ha invocado a {card.Name}", 200, "white");
            Console.ReadKey(true);
            PBTout.PrintField(adversary, this);
            this.PlayCard(adversary);
            return;
        }


        public int GetSelection(int size, string w2s)
        {
            System.Console.WriteLine("Choose a " + w2s);
            var Entry = Console.ReadLine();
            int choice = 0;

            if (Entry != "")
            {
                choice = Convert.ToInt32(Entry);
            }

            while (!IsIndexOk(choice, size))
            {
                System.Console.WriteLine("Indexa bien comunista >=(");
                choice = Convert.ToInt32(Console.ReadLine());
            }

            return choice - 1;
        }


        private bool IsIndexOk(int index, int size)
        {
            if (index > size || index <= 0)
                return false;
            return true;
        }


        public void ShowDeck()
        {
            foreach (Card c in Deck.Cards)
            {
                c.WriteCard();
            }
        }


        public bool IsDeckEmpty() { return !(Deck.Cards.Count() > 0); }


        public bool IsFieldFull()
        {
            foreach (Card c in Field)
                if (c == null)
                    return false;
            return true;
        }


        public int GetFreeSpace()
        {
            for (int i = 0; i < Field.Count(); i++)
            {
                if (Field[i] == null) { return i; }
            }
            System.Console.WriteLine("Entro a GetFreeSpace con el campo lleno y no deberia, retorno 0");
            return 0;
        }


        public bool IsFieldEmpty()
        {
            foreach (Card c in Field)
                if (c != null)
                    return false;
            return true;
        }


        public void PlayCardFromDeck(Card card, int iField)
        {
            Field[iField] = card;
            Deck.Cards.Remove(card);
        }


    }
}