using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private static SoundManager instance;

    public static SoundManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<SoundManager>();

                if (instance == null)
                {
                    GameObject soundManagerObject = new GameObject("SoundManager");
                    instance = soundManagerObject.AddComponent<SoundManager>();
                }
            }
            return instance;
        }
    }


    public AudioClip Shootpistol;
    private AudioSource pistol;

    public AudioClip Shootrifle;
    private AudioSource rifle;

    public AudioClip Shootshotgun;
    private AudioSource shotgun;

    public AudioClip useaxe;
    private AudioSource axe;

    public AudioClip usedagger;
    private AudioSource dagger;

    public AudioClip usesword;
    private AudioSource sword;

    public AudioClip hit;
    private AudioSource attack;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        pistol = gameObject.AddComponent<AudioSource>();
        rifle = gameObject.AddComponent<AudioSource>();
        shotgun = gameObject.AddComponent<AudioSource>();
        axe = gameObject.AddComponent<AudioSource>();
        dagger = gameObject.AddComponent<AudioSource>();
        sword = gameObject.AddComponent<AudioSource>();
        attack = gameObject.AddComponent<AudioSource>();
    }

    public void PlayAttackSound()
    {
        attack.Play();
    }

    public void PlayPistolSound()
    {
        pistol.Play();
    }

    public void PlayRifleSound()
    {
        rifle.Play();
    }

    public void PlayShotgunSound()
    {
        shotgun.Play();
    }

    public void PlayAxeSound()
    {
        axe.Play();
    }

    public void PlayDaggerSound()
    {
        dagger.Play();
    }

    public void PlaySwordSound()
    {
        sword.Play();
    }

}
