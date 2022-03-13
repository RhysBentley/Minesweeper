using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralInfo : MonoBehaviour
{
    public bool isBomb;
    public int nearByBombs;
    public GameObject[] adjacentFields;
    public GameObject[] numbers;
    public GameObject searchedField;

    public void determineLeftClick()
    {
        if (nearByBombs > 0)
        {
            GameObject selectedNumber = numbers[nearByBombs - 1];
            GameObject number = Instantiate(selectedNumber, (gameObject.transform.position), Quaternion.identity);
            number.transform.Rotate(0,0,180);
            number.transform.Rotate(90,0,0);
            GameObject spanwedSearchedField = Instantiate(searchedField, (gameObject.transform.position), Quaternion.identity);
            Destroy(gameObject);
        }
        else
        {
            foreach (GameObject adjacentField in adjacentFields)
            {
                if (adjacentField != null)
                {
                    GameObject spanwedSearchedField = Instantiate(searchedField, (gameObject.transform.position), Quaternion.identity);
                    GeneralInfo adjacentFieldScript = adjacentField.GetComponent<GeneralInfo>();
                    adjacentFieldScript.determineLeftClick(); //Causes stack overflow since it is calling itself
                }
            }
            Destroy(gameObject);
        }
    }
}
