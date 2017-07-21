using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GeneticManger : MonoBehaviour {
    public int PopulationLength = 200;

    public int WeightsLenght = 5;

    public float MutationRate = 0.05f;

    public Dna[] Population;

    public int GenerationNumber = 0;

    public GameObject FlappyBirdPrefab;
 
    public Text GenerationNumberText;


    //Reference
    private GameManger Game_Manger;
    void Start ()
    {
        
        Game_Manger = GameObject.FindGameObjectWithTag("GameManger").GetComponent<GameManger>();

        Population = new Dna[PopulationLength];

		for(int i=0;i< PopulationLength; i++)
        {

            Population[i] = new Dna();

            Population[i].Weight = new float[WeightsLenght];

            for(int j=0;j< WeightsLenght; j++)
            {
                Population[i].Weight[j] = Random.Range(-1.0f, 1.0f);
            }


            Population[i].Fitness = 0;
            
            GameObject  prefab = (GameObject)Instantiate(FlappyBirdPrefab, new Vector3(-7, 0, 0), new Quaternion(0, 0, 0, 0));

            Population[i].Prefab = prefab;

            prefab.GetComponent<FlappyBirdObject>().ParentIndex = i;

        }
        //Start The Generation Test   
        GenerationNumberText.text = GenerationNumber.ToString();
        Game_Manger.NumberOfFlappyBird = PopulationLength;
        Game_Manger.TimerFitness = 0;
        RePopulitionbool = false;
    }








    private bool RePopulitionbool = true;


	void Update ()
    {
        //Check If Populition Test Is End
		if(Game_Manger.NumberOfFlappyBird<=0 && RePopulitionbool == false)
        {
            RePopulitionbool = true;
            //Start RePopulition
            StartCoroutine(RePopulition());
        }
	}




    private Dna[] BestPopulationSelect;
    IEnumerator RePopulition()
    {

        //Wait To Clean All Map
        yield return new WaitForSeconds(2f);






        // Sort Populition By Fitness Score
        for (int i = 0; i < Population.Length; i++)
        {
            for (int j = i + 1; j < Population.Length; j++)
            {
                if (Population[i].Fitness < Population[j].Fitness)
                {
                    Dna Temp = Population[i];
                    Population[i] = Population[j];
                    Population[j] = Temp;
                }
            }
        }





        //Selection 25% of The Best Popultion  by fitness
        BestPopulationSelect = new Dna[Population.Length / 4];    
        
        for (int i=0;i<PopulationLength/4;i++)
        {
            BestPopulationSelect[i] = new Dna();
            BestPopulationSelect[i] = Population[i];
        }






        //Make New Populition
        Population = new Dna[PopulationLength];



        //Cross Over to 25% of The Best Populition
        for (int i=0;i<PopulationLength/4;i++)
        {

            int a = Random.Range(0, BestPopulationSelect.Length);



            Population[i] = new Dna();



            Population[i].Weight = new float[WeightsLenght];


            int Mid = Random.Range(1, WeightsLenght - 1);

            for(int j=0;j<Mid;j++)
            {
                Population[i].Weight[j] =BestPopulationSelect[i].Weight[j];
            }
            for(int j=Mid ; j < WeightsLenght ; j++)
            {
                Population[i].Weight[j] = BestPopulationSelect[a].Weight[j];
            }

    
        }



        int Index = 0;
        //Put the 25% of The best population in the new population Without do nothing
        for (int i= PopulationLength / 4; i<PopulationLength/2;i++)
        {
            Population[i] = new Dna();

            Population[i].Weight = new float[BestPopulationSelect[0].Weight.Length];

            for (int j=0;j< WeightsLenght; j++)
            {
                Population[i].Weight[j] = BestPopulationSelect[Index].Weight[j];
            }

            Index++;
        }



        //The Rest Of the population  (50%) Make it New
        for (int i= PopulationLength / 2 ; i< PopulationLength ; i++)
        {
            Population[i] = new Dna();

            Population[i].Weight = new float[BestPopulationSelect[0].Weight.Length];

            for (int j = 0; j < Population[i].Weight.Length; j++)
            {
                Population[i].Weight[j] = Random.Range(-1.0f, 1.0f);
            }

        }

        //To Make The GameManger Start Building Walls
        Game_Manger.NumberOfFlappyBird = PopulationLength;
        Game_Manger.Index = Game_Manger.Timer;
        Game_Manger.TimerFitness = 0;


        //Wait To Start Build Walls in The Map
        yield return new WaitForSeconds(2f);



        //Mutate  And Instantiate Prefab
        for (int i=0;i<PopulationLength;i++)
        {
            Mutate(i);

            //Instantiate Prefab
            GameObject prefab = (GameObject)Instantiate(FlappyBirdPrefab, new Vector3(-7 , 0, 0), new Quaternion(0, 0, 0, 0));

            Population[i].Prefab = prefab;
            Population[i].Fitness = 0;
            prefab.GetComponent<FlappyBirdObject>().ParentIndex = i;
        }



        //Start The Generation Test        
        RePopulitionbool = false;
        
        GenerationNumber++;



        GenerationNumberText.text = GenerationNumber.ToString();

    }

    void Mutate(int Index)
    {
        for (int i = 0; i < WeightsLenght; i++)
        {
            if (Random.Range(0.0f, 1.0f) < MutationRate)
            {
                Population[Index].Weight[i] = Random.Range(-1.0f, 1.0f);
            }
        }

    }

}
