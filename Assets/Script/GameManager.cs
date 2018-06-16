using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    public WallBuilder wallBuilder;
    public int numberOfFlappyBird = 0;
    public float bestFitness = 0;
    public float currentFitness = 0;

    private List<GameObject> walls = new List<GameObject>();
    private bool testEnd = true;


    public static GameManager singleton;

    private void Awake()
    {
        if (singleton == null)
        {
            singleton = this;
        }
    }

    void Update()
    {
        //if (testEnd)
        //{
        //    return;
        //}

        if (numberOfFlappyBird <= 0)
        {
            EndTest();
            return;
        }

        currentFitness += Time.deltaTime;
    }


    private void EndTest()
    {
        testEnd = true;
        walls.Clear();
        wallBuilder.StopBuilding();

        if (bestFitness < currentFitness)
        {
            bestFitness = currentFitness;
        }

        GeneticManger.singleton.StartRePopulition();
    }

    public void StartTest(int PopulationLength)
    {
        numberOfFlappyBird = PopulationLength;
        testEnd = false;
        wallBuilder.ResumeBuiling();
        currentFitness = 0;
    }

    public void AddWall(GameObject wall)
    {
        walls.Add(wall);
    }

    public GameObject GetWallByIndex(int index)
    {
        return walls[index];
    }

}
