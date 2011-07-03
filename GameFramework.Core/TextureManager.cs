using System;
using System.Collections.Generic;
using System.Diagnostics;
using Tao.DevIl;
using Tao.OpenGl;

namespace GameFramework.Core
{
    public class TextureManager : IDisposable
    {
        private Dictionary<string, Texture> _textureStore = new Dictionary<string, Texture>();

        public Texture GetTexture(string textureId)
        {
            Debug.Assert(TextureExists(textureId));
            return _textureStore[textureId];
        }

        public void LoadTexture(string textureId, string path)
        {
            Debug.Assert(!TextureExists(textureId));
            int devilId = 0;
            Il.ilGenImages(1, out devilId);
            Il.ilBindImage(devilId);

            if (!Il.ilLoadImage(path))
            {
                Debug.Assert(false, "Could not open file. [ " + path + "].");
            }

            Ilu.iluFlipImage();
            int width = Il.ilGetInteger(Il.IL_IMAGE_WIDTH);
            int height = Il.ilGetInteger(Il.IL_IMAGE_HEIGHT);
            int openGlId = Ilut.ilutGLBindTexImage();

            Debug.Assert(openGlId != 0);
            Il.ilDeleteImages(1, ref devilId);

            _textureStore.Add(textureId, new Texture(openGlId, width, height));
        }

        public void Dispose()
        {
            foreach (Texture tex in _textureStore.Values)
            {
                Gl.glDeleteTextures(1, new int[] { tex.Id });
            }
        }

        private bool TextureExists(string textureId)
        {
            return _textureStore.ContainsKey(textureId);
        }
    }
}