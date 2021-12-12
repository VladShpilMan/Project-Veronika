using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    private GameObject _camera;

    public void Start()
    {
        StartCoroutine(StartFunction());
    }

    private void Update()
    {
        _camera = GameObject.FindWithTag("MainCamera");
        Debug.Log("==========" + _camera.transform.position);
    }

    private IEnumerator StartFunction()
    {
        yield return new WaitForSeconds(0.4f);

        _camera = GameObject.FindWithTag("MainCamera");
    }
}
