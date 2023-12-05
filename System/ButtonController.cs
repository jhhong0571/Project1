using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    public GameObject NextTurn;

    // Start is called before the first frame update
    void Start()
    {
        SetActive(false);
    }

    public void SetActive(bool isActive)
    {
        NextTurn.SetActive(isActive);
    }

    // Update is called once per frame
    void Update()
    {
    }
}
