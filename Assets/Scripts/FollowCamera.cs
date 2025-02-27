﻿using UnityEngine;
using System.Collections;

public class FollowCamera : MonoBehaviour
{

    public GameObject player; // тут объект игрока
    //private Vector3 offset;
    public Transform target;
    public float smooth = 5.0f;
    public Vector3 offset;

    void Start()
    {
        offset = transform.position - player.transform.position;
    }

    void LateUpdate()
    {
        transform.position = player.transform.position + offset;
    }
    void Update()
    {
        //transform.position = Vector3.Lerp(transform.position, target.position + offset, Time.deltaTime * smooth);
    }

    //offset - точная позиция камера относительно игрока.Всегда будет висеть сзди и выше на столько-то единиц.
}