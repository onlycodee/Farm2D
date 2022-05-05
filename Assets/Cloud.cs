using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour
{
    [SerializeField] float speed = 5f;
    [SerializeField] float offset = 1.5f;
    float leftScreen;
    float rightScreen;

    private void Awake()
    {
        leftScreen = Camera.main.ViewportToWorldPoint(Vector3.zero).x;
        rightScreen = Camera.main.ViewportToWorldPoint(Vector3.right).x;
        speed = UnityEngine.Random.Range(.1f, 1f);
    }

    private void Update()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);
        if (transform.position.x > rightScreen + offset)
        {
            transform.position = new Vector3(leftScreen - offset, transform.position.y);
        }
    }
}
