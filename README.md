# 구현기능 설명
## 필수기능 구현
### 전승윤
- 키보드 또는 마우스를 사용한 입력 처리
  -	SC.PlayerContorol.cs에서 키보드 입력을 통해 움직임과 행동제어
  - Horiwontal, vertical입력 축으로 이동방향을 계산
  - KeycodeRun(LeftShift)로 달리기 모드 활성화
  - KeyCodeJump(jump)로 점프실행
  - KeyCodeReload (R)로 재장전 구현
  - KeyCodeToggleAuto(B)로 무기 연사/단발 모드 토글

  -	SC_MouseControl.cs에서 마우스의 X와Y 이동입력을 받아 카메라 회전 처리
  - UpdateRotaion(float mouseX, float mouseY)를 통해 캐릭터의 회전값 반영

- Rigidbody를 활용한 gameObject 움직임 제어
  - SC_WeaponRifle.cs에서 총알 오브젝트를 생성하고 Rigidbody를 부착하여 물리적 속성을 통해 움직임을 구현
  - Rigidbody의 velocity를 사용해 총알을 발사(shootDirection * Bullet Speed). 

  - SC_WeaponRifle.cs에서 총알 오브젝트를 생성하고 Rigidbody를 부착하여 물리적 속성을 통해 움직임을 구현
  - Rigidbody의 velocity를 사용해 총알을 발사(shootDirection * Bullet Speed). 

- 스크립트를 사용한 primitive GameObject Mesh제어
  - SC_WeaponRifle.cs에서 총알(Bullet) 오브젝트는 스크립트로 생성되며, SphereCollider를 추가하여 크기를 조정, collider.radius = 0.1f로 충돌 반경 설정.스크립트를 통해 물리적 속성 및 메시에 기반한 충돌관리


- 최소1개 이상의 Non-Primitive GameObject활용
  - SC_PlayerControl.cs에서 Animator와 관련된 캐릭터 Assets : AssetStore_Assets dir에서 관리, 발포, 장전, 달리기, 걷기, 대기 등 애니메이션을 활용

### 김태훈
- GameObject 간 충돌 처리
  - 좀비 오른쪽 손 부분에 작은 sphere를 부착하고, 해당 sphere에 sphere collider와 Rigidbody를 추가하였다. 이 collider와 player의 collider가 부딪히면 OnTriggerEnter 이벤트가 발생하고, 해당 이벤트가 발생하면 Player의 HP가 감소한다(데미지 양은 좀비 손에 있는 sphere에 적용된 스크립트 SC_ZombieHand의 Damage 속성값을 따른다). 또한 좀비 전체 mesh에도 collider를 추가하여, 총알과 부딪히면 좀비 체력이 감소하게 하였다.
  - 스크립트: SC_ZombieHand, SC_Bullet
- Non-primitive object 사용 및 NavMesh
  - 좀비 캐릭터와 애니메이션은 https://www.mixamo.com/ 에서 다운로드하였으며, 캐릭터는 Yaku J Ignite 모델을 사용, 애니메이션은 Zombie Attack, Zombie Dying, Zombie Idle, Zombie Reaction Hit, Zombie Run, Zombie Walk이다. Zombie Attack은 좀비와 Player 거리가 2.5 이하일 때 동작하고, Zombie Dying은 좀비의 체력이 0일 때 동작하며, Zombie Reaction Hit은 총알과 부딪혔을 때, Zombie Walk는 좀비가 걸어갈 때, Zombie Idle은 좀비가 가만히 있을 때 작동한다. Zombie Run은 좀비와 Player 거리가 8이하일 때 작동한다. NavMesh AI와 Agent를 사용하여, 좀비가 이동할 수 있는 곳을 지정하고, 좀비와 Player 간 거리에 따라 이동 속도를 다르게 하였다. Player와 거리가 멀 때는 4초 가만히 있다가 10초 걷고(랜덤한 Waypoint를 향해 걷는다), Player와 거리가 가까울 때는 달린다.
  - 스크립트: ZombieAttackingState.cs, ZombieIldeState.cs, ZombieWalkingState.cs, ZombieRunningState.cs
 
### 문성필
- 최소1개 이상의 Non-Primitive GameObject활용(맵 구현)플레이어와 좀비들이 움직이는 맵 구현 – Unity Asset Store 활용

### 윤선재
- 키보드 또는 마우스를 사용한 입력 처리
  - SC_UI.cs에서 마우스 입력을 통해 게임을 실행시킨다.

- 최소1개 이상의 Non-Primitive GameObject활용
  - Menu Panel : 시작 화면 구성. UI의 Panel로 구현. 중앙에 게임 시작 버튼을 가지고 있으며, 클릭하면 카메라가 전환되면서 게임을 시작한다.
  - Menu Camera : 메뉴 패널을 비추는 카메라. Camera로 구현. 게임을 실행시키는 시점에 활성화되어 있으며, 게임이 시작될 때 비활성화 된다.
  - Game Panel : 게임 화면 구성. UI의 Panel로 구현. 좌측 하단에는 체력 정보를, 우측 하단에는 무기 정보를 보여준다.
 

## 필수기능 구현
### 전승윤
- CharacterController를 이용한 움직임 구현
  - Player Object의 이동 물리적 구현-> unity의 character controler 입력방향처리(direction),속도계산(moveForce), Jump(CharacterController.isGrounded, jumpPower)등의 움직임 구현

- Player Object의 중력처리
  - CharacterController.isGrounded를 사용하여 캐릭터가 공중에 있을경우 중력 계수(coefficientGravity)와 시간(Time.deltaTime)을 곱해 Y축으로 지속적인 힘을 가한다.

- Player Object의 애니메이션 구현
  - Unity AssetStore의 asset을 사용하여 애니메이션을 구현 
  - PlayerMovent는 Blend Tree를 이용해 PlayerSpeed parameter를 사용해 제어
  - reloadRifle, fireRifle의 경우 isAttack, Reload tirriger parameter를 이용해 제어
  - 추가적으로 player의 행동의 우선도를 제어하기위해 애니메이션의 상태를 이용(IsInState)

- script를 통한 player의 행동 기능 전반 구현
  - player캐릭터전반적인 구현(움직임, 행동순위등등), player 총기의 전반적인 구현(발사, 발사모드 변경 등등)
  - 작성 Script : SC_CameraAim, SC_MouseControl, SC_PlayerAnimatorControler, SC_PlayerControl, SC_PlayerHP, SC_PlayerMove, SC_Status, SC_WeaponRifle, SC_WeaponSetting

### 김태훈
- PlayerDead animation
  - Player의 체력이 0이되어 죽을 때 재생되는 애니메이션을 제작하였다. 해당 애니메이션은 60프레임 동안 y값이 약 1.5 감소하고, z축으로 90도 회전하여 쓰러지는 장면을 연출하였다.
- ZombieSpawner
  - Cube를 하나 생성하고, 해당 좌표에 좀비가 생성되도록 하였다. 좀비는 처음 시작 시 1마리 소환되며, 모든 좀비가 죽으면 10초 뒤 이전 좀비 수의 2배의 좀비가 생성된다.
- BloodyScreen
  - 좀비에게 공격을 받으면, 화면에 붉은색 이팩트가 표시된다.
### 윤선재
- 스크립트를 이용한 UI 로직 구현
  - SC_UI.cs
	  - GameStart() : 처음 시작 화면에서 게임 시작 버튼의 동작을 구현. 버튼을 클릭하면 카메라가 Main Camera로 전화되며 메뉴 패널이 비활성화되고 체력과 잔탄 수가 나타나는 게임 패널이 활성화되며 게임이 시작된다.
    - Update() : 게임 패널에서 실시간으로 현재 체력과 잔탄 수를 갱신한다.

# 게임플레이
## 게임 조작방법
### 이동
기본적인 이동은 WASD, 점프는 space로 가능하다.
### 화면제어
마우스를 사용해 플레이어의 시점을 변경한다
### 재장전
총알이 부족할때는 R키를 눌러 장전이 가능하다. 장전중에는 달리기, 총알 발사가 불가능하다
### 발사모드설정
B키를 눌러 발사모드를 정할 수 있다.(단발/연발)
### 발사/몬스터 처치
마우스의 좌클릭을 통해 총알을 발사 할 수 있다. 이때 발사중에는 달리기가 불가능하다. 이 총알의 데미지는 20이며 좀비의 체력은100이다.
### 달리기
달리기는 shift를 누른 상태로 이동을 하면 달린다.
### 피격
좀비에게 피격당할시 화면에 붉은 이펙트가 표시되며 20의 데미지를 받는다. 플레이어의 총 체력은 100이다.

## GAME OVER
본게임의 목적은 최대한 오래 살아남는 것이다. 한 stage의 좀비를 모두 처치하면 다음 stage에는 좀비수가 2배로 증가하여 스폰된다. 좀비에게 모든 체력이 소진될 시 GAME OVER가 되며 player character가 눕는다.

# 동작 영상
[![IMAGE ALT TEXT HERE](https://img.youtube.com/vi/rrctW8OyPQQ/0.jpg)](https://youtu.be/rrctW8OyPQQ)

# Demo video(en)
[![IMAGE ALT TEXT HERE](https://img.youtube.com/vi/Rz6-eAav3hc/0.jpg)](https://youtu.be/Rz6-eAav3hc)


# References
[FPS Full Game Tutorial | Unity by Mike's code](https://www.youtube.com/playlist?list=PLtLToKUhgzwm1rZnTeWSRAyx9tl8VbGUE)
