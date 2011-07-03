using System.Collections.Generic;

namespace GameFramework.Core
{
    public class Text
    {
        private Font _font;
        private List<CharacterSprite> _bitmapText = new List<CharacterSprite>();
        private string _text;
        private Color _color;
        private Vector _dimensions;
        int _maxWidth = -1;

        public List<CharacterSprite> CharacterSprites
        {
            get { return _bitmapText; }
        }

        public Text(string text, Font font)
            : this(text, font, new Color(1, 1, 1, 1))
        { }

        public Text(string text, Font font, Color color)
            : this(text, font, color, -1)
        { }

        public Text(string text, Font font, Color color, int maxWidth)
            : this(text, font, color, maxWidth, 0, 0)
        { }

        public Text(string text, Font font, Color color, int maxWidth, double x, double y)
        {
            _text = text;
            _font = font;
            _color = color;
            _maxWidth = maxWidth;
            CreateText(x, y);
        }

        private void CreateText(double x, double y)
        {
            CreateText(x, y, _maxWidth);
        }

        public void CreateText(double x, double y, double maxWidth)
        {
            _bitmapText.Clear();
            double currentX = 0;
            double currentY = 0;
            string[] words = _text.Split(' ');
            foreach (var word in words)
            {
                Vector nextWordLength = _font.MeasureFont(word);
                if (maxWidth != -1 && (currentX + nextWordLength.X) > maxWidth)
                {
                    currentX = 0;
                    currentY += nextWordLength.Y;
                }
                string wordWithSpace = word + " ";

                foreach (char c in wordWithSpace)
                {
                    var sprite = _font.CreateSprite(c);
                    float xOffset = ((float)sprite.Data.XOffset) / 2;
                    float yOffset = (((float)sprite.Data.Height) * 0.5f) + ((float)sprite.Data.YOffset);
                    sprite.Sprite.SetPosition(x + currentX + xOffset, y - currentY - yOffset);
                    currentX += sprite.Data.XAdvance;
                    _bitmapText.Add(sprite);
                }
            }

            _dimensions = _font.MeasureFont(_text);
            _dimensions.Y = currentY;
            SetColor(_color);
        }

        public void SetPosition(double x, double y)
        {
            CreateText(x, y);
        }

        public void SetColor(Color color)
        {
            _color = color;
            foreach (var characterSprite in _bitmapText)
            {
                characterSprite.Sprite.SetColor(_color);
            }
        }

        public double Width
        {
            get { return _dimensions.X; }
        }

        public double Height
        {
            get { return _dimensions.Y; }
        }
    }
}