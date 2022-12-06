using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyHealthSystem : MonoBehaviour
{
   [SerializeField] WaterBender waterBenderController;
    [SerializeField] float _TurnSpeed;
    [SerializeField] Animator _Anim;
   private Transform waterBendTarget;
    //Vector3 EnemyTarget;
    public int currentHealth;
    public int maxHealth = 100;
    public static bool isEnemyDead = false;
    private int range = 30;
    private bool canShootAtPlayer = true;
    public AudioSource enemyFire;
    public GameObject player;
    private int damage = 10;
    public HealthBar healthBar;
    private bool hasWaterFired = false;
    private List<GameObject> _clonePlayer = new List<GameObject>();

    //public void WaterBend(Vector3 target)
   // {
      //  waterBendTarget = target;
       // StopAllCoroutines();
        //StartCoroutine(Coroutine_WaterBend());
   // }

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    private void Update()
    {

        if (currentHealth > 0 && isEnemyDead == false)
        {
            if (hasWaterFired == false)
                StopAllCoroutines();
                //StartCoroutine(Coroutine_WaterBend());
                //FireGun();
                ShootAtPlayer();
//                StartCoroutine(Coroutine_WaterBend());

        }

    }
        public void TakeDamage(int damage)
    {
        currentHealth = currentHealth - damage;
        healthBar.SetHealth(currentHealth);

        if (currentHealth <= 0 && isEnemyDead == false)
        {
            //StartCoroutine(Coroutine_WaterBend());
            //Destroy(GameObject.FindWithTag("Void"));
            var clonePlayer = Instantiate(player, new Vector3(0, 0, 0), Quaternion.identity);
            _clonePlayer.Add(clonePlayer);
            Debug.Log("DEAD: " + currentHealth);
            //Destroy(clonePlayer);
            //gameObject.GetComponent<Animator>().Play("Dying");
            isEnemyDead = true;

        }
    }

    IEnumerator Coroutine_WaterBend()
    {
        canShootAtPlayer = false;
        Ray rayFrom = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        //StartCoroutine(Coroutine_Turn());
        //gameObject.GetComponent<Animator>().Play("WaterSplineObject");
        //gameObject.GetComponent<AudioSource>().Play();
        enemyFire.Play();
       // var clonePlayer = Instantiate(player, new Vector3(0, 0, 0), Quaternion.identity);
        //_clonePlayer.Add(clonePlayer);
       // clonePlayer = player;
        player.GetComponent<PlayerHealths>().PlayerTakeDamage(damage);
       
        yield return new WaitForSeconds(1.2f);

        canShootAtPlayer = true;
        //gameObject.GetComponent<Animator>().Play("WaterSplineObject");
        if (Physics.Raycast(rayFrom, out hit))
        {
            yield return StartCoroutine(Coroutine_Turn());
            waterBendTarget = hit.transform;
            _Anim.SetTrigger("WaterBend");
                    if (hit.collider.CompareTag("Player"))
                    {
                        Debug.Log("Shot : " + damage);
                        hit.collider.GetComponent<PlayerHealths>().PlayerTakeDamage(damage);
                    }
                    else
                    {
                        Debug.Log("MISS");
                    }
        }
        yield return null;
    }
    //yield return null;

    void ShootAtPlayer()
    {

    Ray rayFrom = new Ray(transform.position, transform.forward);
    RaycastHit hit;

    if (Physics.Raycast(rayFrom, out hit, range))
    {
    if (hit.collider.CompareTag("Player"))
    {

    if (canShootAtPlayer)
    {
        //StartCoroutine(FireGun());
        StartCoroutine(Coroutine_WaterBend());


                }

            }

        }
    }

    //}

    //}


    //IEnumerator FireGun()
    //{
    
      //canShootAtPlayer = false;

    //gameObject.GetComponent<Animator>().Play("CharacterAnimatorVoid");

    //enemyFire.Play();

    //player.GetComponent<PlayerHealths>().PlayerTakeDamage(damage);

    //yield return new WaitForSeconds(1.2f);

    //canShootAtPlayer = true;


    //gameObject.GetComponent<Animator>().Play("CharacterAnimatorVoid");

    // }
    private void AnimationCallback_WaterBend()
    {
        waterBenderController.Attack(waterBendTarget);
    }

    IEnumerator Coroutine_Turn()
    {
        Ray rayFrom = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(rayFrom, out hit, range))
        {
            Vector3 direction = (hit.point - transform.position);
            direction.y = 0;
            direction = direction.normalized;
            Vector3 startForward = transform.forward;
            float angle = Vector3.Angle(startForward, direction);
            _Anim.SetFloat("Turn", Vector3.Cross(startForward, direction).y);
            float lerp = 0;
            while (lerp < 1)
            {
                transform.forward = Vector3.Slerp(startForward, direction, lerp);
                lerp += Time.deltaTime * _TurnSpeed / angle;
                yield return null;
            }
            _Anim.SetFloat("Turn", 0);

        }
    }
       
    }
  


