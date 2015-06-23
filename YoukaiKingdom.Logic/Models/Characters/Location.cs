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

        public Location(double x, double y, int perimeterWidth, int perimeterHeight, int fieldOfView)
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
