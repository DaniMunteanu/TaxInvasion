using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class PlacementSystem : MonoBehaviour
{
    [SerializeField]
    private Camera cam;
    [SerializeField]
    private Tilemap tilemap;
    [SerializeField]
    private GameObject hexIndicator;
    [SerializeField]
    PiratesDatabaseSO piratesDatabase;
    [SerializeField]
    MainUI mainUI;
    private Vector3Int cellPos;
    private Vector3Int pirateSelector;
    private Vector3 cellCenterPos;
    private Dictionary<Vector3Int, Pirate> placedPirates = new Dictionary<Vector3Int,Pirate>();
    private Pirate lastSelectedPirate;

    private void Awake()
    {
        hexIndicator.SetActive(false);
        InitializePlacedPiratesData();
    }

    private void InitializePlacedPiratesData()
    {
        placedPirates.Add(new Vector3Int(8,-4,0),null);
        placedPirates.Add(new Vector3Int(7,-4,0),null);
        placedPirates.Add(new Vector3Int(6,-4,0),null);
        placedPirates.Add(new Vector3Int(5,-4,0),null);

        placedPirates.Add(new Vector3Int(7,-3,0),null);
        placedPirates.Add(new Vector3Int(6,-3,0),null);
        placedPirates.Add(new Vector3Int(5,-3,0),null);
        placedPirates.Add(new Vector3Int(4,-3,0),null);
        
        placedPirates.Add(new Vector3Int(8,-2,0),null);
        placedPirates.Add(new Vector3Int(7,-2,0),null);
        placedPirates.Add(new Vector3Int(6,-2,0),null);
        placedPirates.Add(new Vector3Int(5,-2,0),null);
        placedPirates.Add(new Vector3Int(4,-2,0),null);

        placedPirates.Add(new Vector3Int(5,-1,0),null);
        placedPirates.Add(new Vector3Int(4,-1,0),null);
        placedPirates.Add(new Vector3Int(3,-1,0),null);

        placedPirates.Add(new Vector3Int(5,0,0),null);
        placedPirates.Add(new Vector3Int(4,0,0),null);
        placedPirates.Add(new Vector3Int(3,0,0),null);

        placedPirates.Add(new Vector3Int(5,1,0),null);
        placedPirates.Add(new Vector3Int(4,1,0),null);
        placedPirates.Add(new Vector3Int(3,1,0),null);

        placedPirates.Add(new Vector3Int(8,2,0),null);
        placedPirates.Add(new Vector3Int(7,2,0),null);
        placedPirates.Add(new Vector3Int(6,2,0),null);
        placedPirates.Add(new Vector3Int(5,2,0),null);
        placedPirates.Add(new Vector3Int(4,2,0),null);

        placedPirates.Add(new Vector3Int(7,3,0),null);
        placedPirates.Add(new Vector3Int(6,3,0),null);
        placedPirates.Add(new Vector3Int(5,3,0),null);
        placedPirates.Add(new Vector3Int(4,3,0),null);

        placedPirates.Add(new Vector3Int(8,4,0),null);
        placedPirates.Add(new Vector3Int(7,4,0),null);
        placedPirates.Add(new Vector3Int(6,4,0),null);
        placedPirates.Add(new Vector3Int(5,4,0),null);

    }

    public void TryPlacing(int pirateID)
    {
        if (hexIndicator.activeSelf)
        {
            Pirate instantiatedPirate = Instantiate(piratesDatabase.piratesData[pirateID].Prefab.GetComponent<Pirate>());
            mainUI.AddUpgradePage(cellPos, instantiatedPirate);
            instantiatedPirate.transform.position = cellCenterPos;
            placedPirates[cellPos] = instantiatedPirate;
        
            hexIndicator.SetActive(false);

            if (lastSelectedPirate != null)
                lastSelectedPirate.UnHighlight();

            lastSelectedPirate = placedPirates[cellPos];
        }
    }

    public void CheckCurrentIndicatorPosition()
    {
        if (placedPirates.ContainsKey(cellPos))
        {
            if (placedPirates[cellPos] == null)
            {
                hexIndicator.SetActive(true);
                return;
            }
        }
        hexIndicator.SetActive(false);
    }

    private void Update()
    {
        // Get mouse position in world coordinates
        Vector3 worldPos = cam.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        worldPos.z = 0f;
        
        //                                                   -18?
        Vector3 pirateOffsetPos = worldPos + new Vector3(0f, -24f * 0.03125f, 0f);

        // Convert world position to tile cell position
        cellPos = tilemap.WorldToCell(worldPos);
        pirateSelector = tilemap.WorldToCell(pirateOffsetPos);

        // Move indicator to cell center
        cellCenterPos = tilemap.GetCellCenterWorld(cellPos);
        hexIndicator.transform.position = cellCenterPos;
        

        if (Mouse.current.leftButton.wasPressedThisFrame) 
        {
            if (placedPirates.ContainsKey(pirateSelector))
            {
                if (placedPirates[pirateSelector] != null)
                {
                    placedPirates[pirateSelector].Highlight();
                    mainUI.ShowUpgradePage(pirateSelector);

                    if (lastSelectedPirate != null && lastSelectedPirate != placedPirates[pirateSelector])
                        lastSelectedPirate.UnHighlight();

                    lastSelectedPirate = placedPirates[pirateSelector];
                }
            }
            else
            {
                if (lastSelectedPirate != null)
                    lastSelectedPirate.UnHighlight();
            }
        }
    }

}
