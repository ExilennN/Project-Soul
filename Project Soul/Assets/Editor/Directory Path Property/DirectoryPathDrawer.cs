using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;




[CustomPropertyDrawer(typeof(DirectoryAttribute))]
public class DirectoryPathDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        // Display the current file path
        string displayedPath = property.stringValue;
        if (displayedPath.StartsWith(Application.dataPath))
        {
            displayedPath = "Assets" + displayedPath.Substring(Application.dataPath.Length);
        }
        EditorGUI.TextField(new Rect(position.x, position.y, position.width - 30, position.height), label.text, displayedPath);

        // Add a button to open the file dialog
        if (GUI.Button(new Rect(position.xMax - 30, position.y, 30, position.height), "..."))
        {
            string path = EditorUtility.OpenFolderPanel("Select Folder", "Assets", "");
            if (!string.IsNullOrEmpty(path))
            {
                if (path.StartsWith(Application.dataPath))
                {
                    path = "Assets" + path.Substring(Application.dataPath.Length);
                }
                else
                {
                    Debug.LogWarning("Selected folder must be inside the Assets folder.");
                    path = property.stringValue; // Reset to previous value if not in Assets folder
                }
                property.stringValue = path;
            }
        }

        EditorGUI.EndProperty();
    }
}
