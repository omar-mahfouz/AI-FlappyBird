using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallPrefab : MonoBehaviour
{

    public float Speed = 3f;

    private GameManager Game_Manger;
    private void Start()
    {
        Game_Manger = GameObject.FindGameObjectWithTag("GameManger").GetComponent<GameManager>();
    }
    void Update ()
    {
        transform.Translate(Vector2.left * Time.deltaTime * Speed);
        if(Game_Manger.numberOfFlappyBird<=0)
        {
            Destroy(gameObject);
        }
	}
}
