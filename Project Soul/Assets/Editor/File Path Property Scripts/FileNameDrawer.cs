using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(FileNameAttribute))]
public class FileNameDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        FileNameAttribute fileNameAttribute = (FileNameAttribute)attribute;
        // Display the current file path
        string displayedPath = property.stringValue;
        if (displayedPath.StartsWith(Application.dataPath))
        {
            displayedPath = "Assets" + displayedPath.Substring(Application.dataPath.Length);
        }
        EditorGUI.TextField(position, label.text, displayedPath);

        // Add a button to open the file dialog
        if (GUI.Button(new Rect(position.xMax - 30, position.y, 30, position.height), "..."))
        {
            string path = EditorUtility.OpenFilePanel("Select File", Application.dataPath, fileNameAttribute.Extension);
            if (!string.IsNullOrEmpty(path))
            {
                if (path.StartsWith(Application.dataPath))
                {
                    path = "Assets" + path.Substring(Application.dataPath.Length);
                }
                property.stringValue = path;
            }
        }
        EditorGUI.EndProperty();
        
    }
}
