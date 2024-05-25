using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public float HorizontalInput { get; private set; }
    public bool IsJump { get; private set; }
    public bool IsShoot { get; private set; }
    public bool IsPause { get; private set; }
    public bool IsReloading { get; private set; }

    public void Update()
    {
        HorizontalInput = Input.GetAxisRaw(GlobalStringVariables.HORIZONTAL_AXIS);
        IsJump = Input.GetButtonDown(GlobalStringVariables.JUMP_BUTTON);
        IsShoot = Input.GetButtonDown(GlobalStringVariables.SHOOT_BUTTON);
        IsPause = Input.GetButtonDown(GlobalStringVariables.PAUSE_BUTTON);
        IsReloading = Input.GetButtonDown(GlobalStringVariables.RELOADING_BUTTON);
    }
}
