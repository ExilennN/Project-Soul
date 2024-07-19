using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FileNameAttribute : PropertyAttribute
{
    public string Extension { get; private set; }

    public FileNameAttribute(string extension = "*")
    {
        Extension = extension;
    }
}
