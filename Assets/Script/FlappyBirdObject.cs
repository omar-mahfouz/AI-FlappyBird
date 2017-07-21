using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using System.Globalization;
using System.Threading;
public class FlappyBirdObject : MonoBehaviour
{
    

    public float JumpPower = 7;
    //The Index In The Population Array
    public int  ParentIndex;

    //Reference
    private GameManger Game_Manger;
    private GeneticManger Genetic_Manger;


    //The Input Variable
    private float Distanse = 0f;
    private float FlappyBirdHeight = 0f;
    private float UpperPipesHeight = 0f;
    private float LowerPipesHeight = 0f;
    private float Velocity = 0f;


    //The Index Of The Wall He will cross it
    private int IndexWalls = 0;

    private void Start()
    {
        Game_Manger = GameObject.FindGameObjectWithTag("GameManger").GetComponent<GameManger>();
        Genetic_Manger = GameObject.FindGameObjectWithTag("GeneticManger").GetComponent<GeneticManger>();
    }


    void Update ()
    {

        //Take The Position Of The Wall
        Vector2 Pos = Game_Manger.Walls[IndexWalls].gameObject.transform.position;
        Pos.x += 0.8f;

        //If Cross The Current Wall Move To The Next Wall
        if (Pos.x - transform.position.x<=0)
        {
            IndexWalls++;
            Pos = Game_Manger.Walls[IndexWalls].gameObject.transform.position;
            Pos.x += 0.8f;
        }

        //The Input Value
        LowerPipesHeight = ((float)Pos.y + 5.0f);
        UpperPipesHeight = (5.0f - (float)Pos.y) - 1.25f;
        FlappyBirdHeight = (float)transform.position.y + 5.0f;
        Distanse = (float)Pos.x - (float)transform.position.x;
        Velocity =(float)GetComponent<Rigidbody2D>().velocity.y;

        //The Weight Value
        float W1 = (float)Genetic_Manger.Population[ParentIndex].Weight[0];
        float W2 = (float)Genetic_Manger.Population[ParentIndex].Weight[1];
        float W3 = (float)Genetic_Manger.Population[ParentIndex].Weight[2];
        float W4 = (float)Genetic_Manger.Population[ParentIndex].Weight[3];
        float W5 = (float)Genetic_Manger.Population[ParentIndex].Weight[4];

        // Neural Network Model  
       float NeuralNetworkResult = (float)Math.Round(((float)Math.Round((double)(Distanse * W1), 6) + (float)Math.Round((double)(LowerPipesHeight * W2), 6) + (float)Math.Round((double)(UpperPipesHeight * W3), 6) + (float)Math.Round((double)(FlappyBirdHeight * W4), 6) + (float)Math.Round((double)(Velocity * W5), 6)), 6);



        //To Resolve Big Number 
        if (NeuralNetworkResult.ToString().Contains('E'))
        {


            if (NeuralNetworkResult > 0)
            {
                NeuralNetworkResult = 1;
            }
            else
            {
                NeuralNetworkResult = -1;
            }           
        }




        NeuralNetworkResult = Sigmoid(NeuralNetworkResult);

        //Jump If bigger Then 0.5
        if (NeuralNetworkResult > 0.5)
        {
            Jump();
        }

       
	}

   //To Check If He Touch The Wall
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            Death();
        }
    }

    private void Jump()
    {
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        GetComponent<Rigidbody2D>().AddForce(Vector2.up * JumpPower);
    }
    

    private void Death()
    {
        Game_Manger.NumberOfFlappyBird--;

        Genetic_Manger.Population[ParentIndex].Fitness = Game_Manger.TimerFitness;
        
        Destroy(gameObject);

    }


    public float Sigmoid(float x)
    {
        return 1.0f / (1.0f + Mathf.Exp(-1.0f * x) );
    }
  

}
