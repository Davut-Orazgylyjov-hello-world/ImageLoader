using UnityEngine;

namespace ImageLoader
{
    public class ImageLoaderLoadingWindow : MonoBehaviour
    {
        [SerializeField] private GameObject loadingWindow;
        private bool _initialized;

        private void Awake() => Initialize(true);
        private void OnEnable() => Initialize(true);
        private void OnDisable() => Initialize(false);

        private void Initialize(bool active)
        {
            if (_initialized == active) return;

            if (active) StorageImages.OnLoading += SetActive;
            else StorageImages.OnLoading -= SetActive;

            _initialized = active;
        }

        private void SetActive(bool active) => loadingWindow.SetActive(active);
    }
}