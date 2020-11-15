using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public SpawnPoint heroSpawnPoint;
    public hero Hero;

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(Hero, heroSpawnPoint.transform);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
