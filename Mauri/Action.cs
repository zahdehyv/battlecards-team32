namespace Compiler
{

    public class Accion
    {

        public string Name { get; private set; }
        public string Description { get; private set; }
        public List<InstructionNode> Effects;
        public int count { get; private set; }

        public void MCount(int a)
        {
            count += a;
        }

        public Accion(string _name, int _count, List<InstructionNode> _effects, string _description)
        {
            Name = _name;
            count = _count;
            Effects = _effects;
            Description = _description;
        }

        public Accion NEW()
        {
            return new Accion(this.Name, this.count, this.Effects, this.Description);
        }
        public void DoAct(Card Self, Card Target, Player P1, Player P2)
        {
            foreach (var item in Effects)
            {
                item.Run(Self, Target, P1, P2);
            }
        }
    }
    // public class AccionAdd : Accion
    // {
    //     public AccionAdd(
    //         string _name,
    //      int _count, List<InstructionNode> _effects,
    //       string _description)
    //       : base(_name, _count, _effects, _description) { }
    // }
}

