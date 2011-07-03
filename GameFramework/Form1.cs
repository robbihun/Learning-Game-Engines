using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GameFramework.Core;
using GameFramework.GameStates;
using Tao.DevIl;
using Tao.OpenGl;

namespace GameFramework
{
    public partial class Form1 : Form
    {
        private bool _fullscreen = false;
        private FastLoop _fastLoop;

        private StateManager _stateManager = new StateManager();
        private TextureManager _textureManager = new TextureManager();

        public Form1()
        {
            InitializeComponent();
            _openGlControl.InitializeContexts();

            // Set up all of our textures
            InitializeTextureManager();

            // Add all of our game states;
            InitializeStateManager();

            // Set up our game display
            SetUpDisplay();

            // Start our game loop
            _fastLoop = new FastLoop(GameLoop);
        }

        private void SetUpDisplay()
        {
            if(_fullscreen)
            {
                FormBorderStyle = FormBorderStyle.None;
                WindowState = FormWindowState.Maximized;
            }
            else
            {
                // Set the default Game Size
                ClientSize = new Size(1024, 768);
            }

            Setup2DGraphics(ClientSize.Width, ClientSize.Height);
        }

        private void InitializeStateManager()
        {
            _stateManager.AddState("splash", new SplashScreenState(_stateManager, 3, "title_menu"));
            _stateManager.AddState("title_menu", new TitleMenuState());
            _stateManager.AddState("sprite_test", new SpriteTestState(_textureManager));
            _stateManager.AddState("font_demo", new FontDemoState(_textureManager));
            _stateManager.AddState("fps", new FPSTestState(_textureManager));

            // Start the default game state
            _stateManager.ChangeState("fps");
        }

        private void InitializeTextureManager()
        {
            Il.ilInit();
            Ilu.iluInit();
            Ilut.ilutInit();
            Ilut.ilutRenderer(Ilut.ILUT_OPENGL);
            _textureManager.LoadTexture("face", "Assets//face.tif");
            _textureManager.LoadTexture("face_alpha", "Assets//face_alpha.tif");
            _textureManager.LoadTexture("font", "Assets//font.tga");
        }

        void GameLoop(double elapsedTime)
        {
            _stateManager.Update(elapsedTime);
            _stateManager.Render();
            _openGlControl.Refresh();
        }

        protected override void OnClientSizeChanged(EventArgs e)
        {
            base.OnClientSizeChanged(e);
            Gl.glViewport(0, 0, ClientSize.Width, ClientSize.Height);
            Setup2DGraphics(ClientSize.Width, ClientSize.Height);
        }

        private void Setup2DGraphics(double width, double height)
        {
            double halfWidth = width/2;
            double halfHeight = height/2;

            Gl.glMatrixMode(Gl.GL_PROJECTION);
            Gl.glLoadIdentity();
            Gl.glOrtho(-halfWidth, halfWidth, -halfHeight, halfHeight, -100, 100);
            Gl.glMatrixMode(Gl.GL_MODELVIEW);
            Gl.glLoadIdentity();
        }
    }
}
