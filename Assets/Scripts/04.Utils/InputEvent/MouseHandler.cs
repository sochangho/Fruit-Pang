using UnityEngine;

public class MouseHandler : IInputHandlerBase
{

    bool IInputHandlerBase.isInputDown => Input.GetButtonDown("Fire1");
    bool IInputHandlerBase.isInputUp => Input.GetButtonUp("Fire1");

    Vector2 IInputHandlerBase.inputPosition => Input.mousePosition;

   
}
