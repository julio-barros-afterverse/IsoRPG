using System;

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

        public static Hex operator -(Hex h1, Hex h2)
        {
            return new Hex(h1.r - h2.r, h1.q - h2.q);
        }

        public static int Distance(Hex h1, Hex h2)
        {
            var diff = h1 - h2;
            return (Math.Abs(diff.r) + Math.Abs(diff.q) + Math.Abs(diff.s))/2;
        }

        public static Hex Lerp(Hex source, Hex dest, float t)
        {
            return new Hex(Util.IntLerp(source.r, dest.r, t + 0.01f), Util.IntLerp(source.q, dest.q, t - 0.01f));
        }

        public override bool Equals(object obj)
        {
            if (obj is Hex h)
            {
                return h.r == this.r && h.q == this.q && h.s == this.s;
            }

            return false;
        }

        public override int GetHashCode()
        {
            return r + (q << 8) + (s << 16);
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}