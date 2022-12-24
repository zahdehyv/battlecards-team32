using YUGIOH;

namespace Compiler
{

    public class Accion
    {

        public string Name { get; private set; }
        public string Description { get; private set; }
        public List<InstructionNode> Effects;
        public int toSelect { get; private set; }
        public Accion(string _name, int _toselect, List<InstructionNode> _effects, string _description)
        {
            Name = _name;
            toSelect = _toselect;
            Effects = _effects;
            Description = _description;
        }
        public void DoAct(Card Self, Card Target, Player P1, Player P2)
        {
            foreach (var item in Effects)
            {
                item.Run(Self.Stats, Target.Stats, P1, P2);
            }
        }
    }
}

