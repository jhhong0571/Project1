using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Linq;

public class PlayerCollisionDetection1 : MonoBehaviour
{
    public int onBattle;
    public float positionX;
    public float positionY;
    public float positionZ;
    public string sceneName;

    public static PlayerCollisionDetection1 Instance;

    void Start()
    {
        FightManager fightManager = FindObjectOfType<FightManager>();

        if (fightManager != null)
        {
            onBattle = fightManager.onBattle;
            sceneName = fightManager.sceneName;
            positionX = fightManager.positionX;
            positionY = fightManager.positionY+0.5f;
            positionZ = fightManager.positionZ;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            BackToStage();
        }
    }
    public void BackToStage()
    {
        FightManager fightManager = FindObjectOfType<FightManager>();
        SceneManager.LoadScene(sceneName);
        Debug.Log("ฟ๖วม!adfasdf");
        fightManager.onBattle = 1;
    }
}