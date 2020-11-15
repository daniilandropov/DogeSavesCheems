using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private float speed = 0.0F;

    [SerializeField]
    private Transform target;

    Vector3 MemoryPosition;

    private void Awake()
    {
        if (!target) target = FindObjectOfType<hero>().GetComponentsInChildren<partOfHero>().FirstOrDefault(x => x.этоТуловище).transform;
    }

    private void Update()
    {
        try
        {
                Vector3 position = target.position;
                position.z = -10.0F;
                position.y += 1.0F;
                position.x += 3.0f;
                transform.position = Vector3.Lerp(transform.position, position, speed * Time.deltaTime);
                MemoryPosition = target.position;

        }
        catch
        {
            transform.position = Vector3.Lerp(transform.position, MemoryPosition, speed * Time.deltaTime);
            Application.Quit();
        }

    }
}
