using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallPrefab : MonoBehaviour
{

    public float Speed = 3f;

    private GameManger Game_Manger;
    private void Start()
    {
        Game_Manger = GameObject.FindGameObjectWithTag("GameManger").GetComponent<GameManger>();
    }
    void Update ()
    {
        transform.Translate(Vector2.left * Time.deltaTime * Speed);
        if(Game_Manger.NumberOfFlappyBird<=0)
        {
            Destroy(gameObject);
        }
	}
}
