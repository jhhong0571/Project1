using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Next : MonoBehaviour
{

    public string scene;

    public void OnButtonClick()
    {
        SceneManager.LoadScene(scene);
    }
}
