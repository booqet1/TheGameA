using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using TMPro;
public class Friend : MonoBehaviour
{
    public int _health;
    [SerializeField] TextMeshPro healthText;
    void Start()
    {
        _health = Random.Range(1, 10);
        healthText.text = _health.ToString();
    }
    // Update is called once per frame
    void Update()
    {
    }
}