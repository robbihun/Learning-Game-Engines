using System;
using GameFramework.Core;
using Tao.OpenGl;

namespace GameFramework.GameStates
{
    public class FontDemoState : IGameState
    {
        private TextureManager _textureManager;
        private Font _font;
        private Text _helloWorld;
        Renderer _renderer = new Renderer();

        public FontDemoState(TextureManager textureManager)
        {
            _textureManager = textureManager;
            _font = new Font(textureManager.GetTexture("font"), FontParser.ParseFile("Assets/font.fnt"));
            _helloWorld = new Text("The quick brown fox jumps over the lazy dog", _font, new Color(1, 0, 0, 1), 400);
        }

        public void Update(double elapsedTime)
        {
            
        }

        public void Render()
        {
            Gl.glClearColor(0.0f, 0.0f, 0.0f, 1.0f);
            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT);
            _renderer.DrawText(_helloWorld);
        }
    }
}