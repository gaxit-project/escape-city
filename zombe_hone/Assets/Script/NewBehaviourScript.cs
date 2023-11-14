using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public bool shotmode = true;
    public int damagePerShot = 20; //弾のダメージ
    public float timeBetweenBullets = 0.15f; //弾を撃つ間隔
    public float range = 100f; //弾の飛距離

    float timer; //経過時間
    Ray shootRay = new Ray(); //rayを、弾の攻撃範囲とする
    RaycastHit shootHit; //弾が当たった物体
    int shootableMask; //撃てるもののみ識別する
    //弾を打った時のエフェクト
    ParticleSystem gunParticles;
    LineRenderer gunLine;
    AudioSource gunAudio;
    Light gunLight;
    float effectsDisplayTime = 0.2f;

    void Awake ()
    {
        //Shootable Layerを取得
        shootableMask = LayerMask.GetMask ("Shootable");
        //コンポーネントを取得
        gunParticles = GetComponent<ParticleSystem> ();
        gunLine = GetComponent <LineRenderer> ();
        gunAudio = GetComponent<AudioSource> ();
        gunLight = GetComponent<Light> ();
    }

    void Update ()
    {
        //経過時間を計測
        timer += Time.deltaTime;

        //弾を打つボタンを押した時、かつ経過時間が弾を打つ間隔よりも大きい場合
		if(Input.GetButton ("Fire1") && timer >= timeBetweenBullets && Time.timeScale != 0)
        {
            //弾を打つ
            Shoot ();
        }

        //経過時間がエフェクトの表示時間よりも大きくなった場合
        if(timer >= timeBetweenBullets * effectsDisplayTime)
        {
            //エフェクトを非表示にする
            DisableEffects ();
        }
    }

    //銃を撃つエフェクトをオフにする処理
    public void DisableEffects ()
    {
        gunLine.enabled = false;
        gunLight.enabled = false;
    }

    //弾を撃つ処理
    void Shoot ()
    {
        //経過時間を初期化
        timer = 0f;

        //弾を撃つエフェクトをオンにする
        gunAudio.Play ();
        gunLight.enabled = true;

        //弾を連写することを想定して、オフにしてからオンにする
        gunParticles.Stop ();
        gunParticles.Play ();

        //射線のスタート位置を設定する
        gunLine.enabled = true;
        gunLine.SetPosition (0, transform.position);

        //弾の攻撃範囲のスタート位置を設定する
        shootRay.origin = transform.position;
        //弾の飛んでいく方向を設定する
        shootRay.direction = transform.forward;

        //弾を飛ばし、(Rayを飛ばし、障害物に当たった場合
        if(Physics.Raycast (shootRay, out shootHit, range, shootableMask))
        {
            //弾が当たった障害物のEnemyHealthスクリプトコンポーネントを取得する
            EnemyHealth enemyHealth = shootHit.collider.GetComponent <EnemyHealth> ();

            //EnemyHealthスクリプトコンポーネントがnullではない場合(敵に弾が当たった場合)
            if(enemyHealth != null)
            {
                //敵にダメージを与える
                enemyHealth.TakeDamage (damagePerShot, shootHit.point);
            }
            //射線を障害物で当たった場所で止める
            gunLine.SetPosition (1, shootHit.point);
        }
        //障害物に当たらなかった場合
        else
        {
            //射線を弾の飛距離分表示する
            gunLine.SetPosition (1, shootRay.origin + shootRay.direction * range);
        }
    }
}