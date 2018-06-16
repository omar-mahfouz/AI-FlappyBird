using System.Collections;
using UnityEngine;

public class GeneticManger : MonoBehaviour
{
    public int PopulationLength = 200;
    public int WeightsLength = 5;
    public float MutationRate = 0.05f;
    public int GenerationNumber = 0;
    public GameObject FlappyBirdPrefab;

    private bool isRePopulition = true;
    private Dna[] Population;
    private Dna[] BestPopulationSelect;

    public static GeneticManger singleton;

    private void Awake()
    {
        if (singleton == null)
        {
            singleton = this;
        }
    }

    private void Start()
    {
        InstaPopulation();
        //Start The Generation Test   
        GameManager.singleton.numberOfFlappyBird = PopulationLength;
        GameManager.singleton.currentFitness = 0;
        isRePopulition = false;
    }

    private void InstaPopulation()
    {
        Population = new Dna[PopulationLength];

        for (int i = 0; i < PopulationLength; i++)
        {
            Population[i] = new Dna();
            Population[i].weights = new float[WeightsLength];

            for (int j = 0; j < WeightsLength; j++)
            {
                Population[i].weights[j] = Random.Range(-1.0f, 1.0f);
            }

            Population[i].fitness = 0;

            GameObject birdGameObject = Instantiate(FlappyBirdPrefab, new Vector3(-7, 0, 0), new Quaternion(0, 0, 0, 0));

            Population[i].bird = birdGameObject;

            birdGameObject.GetComponent<Bird>().SetDna(Population[i]);

        }
    }

    public void StartRePopulition()
    {
        if (isRePopulition)
        {
            return;
        }
        isRePopulition = true;
        StartCoroutine(RePopulition());
    }

    IEnumerator RePopulition()
    {
        yield return new WaitForSeconds(2f);

        SortPopulition();
        SelectBestPopulition();
        CrossOverTheBestPopulationSelection();
        FillThePopulationWithTheBestPopulation();
        FillTheRestPopulationWithNewDna();
        Mutate();

        GameManager.singleton.StartTest(PopulationLength);
        yield return new WaitForSeconds(2f);

        InstantiateBirds();

        isRePopulition = false;
        GenerationNumber++;
    }

    private void SortPopulition()
    {
        for (int i = 0; i < Population.Length; i++)
        {
            for (int j = i + 1; j < Population.Length; j++)
            {
                if (Population[i].fitness < Population[j].fitness)
                {
                    Dna Temp = Population[i];
                    Population[i] = Population[j];
                    Population[j] = Temp;
                }
            }
        }
    }

    private void SelectBestPopulition()
    {
        BestPopulationSelect = new Dna[Population.Length / 4];

        for (int i = 0; i < PopulationLength / 4; i++)
        {
            BestPopulationSelect[i] = new Dna();
            BestPopulationSelect[i] = Population[i];
        }
    }

    private void CrossOverTheBestPopulationSelection()
    {
        Population = new Dna[PopulationLength];

        for (int i = 0; i < PopulationLength / 4; i++)
        {
            int a = Random.Range(0, BestPopulationSelect.Length);

            Population[i] = new Dna();
            Population[i].weights = new float[WeightsLength];

            int Mid = Random.Range(1, WeightsLength - 1);

            for (int j = 0; j < Mid; j++)
            {
                Population[i].weights[j] = BestPopulationSelect[i].weights[j];
            }
            for (int j = Mid; j < WeightsLength; j++)
            {
                Population[i].weights[j] = BestPopulationSelect[a].weights[j];
            }
        }
    }

    private void FillThePopulationWithTheBestPopulation()
    {
        int Index = 0;
        //Put the 25% of The best population in the new population Without do nothing
        for (int i = PopulationLength / 4; i < PopulationLength / 2; i++)
        {
            Population[i] = new Dna();

            Population[i].weights = new float[BestPopulationSelect[0].weights.Length];

            for (int j = 0; j < WeightsLength; j++)
            {
                Population[i].weights[j] = BestPopulationSelect[Index].weights[j];
            }

            Index++;
        }
    }

    private void FillTheRestPopulationWithNewDna()
    {
        //The Rest Of the population  (50%) Make it New
        for (int i = PopulationLength / 2; i < PopulationLength; i++)
        {
            Population[i] = new Dna();

            Population[i].weights = new float[BestPopulationSelect[0].weights.Length];

            for (int j = 0; j < Population[i].weights.Length; j++)
            {
                Population[i].weights[j] = Random.Range(-1.0f, 1.0f);
            }

        }
    }

    private void Mutate()
    {
        for (int i = 0; i < PopulationLength; i++)
        {
            Dna dna = Population[i];

            for (int j = 0; j < WeightsLength; j++)
            {
                if (Random.Range(0.0f, 1.0f) < MutationRate)
                {
                    dna.weights[j] = Random.Range(-1.0f, 1.0f);
                }
            }
        }
    }

    private void InstantiateBirds()
    {
        for (int i = 0; i < PopulationLength; i++)
        {
            //Instantiate Prefab
            GameObject prefab = Instantiate(FlappyBirdPrefab, new Vector3(-7, 0, 0), new Quaternion(0, 0, 0, 0));
            Population[i].bird = prefab;
            Population[i].bird = prefab;
            Population[i].fitness = 0;
            prefab.GetComponent<Bird>().SetDna(Population[i]);
        }
    }
}
