namespace YUGIOH
{
    public class BoardValue : ICloneable
    {
        public int cpValue;
        public int opValue;

        public BoardValue(int value1, int value2)
        {
            cpValue = value1;
            opValue = value2;
        }
        public BoardValue(BoardValue bv)
        {
            cpValue = bv.cpValue;
            opValue = bv.opValue;
        }

        public Object Clone()
        {
            return new BoardValue(this);
        }
    }
}