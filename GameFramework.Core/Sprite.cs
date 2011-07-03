namespace GameFramework.Core
{
    public class Sprite
    {
        internal const int VertexAmount = 6;
        private Vector[] _vertexPositions = new Vector[VertexAmount];
        private Color[] _vertexColors = new Color[VertexAmount];
        private Point[] _vertexUVs = new Point[VertexAmount];
        private Texture _texture = new Texture();
        public Sprite()
        {
            InitVertexPositions(new Vector(0, 0, 0), 1, 1);
            SetColor(new Color(1, 1, 1, 1));
            SetUVs(new Point(0, 0), new Point(1, 1));
        }

        public Texture Texture
        {
            get { return _texture; }
            set 
            { 
                _texture = value;
                InitVertexPositions(GetCenter(), _texture.Width, _texture.Height);
            }
        }

        public Vector[] VertexPositions { get { return _vertexPositions; } }

        public Color[] VertexColors { get { return _vertexColors; } }

        public Point[] VertexUVs { get { return _vertexUVs; } }

        private Vector GetCenter()
        {
            double halfW = GetWidth()/2;
            double halfH = GetHeight()/2;

            return new Vector(_vertexPositions[0].X + halfW, _vertexPositions[0].Y - halfH, _vertexPositions[0].Z);
        }

        private void InitVertexPositions(Vector position, double width, double height)
        {
            double halfW = width/2;
            double halfH = height/2;

            _vertexPositions[0] = new Vector(position.X - halfW, position.Y + halfH, position.Z);
            _vertexPositions[1] = new Vector(position.X + halfW, position.Y + halfH, position.Z);
            _vertexPositions[2] = new Vector(position.X - halfW, position.Y - halfH, position.Z);

            _vertexPositions[3] = new Vector(position.X + halfW, position.Y + halfH, position.Z);
            _vertexPositions[4] = new Vector(position.X + halfW, position.Y - halfH, position.Z);
            _vertexPositions[5] = new Vector(position.X - halfW, position.Y - halfH, position.Z);
        }

        public double GetWidth()
        {
            return _vertexPositions[1].X - _vertexPositions[0].X;
        }

        public double GetHeight()
        {
            return _vertexPositions[0].Y - _vertexPositions[2].Y;
        }

        public void SetWidth(float width)
        {
            InitVertexPositions(GetCenter(), width, GetHeight());
        }

        public void SetHeight(float height)
        {
            InitVertexPositions(GetCenter(), GetWidth(), height);
        }

        public void SetPosition(double x, double y)
        {
            SetPosition(new Vector(x, y, 0));
        }

        public void SetPosition(Vector vector)
        {
            InitVertexPositions(vector, GetWidth(), GetHeight());
        }

        public void SetColor(Color color)
        {
            for (int i = 0; i < VertexAmount; i++)
            {
                _vertexColors[i] = color;
            }
        }

        public void SetUVs(Point topLeft, Point bottomRight)
        {
            _vertexUVs[0] = topLeft;
            _vertexUVs[1] = new Point(bottomRight.X, topLeft.Y);
            _vertexUVs[2] = new Point(topLeft.X, bottomRight.Y);

            _vertexUVs[3] = new Point(bottomRight.X, topLeft.Y);
            _vertexUVs[4] = bottomRight;
            _vertexUVs[5] = new Point(topLeft.X, bottomRight.Y);
        }

    }
}