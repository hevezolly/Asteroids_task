using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Input Schema/Selected")]
public class SelectedInputSchema : PlayerInputSchema
{
    [SerializeField]
    private PlayerInputSchema currentSchema;

    public bool InputEnabled = true;

    public PlayerInputSchema CurrentSchema => currentSchema;

    public void SetSchema(PlayerInputSchema newSchema)
    {
        newSchema.Initiate(playerTransfrom);
        currentSchema = newSchema;
    }

    protected override void Init()
    {
        currentSchema.Initiate(playerTransfrom);
    }

    public override float GetAcceleration()
    {
        if (!InputEnabled)
            return 0;
        return currentSchema.GetAcceleration();
    }

    public override Vector2 GetDisiredDirection()
    {
        if (!InputEnabled)
            return Vector2.zero;
        return currentSchema.GetDisiredDirection();
    }

    public override bool GetShot()
    {
        if (!InputEnabled)
            return false;
        return currentSchema.GetShot();
    }
}
