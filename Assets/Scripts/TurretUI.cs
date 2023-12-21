using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretUI : MonoBehaviour
{
    private Node target;
    public Vector3 offset;

    private BuildManager buildManager;

    private UIManager uiManager;
    // Start is called before the first frame update
    void Start()
    {
        buildManager = BuildManager.instance;
        uiManager = GameObject.FindWithTag("Managers").transform.Find("UIManager").gameObject.GetComponent<UIManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetTarget(Node _target)
    {
        target = _target;
        transform.rotation = Camera.main.transform.rotation;
        transform.position = target.transform.position + offset;
        uiManager.SetSellText(buildManager.turretToBuild.GetSellValue());
        gameObject.SetActive(true);
    }

    public void HideUI()
    {
        gameObject.SetActive(false);
    }

    public void Sell()
    {
        buildManager.SellTurret();
        buildManager.UnselectNode();
    }
}
