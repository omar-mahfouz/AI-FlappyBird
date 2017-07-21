using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameManger : MonoBehaviour {

    public int NumberOfFlappyBird = 0;
    //Fitness Score
    public float TimerFitness = 0;

    public GameObject WallPrefab;

    public float Timer=3.5f;



    public Text BestFitnessText;
    public Text BirdAliveText;


    public float Index=3.5f;


    public float BestFitness = 0;




    public List<GameObject> Walls = new List<GameObject>();


	void Update ()
    {

        BirdAliveText.text = NumberOfFlappyBird.ToString();

        //Check If The Test End
        if (NumberOfFlappyBird<=0)
        {
            Walls.Clear();
            Index = 0;
            return;
        }

        //Fitness Score
        TimerFitness += Time.deltaTime;

     



        //Print The Best Fitness By Text
        if(BestFitness<TimerFitness)
        {
            BestFitness = TimerFitness;
            BestFitnessText.text = BestFitness.ToString();
        }

        

        Index += Time.deltaTime;


        //Building Wall 
        if(Index>=Timer)
        {
            BuildWall();
            Index = 0;
        }



		
	}

    void BuildWall()
    {
        GameObject W=(GameObject)Instantiate(WallPrefab, new Vector3(12, Random.Range(-2.8f, 2.6f), 0), new Quaternion(0, 0, 0, 0));
        //Add The New Wall To Walls List
        Walls.Add(W);
    }


}
