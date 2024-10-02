using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

public class FileNameAttribute : PropertyAttribute
{
    public string Extension { get; private set; }

    public FileNameAttribute(string extension = "*")
    {
        Extension = extension;
    }
}
