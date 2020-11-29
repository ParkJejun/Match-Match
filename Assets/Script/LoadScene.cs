using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    public void ChangeScene(string x)
    {
        SoundManager.Instance.PlaySound("Button");
        SceneManager.LoadScene(x);
    }
}
