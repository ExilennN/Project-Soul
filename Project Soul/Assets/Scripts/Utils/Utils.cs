using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public static class Utils
{
    public static int RoundedNumberFromFloatToInt(float number)
    {
        int floorValue = (int)number;
        float fractionalPart = number - floorValue;

        if (Mathf.Abs(fractionalPart) >= 0.4f && Mathf.Abs(fractionalPart) <= 0.6f)
        {
            return floorValue;
        }
        else
        {
            return 999999;
        }
    }
    public static void DeleteObjectsByTag(string tag)
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag(tag);
        foreach (GameObject obj in objects) { GameObject.Destroy(obj); }
    }
    public static TextMesh CreateWorldText(Transform parent, string text, Vector3 localPosition, Color color, Vector3 localScale, int fontSize = 40, TextAnchor textAnchor = TextAnchor.UpperLeft, TextAlignment textAlignment = TextAlignment.Left, int sortingOrder = 5000)
    {
        GameObject gameObject = new GameObject("World_Text", typeof(TextMesh));
        Transform transform = gameObject.transform;
        transform.SetParent(parent, false);
        transform.localPosition = localPosition;
        TextMesh textMesh = gameObject.GetComponent<TextMesh>();
        textMesh.anchor = textAnchor;
        textMesh.color = color;
        textMesh.alignment = textAlignment;
        textMesh.fontSize = fontSize;
        textMesh.text = text;
        textMesh.tag = "WorldTextDebug";
        textMesh.transform.localScale = localScale;
        textMesh.GetComponent<MeshRenderer>().sortingOrder = sortingOrder;
        return textMesh;
    }
    public static Vector3 GetMouseWorldPosition()
    {
        Vector3 vec = GetMouseWorldPositionWithZ(Input.mousePosition, Camera.main);
        vec.z = 0f;
        return vec;
    }

    private static Vector3 GetMouseWorldPositionWithZ(Vector3 screenPosition, Camera worldCamera)
    {
        Vector3 worldPos = worldCamera.ScreenToWorldPoint(screenPosition);
        return worldPos;
    }
}
