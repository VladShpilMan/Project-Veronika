using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardianAIComponent : MonoBehaviour
{
    [SerializeField] private float stoppingDistance;
    [SerializeField] private int positionOfPatrol;
    [SerializeField] private Transform point;
    private Transform character;

    private bool _chill = false;
    private bool _angry = false;
    private bool _goBack = false;

    public bool Chill => _chill;
    public bool Angry => _angry;
    public bool GoBack => _goBack;

    private void Start()
    {
        character = GameObject.FindGameObjectWithTag("Character").transform;
    }

    public void GuardianAIUpdate()
    {
        EnemyLogic();
        Debug.Log("Chill" + _chill);
        Debug.Log("Angry" + _angry);
        Debug.Log("GoBack" + _goBack);
    }

    private void EnemyLogic()
    {
        if (Vector2.Distance(transform.position, point.position) < positionOfPatrol && _angry == false)
        {
            _chill = true;
            _angry = false;
            _goBack = false;
        }

        if (Vector2.Distance(transform.position, character.position) < stoppingDistance && !Character.IsDie)
        {
            _angry = true;
            _chill = false;
            _goBack = false;
        }

        if (Vector2.Distance(transform.position, character.position) > stoppingDistance && _chill == false)
        {
            _angry = false;
            _goBack = true;
            _chill = false;
        }
    }
}
