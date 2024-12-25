using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_WeaponRifle : MonoBehaviour
{
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
    public Rigidbody Bullet; // 총알 Prefab
    public AudioClip GunShot; // 총알 발사 소리
    public float BulletSpeed; // 총알 속도
    private Transform bulletSpawnPoint; // 총알 발사 위치
    private int BulletDamage = 20;
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
        // isAuto 값을 토글
        weaponSetting.isAuto = !weaponSetting.isAuto;
       // Debug.Log($"Weapon Auto Mode Toggled: {weaponSetting.isAuto}");
    }

    public void Reload()
    {
        if (isReloading) return; // 이미 재장전 중이면 실행하지 않음
        if (weaponSetting.currentAmmo == weaponSetting.maxAmmo) return;

        animatorControler.SetTrigger("Reload"); // 재장전 애니메이션 트리거
        isReloading = true; // 재장전 상태 설정
        lastReloadTime = Time.time; // 재장전 시작 시간 기록
        StartCoroutine("OnReloadEffect"); // 재장전 코루틴 실행
    }

    private IEnumerator OnReloadEffect()
    {
       
        // 애니메이션 대기 시간
        yield return new WaitForSeconds(3f);

        weaponSetting.currentAmmo = weaponSetting.maxAmmo;
        isReloading = false;

        Debug.Log("탄약 재장전 완료!");
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

                    
                    Vector3 shootDirection = bulletSpawnPoint.forward; 
                    rb.velocity = shootDirection * BulletSpeed;

                    AudioSource.PlayClipAtPoint(GunShot, bulletSpawnPoint.position);
                }
                else
                {
                    // Debug.LogError($"총알 생성 실패: BulletSpawnPoint = {bulletSpawnPoint}, Bullet = {Bullet}");
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
