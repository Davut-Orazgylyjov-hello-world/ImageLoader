using System;
using UnityEngine;

namespace ImageLoader
{
    [Serializable]
    public class ImageFile
    {
        public Sprite sprite;
        public string imagePath;
        private byte[] _data;

        public ImageFile(byte[] data, string imagePath)
        {
            _data = data;
            this.imagePath = imagePath;
            sprite = data.ToSprite();
        }
    }
}