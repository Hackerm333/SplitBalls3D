using UnityEditor;
using UnityEngine;

public class EditorTools : EditorWindow {


    [MenuItem("Tools/Reset PlayerPrefs")]
    public static void ResetPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
        Debug.Log("*************** PlayerPrefs Was Deleted ***************");
    }
}
