using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class DirectoryAttribute : PropertyAttribute { }
public class DirectoryReference : MonoBehaviour
{
    [Directory]
    public string directoryPath;
}
