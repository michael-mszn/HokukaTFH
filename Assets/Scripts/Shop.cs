using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public TurretBlueprint watchTower;
    public TurretBlueprint multihitTower;
    public TurretBlueprint crossbowTower;
    private BuildManager buildManager;
    // Start is called before the first frame update
    void Start()
    {
        buildManager = BuildManager.instance;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SelectWatchTower()
    {
        buildManager.SelectTurretToBuild(watchTower);
    }
    
    public void SelectCrossbowTower()
    {
        buildManager.SelectTurretToBuild(crossbowTower);
    }
    
    public void SelectMultihitTower()
    {
        buildManager.SelectTurretToBuild(multihitTower);
    }
}
