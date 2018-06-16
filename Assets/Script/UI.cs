using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    public Text bestFitnessText;
    public Text currentFitnessText;
    public Text birdAliveText;
    public Text generationNumberText;

    private void Update()
    {
        bestFitnessText.text = GameManager.singleton.bestFitness.ToString();
        currentFitnessText.text = GameManager.singleton.currentFitness.ToString();
        birdAliveText.text = GameManager.singleton.numberOfFlappyBird.ToString();
        generationNumberText.text = GeneticManger.singleton.GenerationNumber.ToString();
    }

}
