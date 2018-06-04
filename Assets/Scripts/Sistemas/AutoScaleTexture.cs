using UnityEngine;
using System.Collections;

public class AutoScaleTexture : MonoBehaviour {
    public Texture2D myTexture;
    public float offsetX;
    public float offsetY;
    public int setNativeWidth;
    public int setNativeHeight;
    void OnGUI()
    {
        AutoResize(setNativeWidth, setNativeHeight);
        GUI.DrawTexture(new Rect(offsetX, offsetY, myTexture.width, myTexture.height), myTexture);
    }
    public static void AutoResize(int nativeWidth, int nativeHeight)
    {
        Vector2 resizeRatio = new Vector2((float)Screen.width / nativeWidth, (float)Screen.height / nativeHeight);
        Matrix4x4.TRS(Vector3.zero, Quaternion.identity, new Vector3(resizeRatio.x, resizeRatio.y, 1.0f));
    }
}
