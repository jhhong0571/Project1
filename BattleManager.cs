using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public static BattleManager instance;

    public int onBattle;
    public Vector3 originalPosition;
    public string originalSceneName;

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
        PlayerPrefs.SetInt("onBattle", onBattle);
        PlayerPrefs.SetFloat("originalPositionX", originalPosition.x);
        PlayerPrefs.SetFloat("originalPositionY", originalPosition.y);
        PlayerPrefs.SetFloat("originalPositionZ", originalPosition.z);
        PlayerPrefs.SetString("originalSceneName", originalSceneName);

        PlayerPrefs.Save();
    }

    public void LoadPlayerData()
    {
        onBattle = PlayerPrefs.GetInt("onBattle", 0);
        originalPosition.x = PlayerPrefs.GetFloat("originalPositionX",0);
        originalPosition.y = PlayerPrefs.GetFloat("originalPositionY", 0);
        originalPosition.z = PlayerPrefs.GetFloat("originalPositionZ", 0);
        originalSceneName = PlayerPrefs.GetString("originalSceneName", "Boss");
    }
}
