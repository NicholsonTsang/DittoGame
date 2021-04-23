using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class Pointer : MonoBehaviour
{
    public Vector3 targetPosition;
    [SerializeField] public RectTransform pointerRectTransform;
    [SerializeField] public Camera uiCamera;
    public float boarder;
    public void Awake()
    {
        boarder = 50f;
        targetPosition = new Vector3(0, 0, 0);
        pointerRectTransform = transform.Find("Pointer").GetComponent<RectTransform>();
    }

    public void Update()
    {
        Vector3 toPosition = targetPosition;
        Vector3 fromPosition = Camera.main.transform.position;
        fromPosition.z = 0f;
        Vector3 dir = (toPosition - fromPosition).normalized;
        float angle = UtilsClass.GetAngleFromVectorFloat(dir);
       
        pointerRectTransform.localEulerAngles = new Vector3(0, 0, angle);

        Vector3 targetPositionScreenPoint = Camera.main.WorldToScreenPoint(targetPosition);
        bool isOffScreen = targetPositionScreenPoint.x <= boarder || targetPositionScreenPoint.x >= Screen.width - boarder || targetPositionScreenPoint.y <= boarder || targetPositionScreenPoint.y >= Screen.height - boarder;

        Debug.Log(isOffScreen);
       
        if (isOffScreen)
        {
            Vector3 cappedTargetScreenPosition = targetPositionScreenPoint;
            if (cappedTargetScreenPosition.x <= 0) cappedTargetScreenPosition.x = boarder;
            else if (cappedTargetScreenPosition.x >= Screen.width) cappedTargetScreenPosition.x = Screen.width - boarder;
            if (cappedTargetScreenPosition.y <= 0) cappedTargetScreenPosition.y = boarder;
            else if (cappedTargetScreenPosition.y >= Screen.height) cappedTargetScreenPosition.y = Screen.height - boarder;

            Vector3 pointerWorldPosition = uiCamera.ScreenToWorldPoint(cappedTargetScreenPosition);
            pointerRectTransform.position = pointerWorldPosition;
            pointerRectTransform.localPosition = new Vector3(pointerRectTransform.localPosition.x, pointerRectTransform.localPosition.y, 0f);
        }
        else
        {
            Vector3 pointerWorldPosition = uiCamera.ScreenToWorldPoint(targetPositionScreenPoint);
            pointerRectTransform.position = pointerWorldPosition;
            pointerRectTransform.localPosition = new Vector3(pointerRectTransform.localPosition.x, pointerRectTransform.localPosition.y, 0f);
        }

    }

    public void TargetUpdate(Vector3 pos)
    {
        targetPosition = pos;
    }

    public void show()
    {
        pointerRectTransform.gameObject.SetActive(true);
    }

    public void hide()
    {
        pointerRectTransform.gameObject.SetActive(false);
    }
}
