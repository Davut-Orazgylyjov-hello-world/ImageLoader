using UnityEditor;
using UnityEngine;

namespace ImageLoader
{
    public class PopupWindowLoadImages : EditorWindow
    {
        private string streamingAssetsPath = "FilesFolderName/NextFolder";

        [MenuItem("OrazgylyjovFuteres/LoadImages")]
        public static void ShowWindow()
        {
            PopupWindowLoadImages window = (PopupWindowLoadImages) GetWindow(typeof(PopupWindowLoadImages));
            window.minSize = new Vector2(300, 500);
            window.maxSize = new Vector2(600, 800);
        }

        private void OnGUI()
        {
            streamingAssetsPath = EditorGUILayout.TextField("streamingAssetsPath", streamingAssetsPath);
            if (GUILayout.Button("Load all Images"))
            {
                Debug.Log($"Start Loading Images: {streamingAssetsPath}");

                if (FindObjectsOfType(typeof(LoadedImages)) is not LoadedImages[] myItems)
                    Debug.LogError($"There should be a LoadedImages class in the scene, add it and try again.");
                else if (myItems.Length > 1)
                    Debug.LogError($"Founded {myItems.Length} instances. " +
                                   $"There should not be more than one instance of a class on the stage");
                else myItems[0].Load();
            }
        }
    }
}