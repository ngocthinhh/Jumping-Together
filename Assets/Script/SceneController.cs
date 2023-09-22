using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    public GameObject player;
    public GameObject camera;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (player.transform.position.y < 5f)
        {
            camera.transform.position = new Vector3(transform.position.x, 0f, -10);
            transform.position = new Vector2(transform.position.x, 5f);
        }
        else if (player.transform.position.y >= 5f && player.transform.position.y < 15f)
        {
            camera.transform.position = new Vector3(transform.position.x, 10f, -10);
            transform.position = new Vector2(transform.position.x, 15f);
        }
        else if (player.transform.position.y >= 15f && player.transform.position.y < 25f)
        {
            camera.transform.position = new Vector3(transform.position.x, 20f, -10);
            transform.position = new Vector2(transform.position.x, 25f);
        }
    }
}
