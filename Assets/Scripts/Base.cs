using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class Base : MonoBehaviour
{
    public Color hoverColor;
    private Color startColor;
    public Color highlightColor;
    private Renderer rend;
    private Vector3 offset = new Vector3(0f, 0.13f,0f);
    private GameObject turret;
    BuildManager buildManager;
    PreviewSystem previewSystem;
    bool isOccupied = false;
    public Material hoverMat;
    private int selectedTurretCost;
    void Start()
    {
        previewSystem = PreviewSystem.instance;
        buildManager = BuildManager.instance;
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;
    }

    void Update()
    {
        if (Shop.isTurretSelected)
        {
            if (Input.GetMouseButtonDown(1)){
                Shop.isTurretSelected = false;
                buildManager.SetTurretToBuild(null);
            }
                
            if (!isOccupied)
            {
               rend.material.color = highlightColor; 
            }
        }     
        else
            rend.material.color = startColor;
    }

    private void OnMouseDown()
    {
        if (buildManager.GetTurretToBuild() == null)
            return;

        if (turret != null)
        {
            return;
        }
        GameObject turretToBuild = buildManager.GetTurretToBuild();
        selectedTurretCost = turretToBuild.GetComponent<Turret>().turretCost;
        if (selectedTurretCost <= UiManager.coinsCount)
        {
            turret = Instantiate(turretToBuild, transform.position + offset, transform.rotation);
            turret.GetComponent<Turret>().canShoot = true;
            UiManager.coinsCount -= selectedTurretCost;
            Shop.isTurretSelected = false;
            isOccupied = true;
        }

    }
    private void OnMouseEnter()
    {
        if (buildManager.GetTurretToBuild() == null)
            return;
        if (isOccupied == false)
        {
            rend.material.color = hoverColor;
            previewSystem.StartShowingPlacementPreview(buildManager.GetTurretToBuild(), transform.position + offset, transform.rotation);
        }
        else
        {
            turret.GetComponent<Turret>().rangeIndicator.enabled = true;
            //turret.GetComponent<Turret>().domeIndicator.SetActive(true);
        }

    }

    private void OnMouseExit()
    {
        if (isOccupied)
        {
            turret.GetComponent<Turret>().rangeIndicator.enabled = false;
            //turret.GetComponent<Turret>().domeIndicator.SetActive(false);
        }
        rend.material.color = startColor;
        previewSystem.StopShowingPreview();
    }




}
