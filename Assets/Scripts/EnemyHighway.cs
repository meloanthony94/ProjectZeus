using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[ExecuteInEditMode]
public class EnemyHighway : MonoBehaviour
{
    public Enemy enemyPrefab;
    public int enemyCount = 60;
    public float spacing = 1;
    public int currentIndex = 0;
    public int frameCount = 0;
    public int visibleCount = 6;
    public bool isMoving = true;

    [SerializeField]
    public List<Enemy> enemyArray;

    private bool e_populateEnemy = false;

    public Constant constantRef;

    private Enemy selectHolder;

    private void OnValidate()
    {
#if UNITY_EDITOR
        e_populateEnemy = true;
#endif
    }

    // Start is called before the first frame update
    void Start()
    {
        int maxCount = Math.Min(visibleCount, enemyArray.Count);
        for (int i = 0; i < enemyArray.Count; i++)
        {
            enemyArray[i].gameObject.SetActive(i < maxCount ? true : false);
        }
    }

    // Update is called once per frame
    void Update()
    {
#if UNITY_EDITOR
        if (!Application.isPlaying)
        {
            if (e_populateEnemy)
            {
                InitiateEnemy();
                e_populateEnemy = false;
            }
        }
#endif

        if (Application.isPlaying && isMoving)
        {

            frameCount = Time.frameCount;

            if (currentIndex < enemyCount)
            {
                // moving
                if (frameCount % constantRef.FRAME_SPEED == 0)
                {
                    // move
                    this.gameObject.transform.position += new Vector3(-spacing * transform.localScale.x, 0, 0);

                    enemyArray[currentIndex].gameObject.SetActive(false);

                    if (currentIndex + visibleCount < enemyCount)
                    {
                        enemyArray[currentIndex + visibleCount].gameObject.SetActive(true);
                    }

                    currentIndex++;

                }
            }
        }

    }

    private void InitiateEnemy()
    {
#if UNITY_EDITOR

        // Clear everythin
        foreach(Enemy e in enemyArray)
        {
            if (!e) continue;
            DestroyImmediate(e.gameObject);

        }
        enemyArray.Clear();


        // Populate Enemy
        for (int i = 0; i < enemyCount; i++)
        {
            Enemy e = PrefabUtility.InstantiatePrefab(enemyPrefab, this.transform) as Enemy;

            e.gameObject.name = $"Enemy [{i}]";
            e.gameObject.transform.localPosition = new Vector3(spacing * i, 0, 0);
            e.highway = this;
            e.myType =(entityType.Type)(i % 4);
            enemyArray.Add(e);
        }
        EditorUtility.SetDirty(this);
#endif
    }

    public Enemy GetEnemy()
    {
        if (currentIndex < enemyCount)
        {
            return enemyArray[currentIndex];
        }
        else
        {
            return null;
        }
    }

    public void Register(Enemy e)
    {
        // already have something
        if (selectHolder)
        {
            // swap
            entityType.Type tmpType = selectHolder.myType;
            selectHolder.TypeSwap(e.myType);
            e.TypeSwap(tmpType);
            selectHolder = null;
        }
        else
        {
            selectHolder = e;
        }
    }
}
