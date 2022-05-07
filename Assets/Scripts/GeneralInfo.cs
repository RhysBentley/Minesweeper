using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralInfo : MonoBehaviour
{
    public bool isBomb;
    public int nearByBombs = 0;
    public List<GameObject> adjacentFields = new List<GameObject>();
    public GameObject[] numbers;
    public GameObject searchedField;
    public int index;
    private List<GameObject> alreadySearchedFields = new List<GameObject>();
    private bool hasBeenSearched;
    private Material Mat;
    private bool isFlagged;

    public void determineLeftClick()
    {
        if (isFlagged == true)
        {
            return;
        }
        if (isBomb == true)
        {
            UnityEditor.EditorApplication.isPlaying = false;
        }
        if (nearByBombs > 0)
        {
            GameObject selectedNumber = numbers[nearByBombs - 1];
            GameObject number = Instantiate(selectedNumber, (gameObject.transform.position), Quaternion.identity);
            number.transform.Rotate(0,0,180);
            number.transform.Rotate(90,0,0);
            GameObject spawnedSearchedField = Instantiate(searchedField, (gameObject.transform.position), Quaternion.identity);
            Destroy(gameObject);
        }
        else
        {
            foreach (GameObject adjacentField in adjacentFields)
            {
                foreach (GameObject alreadySearchedField in alreadySearchedFields)
                {
                    if (alreadySearchedField == adjacentField)
                    {
                        hasBeenSearched = true;
                        return;
                    }
                }
                hasBeenSearched = false;
                setSearchedFields(adjacentField);
            }
            Destroy(gameObject);
        }
    }

    public void plantFlag()
    {
        MeshRenderer meshGameObject = gameObject.GetComponent<MeshRenderer>();
        if (meshGameObject.material.color != Color.green)
        {
            meshGameObject.material.color = Color.green;
            isFlagged = true;
        }
        else
        {
            meshGameObject.material.color = new Color (0.6415094f,0.6415094f,0.6415094f);
            isFlagged = false;
        }
    }

    public void setSearchedFields(GameObject adjacentField)
    {
        if (adjacentField != null)
        {
            if (hasBeenSearched == false)
            {
                alreadySearchedFields.Add(adjacentField);
                GameObject spanwedSearchedField = Instantiate(searchedField, (gameObject.transform.position), Quaternion.identity);
                GeneralInfo adjacentFieldScript = adjacentField.GetComponent<GeneralInfo>();
                adjacentFieldScript.determineLeftClick();
            }
        }
        else
        {
            Destroy(gameObject);
            GameObject spanwedSearchedField = Instantiate(searchedField, (gameObject.transform.position), Quaternion.identity);
        }
    }
}
