using UnityEngine;
using System.Collections;

public class turret : MonoBehaviour
{
    public GameObject door1, door2, door3;
    public GameObject cheeseBullet;
    public playerController playerScript;
    public float delay;
    public float miniDelay;
    public Vector2 bulletSpeed;    
    public Vector2 spawnPosition;
    public int bulletPattern;
    public bool startShooting;

    AudioSource audio;

    bool shootingBullet;

    // Use this for initialization
    void Start()
    {
        startShooting = false;
        shootingBullet = false;
        audio = GetComponent<AudioSource>();
        //spawnPosition = GetComponent<Transform>().position;
    }

    // Update is called once per frame
    void Update()
    {
        if (!startShooting)
        {
            checkShooting();
        }            
        else if(!shootingBullet)
        {
            if (bulletPattern == 1)
                StartCoroutine(shootBall1());
            else if (bulletPattern == 2)
                StartCoroutine(shootBall2());
            else if (bulletPattern == 3)
                StartCoroutine(shootBall3());
        }
    }

    void checkShooting()
    {
        if (!door1 && !door2 && !door3)
            startShooting = true;
    }

    IEnumerator shootBall1()
    {
        shootingBullet = true;        
        GameObject bullet1 = Instantiate(cheeseBullet, new Vector3(spawnPosition.x, spawnPosition.y, 0), Quaternion.identity) as GameObject;
        audio.Play();
        bullet1.GetComponent<Transform>().parent = GetComponent<Transform>();
        bullet1.GetComponent<Rigidbody2D>().velocity = bulletSpeed;
        yield return new WaitForSeconds(delay);
        shootingBullet = false;
    }

    IEnumerator shootBall2()
    {
        shootingBullet = true;        
        GameObject bullet1 = Instantiate(cheeseBullet, new Vector3(spawnPosition.x, spawnPosition.y, 0), Quaternion.identity) as GameObject;
        audio.Play();
        bullet1.GetComponent<Transform>().parent = GetComponent<Transform>();
        bullet1.GetComponent<Rigidbody2D>().velocity = bulletSpeed;
        yield return new WaitForSeconds(miniDelay);
        GameObject bullet2 = Instantiate(cheeseBullet, new Vector3(spawnPosition.x, spawnPosition.y, 0), Quaternion.identity) as GameObject;
        audio.Play();
        bullet2.GetComponent<Transform>().parent = GetComponent<Transform>();
        bullet2.GetComponent<Rigidbody2D>().velocity = bulletSpeed;
        yield return new WaitForSeconds(delay);
        shootingBullet = false;
    }

    IEnumerator shootBall3()
    {
        shootingBullet = true;        
        GameObject bullet1 = Instantiate(cheeseBullet, new Vector3(spawnPosition.x, spawnPosition.y, 0), Quaternion.identity) as GameObject;
        audio.Play();
        bullet1.GetComponent<Transform>().parent = GetComponent<Transform>();
        bullet1.GetComponent<Rigidbody2D>().velocity = bulletSpeed;
        yield return new WaitForSeconds(miniDelay);
        GameObject bullet2 = Instantiate(cheeseBullet, new Vector3(spawnPosition.x, spawnPosition.y, 0), Quaternion.identity) as GameObject;
        audio.Play();
        bullet2.GetComponent<Transform>().parent = GetComponent<Transform>();
        bullet2.GetComponent<Rigidbody2D>().velocity = bulletSpeed;
        yield return new WaitForSeconds(miniDelay);
        GameObject bullet3 = Instantiate(cheeseBullet, new Vector3(spawnPosition.x, spawnPosition.y, 0), Quaternion.identity) as GameObject;
        audio.Play();
        bullet3.GetComponent<Transform>().parent = GetComponent<Transform>();
        bullet3.GetComponent<Rigidbody2D>().velocity = bulletSpeed;
        yield return new WaitForSeconds(delay);
        shootingBullet = false;
    }
}