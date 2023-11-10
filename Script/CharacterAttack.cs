using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAttack : MonoBehaviour
{
    private Transform characterBody;

    Animator animator;
    bool isAttack = false;
    bool hasWeapon = false;

    void Awake()
    {
        animator = characterBody.GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        animator = characterBody.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Attack();
    }

    public void Attack()
    {
        if (Input.GetMouseButtonDown(0))
        {
            animator.SetTrigger("isAttack");
        }
    }
}
