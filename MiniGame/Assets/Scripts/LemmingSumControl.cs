﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LemmingSumControl : MonoBehaviour
{
    private static LemmingSumControl _instance;
    public static LemmingSumControl _Instance
    {
        get
        {
            return _instance;
        }
        set
        {
            _instance = value;
        }
    }

    //旅鼠个数
    [SerializeField]
    private int lemmingNum;
    public int LemmingNum
    {
        get
        {
            return lemmingNum;
        }
        set
        {
            lemmingNum = value;
        }
    }

    //当前放置的旅鼠个数
    [SerializeField]
    private int placeLemming;

    //存储格子信息
    private List<Tuple<int, int>> list = new List<Tuple<int, int>>();

    private List<GameObject> lemmingObj = new List<GameObject>();

    //旅鼠群空物体
    private GameObject player;

    public GameObject lemmingInstance;

    private GameObject newSquare;

    private int IndexX;
    private int IndexY;

    //增加的情况
    public int situation;
    // Start is called before the first frame update
    void Start()
    {
        lemmingNum = 0;
        placeLemming = 0;
        newSquare = null;
        player = GameObject.FindWithTag("Player");

        IndexX = 0;
        IndexY = 0;

        situation = 0;
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            lemmingNum++;
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            lemmingNum--;
            placeLemming--;

            list.RemoveAt(list.Count - 1);
            Destroy(lemmingObj[lemmingObj.Count - 1]);
            lemmingObj.RemoveAt(lemmingObj.Count - 1);
        }
        if (lemmingNum < 0)
        {
            lemmingNum = 0;
        }

        while (placeLemming < lemmingNum)
        {
            int sqrtNum = (int)Math.Sqrt(lemmingNum);
            int remaining = lemmingNum - sqrtNum * sqrtNum;

            if (remaining == 0)
            {
                IndexX = sqrtNum - 1;
                IndexY = sqrtNum - 1;

                CreateInstance(IndexX, IndexY);

                situation = 1;
            }
            else if (remaining <= sqrtNum)
            {
                IndexX = sqrtNum;
                IndexY = remaining - 1;

                CreateInstance(IndexX, IndexY);

                situation = 2;
            }
            else if (remaining <= 2 * sqrtNum)
            {
                IndexX = remaining - sqrtNum - 1;
                IndexY = sqrtNum;

                CreateInstance(IndexX, IndexY);

                situation = 3;
            }
        }
    }

    private void CreateInstance(int IndexX, int IndexY)
    {
        list.Add(new Tuple<int, int>(IndexX, IndexY));

        newSquare = GameObject.Instantiate(lemmingInstance);
        newSquare.transform.parent = player.transform;
        newSquare.GetComponent<LemmingManager>().GetPosition(IndexX, IndexY);

        lemmingObj.Add(newSquare);

        placeLemming++;
    }
}