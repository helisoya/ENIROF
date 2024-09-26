using System.Collections;
using UnityEngine;

public class TriangleEye : Enemy
{
    protected override void Update()
    {
        base.Update();
        animator.SetBool("IsTeleporting", !waitNewDirection);
    }
}
