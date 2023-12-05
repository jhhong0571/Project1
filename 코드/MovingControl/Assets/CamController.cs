using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamController : MonoBehaviour
{
    [SerializeField]
    private Transform characterBody;
    [SerializeField]
    private Transform cameraArm;

    Animator animator;

    public FixedTouchField touchField;
    public FixedJoystick joystick;

    void Start()
    {
        animator = characterBody.GetComponent<Animator>();
        Move();
    }

    void Update()
    {
        LookAround();
    }

    private void LookAround()
    {
        Vector2 Delta = new Vector2(touchField.TouchDist.x, touchField.TouchDist.y);
        Vector3 camAngle = cameraArm.rotation.eulerAngles;

        float x = camAngle.x - Delta.y;
        if (x < 180f)
        {
            x = Mathf.Clamp(x, -1f, 70f);
        }
        else
        {
            x = Mathf.Clamp(x, 335f, 361f);
        }

        cameraArm.rotation = Quaternion.Euler(x, camAngle.y + Delta.x, camAngle.z);
    }

    private void Move()
    {
        Vector2 moveInput = new Vector2(joystick.input.x, joystick.input.y);
        bool isMove = moveInput.magnitude != 0;
        animator.SetBool("isMove", isMove);
        if (isMove)
        {
            Vector3 lookForward = new Vector3(cameraArm.forward.x, 0f, cameraArm.forward.z).normalized;
            Vector3 lookRight = new Vector3(cameraArm.right.x, 0f, cameraArm.right.z).normalized;
            Vector3 moveDir = lookForward * moveInput.y + lookRight * moveInput.x;

            characterBody.forward = lookForward;
            transform.position += moveDir * Time.deltaTime * 5f;
        }
    }


}
