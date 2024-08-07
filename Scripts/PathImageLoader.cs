using System;
using UnityEngine;
using UnityEngine.UI;

namespace ImageLoader
{
    [Serializable]
    public class PathImageLoader : MonoBehaviour
    {
        private enum UIType
        {
            Image,
            RawImage,
            SpriteRenderer
        }

        public static event Func<String, Sprite> GetSprite;

        [SerializeField] private UIType uiType;

        [Header("path/filename.png")] [SerializeField]
        private string imagePath;

        public bool activateAtStart;

        private void Start()
        {
            if (activateAtStart) Activate();
        }

        private void Activate()
        {
            Sprite sprite = null;
            
            //Take image from Func GetSprite
            if (GetSprite != null)
                sprite = GetSprite(StreamingAssets.CombineWithStreamingAssetsPath(imagePath));
            
            if(!sprite) 
                sprite = StreamingAssets.GetSprite(imagePath);
            
            if (!sprite) Debug.LogError($"Failed to get image for Object {transform.name}");

            switch (uiType)
            {
                case UIType.Image:
                {
                    var image = GetComponent<Image>();
                    image.sprite = sprite;
                    break;
                }
                case UIType.RawImage:
                {
                    var rawImage = GetComponent<RawImage>();
                    rawImage.texture = sprite.texture;
                    break;
                }
                case UIType.SpriteRenderer:
                {
                    var spriteRenderer = GetComponent<SpriteRenderer>();
                    spriteRenderer.sprite = sprite;
                    break;
                }
                default:
                    Debug.LogError($"The selected {uiType} type was not found on the object {transform.name}");
                    break;
            }
        }

        public void ChangeImagePath(string newPath)
        {
            imagePath = newPath;
            Activate();
        }
    }
}