using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Camera playerCamera;

    // Update is called once per frame
    void Update()
    {
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
