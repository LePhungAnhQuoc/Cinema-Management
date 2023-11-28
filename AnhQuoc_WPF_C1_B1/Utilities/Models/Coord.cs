namespace AnhQuoc_WPF_C1_B1
{
    class Coord
    {
        #region Properties
        public int X { get; set; }
        public int Y { get; set; }
        #endregion

        #region Constructors
        public Coord() { }
        public Coord(int x, int y)
        {
            X = x;
            Y = y;
        }
        #endregion
    }
}
