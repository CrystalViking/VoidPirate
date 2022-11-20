using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinionController : MonoBehaviour, IEnemy
{

    public enum EnemyState
    {
        Idle,
        Wander,
        Die,
        Sacrifice
    };

    public float speed;
    private Transform player;
    private Animator anim;
    public float health;
    public float maxHealth;
    public float X;
    public float Y;
    public GameObject healthBar;
    public Slider healthBarSlider;
    public EnemyState currState = EnemyState.Wander;

    void Start()
    {
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        health = maxHealth;
        X = Random.Range(-100, 100);
        Y = Random.Range(-100, 100);
        StartCoroutine(ChangeDir());
    }

    
    void Update()
    {
        switch (currState)
        {

            case (EnemyState.Idle):
                Idle();
                break;
            case (EnemyState.Wander):
                Wander();              
                break;
            case (EnemyState.Die):
                break;
            case (EnemyState.Sacrifice):
                break;
        }

    }

    void Wander()
    {
        anim.SetBool("IsMoving", true);      
        //transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        transform.position = Vector2.MoveTowards(transform.position, new Vector2(X, Y), speed * Time.deltaTime);

    }

    IEnumerator ChangeDir()
    {
        
        yield return new WaitForSeconds(5.0f);
        X = -X;
        Y = -Y;
        StartCoroutine(ChangeDir());

    }

    void Idle()
    {
         anim.SetBool("IsMoving", false);
    }

    public void TakeDamage(float damage)
    {
        healthBar.SetActive(true);
        health -= damage;
        healthBarSlider.value = CalculateHealthPercentage();
        CheckDeath();
    }

    public void Sacrifice()
    {
        healthBar.SetActive(false);
        anim.SetBool("IsSacrificed", true);

        this.enabled = false;
        currState = EnemyState.Sacrifice;
        Destroy(gameObject, 0.4f);

    }

    private void CheckDeath()
    {
        if (health <= 0)
        {
            healthBar.SetActive(false);
            anim.SetBool("IsDead", true);

            this.enabled = false;

            currState = EnemyState.Die;
            RoomController.instance.StartCoroutine(RoomController.instance.RoomCorutine());
            Destroy(gameObject, 0.4f);
        }
    }

    private float CalculateHealthPercentage()
    {
        return (health / maxHealth);
    }

    public void SetUseRoomLogicTrue()
    { }
    public void SetUseRoomLogicFalse()
    { }

    public void SetActiveBehaviourTrue()
    { }
    public void SetActiveBehaviourFalse()
    { }

    public bool HasFullHealth()
    {
        if (health == maxHealth)
            return true;
        else
            return false;
    }

    public virtual void SetHealth(float maxhealth)
    {
        health = maxhealth;
    }

    public virtual float GetMaxHealth()
    {
        return maxHealth;
    }

    public virtual void SetUndestructible(bool G)
    {

    }

    global::EnemyState IEnemy.GetEnemyState()
    {
        throw new System.NotImplementedException();
    }

    public GameObject GetParent()
    {
        return transform.parent.gameObject;
    }
}
