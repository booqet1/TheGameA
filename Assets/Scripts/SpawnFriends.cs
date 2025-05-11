using System.Collections;
using UnityEngine;

public class SpawnFriends : MonoBehaviour
{
    [SerializeField] private GameObject _friendPrefab;
    [SerializeField] private GameObject LosePanel;
    [SerializeField] private GameObject WinPanel;

    void Start()
    {
        LosePanel.SetActive(false);
        WinPanel.SetActive(false);
        Time.timeScale = 1;

        for (int i = 0; i < 50; i++)
        {
            Vector3 spawnPosition = new Vector3(Random.Range(-17f, 17f), 0f, Random.Range(-12f, 12f));
            Instantiate(_friendPrefab, spawnPosition, Quaternion.identity);
        }
    }
}
