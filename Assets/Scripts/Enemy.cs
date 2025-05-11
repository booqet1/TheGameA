using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour
{
    public int _health_enemy;
    [SerializeField] TextMeshPro enemyhealthText;
    private GameObject[] friends;
    private GameObject player;
    [SerializeField] private PlayerController PlayerController;
    private GameObject closestFriend;
    public GameObject nearest;
    [SerializeField] private float _enemyspeed;
    private Rigidbody rb;

    private void Start()
    {
        _health_enemy = Random.Range(1, 50);
        enemyhealthText.text = _health_enemy.ToString();
        friends = GameObject.FindGameObjectsWithTag("Friend");
        rb = GetComponent<Rigidbody>(); // Получаем компонент Rigidbody
    }

    void Update()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        // Если игрок существует
        if (player != null)
        {
            int playerHealth = PlayerController.health;

            Vector3 targetPosition;

            if (_health_enemy > playerHealth)
            {
                targetPosition = player.transform.position;
            }
            else
            {
                nearest = FindClosestFriend();
                targetPosition = nearest != null ? nearest.transform.position : transform.position;
            }

            // Перемещение с использованием Rigidbody
            Vector3 direction = (targetPosition - transform.position).normalized;
            rb.MovePosition(transform.position + direction * _enemyspeed * Time.deltaTime);
        }
    }

    GameObject FindClosestFriend()
    {
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        GameObject closest = null;

        foreach (GameObject go in friends)
        {
            // Проверяем, не уничтожен ли друг
            if (go != null)
            {
                Vector3 diff = go.transform.position - position;
                float curDistance = diff.sqrMagnitude;

                if (curDistance < distance)
                {
                    closest = go;
                    distance = curDistance;
                }
            }
        }

        return closest;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Friend"))
        {
            Friend friend = collision.gameObject.GetComponent<Friend>();
            if (friend != null)
            {
                int healthFriend = friend._health;
                _health_enemy += healthFriend;
                enemyhealthText.text = _health_enemy.ToString();
                Destroy(collision.gameObject);
            }
        }
        else if (collision.gameObject.CompareTag("Player"))
        {
            int playerHealth = PlayerController.health;

            if (_health_enemy > playerHealth)
            {
                Destroy(collision.gameObject); // Удаляем игрока
            }
        }
    }

    public bool isAlive()
    {
        return _health_enemy > 0;
    }

    public void Hit(int damage)
    {
        _health_enemy -= damage;
    }
}
