using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cor : MonoBehaviour
{
    [SerializeField] float time = 3;
    [SerializeField] int go;

    Coroutine timerCoroutine;

    // Start is called before the first frame update
    void Start()
    {
        timerCoroutine = StartCoroutine(Timer(time));
        StartCoroutine(StoryTime());
        StartCoroutine(WaitAction());
    }

    // Update is called once per frame
    void Update()
    {
        //time -= Time.deltaTime;
        //if(time <= 0)
        //{
        //    time = 3;
        //    print("hello");
        //}
    }

    IEnumerator Timer(float time)
    {
        while (true)
        {
            yield return new WaitForSeconds(time);
            //check perception
            print("tick");
        }
        
        //yield return null; 
    }

    IEnumerator StoryTime()
    {
        print("Hello");
        yield return new WaitForSeconds(time);
        print("welcome to the new world");
        yield return new WaitForSeconds(time);
        print("byebey");

        StopCoroutine(timerCoroutine);

        yield return null;
    }

    IEnumerator WaitAction()
    {
        yield return new WaitUntil(() => go == 2);
        print("go");
        yield return null;
    }
}
