using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mob : MonoBehaviour
{
    public string mobName;
    [SerializeField] private HealthBar healthBar;
    public int maxHealth;
    public int currentHealth;
    public int attackDamage;
    public float speed;
    public Waypoints lane;
    private AnimationManager animationManager;
    private UIManager uiManager;
    private Transform target;
    private int waypointIndex;
    public string statusCondition;
    private bool isDead;
    private AudioManager audioManager;
    
    // Start is called before the first frame update
    void Start()
    {
        animationManager = GameObject.FindWithTag("Managers").transform.Find("AnimationManager").gameObject.GetComponent<AnimationManager>();
        uiManager = GameObject.FindWithTag("Managers").transform.Find("UIManager").gameObject.GetComponent<UIManager>();
        healthBar = GetComponentInChildren<HealthBar>();
        healthBar.UpdateHealthBar(currentHealth, maxHealth);
        healthBar.DisableHealthbar();
        waypointIndex = 0;
        target = lane.waypoints[0];
        statusCondition = "none";
        audioManager = AudioManager.instance;
        
        //unelegant bandaid fix; cant be handled by mobmanager since healthbar is created at runtime
        if (mobName == "Volibear")
        {
            healthBar.setOffset(new Vector3(0, 69, 0));
            //To avoid GetComponent<> calls in every frame
            InvokeRepeating("VolibearBossHealMap", 2.5f, 2.5f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Travel();
    }

    public void ReceiveDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            animationManager.PlayDeathAnimation(gameObject);
            speed = 0f;
            isDead = true;
            healthBar.DisableHealthbar();
            UIManager.money += (maxHealth / 10);
            uiManager.UpdateMoneyText();
            Invoke("Die", 3.0f);
        }
        else
        {
            healthBar.LightUpHealthbar(1f);
            healthBar.UpdateHealthBar(currentHealth, maxHealth);
        }
    }

    public void Freeze()
    {
        statusCondition = "frozen";
        speed /= 2;
        animationManager.PlayFreezeAnimation(gameObject);
    }
    
    public void ThawOut()
    {
        statusCondition = "thawing out";
        speed *= 2;
        animationManager.PlayWalkAnimation(gameObject);
    }

    private void Die()
    {
        Destroy(gameObject);
    }
    
    private void Travel() {
            Vector3 direction = target.position - transform.position;
            transform.Translate(direction.normalized * speed * Time.deltaTime, Space.World);
            
            if (Vector3.Distance(transform.position, target.position) <= 0.7f)
            {
                FetchNextWaypoint();
            }
    }
    
    private void FetchNextWaypoint() 
    {
        if(waypointIndex >= lane.waypoints.Length -1)
        {
            if (UIManager.lives != 0)
            {
                UIManager.lives -= attackDamage;
                uiManager.UpdateLives();
                audioManager.PlayDamageSound();
            }
            Destroy(gameObject);
        }
        else {
            waypointIndex++;
            target = lane.waypoints[waypointIndex];
        }
    }

    private void VolibearBossHealMap()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemy in enemies)
        {
            Mob ally = enemy.GetComponent<Mob>();
            
            if (ally.currentHealth < ally.maxHealth)
            {
                ally.currentHealth += 150;
                if (ally.currentHealth > ally.maxHealth)
                {
                    ally.currentHealth = ally.maxHealth;
                }
                
                if(ally.healthBar.GetDisplayTime() == 0f)
                {
                    ally.healthBar.UpdateHealthBar(currentHealth, maxHealth);
                    ally.healthBar.LightUpHealthbar(1f);
                }
            }
        }
    }
    
    
    public void setMobMaxHealth(int newHealth)
    {
        maxHealth = newHealth;
        currentHealth = maxHealth;
    }

    public void setMobSpeed(float newSpeed)
    {
        speed = newSpeed;
    }

    public float getMobSpeed()
    {
        return speed;
    }
    
    public void setMobAttackDamage(int newAttackDamage)
    {
        attackDamage = newAttackDamage;
    }
    
    public void setLane(GameObject destination)
    {
        lane = destination.GetComponent<Waypoints>();
    }

    public void setStatusCondition(string condition)
    {
        statusCondition = condition;
    }

    public string getStatusCondition()
    {
        return statusCondition;
    }

    public HealthBar getHealthbar()
    {
        return healthBar;
    }
    
    public bool isMobDead()
    {
        return isDead;
    }
    
}
