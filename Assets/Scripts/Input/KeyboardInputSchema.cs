using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Input Schema/Keyboard")]
public class KeyboardInputSchema : PlayerInputSchema
{
    public override float GetAcceleration()
    {
        return Mathf.Max(Input.GetAxisRaw("Vertical"), 0);
    }

    public override Vector2 GetDisiredDirection()
    {
        var axis = Input.GetAxisRaw("Horizontal");
        return playerTransfrom.right * axis;
    }

    public override bool GetShot()
    {
        return Input.GetKeyDown(KeyCode.Space);
    }
}
