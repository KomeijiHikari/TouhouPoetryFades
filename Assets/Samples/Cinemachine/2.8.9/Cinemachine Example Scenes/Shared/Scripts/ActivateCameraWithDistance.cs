using UnityEngine;

namespace Cinemachine.Examples
{

[AddComponentMenu("")] // Don't display in add component menu
public class ActivateCameraWithDistance : MonoBehaviour
{
    public GameObject obj;
    public float distanceToObject = 15f;
    public CinemachineVirtualCameraBase 先;
    public CinemachineVirtualCameraBase 后;
    
    CinemachineBrain brain;

    void Start()
        { 
        brain = Camera.main.GetComponent<CinemachineBrain>();
        SwitchCam(先);
    }

    // Update is called once per frame
    void Update()
    {

        if (obj && 后)
        {
            if (Vector3.Distance(transform.position, obj.transform.position) < distanceToObject)
            {
                SwitchCam(后);
            }
            else
            {
                SwitchCam(先);
            }
        } 
        }

    public void SwitchCam(CinemachineVirtualCameraBase vcam)
    {
        if (brain == null || vcam == null)
            return;
        if (brain.ActiveVirtualCamera != (ICinemachineCamera)vcam)
            vcam.MoveToTopOfPrioritySubqueue();      
    }
}

}