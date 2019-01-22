using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemymotion : MonoBehaviour
{
    private Animator animator;
    //enemyコントロール用
    public int ene_flag = 1;
    public float ene_speed = 1f;
    public float ene_rotationspeed = 1f;
    public float ene_posrange = 10f;
    private Vector3 targetpos;
    private Vector3 currentpos;
    private float changetarget = 50f;
    private float targetdistance;
    public bool first_flag = true;
    //attack
    private float attack_interval = 2.0f;
    private float default_attack_time = 2.0f;
    public bool attack_result;
    //hirumi
    public float hirumi_interval = 6.0f;
    private float default_hirumi_time = 6.0f;
    private GameObject gameObject_player;
    private GameObject gameobject_a;
    private GameObject gameobject_b;
    private Vector3 A_pos, B_pos;
    public float tar_dis,cur_dis;
    //enemy status
    private Slider ene_slider;
    private int enemy_maxHP = 10;
    public int enemy_preHP ;
    public int enemy_damage = 5;
    public bool dam_flag = false;
    public int damage = 1;
    private bool stop_flag = false;
    
    

    Vector3 GetRandomPosition(Vector3 currentpos)
    {
        return new Vector3(Random.Range(-ene_posrange + currentpos.x, ene_posrange + currentpos.x), 0, Random.Range(-ene_posrange + currentpos.z, ene_posrange + currentpos.z));
    }

    void enemy_haikai()
    {
        if (targetdistance < changetarget) targetpos = GetRandomPosition(transform.position);
        Quaternion targetRotation = Quaternion.LookRotation(targetpos - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * ene_rotationspeed);
        transform.Translate(Vector3.forward * ene_speed * Time.deltaTime);
        //Debug.Log("haikai");
    }

    void enemy_tsuiseki()
    {
        Vector3 targetpos = gameobject_a.transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(targetpos - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * ene_rotationspeed);
        transform.Translate(Vector3.forward * ene_speed * Time.deltaTime);
    }

    void enemy_yudo()
    {

        if (cur_dis > (tar_dis / 3))
        {
            targetpos = gameobject_a.transform.position;
        }
        else
        {
            targetpos = GetRandomPosition(transform.position);
            ene_flag = 1;
            first_flag = true;
            ene_speed = 1f;
            ene_rotationspeed = 1f;
        }
        Quaternion targetRotation = Quaternion.LookRotation(targetpos - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * ene_rotationspeed);
        transform.Translate(Vector3.forward * ene_speed * Time.deltaTime);
        A_pos = gameobject_a.transform.position;
        B_pos = gameobject_b.transform.position;
        cur_dis = Vector3.Distance(A_pos, B_pos);

    }

    void enemy_attack()
    {
        //方向修正
        Vector3 targetpos = gameobject_a.transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(targetpos - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * ene_rotationspeed);
        ene_flag = 5;

        

        //animatorのコントロール
        if (default_attack_time <= attack_interval)
        {
            stop_flag = true;
            animator.SetBool("is_enemy_walking", false);
            animator.SetBool("is_enemy_attack", true);
            //攻撃の当たり判定
            StartCoroutine("delay_attack");
            StartCoroutine("seni_attack");
            attack_interval = 0f;
        }
    }

    private IEnumerator delay_attack()
    {
        yield return new WaitForSeconds(2.0f);
        if (dam_flag == true)
        {
            attack_result = true;
            //Debug.Log("プレイヤーへダメージ");
        }
        else
        {
            attack_result = false;
            //Debug.Log("プレイヤーへ攻撃失敗");
        }
        stop_flag = false;
    }

    private IEnumerator seni_damage()
    {
        yield return new WaitForSeconds(1.0f);
        animator.SetBool("is_enemy_damage", false);
    }

    private IEnumerator seni_attack()
    {
        yield return new WaitForSeconds(0.3f);
        animator.SetBool("is_enemy_attack", false);
        //ene_flag = 1;
    }

    private IEnumerator time_destroy()
    {
        yield return new WaitForSeconds(3.1f);
        Destroy(this.gameObject);
    }

    void Start()
    {
        animator = GetComponent<Animator>();
        targetpos = GetRandomPosition(transform.position);

        ene_slider = GameObject.Find("Slider").GetComponent<Slider>();
        ene_slider.value = enemy_maxHP;
        ene_slider.maxValue = enemy_maxHP;
        enemy_preHP = enemy_maxHP;
        gameobject_a = GameObject.Find("unitychan");
        gameobject_b = this.gameObject;
        animator.SetBool("is_enemy_walking", true);

    }

    void Update()
    {
        if (stop_flag == false)
        {
            if (default_hirumi_time > hirumi_interval)
            {
                hirumi_interval += Time.deltaTime;
            }
            if (default_attack_time > attack_interval)
            {
                attack_interval += Time.deltaTime;//毎フレームの時間を加算.
            }
            A_pos = gameobject_a.transform.position;
            B_pos = gameobject_b.transform.position;
            cur_dis = Vector3.Distance(A_pos, B_pos);
            if (cur_dis < 1)
            {
                if (ene_flag != 5)
                {
                    ene_flag = 4;
                }
                animator.SetBool("is_enemy_walking", false);
            }
            else
            {
                animator.SetBool("is_enemy_walking", true);
            }

            if (ene_flag == 1)
            {
                //Debug.Log("徘徊中");
                targetdistance = Vector3.SqrMagnitude(transform.position - targetpos);
                enemy_haikai();
            }
            else if (ene_flag == 2)
            {
                //Debug.Log("誘導中");
                targetdistance = Vector3.SqrMagnitude(transform.position - targetpos);
                enemy_yudo();
            }
            else if (ene_flag == 3)
            {
                //Debug.Log("追跡中");
                targetdistance = Vector3.SqrMagnitude(transform.position - targetpos);
                enemy_tsuiseki();
            }
            else if (ene_flag == 4)
            {
                enemy_attack();
            }
            else { }
        }
        else { }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "bullet")
        {
            //Debug.Log("ダメージ");
            if(default_hirumi_time <= hirumi_interval)
            {
                animator.SetBool("is_enemy_damage", true);
                hirumi_interval = 0f;
            }
            enemy_preHP -= damage;
            ene_slider.value = enemy_preHP;
            if (enemy_preHP <= 0)
            {
                stop_flag = true;
                GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                animator.SetBool("is_enemy_death", true);
                StartCoroutine("time_destroy");
                //Debug.Log("enemy死亡");
            }
            else
            {
                StartCoroutine("seni_damage");
            }
            ene_flag = 3;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Stage")
        {
            //Debug.Log("衝突");
            transform.Rotate(new Vector3(0, 1, 0), 180);
            targetpos = gameobject_a.transform.position;
            if(first_flag == true)
            {
                A_pos = gameobject_a.transform.position;
                B_pos = gameobject_b.transform.position;
                tar_dis = Vector3.Distance(A_pos, B_pos);
                first_flag = false;
                ene_speed = 1.5f;
                ene_rotationspeed = 3f;
            }
            ene_flag = 2;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
           // Debug.Log("落下");
    }

}