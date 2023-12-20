using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using System.Collections;


[RequireComponent(typeof(CharacterController))]
public class PlayerControllerWithCamera : MonoBehaviour
{
    [Header("�ړ��̑���"), SerializeField]
    private float _speed = 3;
    private float weapon_speed = 0.3f;
    
    [Header("run"), SerializeField]
    private float run_speed = 2f;

    [Header("damage"), SerializeField]
    private float damage_speed = 0.8f;

    [Header("�W�����v����u�Ԃ̑���"), SerializeField]
    private float _jumpSpeed = 7;

    [Header("�d�͉����x"), SerializeField]
    private float _gravity = 15;

    [Header("�������̑��������iInfinity�Ŗ������j"), SerializeField]
    private float _fallSpeed = 10;

    [Header("�����̏���"), SerializeField]
    private float _initFallSpeed = 2;

    [Header("�J����"), SerializeField]
    private Camera _targetCamera;

    [Header("リロード終わり音"), SerializeField]
    private AudioSource relordstart;
    [Header("リロード開始音"), SerializeField]
    private AudioSource relordend;

    public Cinemachinecamara Vircamscript;
    public float gunguidrange;

    public float look_sensi;
    public float aimlook_length;

    private Transform _transform;
    private CharacterController _characterController;

    private Vector2 _inputMove;
    private Vector2 _look;
    bool aim=false;
    bool pastaim=false;

    bool run = false;
    bool damage = false;

    public float stmax = 1000;
    public float st = 1000;
    private bool stheal=true;
    private float sthealtimer=0f;
    public float sthealtime=1f;

    public Slider stBar;

    private float _verticalVelocity;
    private float _turnVelocity;
    private bool _isGroundedPrev;

    private Animator anim;
    [Header("リロードの有効無効にします。無効時bUIはいらなくなります。"), SerializeField]
    private bool relordfunction=false;
    bool endrelord=false;
    private bool walk=false;
    bool relordmode=false;
    LineRenderer itemguid;
    private GameObject[] itemlist;
    public weaponscript weapsc;
    LineRenderer gunLine;
    Ray shootRay = new Ray();
    RaycastHit shootHit;
    private GameManager gameManager;
    public bulletLvUI bUI;
    public sordLvUI sUI;
    private bool wepchange=false;
    private bool pastchange=false;
    private bool wait =false;
    private bool runwait =false;
    private bool slowwait =false;
    private bool Damagewait =false;
    void Start()
    {
        gunLine = GetComponent <LineRenderer> ();
        anim = gameObject.GetComponent<Animator>();
        gameManager = FindObjectOfType<GameManager>();
    }

    /// <summary>
    /// �ړ�Action(PlayerInput������Ă΂��)
    /// </summary>
    public void OnMove(InputAction.CallbackContext context)
    {
        if(!gameManager.playerinput)return;
        _inputMove = context.ReadValue<Vector2>();
    }
    public void OnLook(InputAction.CallbackContext context)
    {
        if(!gameManager.playerinput)return;
        _look = context.ReadValue<Vector2>();
    }
    public void OnAim(InputAction.CallbackContext context)
    {
        if(!gameManager.playerinput)return;
        if (!context.performed){
            aim=false;
            
            return;
        }
        aim=true;
        
        // ���͒l��ێ����Ă���

    }
    public void OnRelord(InputAction.CallbackContext context){
        if(!gameManager.playerinput)return;
        if(relordfunction==false)return;
        if(anim.GetBool("relord"))return;
        if (!context.performed){
            relordmode=false;
            
            return;
        }
        relordmode=true;
    }
    public void OnChangeWeapon(InputAction.CallbackContext context){
        if(!gameManager.playerinput)return;
        if(anim.GetBool("relord"))return;
        if (!context.performed){
            wepchange=false;
            return;
        }
        wepchange=true;
    }

    public void OnRun(InputAction.CallbackContext context)
    {
        if(!gameManager.playerinput)return;
        if (!context.performed || st <= 30f)
        {
            run = false;

            return;
        }
        run = true;
    }

    public void P_Damage()
    {
        StartCoroutine("Wait_damage");
    }

    IEnumerator Wait_damage()
    {
        damage = true;
        anim.SetBool("itaiyou", damage);
        yield return new WaitForSeconds(1);
        damage = false;
        anim.SetBool("itaiyou", damage);
    }

    /// <summary>
    /// �W�����vAction(PlayerInput������Ă΂��)
    /// </summary>
    public void OnJump(InputAction.CallbackContext context)
    {
        if(!gameManager.playerinput)return;
        // �{�^���������ꂽ�u�Ԃ����n���Ă��鎞��������
        if (!context.performed || !_characterController.isGrounded) return;

        // ����������ɑ��x��^����
        _verticalVelocity = _jumpSpeed;
    }

    private void Awake()
    {
        _transform = transform;
        _characterController = GetComponent<CharacterController>();

        if (_targetCamera == null)
            _targetCamera = Camera.main;
    }

    public void Dash(float stdown)
    {
        if (st <= 0) return;

        st = st - stdown;
        sthealtimer=0f;
        if (stBar != null)
        {
            stBar.value = (int)st;
        }
        if (st <= 0)
        {
            st = 0;
            run = false;
        }
    }

    public void stGain(float gain)
    {
        if (st >= stmax || st < 0||!stheal) return;

        st = st + gain;
        if (st >= stmax) st = stmax;
        if (stBar != null)
        {
            stBar.value = (int)st;
        }
    }
    private void DamageSound() //歩いた時のSound
        {
            if (Damagewait==false) {
            StartCoroutine("Damage_Wait_sound");
        }
        }
        IEnumerator Damage_Wait_sound()
        {
            gameManager.Sounddamage();
            Damagewait = true;
            yield return new WaitForSeconds(1.0f);
            Damagewait = false;
        }
    private void Footsound() //歩いた時のSound
        {
            if (wait==false) {
            StartCoroutine("Wait_sound");
        }
        }
        IEnumerator Wait_sound()
        {
            gameManager.Soundfoot();
            wait = true;
            yield return new WaitForSeconds(0.5f);
            wait = false;
        }
    private void RunFootsound() //走った時のSound
        {
            if (runwait==false) {
            StartCoroutine("Run_Wait_sound");
        }
        }
    IEnumerator Run_Wait_sound()
        {
            gameManager.Soundfoot();
            runwait = true;
            yield return new WaitForSeconds(0.2f);
            runwait = false;
        }
    private void SlowFootsound() //遅歩きのSound
        {
            if (slowwait==false) {
            StartCoroutine("Slow_Wait_sound");
        }
        }
        IEnumerator Slow_Wait_sound()
        {
            gameManager.Soundfoot();
            slowwait = true;
            yield return new WaitForSeconds(0.8f);
            slowwait = false;
        }

    private void Update()
    {
        if(damage) //ダメージ受けたとき
        {
            DamageSound();
        }
        if(anim.GetBool("Die"))return;
        if(Time.timeScale==0f)return;
        var isGrounded = _characterController.isGrounded;
        //着地状態の処理。ジャンプに関係
        if (isGrounded && !_isGroundedPrev)
        {
            // ���n����u�Ԃɗ����̏������w�肵�Ă���
            _verticalVelocity = -_initFallSpeed;
        }
        else if (!isGrounded)
        {
            // �󒆂ɂ���Ƃ��́A�������ɏd�͉����x��^���ė���������
            _verticalVelocity -= _gravity * Time.deltaTime;

            // �������鑬���ȏ�ɂȂ�Ȃ��悤�ɕ␳
            if (_verticalVelocity < -_fallSpeed)
                _verticalVelocity = -_fallSpeed;
        }

        _isGroundedPrev = isGrounded;

        // �J�����̌����i�p�x[deg]�j�擾
        var cameraAngleY = _targetCamera.transform.eulerAngles.y;
        //ボタン入力による武器替え処理
        bool relo=anim.GetBool("relord");
        if(relo){
        }else if(wepchange!=pastchange){
            if(!wepchange){
                pastchange=wepchange;
            }else{
                if(weapsc.changenextweapon()){
                    gameManager.Soundchangeweapon();
                    bUI.changeweapon(weapsc.weaponnumber[1]);
                    sUI.changeweapon(weapsc.weaponnumber[0]);
                }
                pastchange=wepchange;
            }
        }
        //AIM武器持ち替え処理　リロード中は無視
        var moveVelocity = new Vector3(0,0,0);
        if(relo){
        }else if(pastaim!=aim||endrelord){
            if(aim){
                gameManager.Soundchangeweapon();
                weapsc.changeweapon(1);
                anim.SetInteger("type",1);
                gunLine.enabled = true;
            }else{
                gameManager.Soundchangeweapon();
                weapsc.changeweapon(0);
                anim.SetInteger("type",0);
                gunLine.enabled = false;
            }
            endrelord=false;
            pastaim=aim;
        }
        //リロード処理
        if(relordmode==true&&weapsc.acseceweapon(1).GetComponent<WeaponStates>().relordcheck()){
            weapsc.changeweapon(1);
            relordstart.Play();
            anim.SetInteger("type",1);
            anim.SetBool("relord",true);
            gunLine.enabled = false;
            Invoke("relordsound", 2.0f);
            Invoke("Relordbullet", 2.5f);
            relordmode=false;
        }else{
            relordmode=false;
        }
        //移動
        weapon_speed=weapsc.weapon.GetComponent<WeaponStates>().speed;

        if (damage)
        {
            moveVelocity = new Vector3(
                _inputMove.x * weapon_speed * _speed * damage_speed,
                _verticalVelocity,
                _inputMove.y * weapon_speed * _speed * damage_speed
            );
        }
        else if (run&&_inputMove != Vector2.zero){
            Dash(250*Time.deltaTime);
            GameObject[] arbinoenemy = GameObject.FindGameObjectsWithTag("arbinoEnemy");
            foreach(GameObject obj in arbinoenemy){
                obj.GetComponent<arbinoPatrol>().sound=6;
            }
            moveVelocity = new Vector3(
                _inputMove.x *weapon_speed*run_speed* _speed,
                _verticalVelocity,
                _inputMove.y *weapon_speed*run_speed* _speed
            );
        }else{
            moveVelocity = new Vector3(
                _inputMove.x * weapon_speed*_speed,
                _verticalVelocity,
                _inputMove.y * weapon_speed*_speed
            );
        }

        moveVelocity = Quaternion.Euler(0, cameraAngleY, 0) * moveVelocity;

        // ���݃t���[���̈ړ��ʂ��ړ����x����v�Z
        var moveDelta = moveVelocity * Time.deltaTime;

        // CharacterController�Ɉړ��ʂ��w�肵�A�I�u�W�F�N�g�𓮂���
        _characterController.Move(moveDelta);
        //linelenderer(予測射線)
        if(aim){
            shootRay.origin = transform.position;
            shootRay.direction = transform.forward;
            gunLine.SetPosition (0, transform.position);
            int shootableMask = LayerMask.GetMask ("enemy");
            if(Physics.Raycast (shootRay, out shootHit, gunguidrange, shootableMask))
            {
                gunLine.SetPosition (1, shootHit.point);
            }
            else
            {
                gunLine.SetPosition (1, shootRay.origin + shootRay.direction * gunguidrange);
            }
        }
        //移動中何をするか
        if(_inputMove != Vector2.zero)
        {
            if(aim)
            {
                SlowFootsound();
            }
            else if(run)
            {
                RunFootsound();
            }
            else
            {
                Footsound();
            }
            walk = true;
            anim.SetBool("run", run);
            anim.SetBool("walk", walk);
            guidupdate();   //緑のやじるし更新
            if(!run)
            {
                sthealtimer+=Time.deltaTime;
                //print($"velocity = {_inputMove}");
                if(sthealtimer<sthealtime){
                    stheal=false;
                }else{
                    stheal=true;
                }
                stGain(30*Time.deltaTime);
            }
        }
        else if(_inputMove == Vector2.zero)
        {
            sthealtimer+=Time.deltaTime*2;
            if(sthealtimer<sthealtime){
                    stheal=false;
            }else{
                stheal=true;
            }
            walk = false;
            //print($"velocity = stop");
            anim.SetBool("walk", walk);
            stGain(100*Time.deltaTime);
        }
        
        if(!aim){
            Vircamscript.visionlengthX=0;
            Vircamscript.visionlengthZ=0;
            if (_look != Vector2.zero)
            {
                Vector2 A=new Vector2(_look.x,_look.y);
                A=A.normalized;
                if(_look.x/A.x<=1){
                    Vircamscript.visionlengthX=aimlook_length*_look.x/A.x;
                    Vircamscript.visionlengthZ=aimlook_length*_look.x/A.x;
                }else{
                    Vircamscript.visionlengthX=aimlook_length;
                    Vircamscript.visionlengthZ=aimlook_length;
                }
                // �ړ����͂�����ꍇ�́A�U�����������s��

                // ������͂���y������̖ڕW�p�x[deg]���v�Z
                var targetAngleY = -Mathf.Atan2(_look.y, _look.x)
                    * Mathf.Rad2Deg + 90;
                // �J�����̊p�x�������U������p�x��␳
                targetAngleY += cameraAngleY;

                // �C�[�W���O���Ȃ��玟�̉�]�p�x[deg]���v�Z
                var angleY = Mathf.SmoothDampAngle(
                    _transform.eulerAngles.y,
                    targetAngleY,
                    ref _turnVelocity,
                    0.1f
                );

                // �I�u�W�F�N�g�̉�]���X�V
                _transform.rotation = Quaternion.Euler(0, angleY, 0);
            }else if (_inputMove != Vector2.zero)
            {
                // �ړ����͂�����ꍇ�́A�U�����������s��

                // ������͂���y������̖ڕW�p�x[deg]���v�Z
                var targetAngleY = -Mathf.Atan2(_inputMove.y*look_sensi, _inputMove.x*look_sensi)
                    * Mathf.Rad2Deg + 90;
                // �J�����̊p�x�������U������p�x��␳
                targetAngleY += cameraAngleY;

                // �C�[�W���O���Ȃ��玟�̉�]�p�x[deg]���v�Z
                var angleY = Mathf.SmoothDampAngle(
                    _transform.eulerAngles.y,
                    targetAngleY,
                    ref _turnVelocity,
                    0.1f
                );

                // �I�u�W�F�N�g�̉�]���X�V
                _transform.rotation = Quaternion.Euler(0, angleY, 0);
            }
        }else{
            Vircamscript.visionlengthX=aimlook_length;
            Vircamscript.visionlengthZ=aimlook_length;
            if (_look != Vector2.zero)
            {
                // �ړ����͂�����ꍇ�́A�U�����������s��

                // ������͂���y������̖ڕW�p�x[deg]���v�Z
                var targetAngleY = -Mathf.Atan2(_look.y, _look.x)
                    * Mathf.Rad2Deg + 90;
                // �J�����̊p�x�������U������p�x��␳
                targetAngleY += cameraAngleY;

                // �C�[�W���O���Ȃ��玟�̉�]�p�x[deg]���v�Z
                var angleY = Mathf.SmoothDampAngle(
                    _transform.eulerAngles.y,
                    targetAngleY,
                    ref _turnVelocity,
                    0.5f
                );

                // �I�u�W�F�N�g�̉�]���X�V
                _transform.rotation = Quaternion.Euler(0, angleY, 0);
            }
        }
    }
    //リロード完了関数
    void relordsound(){
        relordend.Play();
    }
    private void Relordbullet(){
        anim.SetInteger("type",1);
        anim.SetBool("relord",false);
        weapsc.acseceweapon(1).GetComponent<WeaponStates>().relord();
        endrelord=true;
        bUI.MesageUpdate();
    }

    //緑の→オブジェクトを更新
    private void guidupdate(){
        itemlist = GameObject.FindGameObjectsWithTag("SphereItem");
        if(itemlist.Length==0){
        }else{
            foreach(GameObject item in itemlist){
                if(!item.GetComponent<GuidLine>().guidemode)continue;
                itemguid=item.GetComponent <LineRenderer> ();
                itemguid.SetPosition (0, _transform.position);
            }
        }
        itemlist = GameObject.FindGameObjectsWithTag("BlockItem");
        if(itemlist.Length==0)return;
        foreach(GameObject item in itemlist){
            if(!item.GetComponent<GuidLine>().guidemode)continue;
            itemguid=item.GetComponent <LineRenderer> ();
            itemguid.SetPosition (0, _transform.position);
        }
    }
}