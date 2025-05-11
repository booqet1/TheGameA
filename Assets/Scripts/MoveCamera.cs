using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MoveCamera : MonoBehaviour
{
    private Transform player;
    public float camPositionSpeed = 5f;
    public Vector3 offset;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    void FixedUpdate()
    {
        Vector3 newCamPosition = new Vector3(player.position.x + offset.x,
        offset.y, player.position.z + offset.z);
        transform.position = Vector3.Lerp(transform.position, newCamPosition,
        camPositionSpeed * Time.deltaTime);
    }
}