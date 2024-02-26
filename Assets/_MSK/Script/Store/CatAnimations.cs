using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatAnimations : MonoBehaviour
{
    [SerializeField] private Vector3 finalPos;

    private Vector3 intialPos;

    private void Awake()
    {
        intialPos = transform.position;
    }

    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, finalPos, 0.1f);
    }

    private void OnDisable()
    {
        transform.position = intialPos;
    }
}
