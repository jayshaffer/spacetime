using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    public GameObject smallEnemy;
    public GameObject mediumEnemy;
    public GameObject largeEnemy;
    public GameObject gameControllerObject;
    public float maxEnemies;
    GameController gameController;
    List<GameObject> enemiesOnScreen;
    float powerupTimer;

    void Start()
    {
        enemiesOnScreen = new List<GameObject>();
        gameController = gameControllerObject.GetComponent<GameController>();
        SpawnEnemies();
        StartCoroutine("AutoSpawn");
        powerupTimer = Time.time + 10;
    }

    void Update()
    {
        List<GameObject> newEnemiesList = enemiesOnScreen;
        foreach(GameObject enemy in enemiesOnScreen){
            if(enemy == null){
                newEnemiesList.Remove(enemy);
            }
        }
        enemiesOnScreen = newEnemiesList;
    }

    IEnumerator AutoSpawn(){
        while(true){
            if(enemiesOnScreen.Count < maxEnemies){
                SpawnEnemies();
            }
            yield return new WaitForSeconds(5.0f);
        }
    }

    void SpawnEnemies()
    {
        float randomX = Random.Range(gameController.minX, gameController.maxX);
        float randomY = Random.Range(gameController.minY, gameController.maxY);
        Vector3 pos = new Vector3(randomX, randomY, transform.position.z);
        enemiesOnScreen.Add(Instantiate(smallEnemy, pos, Quaternion.identity));
    }
}
