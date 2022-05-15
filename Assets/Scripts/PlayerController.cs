using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Camera playerCamera;

    void Update()
    {
        //Event happening on left mouse button
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit raycastHit))
            {
                GameObject hitGameObject = raycastHit.transform.gameObject;
                GeneralInfo hitGameObjectScript = hitGameObject.GetComponent<GeneralInfo>();
                hitGameObjectScript.determineLeftClick();
            }
        }
        //Event happening on right mouse button
        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit raycastHit))
            {
                GameObject hitGameObject = raycastHit.transform.gameObject;
                GeneralInfo hitGameObjectScript = hitGameObject.GetComponent<GeneralInfo>();
                hitGameObjectScript.plantFlag();
            }
        }
    }
}
