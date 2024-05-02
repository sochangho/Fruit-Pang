using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InputManager
{
    Transform m_Container;

#if UNITY_ANDROID && !UNITY_EDITOR
    IInputHandlerBase m_InputHandler = new TouchHandler();
#else
    IInputHandlerBase m_InputHandler = new MouseHandler();
#endif

    public InputManager(Transform container)
    {
        m_Container = container;
    }


    public bool IsTouchDown => m_InputHandler.isInputDown;
    public bool IsTouchUp => m_InputHandler.isInputUp;
    public Vector2 touchPosition => m_InputHandler.inputPosition;
    public Vector2 touch2BoardPostion => TouchToPostion(m_InputHandler.inputPosition);

    public Vector2 touch2WorldPostion => Touch2WorldPosition(m_InputHandler.inputPosition);



    Vector2 TouchToPostion(Vector3 vtInput)
    {

        Vector3 vtMousePos = Camera.main.ScreenToViewportPoint(vtInput); // 스크린(픽셀) -> 월드

        //월드 -> Container 로컬좌표로 변환
        Vector3 vtContainerLocal = m_Container.transform.InverseTransformPoint(vtMousePos);

        return vtContainerLocal;
    }

    Vector2 Touch2WorldPosition(Vector3 vtInput)
    {
        Vector3 worldPoint = Camera.main.ScreenToWorldPoint(vtInput);

        return worldPoint;
    }


    public Swipe EvalSwipeDir(Vector2 vtStart, Vector2 vtEnd)
    {
        return TouchEvaluator.EValSwipeDir(vtStart, vtEnd);
    }



}
