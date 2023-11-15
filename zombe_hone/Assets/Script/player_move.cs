using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerControllerWithCamera : MonoBehaviour
{
    [Header("�ړ��̑���"), SerializeField]
    private float _speed = 3;
    [Header("�ړ����"), SerializeField]
    private float aims_speed = 0.3f;

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
    
    private float _verticalVelocity;
    private float _turnVelocity;
    private bool _isGroundedPrev;

    private Animator anim;
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
        // ���͒l��ێ����Ă���
        _inputMove = context.ReadValue<Vector2>();
    }
    public void OnLook(InputAction.CallbackContext context)
    {
        // ���͒l��ێ����Ă���
        _look = context.ReadValue<Vector2>();
    }
    public void OnAim(InputAction.CallbackContext context)
    {
        if (!context.performed){
            aim=false;
            
            return;
        }
        aim=true;
        
        // ���͒l��ێ����Ă���

    }
    public void OnRelord(InputAction.CallbackContext context){
        if(anim.GetBool("relord"))return;
        if (!context.performed){
            relordmode=false;
            
            return;
        }
        relordmode=true;
    }

    /// <summary>
    /// �W�����vAction(PlayerInput������Ă΂��)
    /// </summary>
    public void OnJump(InputAction.CallbackContext context)
    {
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

    private void Update()
    {
        if(anim.GetBool("Die"))return;
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

        //AIM武器持ち替え処理　リロード中は無視
        var moveVelocity = new Vector3(0,0,0);
        if(anim.GetBool("relord")){
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
        if(anim.GetBool("relord")){
            moveVelocity = new Vector3(
                _inputMove.x * aims_speed*_speed,
                _verticalVelocity,
                _inputMove.y * aims_speed*_speed
            );
        }else if(!aim){
            moveVelocity = new Vector3(
                _inputMove.x * _speed,
                _verticalVelocity,
                _inputMove.y * _speed
            );
        }else{
            moveVelocity = new Vector3(
                _inputMove.x * aims_speed*_speed,
                _verticalVelocity,
                _inputMove.y * aims_speed*_speed
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
            walk = true;
            //print($"velocity = {_inputMove}");
            anim.SetBool("walk", walk);
            guidupdate();   //緑のやじるし更新
        }
        else if(_inputMove == Vector2.zero)
        {
            walk = false;
            //print($"velocity = stop");
            anim.SetBool("walk", walk);
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