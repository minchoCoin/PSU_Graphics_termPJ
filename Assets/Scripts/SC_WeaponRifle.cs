using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_WeaponRifle : MonoBehaviour
{
    public SC_PlayerControl playerControl=null;
    [Header("weapon setting")]
    [SerializeField]
    private WeaponSetting weaponSetting;

    private float lastAttackTime = 0;
    private float lastReloadTime = 0;
    private bool isReloading = false;
  


    [Header("weapon Audio Clips")]
    [SerializeField]
    private AudioClip AC_TakeOutWeapon;

    [Header("Muzzle flashs")]
    [SerializeField]
    private GameObject muzzleFlashEffect;

    private AudioSource audioSC;
    private SC_PlayerAnimatorControler animatorControler;

    [Header("Bullets")]
    public Rigidbody Bullet; // �Ѿ� Prefab
    public AudioClip GunShot; // �Ѿ� �߻� �Ҹ�
    public float BulletSpeed; // �Ѿ� �ӵ�
    private Transform bulletSpawnPoint; // �Ѿ� �߻� ��ġ
    private int BulletDamage = 15;
    public int MaxAmmo => weaponSetting.maxAmmo;
    public int CurrentAmmo => weaponSetting.currentAmmo;
    public bool IsAuto => weaponSetting.isAuto;

    private void PlaySound(AudioClip NewCips)
    {
        audioSC.Stop();
        audioSC.clip = NewCips;
        audioSC.Play();
    }

    private void Awake()
    {
        playerControl = GetComponentInParent<SC_PlayerControl>();
        audioSC = GetComponent<AudioSource>();
        animatorControler = GetComponentInParent<SC_PlayerAnimatorControler>();

        bulletSpawnPoint = transform.Find("arms/assault_rifle_01/BulletSpawnPoint");
    }


    private void OnEnable()
    {
        PlaySound(AC_TakeOutWeapon);
        muzzleFlashEffect.SetActive(false);
    }
   
    void Update()
    {
        
    }

    public void StartWeaponAction(int type = 0)
    {
      if(weaponSetting.isAuto)
            StartCoroutine("OnAttackLoop");
        
        else
            OnAttack();
        
    }

    public void StopWeaponAction(int type = 0)
    {
        if (weaponSetting.isAuto)
            StopCoroutine("OnAttackLoop");

        animatorControler.ResetTrigger("IsAttack");

    }

    private IEnumerator OnAttackLoop()
    {
        while (true)
        {
            OnAttack();
            yield return null;
        }
    }

    public void ChangeAuto()
    {
        // isAuto ���� ���
        weaponSetting.isAuto = !weaponSetting.isAuto;
       // Debug.Log($"Weapon Auto Mode Toggled: {weaponSetting.isAuto}");
    }

    public void Reload()
    {
        if (isReloading) return; // �̹� ������ ���̸� �������� ����
        if (weaponSetting.currentAmmo == weaponSetting.maxAmmo) return;

        animatorControler.SetTrigger("Reload"); // ������ �ִϸ��̼� Ʈ����
        isReloading = true; // ������ ���� ����
        lastReloadTime = Time.time; // ������ ���� �ð� ���
        StartCoroutine("OnReloadEffect"); // ������ �ڷ�ƾ ����
    }

    private IEnumerator OnReloadEffect()
    {
       
        // �ִϸ��̼� ��� �ð�
        yield return new WaitForSeconds(3f);

        weaponSetting.currentAmmo = weaponSetting.maxAmmo;
        isReloading = false;

        Debug.Log("ź�� ������ �Ϸ�!");
    }

    public void OnAttack()
    {
        if (isReloading) return;

        if (weaponSetting.currentAmmo > 0)
        {
            if (Time.time - lastAttackTime > 1 / weaponSetting.attackSpeed)
            {
                if (animatorControler.AnimeMoveSpeed > 0.5f) return;

                lastAttackTime = Time.time;

                weaponSetting.currentAmmo -= 1;
                animatorControler.SetTrigger("IsAttack");

                animatorControler.PlayAnime("fireRifle", -1, 0);
                StartCoroutine("OnMuzzleFlashEffect");


                if (bulletSpawnPoint != null && Bullet != null)
                {
                   
                    Quaternion adjustedRotation = bulletSpawnPoint.rotation * Quaternion.Euler(90, 0, 0);

                    Rigidbody rb = Instantiate(Bullet, bulletSpawnPoint.position, bulletSpawnPoint.rotation);

                    SphereCollider collider = rb.gameObject.AddComponent<SphereCollider>();
                    collider.radius = 0.1f; 

                    
                    rb.collisionDetectionMode = CollisionDetectionMode.Continuous;

                    SC_Bullet bulletScript = rb.gameObject.AddComponent<SC_Bullet>();

                    
                    bulletScript.bulletDamage = BulletDamage;
                    bulletScript.PlayerControl = playerControl;
                    
                    Vector3 shootDirection = bulletSpawnPoint.forward; 
                    rb.velocity = shootDirection * BulletSpeed;

                    AudioSource.PlayClipAtPoint(GunShot, bulletSpawnPoint.position);
                }
                else
                {
                    // Debug.LogError($"�Ѿ� ���� ����: BulletSpawnPoint = {bulletSpawnPoint}, Bullet = {Bullet}");
                }

            }
        }
        else
        {
            Reload();
        }
    }

    private IEnumerator OnMuzzleFlashEffect()
    {
        muzzleFlashEffect.SetActive(true);
        yield return new WaitForSeconds(1/(weaponSetting.attackSpeed)*0.3f);
        muzzleFlashEffect.SetActive(false);
    }
}
