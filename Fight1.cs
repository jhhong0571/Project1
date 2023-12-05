using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher1 : MonoBehaviour
{
    public static SceneSwitcher1 Instance;

    public string scene;
    // 오브젝트에 다른 씬으로 이동할 때 호출되는 함수
    void start()
    {
        FightManager fightManager = FindObjectOfType<FightManager>();
        if (fightManager.live1 == 1)
            gameObject.SetActive(true);
        else
            gameObject.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            TeleportLocation1();
        }
    }
    public void TeleportLocation1()
    {
        FightManager fightManager = FindObjectOfType<FightManager>();

        Destroy(GameObject.Find("tutenemy2"));
        fightManager.live1 = 0;
        fightManager.sceneName = SceneManager.GetActiveScene().name;
        fightManager.positionX = GameObject.Find("Player").transform.position.x;
        fightManager.positionY = GameObject.Find("Player").transform.position.y;
        fightManager.positionZ = GameObject.Find("Player").transform.position.z;
        SceneManager.LoadScene(scene);
    }
}