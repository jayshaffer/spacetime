using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySmall : MonoBehaviour
{

    public float followDistance;
    public float moveSpeed;
    public List<BulletSpawner> spawners;
    public float fireAmount;
    public float rotationTimes = 1;
    public Hurtable hurtable;
    public GameObject damageIndicator;
    Transform playerTransform;
    PlayerController playerController;
    bool moving = false;
    float moveDelay = 3;
    float lastMove;
    Vector3 moveDestination;
    GameController gameController;
    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player"); 
        playerTransform = player.GetComponent<Transform>(); 
        playerController = player.GetComponent<PlayerController>();
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        lastMove = Time.time;
    }

    void Update()
    {
        if(hurtable.time <= 0){
            Destroy(gameObject);
        }
        if (lastMove + moveDelay < Time.time && !moving)
        {
            StartCoroutine("MoveToRandom");
        }
        float latestHit = hurtable.GetHitQueueNext();
        if(latestHit != 0 && hurtable.time <= 0){
            playerController.hurtable.Hit(-10, true);
        }
    }

    IEnumerator Spin()
    {
        for (int i = 0; i < 36 * rotationTimes; i++)
        {
            transform.Rotate(new Vector3(0, 0, transform.position.z - 10));
            yield return null;
        }
    }

    IEnumerator FireBurst()
    {
        for (int i = 0; i < fireAmount; i++)
        {
           foreach(BulletSpawner spawner in spawners)
            {
                spawner.Fire();
            }
            yield return new WaitForSeconds(.2f);
        }
    }

    IEnumerator MoveToRandom()
    {
        moving = true;
        float randomX = Random.Range(gameController.minX, gameController.maxX);
        float randomY = Random.Range(gameController.minY, gameController.maxY);
        moveDestination = new Vector3(randomX, randomY, transform.position.z);
        while (transform.position.x != moveDestination.x && transform.position.y != moveDestination.y)
        {
            transform.position = Vector3.MoveTowards(transform.position, moveDestination, moveSpeed);
            yield return null;
        }
        lastMove = Time.time;
        StartCoroutine("Spin");
        StartCoroutine("FireBurst");
        moving = false;
    }
}
