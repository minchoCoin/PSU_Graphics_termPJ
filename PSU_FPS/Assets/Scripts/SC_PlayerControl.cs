using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_PlayerControl : MonoBehaviour
{
    [Header("Audio Clips")]
    [SerializeField]
    private AudioClip audioClipWalk;
    [SerializeField]
    private AudioClip audioClipRun;

    [Header("Input KeyBoardCodes")]
    [SerializeField]
    private KeyCode KeyCodeRun = KeyCode.LeftShift;
    [SerializeField]
    private KeyCode KeyCodeJump = KeyCode.Space;

    private SC_MouseControle rotateToMouse;
    private SC_PlayerMove moveKeyborad;
    private SC_Status status;
    private SC_PlayerAnimatorControler animatorControler;
    private AudioSource audioSC;
   


    void Awake()
    {
        moveKeyborad = GetComponent<SC_PlayerMove>();
        rotateToMouse = GetComponent<SC_MouseControle>();
        status = GetComponent<SC_Status>();
        animatorControler = GetComponent<SC_PlayerAnimatorControler>();
        audioSC = GetComponent<AudioSource>();

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
       
    }

    // Update is called once per frame
    void Update()
    {
        UpdateMouseRotation();
        UpdateKeyboardMove();
        UpdateJump();

    }


    private void UpdateMouseRotation()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");
        //Debug.Log(mouseX);
        //Debug.Log("\n");
        //Debug.Log(mouseY);

        rotateToMouse.UpdateRotation(mouseX, mouseY);
    }

    private void UpdateKeyboardMove()
    {
        float x_keyboard = Input.GetAxis("Horizontal");
        float z_keyboard = Input.GetAxis("Vertical");

        if (x_keyboard != 0 || z_keyboard != 0)
        {
            bool isRun = false;
            if (z_keyboard > 0) isRun = Input.GetKey(KeyCodeRun);

            if (isRun)
            {
                animatorControler.SetAnimeMoveSpeed = 1;
                moveKeyborad.SetMoveSpeed = status.RunSpeed;
                audioSC.clip = audioClipRun;
            }
            else
            {
                animatorControler.SetAnimeMoveSpeed = 0.5f;
                moveKeyborad.SetMoveSpeed = status.WalkSpeed;
                audioSC.clip = audioClipWalk;
            }

            if (!audioSC.isPlaying)
            {
                audioSC.loop = true;
                audioSC.Play();
            }
        }
        else
        {
            moveKeyborad.SetMoveSpeed = 0;
            animatorControler.SetAnimeMoveSpeed = 0;

            if (audioSC.isPlaying)
            {
                audioSC.Stop();
            }

        }
        moveKeyborad.MoveTo(new Vector3(x_keyboard, 0, z_keyboard));
    }

    private void UpdateJump()
    {
        if (Input.GetKey(KeyCodeJump))
        {
            moveKeyborad.Jump();
        }
    }

}
