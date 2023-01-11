using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttacking : MonoBehaviour, IWeaponData
{
    public Animator animator;

    public float delay = 0.3f;
    [SerializeField]
    private bool attackBlocked;
    public AudioSource weaponAudioSource;

    [SerializeField]
    WeaponSO weaponData;

    public bool IsAttacking { get; private set; }
    public Transform circleOrigin;
    public float radius;

    public void ResetIsAttacking()
    {
        IsAttacking = false;
    }

    public void OnEnable()
    {
        attackBlocked = false;
        IsAttacking = false;
        weaponAudioSource.Stop();
    }

    // Start is called before the first frame update
    void Start()
    {
        weaponAudioSource = GetComponent<AudioSource>();
        weaponAudioSource.clip = weaponData.shootSound;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsAttacking)
            return;
        if (Input.GetKey(KeyCode.Mouse0))
        {
            if (GetComponentInParent<PlayerMovement>().IsMovementActive())
            {
                Attack();
            }
            
        }
    }

    public void Attack()
    {
        if (attackBlocked)
        {
            return;
        }

        weaponAudioSource.Play();
        animator.SetTrigger("Attack");
        IsAttacking = true;
        attackBlocked = true;
        DetectColliders();
        StartCoroutine(DelayAttack());

    }

    private IEnumerator DelayAttack()
    {
        yield return new WaitForSeconds(delay);
        attackBlocked = false;
        IsAttacking = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Vector3 position = circleOrigin == null ? Vector3.zero : circleOrigin.position;
        Gizmos.DrawWireSphere(position, radius);
    }

    public void DetectColliders()
    {
        foreach (Collider2D collider in Physics2D.OverlapCircleAll(circleOrigin.position, radius))
        {
            //Debug.Log(collider.name);

            if (collider.CompareTag("Enemy") || collider.CompareTag("Boss"))
            {


                collider.GetComponent<IEnemy>().TakeDamage(weaponData.maxDamage);
            }
        }
    }

    public WeaponSlot GetWeaponSlot()
    {
        return weaponData.weaponSlot;
    }

    public WeaponType GetWeaponType()
    {
        return weaponData.weaponType;
    }

    public WeaponSO GetWeaponData()
    {
        return weaponData;
    }
}
