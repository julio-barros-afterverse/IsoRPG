namespace Model
{
    public class Hex
    {
        public readonly int r;
        public readonly int q;
        public readonly int s;

        public Hex(int r, int q)
        {
            this.r = r;
            this.q = q;
            this.s = -q - r;
        }
    }
}