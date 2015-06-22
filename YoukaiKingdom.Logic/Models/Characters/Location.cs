namespace YoukaiKingdom.Logic.Models.Characters
{
    public struct Location
    {
        public Location(double x, double y)
            : this()
        {
            this.X = x;
            this.Y = y;
        }

        public double X { get; set; }

        public double Y { get; set; }
    }
}
