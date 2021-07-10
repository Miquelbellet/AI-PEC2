using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class GameControllerScript : MonoBehaviour
{
    public Camera mainCamera;
    public GameObject playerTank, enemiePrefab, spawnPoints;
    public TextMeshProUGUI timer, scoreTxt;
    public int initNumOfEnemies;

    private int score, numOfEnemies;
    private float time;
    void Start()
    {
        numOfEnemies = initNumOfEnemies;
        RespawnEnemies();
    }

    private void Update()
    {
        CheckPlayer();
        if (playerTank.activeSelf)
        {
            time += Time.deltaTime;
            timer.text = "Time: " + time.ToString("F2");
        }
    }

    void FixedUpdate()
    {
        mainCamera.transform.position = new Vector3(playerTank.transform.position.x, playerTank.transform.position.y + 70, playerTank.transform.position.z);
    }

    void RespawnEnemies()
    {
        var numChild = Random.Range(0, spawnPoints.transform.childCount);
        for (int i = 0; i < initNumOfEnemies; i++)
        {
            Instantiate(enemiePrefab, spawnPoints.transform.GetChild(numChild).transform);
        }
    }

    void CheckPlayer()
    {
        if (!playerTank.gameObject.activeSelf)
        {
            Invoke("RestartGame", 3f);
        }
    }

    void RestartGame()
    {
        SceneManager.LoadScene("ActivitatScene");
    }

    public void DeadTank()
    {
        score += 1;
        scoreTxt.text = score.ToString();
        if(score >= numOfEnemies)
        {
            RespawnEnemies();
            numOfEnemies += initNumOfEnemies;
        }
    }
}
