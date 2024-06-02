using System.IO;
using UnityEngine;

namespace UnityFileSystem
{
    public static class StreamingAssets
    {
        public static Sprite GetSprite(string filePath)
        {
            try
            {
                byte[] imageBytes = GetFileBytes(GetImagePath(filePath));
                return ConvertBytesToSprite(imageBytes);
            }
            catch
            {
                Debug.LogError($"Failed to get image");
                return null;
            }
        }

        private static string GetImagePath(string imagePath)
        {
            return Path.Combine(Application.streamingAssetsPath, imagePath);
        }

        private static byte[] GetFileBytes(string filePath)
        {
            return File.ReadAllBytes(filePath);
        }
    
        private static Sprite ConvertBytesToSprite(byte[] data)
        {
            var tex = new Texture2D(2, 2);
            tex.LoadImage(data);
            var pivot = new Vector2(0.5f, 0.5f);
            var sprite = Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height), pivot, 100.0f);
            return sprite;
        }
    }
}