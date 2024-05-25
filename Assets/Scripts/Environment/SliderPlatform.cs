using UnityEngine;

[RequireComponent(typeof(SliderJoint2D))]
public class SliderPlatform : MonoBehaviour
{
    private SliderJoint2D joint;

    private JointLimitState2D savedState;

    private void Awake()
    {
        joint = GetComponent<SliderJoint2D>();
        savedState = JointLimitState2D.UpperLimit;
    }

    private void Update()
    {
        if (joint.limitState != savedState && joint.limitState != JointLimitState2D.Inactive)
        {
            var motor = joint.motor;
            motor.motorSpeed *= -1;
            joint.motor = motor;    
            savedState = joint.limitState;
        }
    }
}
