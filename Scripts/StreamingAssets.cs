using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

namespace ImageLoader
{
    public static class StreamingAssets
    {
        public static Sprite GetSprite(string filePath)
        {
            try
            {
                byte[] imageBytes = GetFileBytes(GetImagePath(filePath));
                return imageBytes.ToSprite();
            }
            catch
            {
                Debug.LogError($"Failed to get image");
                return null;
            }
        }

        public static Sprite ToSprite(this byte[] data)
        {
            var tex = new Texture2D(2, 2);
            tex.LoadImage(data);
            var pivot = new Vector2(0.5f, 0.5f);
            var sprite = Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height), pivot, 100.0f);
            return sprite;
        }

        private static string GetImagePath(string imagePath)
        {
            try
            {
                return Path.Combine(Application.streamingAssetsPath, imagePath);
            }
            catch
            {
                Debug.LogError($"Failed to get image path");
                return null;
            }
        }

        private static Texture2D GetTextureByPath(string path)
        {
            try
            {
                return File.Exists(path) ? GetTexture(path) : null;
            }
            catch
            {
                Debug.LogError($"Failed to");
                return null;
            }
        }

        public static List<Sprite> GetSpritesFromContendFolder(string pathToContentFolder)
        {
            try
            {
                pathToContentFolder = GetImagePath(pathToContentFolder);
                List<Sprite> sprites = new List<Sprite>();

                if (Directory.Exists(pathToContentFolder))
                {
                    string[] pathsToImages = GetImagesPathFromContentFolder(pathToContentFolder);
                    foreach (string pathToImage in pathsToImages)
                    {
                        Texture2D texture = GetTextureByPath(pathToImage);
                        if (texture != null)
                            sprites.Add(GenerateSprite(texture));
                    }
                }

                return sprites;
            }
            catch
            {
                Debug.LogError($"Failed to get sprites from contend dolder: {pathToContentFolder}");
                return null;
            }
        }

        public static List<ImageFile> GetImageFilesFromContendFolder(string pathToContentFolder)
        {
            try
            {
                pathToContentFolder = GetImagePath(pathToContentFolder);
                List<ImageFile> imageFiles = new List<ImageFile>();

                if (Directory.Exists(pathToContentFolder))
                {
                    string[] pathsToImages = GetImagesPathFromContentFolder(pathToContentFolder);
                    foreach (string pathToImage in pathsToImages)
                    {
                        byte[] data = GetFileBytes(pathToImage);
                        imageFiles.Add(new ImageFile(data, pathToImage));
                    }
                }

                return imageFiles;
            }
            catch
            {
                Debug.LogError($"Failed to get ImageFile from contend folder: {pathToContentFolder}");
                return null;
            }
        }

        public static string[] GetImagesPathFromContentFolder(string pathContentFolder)
        {
            try
            {
                return Directory.GetFiles(pathContentFolder, "*.*").Where(str =>
                    str.EndsWith(".png") || str.EndsWith(".PNG") || str.EndsWith(".jpg") || str.EndsWith(".JPG") ||
                    str.EndsWith(".BMP") || str.EndsWith(".bmp")).ToArray();
            }
            catch
            {
                Debug.LogError($"Failed to get images path from content folder: {pathContentFolder}");
                return null;
            }
        }


        public static Sprite GenerateSprite(Texture2D texture)
        {
            try
            {
                Sprite sprite = Sprite.Create(texture, new Rect(0.0f, 0.0f, texture.width, texture.height),
                    new Vector2(0.5f, 0.5f), 100.0f);
                sprite.name = texture.name;
                return sprite;
            }
            catch
            {
                Debug.LogError($"Failed to generate sprite");
                return null;
            }
        }


        public static byte[] GetFileBytes(string fileFullPath)
        {
            try
            {
                byte[] fileBytes = null;
                if (File.Exists(fileFullPath))
                {
                    using FileStream fileStream = File.OpenRead(fileFullPath);
                    BinaryReader binaryReader1 = new(fileStream);
                    using BinaryReader binaryReader = binaryReader1;
                    fileBytes = binaryReader.ReadBytes((int) fileStream.Length);
                }

                return fileBytes;
            }
            catch
            {
                Debug.LogError($"Failed to get file bytes: {fileFullPath}");
                return null;
            }
        }

        public static Texture2D GetTexture(string url)
        {
            Texture2D result = new(2, 2)
            {
                name = Path.GetFileNameWithoutExtension(url)
            };
            try
            {
                return result.LoadImage(GetFileBytes(url)) ? result : result;
            }
            catch
            {
                return result;
            }
        }
    }
}