using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class UI_FollowObj : MonoBehaviour {
    [SerializeField]
    Camera UI_Camera; // UI camera
    [SerializeField]
    RectTransform image; // UI element
    [SerializeField]
    GameObject obj; // 3D object
    [SerializeField]
    Canvas ui_Canvas;
    // Update is called once per frame
    void Update () {
        UpdateNamePosition();
    }
    /// <summary>
    ///Update image location
    /// </summary>
    void UpdateNamePosition()
    {
        Vector2 mouseDown = Camera.main.WorldToScreenPoint(obj.transform.position);
        Vector2 mouseUGUIPos = new Vector2();
        bool isRect = RectTransformUtility.ScreenPointToLocalPointInRectangle(ui_Canvas.transform as RectTransform, mouseDown, UI_Camera, out mouseUGUIPos);
        if (isRect)
        {
            image.anchoredPosition = mouseUGUIPos;
        }
    }
}