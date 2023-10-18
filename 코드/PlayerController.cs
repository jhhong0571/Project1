using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public CharacterController SelectPlayer; // ������ ĳ���� ��Ʈ�ѷ�
    public float Speed;  // �̵��ӵ�
    private float Gravity; // �߷�   
    private Vector3 MoveDir; // ĳ������ �����̴� ����.
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
        rundodge = 21
    }
    // Start is called before the first frame update
    void Start()
    {
        // �⺻��
        Speed = 5.0f;
        Gravity = 10.0f;
        MoveDir = Vector3.zero;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (SelectPlayer == null) return;
        // ĳ���Ͱ� �ٴڿ� �پ� �ִ� ��츸 �۵��մϴ�.
        // ĳ���Ͱ� �ٴڿ� �پ� ���� �ʴٸ� �ٴ����� �߶��ϰ� �ִ� ���̹Ƿ�
        // �ٴ� �߶� ���߿��� ���� ��ȯ�� �� �� ���� �����Դϴ�.
        if (SelectPlayer.isGrounded)
        {
            // Ű���忡 ���� X, Z �� �̵������� ���� �����մϴ�.
            MoveDir = new Vector3(-Input.GetAxis("Horizontal"), 0, -Input.GetAxis("Vertical"));
            // ������Ʈ�� �ٶ󺸴� �չ������� �̵������� ������ �����մϴ�.
            MoveDir = SelectPlayer.transform.TransformDirection(MoveDir);
            // �ӵ��� ���ؼ� �����մϴ�.
            MoveDir *= Speed;
        }
        // ĳ���Ͱ� �ٴڿ� �پ� ���� �ʴٸ�
        else
        {
            // �߷��� ������ �޾� �Ʒ������� �ϰ��մϴ�.
            // �� �� �ٴڿ� ���� ������ -y���� ��� �������� ��ġ �߷°��ӵ� ���� ��ó�� �ۿ��մϴ�.
            MoveDir.y -= Gravity * Time.deltaTime;
        }
        // �� �ܰ������ ĳ���Ͱ� �̵��� ���⸸ �����Ͽ�����,
        // ���� ĳ������ �̵��� ���⼭ ����մϴ�.
        SelectPlayer.Move(MoveDir * Time.deltaTime);
        UpdateState();
    }

    public void FixedUpdate()
    {
        MoveCharacter();
    }

    public void MoveCharacter()
    {
        MoveDir.x = Input.GetAxis("Horizontal");
        MoveDir.z = Input.GetAxis("Vertical");
    }

    private void UpdateState()
    {
        bool CheckMove = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D);
        bool CheckMove2 = Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D);
        if (CheckMove)
        {
            animator.SetInteger(animationState, (int)Stats.run);
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
