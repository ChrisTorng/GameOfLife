using System;

namespace GameOfLife.Library
{
    public struct AreaSize : IEquatable<AreaSize>
    {
        public AreaSize(int width, int height)
        {
            if (width <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(width));
            }

            if (height <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(height));
            }

            this.Width = width;
            this.Height = height;
        }

        public int Width { get; }

        public int Height { get; }

        public static AreaPosition GetOffsetPosition(int x, int y)
        {
            return new AreaPosition(x, y);
        }

        public bool InThisArea(AreaPosition position)
            => position.X >= 0 && position.X < this.Width &&
               position.Y >= 0 && position.Y < this.Height;

        public override bool Equals(object obj)
        {
            return obj is AreaSize size && this.Equals(size);
        }

        public bool Equals(AreaSize other)
        {
            return this.Width == other.Width &&
                   this.Height == other.Height;
        }

        public override int GetHashCode()
        {
            int hashCode = 859600377;
            hashCode = (hashCode * -1521134295) + this.Width.GetHashCode();
            hashCode = (hashCode * -1521134295) + this.Height.GetHashCode();
            return hashCode;
        }

        public static bool operator ==(AreaSize left, AreaSize right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(AreaSize left, AreaSize right)
        {
            return !(left == right);
        }

        public void ForEachPosition(Action<AreaPosition> action)
        {
            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            for (int x = 0; x < this.Width; x++)
            {
                for (int y = 0; y < this.Height; y++)
                {
                    action(new AreaPosition(x, y));
                }
            }
        }
    }
}
