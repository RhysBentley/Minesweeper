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
    private Material Material1;
    private int randomIndex;
    private Test selectedBombScript;

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
                spawnGameObject(posX,posZ);
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
    }
    
    //Spawns the selected object
    void spawnGameObject(float X,float Z)
    {
        Clone = Instantiate(GameObjectToSpawn, (transform.position + new Vector3(X,0,Z)), Quaternion.identity);
        return;
    }

    void findRandomGameObject()
    {
        randomIndex = Random.Range(0, spawnedGameObjects.Length - 1);
        randomGameObject = spawnedGameObjects[randomIndex];
        Material1 = randomGameObject.GetComponent<MeshRenderer>().material;
        if (Material1.color == Color.red)
        {
            findRandomGameObject();
        }
        else
        {
            selectedBombScript = randomGameObject.GetComponent<Test>();
            selectedBombScript.thisIsATest(); //This is a test
            Material1.color = Color.red;
        }
    }
}