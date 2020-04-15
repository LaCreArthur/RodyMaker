using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RA_BtnMastico : MonoBehaviour, IDragHandler {

    private bool dragging;

    public Canvas parentCanvasOfImageToMove;
    Vector2 pos;

    public void OnDrag(PointerEventData eventData)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(parentCanvasOfImageToMove.transform as RectTransform, eventData.position, parentCanvasOfImageToMove.worldCamera, out pos);
        transform.position = parentCanvasOfImageToMove.transform.TransformPoint(pos);
    }
}