using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] fireballs;

    private Animator anim;
    private PlayerController playerController;
    private float cooldownTimer = Mathf.Infinity;
    private bool attacked;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        playerController = GetComponent<PlayerController>();
    }

    private void Update()
    {            //  left click GetMouseButton(0)   spacebar GetKeyDown(KeyCode.Space)
        // Input.GetButtonDown("Fire1")
        if (Input.GetButtonDown("Fire1") && cooldownTimer > attackCooldown && playerController.canAttack()){
            attacked = false;
            Attack();
            cooldownTimer = 0;
            anim.SetTrigger("attack");
        }
        cooldownTimer += Time.deltaTime;
    }

    private void Attack()
    {
        if(!attacked){
            int fireball = FindFireball();
            fireballs[fireball].transform.position = firePoint.position;
            fireballs[fireball].GetComponent<Projectile>().SetDirection(Mathf.Sign(transform.localScale.x));
            attacked = true;
        }
    }

        
    private int FindFireball()
    {
        for (int i = 0; i < fireballs.Length; i++)
        {
            if (!fireballs[i].activeInHierarchy)
                return i;
        }
        return 0;
    }
}