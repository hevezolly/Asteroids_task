using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Input Schema/Keyboard + Mouse")]
public class KeyboardMouseInputSchema : PlayerInputSchema
{

    private Camera camera;

    public override float GetAcceleration()
    {
        return (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow) || Input.GetMouseButton(1)) ? 1 : 0;
    }

    protected override void Init()
    {
        camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    public override Vector2 GetDisiredDirection()
    {
        var mousePosition = camera.ScreenToWorldPoint(Input.mousePosition);
        return (mousePosition - playerTransfrom.position).normalized;
    }

    public override bool GetShot()
    {
        return Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0);
    }
}
