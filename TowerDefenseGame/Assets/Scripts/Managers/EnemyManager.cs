﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    //Put enemies in descending order point wise
    enum eEnemy {
        ENEMY5 = 20,
        ENEMY4 = 10,
        ENEMY3 = 5,
        ENEMY2 = 3,
        ENEMY1 = 1
    }



    /// <summary>
    /// The paths this manager will spawn things on will be adaptive to how many inserted into the list
    /// </summary>
    List<BasePath> paths = new List<BasePath>();

    /// <summary>
    /// A field to help testing
    /// </summary>
    [SerializeField]
    BasePath newPath;

    /// <summary>
    /// A list of all the enemies part of the wave
    /// </summary>
    List<GameObject> enemies = new List<GameObject>();

    /// <summary>
    /// What wave the player is currently on
    /// </summary>
    private int waveNum = 0;

    /// <summary>
    /// How long the wave has been going
    /// </summary>
    private float waveProgress = 0.0f;

    /// <summary>
    /// How long the wave will last in seconds
    /// </summary>
    private float waveTime = 8;

    /// <summary>
    /// How longer each wave will get as the waves continue
    /// </summary>
    private float waveTimeAdd = 1.2f;

    /// <summary>
    /// The time between each enemy spawning
    /// </summary>
    private float waveInterval;

    /// <summary>
    /// Time remaining on the next enemy spawn
    /// </summary>
    private float waveIntervalProgress;

    /// <summary>
    /// Points used to determine wave strength
    /// </summary>
    private int waveValue;

    /// <summary>
    /// How many points are added each wave
    /// </summary>
    private int waveAdd;

    /// <summary>
    /// Determines if the wave is going or not
    /// </summary>
    private bool waveIsAttacking = false;

    /// <summary>
    /// The current amount of enemies spawned in the wave
    /// </summary>
    private int enemiesSpawned = 0;



    // Start is called before the first frame update
    void Start()
    {
        //If difficulty is made later, change this to use that
        waveValue = 15;
        waveAdd = 5;

        paths.Add(newPath);

    }

    // Update is called once per frame
    void Update()
    {
        
        if (waveIsAttacking)
        {
            waveProgress -= Time.deltaTime;
            waveIntervalProgress -= Time.deltaTime;
            
            if (waveIntervalProgress < 0)
            {
                waveIntervalProgress += waveInterval;

                GameObject enemy = enemies[enemiesSpawned];
                enemy.transform.position = new Vector3(0,0,-2);
                float speed = enemy.GetComponent<BaseEnemy>().startSpeed;
                paths[0].AddFollower(new BasePath.Follower(enemy, speed));

                enemiesSpawned++;
            }


            if (waveProgress <= 0)
            {
                waveIsAttacking = false;
            }
        } else
        {
            if (Input.GetKeyDown(KeyCode.Space) /*&& enemies.Count == 0*/)
            {
                //TEST CODE, REMOVE IF NECESSARY
                enemies.Clear();
                // ---------


                //Increase wave strength
                waveNum++;
                waveValue += waveAdd;
                waveAdd = (int) Mathf.Ceil(waveAdd * 1.1f);

                //Increase wave length in time
                waveTime += waveTimeAdd;
                waveProgress = waveTime;
                waveTime *= .95f;

                enemiesSpawned = 0;
                waveIsAttacking = true;

                CreateWave();

                waveInterval = waveTime / enemies.Count;
            }
        }
    }

    /// <summary>
    /// Buffers the wave into a list
    /// </summary>
    private void CreateWave()
    {

        int waveAmount = waveValue;

        var enemyTypes = Enum.GetValues(typeof(eEnemy));

        for (int i = enemyTypes.Length - 1; i >= 0; i--)
        {
            int value = (int) enemyTypes.GetValue(i);
            if (value != 1)
            {
                while (value <= (int)Mathf.Ceil(waveAmount / 4.0f))
                {
                    AddEnemyUsingEnum((eEnemy) enemyTypes.GetValue(i));
                    waveAmount -= value;
                }

            }
            else
            {
                for (int j = 0; j < waveAmount; j++)
                {
                    AddEnemyUsingEnum(eEnemy.ENEMY1);
                }
            }
        }
    }



    private void AddEnemyUsingEnum(eEnemy enemy)
    {

        GameObject newEnemy;
        BaseEnemy values;
        switch(enemy)
        {
            case eEnemy.ENEMY1:
                newEnemy = new GameObject();
                newEnemy.transform.position = new Vector3(0,0,-20);
                AddPixelSprite(newEnemy, new Color(255, 0, 0));
                values = newEnemy.AddComponent<BaseEnemy>();
                values.Health = 3;
                values.Damage = 3;
                values.startSpeed = 2.5f;

                enemies.Add(newEnemy);
                break;
            case eEnemy.ENEMY2:
                newEnemy = new GameObject();
                newEnemy.transform.position = new Vector3(0, 0, -20);
                AddPixelSprite(newEnemy, new Color(0, 0, 255));
                values = newEnemy.AddComponent<BaseEnemy>();
                values.Health = 10;
                values.Damage = 6;
                values.startSpeed = 2.0f;

                enemies.Add(newEnemy);
                break;
            case eEnemy.ENEMY3:
                newEnemy = new GameObject();
                newEnemy.transform.position = new Vector3(0, 0, -20);
                AddPixelSprite(newEnemy, new Color(0, 255, 0));
                values = newEnemy.AddComponent<BaseEnemy>();
                values.Health = 20;
                values.Damage = 15;
                values.startSpeed = 2.0f;

                enemies.Add(newEnemy);
                break;
            case eEnemy.ENEMY4:
                newEnemy = new GameObject();
                newEnemy.transform.position = new Vector3(0, 0, -20);
                AddPixelSprite(newEnemy, new Color(0, 255, 255));
                values = newEnemy.AddComponent<BaseEnemy>();
                values.Health = 40;
                values.Damage = 20;
                values.startSpeed = 2.0f;

                enemies.Add(newEnemy);
                break;
            case eEnemy.ENEMY5:
                newEnemy = new GameObject();
                newEnemy.transform.position = new Vector3(0, 0, -20);
                AddPixelSprite(newEnemy, new Color(255, 0, 255));
                values = newEnemy.AddComponent<BaseEnemy>();
                values.Health = 100;
                values.Damage = 30;
                values.startSpeed = 1.2f;

                enemies.Add(newEnemy);
                break;
        }
    }

    private void AddPixelSprite(GameObject colorObject, Color color)
    {
        SpriteRenderer renderer = colorObject.AddComponent<SpriteRenderer>();
        renderer.sprite = Resources.Load<Sprite>("Textures/Misc/White1x1");
        renderer.color = color;

        renderer.transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
    }
}
