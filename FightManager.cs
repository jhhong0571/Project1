using UnityEngine;

public class FightManager : MonoBehaviour
{
    public static FightManager instance;

    public int live;
    public int live1;
    public int onBattle;
    public float positionX;
    public float positionY;
    public float positionZ;
    public string sceneName;

    public int treasure;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SavePlayerData()
    {
        PlayerPrefs.SetInt("OnBattle", onBattle);
        PlayerPrefs.SetInt("Live", live);
        PlayerPrefs.SetInt("Live1", live1);
        PlayerPrefs.SetFloat("PositionX", positionX);
        PlayerPrefs.SetFloat("PositionY", positionY);
        PlayerPrefs.SetFloat("PositionZ", positionZ);
        PlayerPrefs.SetString("SceneName", sceneName);
        PlayerPrefs.SetInt("Treasure", treasure);

        PlayerPrefs.Save();
    }

    public void LoadPlayerData()
    {
        live = PlayerPrefs.GetInt("Live", 1);
        live1 = PlayerPrefs.GetInt("Live1", 1);
        onBattle = PlayerPrefs.GetInt("onBattle", 2);
        positionX = PlayerPrefs.GetFloat("PositionX", 0);
        positionY = PlayerPrefs.GetFloat("PositionY", 0);
        positionZ = PlayerPrefs.GetFloat("PositionZ", 0);
        sceneName = PlayerPrefs.GetString("SceneName", "Boss");
        treasure = PlayerPrefs.GetInt("Treasure", 1);
    }
}
