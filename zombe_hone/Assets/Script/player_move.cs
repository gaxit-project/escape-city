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
    private bool walk=false;

    LineRenderer itemguid;
    private GameObject[] itemlist;
    private GameObject hand;
    LineRenderer gunLine;
    Ray shootRay = new Ray();
    RaycastHit shootHit;
    private GameManager gameManager;
    void Start()
    {
        gunLine = GetComponent <LineRenderer> ();
        anim = gameObject.GetComponent<Animator>();
        hand=GameObject.Find("righthand");
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

        //AIM武器持ち替え処理
        var moveVelocity = new Vector3(0,0,0);
        if(pastaim!=aim){
            if(aim){
                gameManager.Soundchangeweapon();
                hand.GetComponent<weaponscript>().weaponnumber=1;
                hand.GetComponent<weaponscript>().changeweapon();
                anim.SetInteger("type",1);
                gunLine.enabled = true;
            }else{
                gameManager.Soundchangeweapon();
                hand.GetComponent<weaponscript>().weaponnumber=0;
                hand.GetComponent<weaponscript>().changeweapon();
                anim.SetInteger("type",0);
                gunLine.enabled = false;
            }
            pastaim=aim;
        }
        //移動
        if(!aim){
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

        if(_inputMove != Vector2.zero)
        {
            walk = true;
            //print($"velocity = {_inputMove}");
            anim.SetBool("walk", walk);
            guidupdate();
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