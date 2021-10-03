using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Menu
{
    public class Quit : MonoBehaviour
    {
        public void QuitApp()
        {
            Application.Quit();
#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
#endif
        }
    }
}
