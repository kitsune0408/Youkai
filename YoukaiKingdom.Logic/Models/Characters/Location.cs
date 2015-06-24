namespace YoukaiKingdom.Logic.Models.Characters
{
    public struct Location
    {
        private const int DefaultPerimeterWidth = 0;

        private const int DefaultPerimeterHeight = 0;

        private const int DefaultFieldOfView = 0;

        public Location(double x, double y, int perimeterWidth = DefaultPerimeterWidth, int perimeterHeight = DefaultPerimeterHeight, int fieldOfView = DefaultFieldOfView)
            : this()
        {
            this.X = x;
            this.Y = y;
            this.PerimeterWidth = perimeterWidth;
            this.PerimeterHeight = perimeterHeight;
            this.FieldOfView = fieldOfView;
        }

        public double X { get; set; }

        public double Y { get; set; }

        public int PerimeterWidth { get; set; }

        public int PerimeterHeight { get; set; }

        public int FieldOfView { get; set; }
    }
}
