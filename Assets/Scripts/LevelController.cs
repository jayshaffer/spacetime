using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    public GameObject smallEnemy;
    public GameObject mediumEnemy;
    public GameObject largeEnemy;
    public GameObject gameControllerObject;
    GameController gameController;

    void Start()
    {
        gameController = gameControllerObject.GetComponent<GameController>();
        SpawnEnemies();
        StartCoroutine("AutoSpawn");
    }

    void Update()
    {

    }

    IEnumerator AutoSpawn(){
        while(true){
            SpawnEnemies();
            yield return new WaitForSeconds(5.0f);
        }
    }

    void SpawnEnemies()
    {
        float randomX = Random.Range(gameController.minX, gameController.maxX);
        float randomY = Random.Range(gameController.minY, gameController.maxY);
        Vector3 pos = new Vector3(randomX, randomY, transform.position.z);
        Instantiate(smallEnemy, pos, Quaternion.identity);
    }
}
