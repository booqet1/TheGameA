using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LoadScenes : MonoBehaviour
{
    public void ChangeScenes(int _number)
    {
        SceneManager.LoadScene(_number);
    }
}