using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;


public class TutGun : MonoBehaviour
{
    public Transform bulletSpawnPoint;
    public GameObject bulletPrefab;
    public float bulletSpeed = 5000f;
    public float gravityForce = 1;
    public GameObject hand;
    private float eccentricity = 0;
    // private float timer;
    private float delay = 3f; // changed from 3 to 2 sec delay
    public static IEnumerator coroutine;
    public static float time = 0;
    public static int round = 0;
    // public static int maxScore = 150; // 2500, 150 for debug testing
    // public static int maxTime = 300;
    public static bool rest = false;
    public int maxScore = 150;

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(5f);
        Debug.Log("Coroutine called");

        StartCoroutine(Fire());
    }

    IEnumerator Fire()
    {
        while (TutCollision.totalScore < maxScore && time < 100000) // 1000 is the score threshold to pass the tutorial round
        {
            yield return new WaitForSeconds(delay);

            time += delay;
            Debug.Log("Time: " + time);

            bulletSpawnPoint.transform.rotation = hand.transform.rotation;
            // ROTATION ECCENTRICITY: 
            bulletSpawnPoint.transform.Rotate(new Vector3(10, eccentricity, 0));
            Quaternion direction = Quaternion.Euler(90, 90, 90);
            //bulletSpawnPoint.transform.position = playerCamera.transform.position + playerCamera.transform.forward * Time.deltaTime * 500.0f;


            var bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
            bullet.GetComponent<Rigidbody>().velocity = bulletSpawnPoint.forward * bulletSpeed;
            // timer = delay;
        }
        Debug.Log("Finished firing");
    }


    void Start()
    {
        Debug.Log("Starting Round " + round);
        coroutine = Wait();
        StartCoroutine(coroutine);
    }

    void Update()
    {
        if (TutCollision.totalScore >= maxScore) // remove CollisionBullet.totalScore >= maxScore || 
        {
            Destroy(GameObject.Find("Player"));
            time = 0;
            CollisionBullet.totalScore = 0;
            SceneManager.LoadScene(6); //break scene, round is at 0
        }

    }


   

}
