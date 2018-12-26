using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class myunitycahnScript : MonoBehaviour {
    public GameObject Player;
    public float speed;
    public float kando;
    private Transform PlayerTransform;
    private Animator animator;
    private Rigidbody _rigidbody;

    // Use this for initialization
    void Start () {
        speed = 5F;
        kando = 3F;
        animator = GetComponent<Animator>();
        PlayerTransform = transform.parent;
        _rigidbody = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float X_Rotation = Input.GetAxis("Mouse X");
        float Y_Rotation = Input.GetAxis("Mouse Y");
        PlayerTransform.transform.Rotate(0, X_Rotation * kando, 0);


        float angleDir = PlayerTransform.transform.eulerAngles.y * (Mathf.PI / 180.0f);
        Vector3 dir1 = new Vector3(Mathf.Sin(angleDir), 0, Mathf.Cos(angleDir));
        Vector3 dir2 = new Vector3(-Mathf.Cos(angleDir), 0, Mathf.Sin(angleDir));


        //歩き
        if (Input.GetKey(KeyCode.W))
        {
            PlayerTransform.transform.position += dir1 * speed * 0.3F * Time.deltaTime;
            animator.SetBool("walk", true);
            
        }
        //走り
        if (Input.GetKey(KeyCode.W)&& Input.GetKey(KeyCode.LeftShift))
        {
            PlayerTransform.transform.position += dir1 * speed  * Time.deltaTime;
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
}

