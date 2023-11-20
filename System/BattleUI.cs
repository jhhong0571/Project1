using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleUI : MonoBehaviour
{
    public GameObject Attack;
    public GameObject Pistol;
    public GameObject Rifle;
    public GameObject Shotgun;
    public GameObject Sword;
    public GameObject Axe;
    public GameObject Dagger;
    //Enemy
    public GameObject EnemyAttack;
    public GameObject Enemyhit;
    //¿Ã∆Â∆Æ
    public GameObject Blood;
    public GameObject PBlood;
    public GameObject Bullet;

    //∞Ê∞Ì
    public GameObject Waring;

    // Start is called before the first frame update
    void Start()
    {
        SetAttack(false);
        SetPistol(false);
        SetRifle(false);
        SetShotgun(false);
        SetSword(false);
        SetAxe(false);
        SetDagger(false);
        SetEnemy(false);
        SetEnemyHit(false);
        SetBlood(false);
        SetPBlood(false);
        SetBullet(false);
        SetWaring(false);
    }
    public void SetWaring(bool isActive)
    {
        Waring.SetActive(isActive);
    }

    public void SetAttack(bool isActive)
    {
        Attack.SetActive(isActive);
    }

    public void SetPistol(bool isActive)
    {
        Pistol.SetActive(isActive);
    }

    public void SetRifle(bool isActive)
    {
        Rifle.SetActive(isActive);
    }

    public void SetShotgun(bool isActive)
    {
        Shotgun.SetActive(isActive);
    }

    public void SetSword(bool isActive)
    {
        Sword.SetActive(isActive);
    }

    public void SetAxe(bool isActive)
    {
        Axe.SetActive(isActive);
    }

    public void SetDagger(bool isActive)
    {
        Dagger.SetActive(isActive);
    }

    public void SetEnemy(bool isActive)
    {
        EnemyAttack.SetActive(isActive);
    }

    public void SetEnemyHit(bool isActive)
    {
        Enemyhit.SetActive(isActive);
    }

    public void SetBlood(bool isActive)
    {
        Blood.SetActive(isActive);
    }

    public void SetPBlood(bool isActive)
    {
        PBlood.SetActive(isActive);
    }

    public void SetBullet(bool isActive)
    {
        Bullet.SetActive(isActive);
    }

    // Update is called once per frame
    void Update()
    {
    }
}
