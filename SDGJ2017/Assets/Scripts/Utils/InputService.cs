using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputService : MonoBehaviour
{

    private static float DeadZone = .1f;

    public static float GetHorizontal()
    {
        var xAxis = Input.GetAxis("Horizontal");
        ClampToDeadZone(ref xAxis);
        return xAxis;
    }

    public static bool JumpHold()
    {
        return Input.GetKey(KeyCode.Space);
    }

    public static bool JumpReleased()
    {
        return Input.GetKeyUp(KeyCode.Space);
    }

    public static bool JumpPressed()
    {
        return Input.GetKeyDown(KeyCode.Space);
    }

    private static void ClampToDeadZone(ref float value)
    {
        value = Mathf.Abs(value) > DeadZone ? value : 0;
    }

}
