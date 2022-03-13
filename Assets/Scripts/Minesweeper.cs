using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minesweeper : MonoBehaviour
{
    //Stating the variables
    public GameObject GameObjectToSpawn;
    private GameObject Clone;
    public int width;
    public int length;
    public float moveAmountX = -1;
    public float moveAmountZ = -1;
    private GameObject[] spawnedGameObjects;

    //Setting the camera position
    public Camera currentCamera;
    private float distanceOfCamera;

    //Setting the bombs
    public int numberOfBombs;
    private GameObject randomGameObject;
    private Material Mat;
    private int randomIndex;
    private GeneralInfo selectedBombScript;

    // At the start of the game
    void Start()
    {
        //Setting some basic variables for future methods
        float posX = 0;
        float posZ = 0;
        int addIndex = 0;
        //Setting the length of the array
        spawnedGameObjects = new GameObject[width * length];
        //Spawning the different objects at different positions based on the loop's value
        for (int i = 0; i < length; i++)
        {
            for (int j = 0; j < width; j++)
            {
                spawnGameObject(posX,posZ,addIndex + j);
                spawnedGameObjects[addIndex + j] = Clone;
                posX = posX + moveAmountX;
            }
            posX = 0;
            posZ = posZ + moveAmountZ;
            addIndex = addIndex + width;
        }
        //Setting the camera's positon above and in the middle of the objects
        GameObject lastGameObject = spawnedGameObjects[spawnedGameObjects.Length - 1];
        Vector3 halfWayPoint = lastGameObject.transform.position / 2;
        currentCamera.transform.position = halfWayPoint + new Vector3(0,length,0);

        //Finding and setting random bombs based on the value given
        for (int i = 0; i < numberOfBombs; i++)
        {
            findRandomGameObject();
        }

        //Setting the nearby bombs values on the GeneralInfo script on the fields depending on the amount of bombs around that field
        foreach (GameObject spawnedGameObject in spawnedGameObjects)
        {
            GeneralInfo currentGameObjectScript = spawnedGameObject.GetComponent<GeneralInfo>();
            if (currentGameObjectScript.isBomb != true)
            {
                int numberOfAdjacentBombs = 0;
                int index = System.Array.IndexOf(spawnedGameObjects, spawnedGameObject);
                currentGameObjectScript.adjacentFields = new GameObject[8];
                numberOfAdjacentBombs = numberOfAdjacentBombs + checkForBombs(index - width - 1, spawnedGameObject, 0);
                numberOfAdjacentBombs = numberOfAdjacentBombs + checkForBombs(index - width, spawnedGameObject, 1);
                numberOfAdjacentBombs = numberOfAdjacentBombs + checkForBombs(index - width + 1, spawnedGameObject, 2);
                numberOfAdjacentBombs = numberOfAdjacentBombs + checkForBombs(index - 1, spawnedGameObject, 3);
                numberOfAdjacentBombs = numberOfAdjacentBombs + checkForBombs(index + 1, spawnedGameObject, 4);
                numberOfAdjacentBombs = numberOfAdjacentBombs + checkForBombs(index + width - 1, spawnedGameObject, 5);
                numberOfAdjacentBombs = numberOfAdjacentBombs + checkForBombs(index + width, spawnedGameObject, 6);
                numberOfAdjacentBombs = numberOfAdjacentBombs + checkForBombs(index + width + 1, spawnedGameObject, 7);
                if (numberOfAdjacentBombs > 0)
                {
                    currentGameObjectScript.nearByBombs = numberOfAdjacentBombs;
                }
            }
        }
    }
    
    //Spawns the selected object
    void spawnGameObject(float X,float Z,int number)
    {
        Clone = Instantiate(GameObjectToSpawn, (transform.position + new Vector3(X,0,Z)), Quaternion.identity);
        Clone.name = Clone.name + number;
        return;
    }

    //Finding and setting random bombs selected on the objects
    void findRandomGameObject()
    {
        randomIndex = Random.Range(0, spawnedGameObjects.Length - 1);
        randomGameObject = spawnedGameObjects[randomIndex];
        Mat = randomGameObject.GetComponent<MeshRenderer>().material;
        if (Mat.color == Color.red)
        {
            findRandomGameObject();
        }
        else
        {
            selectedBombScript = randomGameObject.GetComponent<GeneralInfo>();
            selectedBombScript.isBomb = true;
            Mat.color = Color.red;
        }
    }

    //Seeing how many fields around the object have isBomb=true
    int checkForBombs(int outcomeOfEquation, GameObject gameObject, int index)
    {
        int numberOfAdjacentBombs = 0;
        if (outcomeOfEquation < spawnedGameObjects.Length - 1 && outcomeOfEquation > 0)
        {
            GameObject adjacentField = spawnedGameObjects[outcomeOfEquation];
            GeneralInfo currentGameObjectScript = gameObject.GetComponent<GeneralInfo>();
            if (checkLocationOfAdjacentField(gameObject,adjacentField))
            {
                currentGameObjectScript.adjacentFields[index] = adjacentField;
                GeneralInfo adjacentFieldScript = adjacentField.GetComponent<GeneralInfo>();
                if (adjacentFieldScript.isBomb == true)
                {
                    numberOfAdjacentBombs = numberOfAdjacentBombs + 1;
                    return numberOfAdjacentBombs;
                }
                else
                {
                    return 0;
                }
            }
            else
            {
                return 0;
            }
        }
        else
        {
            return 0;
        }
    }

    //Checking the location of the adjacentField to the current gameObject
    bool checkLocationOfAdjacentField(GameObject gameObject, GameObject adjacentField)
    {
        if (gameObject.transform.position + new Vector3(moveAmountX,0,0) == adjacentField.transform.position)
        {
            return true;
        }
        if (gameObject.transform.position + new Vector3(0,0,moveAmountZ) == adjacentField.transform.position)
        {
            return true;
        }
        if (gameObject.transform.position - new Vector3(moveAmountX,0,0) == adjacentField.transform.position)
        {
            return true;
        }
        if (gameObject.transform.position - new Vector3(0,0,moveAmountZ) == adjacentField.transform.position)
        {
            return true;
        }
        if (gameObject.transform.position + new Vector3(moveAmountX,0,moveAmountZ) == adjacentField.transform.position)
        {
            return true;
        }
        if (gameObject.transform.position - new Vector3(moveAmountX,0,moveAmountZ) == adjacentField.transform.position)
        {
            return true;
        }
        if (gameObject.transform.position + new Vector3(moveAmountX,0,0) - new Vector3(0,0,moveAmountZ) == adjacentField.transform.position)
        {
            return true;
        }
        if (gameObject.transform.position - new Vector3(moveAmountX,0,0) + new Vector3(0,0,moveAmountZ) == adjacentField.transform.position)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}