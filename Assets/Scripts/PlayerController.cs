using System;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

[RequireComponent(typeof(Rigidbody), typeof(BoxCollider))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private FixedJoystick _joystick;
    [SerializeField] private float _moveSpeed;
    [SerializeField] TextMeshProUGUI forceText;
    [SerializeField] private GameObject WinPanel;
    [SerializeField] private GameObject LosePanel;

    public int health = 100;  // Устанавливаем начальное здоровье игрока

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Friend"))
        {
            Friend friend = collision.gameObject.GetComponent<Friend>();
            if (friend != null)
            {
                int healthFriend = friend._health;
                health += healthFriend;
                forceText.text = health.ToString();
                Destroy(collision.gameObject);
            }
        }
        else if (collision.gameObject.CompareTag("Enemy"))
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            if (enemy != null)
            {
                int healthEnemy = enemy._health_enemy;
                enemy.Hit(health);
                health = Mathf.Max(health - healthEnemy, 0);  // Проверка на отрицательное здоровье
                forceText.text = health.ToString();

                if (!enemy.isAlive())
                {
                    Destroy(collision.gameObject);
                    Time.timeScale = 0;
                    WinPanel.SetActive(true);
                }
                else
                {
                    EventSystem.current.sendNavigationEvents = false;  // Отключение навигации
                    Time.timeScale = 0;  // Останавливаем время при поражении
                    LosePanel.SetActive(true);
                }
            }
        }
    }

    private void FixedUpdate()
    {
        Vector3 velocity = new Vector3(_joystick.Horizontal * _moveSpeed, _rigidbody.velocity.y, _joystick.Vertical * _moveSpeed);
        _rigidbody.velocity = velocity;

        if (_joystick.Horizontal != 0 || _joystick.Vertical != 0)
        {
            transform.rotation = Quaternion.LookRotation(velocity);
        }
    }
}
