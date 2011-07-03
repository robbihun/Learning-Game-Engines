using System;
using GameFramework.Core;
using Tao.OpenGl;

namespace GameFramework.GameStates
{
    public class FPSTestState : IGameState
    {
        private TextureManager _textureManager;
        private Font _font;
        private Text _fpsText;
        Renderer _renderer = new Renderer();
        FramesPerSecond _fps = new FramesPerSecond();

        public FPSTestState(TextureManager textureManager)
        {
            _textureManager = textureManager;
            _font = new Font(textureManager.GetTexture("font"), FontParser.ParseFile("Assets/font.fnt"));
            _fpsText = new Text("FPS: ", _font);
        }

        public void Update(double elapsedTime)
        {
            _fps.Process(elapsedTime);
        }

        public void Render()
        {
            Gl.glClearColor(0.0f, 0.0f, 0.0f, 1.0f);
            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT);
            _fpsText = new Text("FPS: " + _fps.CurrentFPS.ToString("00.0"), _font);
            _renderer.DrawText(_fpsText);
            _renderer.Render();
        }
    }
}