using System;
using GameFramework.Core;
using Tao.OpenGl;

namespace GameFramework.GameStates
{
    public class SpriteTestState : IGameState
    {
        private readonly Renderer _renderer = new Renderer();
        private readonly Sprite _testSprite = new Sprite();
        private readonly Sprite _testSprite2 = new Sprite();
        private readonly TextureManager _textureManager;

        public SpriteTestState(TextureManager textureManager)
        {
            _textureManager = textureManager;
            _testSprite.Texture = _textureManager.GetTexture("face_alpha");
            _testSprite.SetHeight(256*0.5f);

            _testSprite2.Texture = _textureManager.GetTexture("face_alpha");
            _testSprite2.SetPosition(-256, -256);
            _testSprite2.SetColor(new Color(1, 0, 0, 1));
        }
        public void Update(double elapsedTime)
        {
            
        }

        public void Render()
        {
            Gl.glClearColor(0.0f, 0.0f, 0.0f, 1.0f);
            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT);
            Gl.glBegin(Gl.GL_TRIANGLES);
            _renderer.DrawSprite(_testSprite);
            _renderer.DrawSprite(_testSprite2);
            Gl.glEnd();
        }
    }
}