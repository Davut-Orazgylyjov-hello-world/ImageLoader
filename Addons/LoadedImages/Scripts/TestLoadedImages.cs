using UnityEngine;

namespace ImageLoader
{
    public class TestLoadedImages : MonoBehaviour
    {
        [SerializeField] private string testFileName;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
                Debug.Log($"TestFile {LoadedImages.Singleton.storageImages.GetImageByName(testFileName).imagePath}");
        }
    }
}