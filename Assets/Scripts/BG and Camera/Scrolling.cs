using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scrolling : MonoBehaviour
{
    public bool scrolling, paralax;

    public float backgroundSize;
    public float paralaxSpeed;

    private Transform cameraTransform;
    private Player _player;
    private GameObject _camera;
    private Transform[] layers;
    private float viewZone = 10;
    private int rightIndex;
    private int leftIndex;

    private float lastCameraX;

    private void Start()
    {
        _player = GetComponent<Player>();
        StartCoroutine(StartFunction());

        layers = new Transform[transform.childCount];

        for (int i = 0; i < transform.childCount; i++)
        {
            layers[i] = transform.GetChild(i);
        }

        leftIndex = 0;
        rightIndex = layers.Length - 1;
    }

    private IEnumerator StartFunction()
    {
        yield return new WaitForSeconds(1f);

        cameraTransform = Camera.main.transform;
        lastCameraX = cameraTransform.position.x;
    }

    private void Update()
    {
        cameraTransform = Camera.main.transform;
        float deltaX = cameraTransform.position.x - lastCameraX;
        transform.position += Vector3.right * (deltaX * paralaxSpeed);
        lastCameraX = cameraTransform.position.x;

        if (cameraTransform.position.x < (layers[leftIndex].transform.position.x + viewZone))
        {
            ScrollLeft();
        }

        if (cameraTransform.position.x > (layers[rightIndex].transform.position.x - viewZone))
        {
            ScrollRight();
        }
    }

    private void ScrollLeft()
    {
        int lastRight = rightIndex;
        layers[rightIndex].position = Vector3.right * (layers[leftIndex].position.x - backgroundSize);
        leftIndex = rightIndex;
        rightIndex--;

        if (rightIndex < 0)
            rightIndex = layers.Length - 1;
    }

    private void ScrollRight()
    {
        int lastLeft = leftIndex;
        layers[leftIndex].position = Vector3.right * (layers[rightIndex].position.x + backgroundSize);
        rightIndex = leftIndex;
        leftIndex++;

        if (leftIndex == layers.Length)
            leftIndex = 0;
    }
}
