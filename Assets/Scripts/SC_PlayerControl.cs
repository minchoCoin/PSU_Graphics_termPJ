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
    [SerializeField]
    private KeyCode KeyCodeToggleAuto = KeyCode.B;
    [SerializeField]
    private KeyCode KeyCodeReload = KeyCode.R;

    [SerializeField]
    private WeaponSetting weaponSetting;

    private SC_MouseControle rotateToMouse;
    private SC_PlayerMove moveKeyborad;
    private SC_Status status;
    private SC_PlayerAnimatorControler animatorControler;
    private AudioSource audioSC;
    private SC_WeaponRifle weaponRifle;

    public int MaxHp => GetComponent<SC_PlayerHP>().maxHP;
    public int CurrentHp => GetComponent<SC_PlayerHP>().HP;
    public int MaxAmmo => weaponRifle.MaxAmmo;
    public int CurrentAmmo => weaponRifle.CurrentAmmo;
    public bool IsAuto => weaponRifle.IsAuto;

    public int Score = 0;

    void Awake()
    {
        moveKeyborad = GetComponent<SC_PlayerMove>();
        rotateToMouse = GetComponent<SC_MouseControle>();
        status = GetComponent<SC_Status>();
        animatorControler = GetComponent<SC_PlayerAnimatorControler>();
        audioSC = GetComponent<AudioSource>();
        weaponRifle = GetComponentInChildren<SC_WeaponRifle>();


        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
       
    }

    // Update is called once per frame
    void Update()
    {
        UpdateMouseRotation();
        UpdateKeyboardMove();
        UpdateJump();
        UpdateWeponAction();
        UpdateWeaponModeToggle();
        UpdateReload();
        animatorControler.DebugAnimatorState(); // ���� ���� �����

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
            if (z_keyboard > 0 && !animatorControler.IsInState("fireRifle") && !animatorControler.IsInState("reloadRifle") ){
                isRun = Input.GetKey(KeyCodeRun);
            }
         
            if (isRun)
            {
                animatorControler.AnimeMoveSpeed = 1;
                moveKeyborad.SetMoveSpeed = status.RunSpeed;
                audioSC.clip = audioClipRun;
                //Debug.Log("Player is Running");
            }
            else
            {
                animatorControler.AnimeMoveSpeed = 0.5f;
                moveKeyborad.SetMoveSpeed = status.WalkSpeed;
                audioSC.clip = audioClipWalk;
                //Debug.Log("Player is Walking");
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
            animatorControler.AnimeMoveSpeed = 0;
            //Debug.Log("Player is Idle");

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

    private void UpdateWeponAction()
    {
        if (Input.GetMouseButtonDown(0))
            weaponRifle.StartWeaponAction();
        else if(Input.GetMouseButtonUp(0))
            weaponRifle.StopWeaponAction();
    }

    private void UpdateWeaponModeToggle()
    {
        if (Input.GetKeyDown(KeyCodeToggleAuto)) // B Ű�� ������ ��
        {
            if (weaponRifle != null)
            {
                weaponRifle.ChangeAuto(); // SC_WeaponRifle�� �޼��� ȣ��
            }
            else
            {
               // Debug.LogError("WeaponRifle�� null�Դϴ�.");
            }
        }
    }

    private void UpdateReload()
    {
        if (Input.GetKeyDown(KeyCodeReload)) // R Ű�� ������ ��
        {
            if (weaponRifle != null)
            {
                weaponRifle.Reload(); // SC_WeaponRifle�� Reload �޼��� ȣ��
            }
            else
            {
               // Debug.LogError("WeaponRifle�� null�Դϴ�.");
            }
        }
    }

}
