using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class myunitycahnScript : MonoBehaviour
{
    public GameObject Player;

    public float speed;
    public float xkando;
    public float ykando;
    public GameObject bullet;
    public Transform muzzle;
    public float buki = 1;
    public int bullet1count;
    public int bullet2count;
    public Text magajin1;
    public Text magajin2;
    public Image reticule;

    private int bullet1magajin = 16;
    private int bullet2magajin = 32;

    private float bulletspeed;
   
    [SerializeField] private GameObject Camera;
    [SerializeField] private GameObject aimCamera;
    private Transform PlayerTransform;
    public Transform neckBone;　//インスペクターで脊髄を選択
    private Animator animator;
    private Rigidbody _rigidbody;
    float yaw, pitch;

    // Use this for initialization
    void Start()
    {
        speed = 5F;
        bullet1count = bullet1magajin;
        bullet2count = bullet2magajin;
        
        animator = GetComponent<Animator>();
        PlayerTransform = transform.parent;
        _rigidbody = this.transform.GetComponent<Rigidbody>();

        //UI
        magajin1.text = bullet1count + "/" + bullet1magajin;
        magajin2.enabled = false;
        reticule.enabled = false;
        

       

    }

    // Update is called once per frame
    void Update()
    {
        //float X_Rotation = Input.GetAxis("Mouse X");
        yaw += Input.GetAxis("Mouse X") * xkando; //マウスの入力X
        pitch += Input.GetAxis("Mouse Y") * ykando;　//マウスの入力Y
        pitch = Mathf.Clamp(pitch, -30, 30); //ピッチ角の制限
        //PlayerTransform.transform.Rotate(0, X_Rotation , 0);
        PlayerTransform.transform.eulerAngles = new Vector3(0, yaw, 0);　//プレイヤー横回転


        float angleDir = PlayerTransform.transform.eulerAngles.y * (Mathf.PI / 180.0f); //回転量を取得、ラジアンに変換
        Vector3 dir1 = new Vector3(Mathf.Sin(angleDir), 0, Mathf.Cos(angleDir));　//正面
        Vector3 dir2 = new Vector3(-Mathf.Cos(angleDir), 0, Mathf.Sin(angleDir)); //左


        //歩き
        if (Input.GetKey(KeyCode.W))
        {
           // _rigidbody.AddForce(transform.forward * speed);
            PlayerTransform.transform.position += dir1 * speed * 0.3F * Time.deltaTime;
            animator.SetBool("walk", true);

        }

        //走り
        if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.LeftShift))
        {
            PlayerTransform.transform.position += dir1 * speed * Time.deltaTime;
            animator.SetBool("run", true);

        }
        //左移動
        if (Input.GetKey(KeyCode.A))
        {
            PlayerTransform.transform.position += dir2 * speed * 0.3F * Time.deltaTime;
            animator.SetBool("left", true);
        }
        //右移動
        if (Input.GetKey(KeyCode.D))
        {
            PlayerTransform.transform.position += -dir2 * speed * 0.3F * Time.deltaTime;
            animator.SetBool("right", true);
        }
        //後ろ移動
        if (Input.GetKey(KeyCode.S))
        {
            PlayerTransform.transform.position += -dir1 * speed * 0.3F * Time.deltaTime;
            animator.SetBool("walkb", true);
        }
        //武器チェンジ
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            buki = 1;
            magajin1.enabled = true;
            magajin2.enabled = false;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            buki = 2;
            magajin1.enabled = false;
            magajin2.enabled = true;
        }
        //カメラ
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            Camera.SetActive(false);
            aimCamera.SetActive(true);
        }
        if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            Camera.SetActive(true);
            aimCamera.SetActive(false);
        }
        //リロード
        if ((Input.GetKeyDown(KeyCode.R)) && (buki == 1))
        {
            bullet1count = bullet1magajin;
        }
        if ((Input.GetKeyDown(KeyCode.R)) && (buki == 2))
        {
            bullet2count = bullet2magajin;
        }
        //UI
        //if (buki == 1)
        //{
            magajin1.text = bullet1count + "/" + bullet1magajin;
        //}
        //if (buki == 2)
        //{
            magajin2.text = bullet2count + "/" + bullet2magajin;
        //}

        //エイム
        if (Input.GetKey(KeyCode.Mouse1))
        {
           
            animator.SetBool("aim", true);
            reticule.enabled = true;
       

           
            //ハンドガン
            if ((Input.GetKeyDown(KeyCode.Mouse0))&&(buki==1))
            {
                
                bulletspeed = 7000;
                if(bullet1count<1)
                {
                    return;
                }
                GameObject bullets = Instantiate(bullet) as GameObject;
                Vector3 force;
                force = muzzle.gameObject.transform.forward * bulletspeed;
                bullets.GetComponent<Rigidbody>().AddForce(force);
                bullets.transform.position = muzzle.position;
                bullet1count -= 1;

            }
            //マシンガン
            if ((Input.GetKey(KeyCode.Mouse0))&&(buki==2))
            {
                
                bulletspeed = 7000;
                if (bullet2count < 1)
                {
                    return;
                }
                GameObject bullets = Instantiate(bullet) as GameObject;
                Vector3 force;
                force = muzzle.gameObject.transform.forward * bulletspeed;
                bullets.GetComponent<Rigidbody>().AddForce(force);
                bullets.transform.position = muzzle.position;
                bullet2count -= 1;

            }

        }
        

        //アニメーションの解除判定
        if (Input.GetKeyUp(KeyCode.W))
        {
            animator.SetBool("walk", false);
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            animator.SetBool("walkb", false);
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            animator.SetBool("left", false);

        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            animator.SetBool("right", false);

        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            animator.SetBool("run", false);

        }
        if(Input.GetKeyUp(KeyCode.Mouse1))
        {
            animator.SetBool("aim", false);
            reticule.enabled = false;

           
        }

    }
    //Updateでアニメーション適用後に直接ボーンをいじる
    protected virtual void LateUpdate()
    {
        if (neckBone != null)
        {
            neckBone.Rotate(0f, 0f, pitch);//ピッチ角 neckボーンを回転
        }
    }
}


