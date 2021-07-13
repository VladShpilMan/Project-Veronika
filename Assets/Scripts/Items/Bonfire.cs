using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonfire : MonoBehaviour
{
    private Light light;
    float rand;

    private void Start()
    {
        light = GetComponentInChildren<Light>();
    }

    //private void FixedUpdate()
    //{
    //    // light.range = Random.Range(17, 24);
    //    rand = Random.Range(30, 60);
    //    light.range = Mathf.MoveTowards(light.range, rand, Time.deltaTime * 5);
    //    Debug.Log(rand);
    //}

    public int fps = 60; //60 раз в секунду будет обновляться стрелка
    public int speed = 1; //за секунду измениться на 500 единиц в одну или другую сторону

    int oldSpotUp;
    int currentSpotUp;
    int newSpotUp; //это новое значение переменной, которое ты где-то должен присвоить

    void Update()
    {
        StartCoroutine(UpdateLight());
    }

    private IEnumerator UpdateLight()
    {
        newSpotUp = Random.Range(30, 60);
        while (light.range != newSpotUp)
        {
            var delta = 0.001f;
            Debug.Log(light.range);
            if (light.range < newSpotUp) light.range += delta;
            else light.range -= delta;
            yield return new WaitForSeconds(1f / fps);
        }
    }

}
