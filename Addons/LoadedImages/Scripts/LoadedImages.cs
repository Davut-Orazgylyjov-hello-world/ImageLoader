using System;
using System.Collections.Generic;
using UnityEngine;

namespace ImageLoader
{
    public class LoadedImages : MonoBehaviour
    {
        public static LoadedImages Singleton { get; private set; }
        public StorageImages storageImages;

        private void Awake()
        {
            Singleton = this;
#if !UNITY_EDITOR
        Load();
#endif
        }

        private void OnEnable()
        {
            PathImageLoader.GetSprite += storageImages.GetSpriteByPath;
        }

        private void OnDisable()
        {
            PathImageLoader.GetSprite -= storageImages.GetSpriteByPath;
        }

        [ContextMenu("Load")]
        public void Load()
        {
            storageImages.LoadImages("Slides");
        }
    }

    [Serializable]
    public class StorageImages
    {
        public static event Action<bool> OnLoading;
        public List<ImageFile> imageFiles;

        public void LoadImages(string pathContentFolder)
        {
            OnLoading?.Invoke(true);
            imageFiles = new List<ImageFile>();
            imageFiles.AddRange(StreamingAssets.GetImageFilesFromContendFolder(pathContentFolder));
            OnLoading?.Invoke(false);
        }

        public ImageFile GetImageByPath(string path)
        {
            return imageFiles.Find(x => x.imagePath.Contains(path));
        }

        public ImageFile GetImageByName(string fileName)
        {
            return imageFiles.Find(x => System.IO.Path.GetFileName(x.imagePath).Contains(fileName));
        }

        public Sprite GetSpriteByPath(string path)
        {
            Sprite sprite = null;
            var imageFile = imageFiles.Find(x => x.imagePath.Contains(path));
            if (imageFile != null) sprite = imageFile.sprite;
            return sprite;
        }

        public Sprite GetSpriteByName(string fileName)
        {
            return imageFiles.Find(x => System.IO.Path.GetFileName(x.imagePath).Contains(fileName)).sprite;
        }
    }
}