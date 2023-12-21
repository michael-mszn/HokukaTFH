using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;


public class LevelManager : MonoBehaviour
{
    public float waveCooldown;
    public float countdown;
    public Transform spawnPoint;
    public MobManager mobManagerScript;
    public UIManager uiManagerScript;
    private int waveCount;
    private static float bossBuff = 1.5f;
    private static float spawnDelay = 0.33f;
    private AudioManager audioManager;
    
    // Start is called before the first frame update
    void Start()
    {
        waveCount = 1;
        audioManager = AudioManager.instance;
    }

    // Update is called once per frame
    void Update()
    {
        if (countdown <= 0f)
        {
            StartCoroutine(SpawnWave());
            countdown = waveCooldown;
            waveCount++;
        }

        //Update UI before countdown to avoid visual 'timer skips'
        if (waveCount < 11)
        {
            uiManagerScript.SetNextWaveCountdownText(Math.Ceiling(countdown).ToString());
            countdown -= Time.deltaTime;
        }
        else
        {
            countdown = 0;
            uiManagerScript.SetNextWaveCountdownText(Math.Ceiling(countdown).ToString());
        }
    }

    private IEnumerator SpawnWave()
    {
        audioManager.PlayWaveSpawnSound();
        switch (waveCount)
        {
            case 1:
                for (int i = 0; i < 6; i++)
                {
                    SummonEnemy(mobManagerScript.viktor, 300, 500f, 1);
                    yield return new WaitForSeconds(spawnDelay);
                }
                break;
            case 2:
                for (int i = 0; i < 6; i++)
                {
                    SummonEnemy(mobManagerScript.olaf, 300, 500f, 1);
                    yield return new WaitForSeconds(spawnDelay);
                }
                break;
            case 3:
                for (int i = 0; i < 6; i++)
                {
                    SummonEnemy(mobManagerScript.darius, 300, 500f, 1);
                    yield return new WaitForSeconds(spawnDelay);
                }
                break;
            case 4:
                for (int i = 0; i < 6; i++)
                {
                    SummonEnemy(mobManagerScript.viktor, 600, 500f, 1);
                    yield return new WaitForSeconds(spawnDelay);
                }
                break;
            case 5:
                for (int i = 0; i < 6; i++)
                {
                    SummonEnemy(mobManagerScript.olaf, 600, 500f, 1);
                    yield return new WaitForSeconds(spawnDelay);
                }
                break;
            case 6:
                for (int i = 0; i < 6; i++)
                {
                    SummonEnemy(mobManagerScript.darius, 600, 500f, 1);
                    yield return new WaitForSeconds(spawnDelay);
                }
                break;
            case 7:
                //After boss spawns, remaining waves should spawn twice as fast
                SummonEnemy(mobManagerScript.volibear, 5400, 6f, 10);
                waveCooldown = waveCooldown / 2;
                audioManager.PlayBearSpawnSound();
                break;
            case 8:
                for (int i = 0; i < 6; i++)
                {
                    SummonEnemy(mobManagerScript.viktor, 900, 500f * bossBuff, 1);
                    yield return new WaitForSeconds(spawnDelay);
                }
                break;
            case 9:
                for (int i = 0; i < 6; i++)
                {
                    SummonEnemy(mobManagerScript.olaf, 900, 500f * bossBuff, 1);
                    yield return new WaitForSeconds(spawnDelay);
                }
                break;
            case 10:
                for (int i = 0; i < 6; i++)
                {
                    SummonEnemy(mobManagerScript.darius, 900, 500f * bossBuff, 1);
                    yield return new WaitForSeconds(spawnDelay);
                }
                break;
            
        }
    }

    //Includes randomized spawnpoint
    private void SummonEnemy(GameObject mob, int health, float speed, int attackDamage)
    {
        Vector3 spawnOffset = new Vector3((int) UnityEngine.Random.Range(-25f, 25f),
            (int) UnityEngine.Random.Range(0f, 3.0f),
            (int) UnityEngine.Random.Range(-25f, 25.0f));
        
        //Set the mob values before spawning it to avoid the first member of a wave having mismatching values
        mobManagerScript.InstantiateMob(mob, health, speed, attackDamage);
        Instantiate(mob, spawnPoint.position + spawnOffset, spawnPoint.rotation * Quaternion.Euler(0, 90f, 0));
    }
}
