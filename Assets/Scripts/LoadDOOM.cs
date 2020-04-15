using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadDOOM : MonoBehaviour
{
    public void LoadDOOMScene()
    {
        SceneManager.LoadScene("IntroMenu");
    }
}
