using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    // 저장할 변수
    public int maxHealth;
    public int health;
    public float initialDefense;
    public int initialDamage;
    public float initialCriticalDamage;
    public float initialCriticalChance;

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
        // 저장
        PlayerPrefs.SetInt("MaxHealth", maxHealth);
        PlayerPrefs.SetInt("health", health);
        PlayerPrefs.SetFloat("InitialDefense", initialDefense);
        PlayerPrefs.SetInt("InitialDamage", initialDamage);
        PlayerPrefs.SetFloat("InitialCriticalDamage", initialCriticalDamage);
        PlayerPrefs.SetFloat("InitialCriticalChance", initialCriticalChance);

        PlayerPrefs.Save();
    }

    public void LoadPlayerData()
    {
        PS ps = PS.instance;
        // 로드
        ps.maxHealth = PlayerPrefs.GetInt("MaxHealth", 100);
        ps.health = PlayerPrefs.GetInt("health", 100);
        ps.InitialDefense = PlayerPrefs.GetFloat("InitialDefense", 0.1f);
        ps.InitialDamage = PlayerPrefs.GetInt("InitialDamage", 40);
        ps.InitialCriticalDamage = PlayerPrefs.GetFloat("InitialCriticalDamage", 1.5f);
        ps.InitialCriticalChance = PlayerPrefs.GetFloat("InitialCriticalChance", 0.2f);
    }

}
