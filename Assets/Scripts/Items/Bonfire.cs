using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonfire : MonoBehaviour
{
    private new Light light;

    private void Start()
    {
        light = GetComponentInChildren<Light>();
    }

    public int fps = 60; //60 раз в секунду будет обновляться стрелка

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

            if (light.range < newSpotUp) light.range += delta;
                else light.range -= delta;
            yield return new WaitForSeconds(1f / fps);
        }
    }
}
