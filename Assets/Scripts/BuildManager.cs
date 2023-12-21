using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    //Store reference to itself (Singleton Pattern)
    //Instance can be accessed from anywhere
    public static BuildManager instance;
    public GameObject watchTowerPrefab;
    public GameObject MultihitTowerPrefab;
    public GameObject CrossbowTowerPrefab;
    public TurretBlueprint turretToBuild;
    private UIManager uiManager;
    private Node selectedNode;
    private AudioManager audioManager;
    public TurretUI turretUI;
    
    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        uiManager = GameObject.FindWithTag("Managers").transform.Find("UIManager").gameObject.GetComponent<UIManager>();
        audioManager = AudioManager.instance;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public bool CanBuild
    {
        get { return turretToBuild != null; }
    }
    
    public bool HasMoney
    {
        get { return UIManager.money >= turretToBuild.cost; }
    }

    public void SelectTurretToBuild(TurretBlueprint turret)
    {
        turretToBuild = turret;
        UnselectNode();
        audioManager.PlayClickSound();
    }

    public void SelectNode(Node node)
    {
        if (selectedNode == node)
        {
            UnselectNode();
            return;
        }
        
        selectedNode = node;
        //turretToBuild = null;
        turretUI.SetTarget(node);
    }

    public void UnselectNode()
    {
        selectedNode = null;
        turretUI.HideUI();
    }

    public void BuildTurretOn(Node node)
    {
        if (UIManager.money >= turretToBuild.cost)
        {
            UIManager.money -= turretToBuild.cost;
            GameObject turret = (GameObject) Instantiate(turretToBuild.prefab, node.transform.position, Quaternion.identity);
            node.turret = turret;
            setBuff(node);
            uiManager.UpdateMoneyText();
            audioManager.PlayBuildSound();
        }
        else
        {
            Debug.Log("Not enough money!");
        }

    }
    
    public void SellTurret()
    {
        UIManager.money += turretToBuild.GetSellValue();
        uiManager.UpdateMoneyText();
        Destroy(selectedNode.turret);
        audioManager.PlaySellSound();
    }

    private void setBuff(Node node) {
        switch (node.buffName)
        {
            case "RangeUp":
                node.turret.GetComponent<Turret>().range *= 1.50f;
                break;
            case "DamageUp":
                node.turret.GetComponent<Turret>().SetHasDamageAmp(true);
                break;
            case "FireRateUp":
                node.turret.GetComponent<Turret>().fireRate *= 2;
                break;
        }
    }
}
