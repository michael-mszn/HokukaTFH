using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public Color hoverColor;
	public string buffName;
    private Color initialColor;
    private MeshRenderer nodeMeshRenderer;
    private BuildManager buildManager;
    
    [Header("Optional turret to spawn during level begin")]
    public GameObject turret;
    
    // Start is called before the first frame update
    void Start()
    {
        nodeMeshRenderer = GetComponent<MeshRenderer>();
        initialColor = nodeMeshRenderer.material.color;
        buildManager = BuildManager.instance;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    //Callback Function that is only called once
    void OnMouseEnter()
    {
        //Only highlight if a turret to build was actually selected
        if (!buildManager.CanBuild)
        {
            return;
        }

        if (buildManager.HasMoney)
        {
            nodeMeshRenderer.material.color = hoverColor;
        }
        else
        {
            nodeMeshRenderer.material.color = Color.red;
        }
    }

    void OnMouseExit()
    {
        nodeMeshRenderer.material.color = initialColor;
    }
    
    void OnMouseDown()
    {
        if (turret != null)
        {
            buildManager.SelectNode(this);
            return;
        }
        
        //User must select a turret first before they can build
        if (!buildManager.CanBuild)
        {
            return;
        }

        buildManager.BuildTurretOn(this);
    }
}
