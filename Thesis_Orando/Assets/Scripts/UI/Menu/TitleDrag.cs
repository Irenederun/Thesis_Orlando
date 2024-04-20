using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;

public class TitleDrag : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    private RectTransform rectTransform;
    private Canvas canvas;
    private CanvasGroup canvasGroup;
    private Vector3 originalPosition;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();

        canvasGroup = GetComponentInParent<CanvasGroup>();

        originalPosition = rectTransform.position;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (canvasGroup != null)
            canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;

        /*eventData.delta：PointerEventData 中的属性，
        它表示当前帧中鼠标或触摸手指的移动量。它是一个二维向量，包含了鼠标或触摸手指在当前帧中相对于上一帧的位移量。
        所以，eventData.delta 表示了当前帧中鼠标或触摸手指的移动方向和距离。
        canvas.scaleFactor：这是 Canvas 的属性，表示了 Canvas 的缩放因子。
        Canvas 的缩放因子是一个浮点数，用于将屏幕坐标系转换为 Canvas 坐标系。
        通过除以 canvas.scaleFactor，可以将鼠标或触摸手指的移动量从屏幕坐标系转换为 Canvas 坐标系。*/

    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (canvasGroup != null)
            canvasGroup.blocksRaycasts = true;
        rectTransform.position = originalPosition;
    }

    /*在 UI 交互中，Raycast用来检测用户是否点击了屏幕上的 UI 元素。
    当用户点击屏幕时，游戏会发射一条射线从摄像机位置经过点击位置，然后检测这条射线是否与某个 UI 元素相交
    从而确定用户是否点击了该 UI 元素。*/
}