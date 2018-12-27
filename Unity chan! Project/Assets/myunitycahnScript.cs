using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class myunitycahnScript : MonoBehaviour
{
    public GameObject Player;

    public float speed;
    public float xkando;
    public float ykando;
    private Transform PlayerTransform;
    public Transform neckBone;　//インスペクターで脊髄を選択
    private Animator animator;
    private Rigidbody _rigidbody;
    float yaw, pitch;

    // Use this for initialization
    void Start()
    {
        speed = 5F;
        
        animator = GetComponent<Animator>();
        PlayerTransform = transform.parent;
        _rigidbody = this.GetComponent<Rigidbody>();
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


