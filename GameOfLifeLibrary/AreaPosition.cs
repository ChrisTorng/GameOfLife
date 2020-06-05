using System;

namespace GameOfLife.Library
{
    public struct AreaPosition : IEquatable<AreaPosition>
    {
        public AreaPosition(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        public int X { get; }

        public int Y { get; }

        public AreaPosition GetOffsetPosition(int x, int y)
        {
            return this + new AreaPosition(x, y);
        }

        public override bool Equals(object obj)
        {
            return obj is AreaPosition position && this.Equals(position);
        }

        public bool Equals(AreaPosition other)
        {
            return this.X == other.X &&
                   this.Y == other.Y;
        }

        public override int GetHashCode()
        {
            int hashCode = 1861411795;
            hashCode = (hashCode * -1521134295) + this.X.GetHashCode();
            hashCode = (hashCode * -1521134295) + this.Y.GetHashCode();
            return hashCode;
        }

        public static bool operator ==(AreaPosition left, AreaPosition right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(AreaPosition left, AreaPosition right)
        {
            return !(left == right);
        }

        public static AreaPosition operator +(AreaPosition left, AreaPosition right)
        {
            return new AreaPosition(left.X + right.X, left.Y + right.Y);
        }

        public static AreaPosition Add(AreaPosition left, AreaPosition right)
        {
            return left + right;
        }
    }
}
