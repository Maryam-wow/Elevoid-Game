using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class VoidBehaviour : MonoBehaviour
{
    [SerializeField] private WaterBendingControll waterBenderObjectReference;

    [SerializeField] private float initialDelay = 5f;
    [SerializeField] private float playerHitDelay = 2f;

    private Transform _tower;
    //public GameObject towerobject;
    private WaterBendingControll _waterBendingControll;

    private Coroutine _attackCoroutine;

    private bool _gotHitByPlayer;
    private int damage = 5;
    public int currentHealth;
    public int maxHealth = 100;
    public WorldSpaceHealthBar healthBar;

    [Header("Self-component references")]
    [SerializeField] Animator _animator;

    private void Start()
    {
        _tower = GameObject.FindWithTag("Tower").transform;

        LookAtTower();
        _attackCoroutine = StartCoroutine(AttackCoroutine(initialDelay));
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);

        healthBar.GetComponent<Slider>().maxValue = maxHealth;
    }

    private void OnDestroy()
    {
        StopAttacking();
        Debug.Log("DEAD");
    }

    public void TakeDamage(int damage)
    {
        currentHealth = currentHealth - damage;
        healthBar.SetHealth(currentHealth);
        _gotHitByPlayer = true;
        Debug.Log("VOIDHEALTH: " + currentHealth);
        
        if (currentHealth <= 0)
            StartDestroySelf();
    }

        public void StopAttacking()
    {
        StopCoroutine(_attackCoroutine);
    }

    IEnumerator AttackCoroutine(float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            if (_gotHitByPlayer)
            {
                yield return new WaitForSeconds(playerHitDelay);
                _gotHitByPlayer = false;
            }

            StartAttack();
            _tower.GetComponent<PlayerHealths>().PlayerTakeDamage(damage);
        }
    }

    void StartAttack()
    {
        _waterBendingControll = Instantiate(waterBenderObjectReference, transform.position, Quaternion.identity);
        
        _waterBendingControll.WaterBend(_tower.position);
    }

    void LookAtTower()
    {
        transform.LookAt(_tower);
    }

    void StartDestroySelf()
    {
        StopCoroutine(_attackCoroutine);
        
        _animator.Play("VoidDestroyAnimation");
        
        Invoke("DestroySelf", 5);
    }

    void DestroySelf()
    {
        Destroy(gameObject);
    }
}