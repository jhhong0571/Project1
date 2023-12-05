using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public CharacterController SelectPlayer; // 제어할 캐릭터 컨트롤러
    public float Speed;  // 이동속도
    private float Gravity; // 중력   
    private Vector3 MoveDir; // 캐릭터의 움직이는 방향.
    Animator animator;
    string animationState = "AnimationState";

    enum Stats
    {
        idle = 0,
        move = 1,
        run = 2,
        interact = 3,
        attack = 4,
        hit = 5,
        dodge = 6,
        moveattack = 11,
        movedodge = 12,
        rundodge = 21,
        xDir = 0,
        yDir = 0,
    }
    // Start is called before the first frame update
    void Start()
    {
        // 기본값
        Speed = 5.0f;
        Gravity = 10.0f;
        MoveDir = Vector3.zero;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (SelectPlayer == null) return;
        // 캐릭터가 바닥에 붙어 있는 경우만 작동합니다.
        // 캐릭터가 바닥에 붙어 있지 않다면 바닥으로 추락하고 있는 중이므로
        // 바닥 추락 도중에는 방향 전환을 할 수 없기 때문입니다.
        if (SelectPlayer.isGrounded)
        {
            float hAxis = Input.GetAxisRaw("Horizontal");
            float vAxis = Input.GetAxisRaw("Vertical");
            // 키보드에 따른 X, Z 축 이동방향을 새로 결정합니다.
            MoveDir = new Vector3(-hAxis, 0, -vAxis);
            // 오브젝트가 바라보는 앞방향으로 이동방향을 돌려서 조정합니다.
            MoveDir = SelectPlayer.transform.TransformDirection(MoveDir);
            // 속도를 곱해서 적용합니다.
            MoveDir *= Speed;
        }
        // 캐릭터가 바닥에 붙어 있지 않다면
        else
        {
            // 중력의 영향을 받아 아래쪽으로 하강합니다.
            // 이 때 바닥에 닿을 떄까지 -y값이 계속 더해져서 마치 중력가속돠 붙은 것처럼 작용합니다.
            MoveDir.y -= Gravity * Time.deltaTime;
        }
        // 앞 단계까지는 캐릭터가 이동할 방향만 결정하였으며,
        // 실제 캐릭터의 이동은 여기서 담당합니다.
        SelectPlayer.Move(MoveDir * Time.deltaTime);
        UpdateState();
    }

    private void UpdateState()
    {
        if(Mathf.Approximately(MoveDir.x, 0) && Mathf.Approximately(MoveDir.z, 0))
        {
            animator.SetBool("isMove", false);
        }
        else
        {
            animator.SetBool("isMove", true);
        }
        animator.SetFloat("xDir", MoveDir.x);
        animator.SetFloat("yDir", MoveDir.z);

        bool CheckMove = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D);
        bool CheckMove2 = Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D);
        if (CheckMove)
        {
            animator.SetInteger(animationState, (int)Stats.move);
        }
        else if (CheckMove && Input.GetKeyDown(KeyCode.LeftShift))
        {
            animator.SetInteger(animationState, (int)Stats.rundodge);
        }
        else if (CheckMove2 && Input.GetKey(KeyCode.LeftShift))
        {
            animator.SetInteger(animationState, (int)Stats.rundodge);
        }
        else if(CheckMove && Input.GetKeyDown(KeyCode.C))
        {
            animator.SetInteger(animationState, (int)Stats.move);
        }
        else if (CheckMove2 && Input.GetKey(KeyCode.C))
        {
            animator.SetInteger(animationState, (int)Stats.move);
        }
        else if(Input.GetKey(KeyCode.W) && Input.GetKeyDown(KeyCode.Mouse0))
        {
            animator.SetInteger(animationState, (int)Stats.moveattack);
        }
        else if (Input.GetKeyDown(KeyCode.W) && Input.GetKey(KeyCode.Mouse0))
        {
            animator.SetInteger(animationState, (int)Stats.moveattack);
        }
        else if (Input.GetKey(KeyCode.W) && Input.GetKeyDown(KeyCode.LeftShift))
        {
            animator.SetInteger(animationState, (int)Stats.movedodge);
        }
        else if (Input.GetKeyDown(KeyCode.W) && Input.GetKey(KeyCode.LeftShift))
        {
            animator.SetInteger(animationState, (int)Stats.movedodge);
        }
        else if (Input.GetKey(KeyCode.F))
        {
            animator.SetInteger(animationState, (int)Stats.interact);
        }
        else if (Input.GetKey(KeyCode.Mouse0))
        {
            animator.SetInteger(animationState, (int)Stats.attack);
        }
        else if (Input.GetKey(KeyCode.LeftShift))
        {
            animator.SetInteger(animationState, (int)Stats.dodge);
        }
        else
        {
            animator.SetInteger(animationState, (int)Stats.idle);
        }
        
    }
}
