using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float dumping = 1.5F;
    public Vector2 offset = new Vector2(2f, 1f);
    public bool isLeft;
    [SerializeField] private Transform character;
    private int lastX;

    [SerializeField] float leftLimit;
    [SerializeField] float rightLimit;
    [SerializeField] float downLimit;
    [SerializeField] float topLimit;

    void Start() {
        offset = new Vector2(Mathf.Abs(offset.x), offset.y);
        StartCoroutine(FindCharacter(isLeft));
    }

    void Update()
    {
        if (character)
        {
            int currentX = Mathf.RoundToInt(character.position.x);

            if (currentX > lastX) isLeft = false;
            else if (currentX < lastX) isLeft = true;

            lastX = Mathf.RoundToInt(character.position.x);

            Vector3 target;

            if (isLeft)
            {
                target = new Vector3(character.position.x - offset.x, character.position.y + offset.y, character.position.z - 10);
            }
            else
            {
                target = new Vector3(character.position.x + offset.x, character.position.y + offset.y, character.position.z - 10);
            }

            Vector3 currentPosition = Vector3.Lerp(transform.position, target, dumping * Time.deltaTime);
            transform.position = currentPosition;
        }

        transform.position = new Vector3
            (
            Mathf.Clamp(transform.position.x, leftLimit, rightLimit),
            Mathf.Clamp(transform.position.y, downLimit, topLimit),
            transform.position.z
            );
    }

    public IEnumerator FindCharacter(bool playerIsLeft)
    {
        yield return new WaitForSeconds(0.1f);
        lastX = Mathf.RoundToInt(character.position.x);

        if (playerIsLeft)
        {
            transform.position = new Vector3(character.position.x - offset.x, character.position.y - offset.y, character.position.z - 10);
        }
        else
        {
            transform.position = new Vector3(character.position.x + offset.x, character.position.y + offset.y, character.position.z - 10);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(new Vector2(leftLimit, topLimit), new Vector2(rightLimit, topLimit));
        Gizmos.DrawLine(new Vector2(leftLimit, downLimit), new Vector2(rightLimit, downLimit));
        Gizmos.DrawLine(new Vector2(leftLimit, topLimit), new Vector2(leftLimit, downLimit));
        Gizmos.DrawLine(new Vector2(rightLimit, topLimit), new Vector2(rightLimit, downLimit));
    }
}
