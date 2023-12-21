using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using Random = System.Random;

public class MobManager : MonoBehaviour
{
    public GameObject volibear;
    public GameObject darius;
    public GameObject viktor;
    public GameObject olaf;
    public List<GameObject> lanes;
    private Mob mobScript;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InstantiateMob(GameObject mob, int health, float speed, int attackDamage)
    {
        //Speed should scale down with more health
        mobScript = mob.GetComponent<Mob>();
        switch (mobScript.mobName)
        {
            case "Olaf":
                mobScript.setMobMaxHealth(health);
                mobScript.setMobSpeed(speed / health * 12);
                mobScript.setMobAttackDamage(attackDamage);
                mobScript.setLane(selectRandomLane());
                mob.transform.localScale = new Vector3(health / 60, health / 60, health / 60);
                break;
            case "Darius":
                mobScript.setMobMaxHealth(health);
                mobScript.setMobSpeed(speed / health * 12);
                mobScript.setMobAttackDamage(attackDamage);
                mobScript.setLane(selectRandomLane());
                mob.transform.localScale = new Vector3(health / 60, health / 60, health / 60);
                break;
            case "Viktor":
                mobScript.setMobMaxHealth(health);
                mobScript.setMobSpeed(speed / health * 12);
                mobScript.setMobAttackDamage(attackDamage);
                mobScript.setLane(selectRandomLane());
                mob.transform.localScale = new Vector3(health / 60, health / 60, health / 60);
                break;
            case "Volibear":
                mobScript.setMobMaxHealth(health);
                mobScript.setMobSpeed(speed);
                mobScript.setMobAttackDamage(attackDamage);
                mobScript.setLane(selectRandomLane());
                mob.transform.localScale = new Vector3(health / 150, health / 150, health / 150);
                break;
        }
    }

    public GameObject selectRandomLane()
    {
        return lanes[(int) UnityEngine.Random.Range(0f, 3.0f)];
    }
}
