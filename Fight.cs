using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    public static SceneSwitcher Instance;

    public string scene;
    // ������Ʈ�� �ٸ� ������ �̵��� �� ȣ��Ǵ� �Լ�
    void start()
    {
        FightManager fightManager = FindObjectOfType<FightManager>();
        if (fightManager.live == 1)
            gameObject.SetActive(true);
        else
            gameObject.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            TeleportLocation();
        }
    }
    public void TeleportLocation()
    {
        FightManager fightManager = FindObjectOfType<FightManager>();

        Destroy(GameObject.Find("tutenemy1"));
        fightManager.live = 0;
        fightManager.sceneName = SceneManager.GetActiveScene().name;
        fightManager.positionX = GameObject.Find("Player").transform.position.x;
        fightManager.positionY = GameObject.Find("Player").transform.position.y;
        fightManager.positionZ = GameObject.Find("Player").transform.position.z;
        SceneManager.LoadScene(scene);
    }
}