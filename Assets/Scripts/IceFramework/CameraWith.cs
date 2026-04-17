using UnityEngine;

public class CameraWith : MonoBehaviour
{
    public Transform TargetActor;

    private void LateUpdate()
    {
        if (TargetActor)
            transform.position = TargetActor.transform.position + Vector3.back;
    }
}
