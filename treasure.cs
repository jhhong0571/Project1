using UnityEngine;
using UnityEngine.SceneManagement;

public class treasure : MonoBehaviour
{
    public static treasure Instance;

    public string scene;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            FightManager fightManager = FindObjectOfType<FightManager>();
            if (fightManager.treasure == 1)
            {
                Debug.Log("ㅆ살아있어!");
                gameObject.SetActive(true);
                TeleportLocation();
            }
            else
            {
                Debug.Log("ㅆ죽었어!");
                gameObject.SetActive(false);
            }
        }
    }
    public void TeleportLocation()
    {
        FightManager fightManager = FindObjectOfType<FightManager>();
        Debug.Log("보물방");
        Destroy(GameObject.Find("treasure"));
        fightManager.treasure = 0;
        fightManager.sceneName = SceneManager.GetActiveScene().name;
        fightManager.positionX = GameObject.Find("Player").transform.position.x;
        fightManager.positionY = GameObject.Find("Player").transform.position.y;
        fightManager.positionZ = GameObject.Find("Player").transform.position.z;
        SceneManager.LoadScene(scene);
    }
}