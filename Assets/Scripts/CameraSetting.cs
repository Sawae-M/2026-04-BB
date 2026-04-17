using Unity.Cinemachine;
using UnityEngine;

public class CameraSetting : MonoBehaviour
{
    [SerializeField] private CinemachineCamera backMirror;
    [SerializeField] private int activePriority = 20;
    [SerializeField] private int idlePriority = 5;
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
            backMirror.Priority = activePriority;

        if(Input.GetKeyUp(KeyCode.Space))
            backMirror.Priority = idlePriority;
    }
}
