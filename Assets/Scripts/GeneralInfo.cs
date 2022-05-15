using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralInfo : MonoBehaviour
{
    ///Variables
    //Bomb Info
    public bool isBomb;
    public int nearByBombs = 0;
    public GameObject[] numbers;
    public float delayOfExplosion = 0.0f;
    public List<GameObject> alreadySearchedBombs = new List<GameObject>();

    //Index of the Minesweeper Script Array
    public int index;

    //Info about adjacentFields
    public GameObject searchedField;
    public List<GameObject> adjacentFields = new List<GameObject>();
    private List<GameObject> alreadySearchedFields = new List<GameObject>();
    private GeneralInfo adjacentFieldScript;
    private bool hasBeenSearched;
    private bool hasBeenRevealed;

    //Flagging the field
    private bool isFlagged;
    private Color originalColour;
    private Color flagColour = Color.red;

    //gameObject Info
    public MeshFilter unsearchedFieldMeshFilter;
    public Mesh searchedFieldMesh;
    private MeshRenderer meshGameObject;

    //Setting some information for later use
    void Start()
    {
        meshGameObject = gameObject.GetComponent<MeshRenderer>();
        originalColour = meshGameObject.material.color;
    }

    //Using when the field has been left clicked, call event is in PlayerController.cs
    public void determineLeftClick()
    {
        if (isFlagged == true)
        {
            return;
        }
        if (isBomb == true)
        {
            meshGameObject.material.color = Color.blue;
            checkForBombs();
            return;
        }
        if (nearByBombs > 0)
        {
            GameObject selectedNumber = numbers[nearByBombs - 1];
            GameObject number = Instantiate(selectedNumber, (gameObject.transform.position), Quaternion.identity);
            number.transform.Rotate(0,0,180);
            number.transform.Rotate(90,0,0);
            hasBeenRevealed = true;
            unsearchedFieldMeshFilter.mesh = searchedFieldMesh;
        }
        else
        {
            //Shows all other empty fields adjacent to this
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
                if (adjacentField != null)
                {
                    if (hasBeenSearched == false)
                    {
                        alreadySearchedFields.Add(adjacentField);
                        adjacentFieldScript = adjacentField.GetComponent<GeneralInfo>();
                        adjacentFieldScript.hasBeenRevealed = true;
                        adjacentFieldScript.unsearchedFieldMeshFilter.mesh = searchedFieldMesh;
                        adjacentFieldScript.determineLeftClick();
                    }
                }
            }
            unsearchedFieldMeshFilter.mesh = searchedFieldMesh;
        }
    }

    //Planting the flag, called from ther player controller
    public void plantFlag()
    {
        //Showing the flag
        if (meshGameObject.material.color != flagColour)
        {
            if (hasBeenRevealed == false)
            {
                meshGameObject.material.color = flagColour;
                isFlagged = true;
            }
        }
        //Hiding the flag
        else
        {
            meshGameObject.material.color = originalColour;
            isFlagged = false;
        }
    }

    public void checkForBombs()
    {
        foreach (GameObject adjacentField in adjacentFields)
        {
            if (adjacentField != null)
            {
                foreach (GameObject alreadySearchedBomb in alreadySearchedBombs)
                {
                    if (alreadySearchedBomb == adjacentField)
                    {
                        hasBeenSearched = true;
                        return;
                    }
                }
                hasBeenSearched = false;
                if (hasBeenSearched == false)
                {
                    alreadySearchedFields.Add(adjacentField);
                    MeshRenderer meshRenderer = adjacentField.GetComponent<MeshRenderer>();
                    adjacentFieldScript = adjacentField.GetComponent<GeneralInfo>();
                    if (adjacentFieldScript.isBomb == true)
                    {
                        meshRenderer.material.color = Color.green;
                    }
                    else
                    {
                        meshRenderer.material.color = Color.blue;
                    }
                    adjacentFieldScript.afterCheckBombs();
                }
            }
            else
            {
                return;
            }
        }
    }

    public void afterCheckBombs()
    {
        delayOfExplosion = delayOfExplosion + 0.2f;
        StartCoroutine(Wait(delayOfExplosion));
    }

    public IEnumerator Wait(float time)
    {
        yield return new WaitForSeconds(time);
        checkForBombs();
    }
}
