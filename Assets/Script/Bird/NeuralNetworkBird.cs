using System;
using System.Linq;
using UnityEngine;

public class NeuralNetworkBird : MonoBehaviour
{
    private float distanse = 0f;
    private float flappyBirdHeight = 0f;
    private float upperPipesHeight = 0f;
    private float lowerPipesHeight = 0f;
    private float velocity = 0f;
    private Rigidbody2D rigid;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    public float GetNeuralNetworkResult(Dna dna, Vector2 currentWallPos)
    {
        CalculateInputValue(currentWallPos);

        float neuralNetworkResult = CalculateNeuralNetworkResult(dna);

        neuralNetworkResult = SolveBigE_NotationNumber(neuralNetworkResult);

        neuralNetworkResult = Sigmoid(neuralNetworkResult);

        return neuralNetworkResult;
    }

    private void CalculateInputValue(Vector2 currentWallPos)
    {
        lowerPipesHeight = (currentWallPos.y + 5.0f);
        upperPipesHeight = (5.0f - currentWallPos.y) - 1.25f;
        flappyBirdHeight = transform.position.y + 5.0f;
        distanse = currentWallPos.x - transform.position.x;
        velocity = rigid.velocity.y;
    }

    private float CalculateNeuralNetworkResult(Dna dna)
    {
        float W1 = dna.weights[0];
        float W2 = dna.weights[1];
        float W3 = dna.weights[2];
        float W4 = dna.weights[3];
        float W5 = dna.weights[4];

        float neuralNetworkResult =
            (float)Math.Round(((float)Math.Round((distanse * W1), 6)
            + (float)Math.Round((lowerPipesHeight * W2), 6)
            + (float)Math.Round((upperPipesHeight * W3), 6)
            + (float)Math.Round((flappyBirdHeight * W4), 6)
            + (float)Math.Round((velocity * W5), 6)), 6);

        return neuralNetworkResult;
    }

    private float SolveBigE_NotationNumber(float neuralNetworkResult)
    {
        if (neuralNetworkResult.ToString().Contains('E'))
        {
            if (neuralNetworkResult > 0)
            {
                neuralNetworkResult = 1;
            }
            else
            {
                neuralNetworkResult = -1;
            }
        }

        return neuralNetworkResult;
    }

    public float Sigmoid(float x)
    {
        return 1.0f / (1.0f + Mathf.Exp(-1.0f * x));
    }

}
