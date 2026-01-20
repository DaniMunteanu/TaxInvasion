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
    [SerializeField]
    EconomySystem economySystem;
    [SerializeField]
    GameObject buffTilesParent;
    private Vector3Int cellPos;
    private Vector3Int pirateSelector;
    private Vector3 cellCenterPos;
    private Dictionary<Vector3Int, Pirate> placedPirates = new Dictionary<Vector3Int, Pirate>();
    private Dictionary<Vector3Int, BuffTile> buffTiles = new Dictionary<Vector3Int, BuffTile>();
    private Pirate lastSelectedPirate;
    

    private void Awake()
    {
        hexIndicator.SetActive(false);
        InitializePlacedPiratesData();
        InitializeBuffTilesData();
    }

    private void PlaceBuffTiles(Vector3Int center)
    {
        buffTiles[center].layers++;
        buffTiles[center].gameObject.SetActive(true);

        placedPirates[center].GrantArmorBuff();

        foreach (BuffTile neighbour in buffTiles[center].neighbours)
        {
            neighbour.layers++;
            neighbour.gameObject.SetActive(true);

            if (placedPirates[neighbour.gridCoords] != null)
                placedPirates[neighbour.gridCoords].GrantArmorBuff();
        }
    }

    private void RemoveBuffTiles(Vector3Int center)
    {
        buffTiles[center].layers--;
        if (buffTiles[center].layers == 0)
        {
            buffTiles[center].gameObject.SetActive(false);
            placedPirates[center].RemoveArmorBuff();
        }

        foreach (BuffTile neighbour in buffTiles[center].neighbours)
        {
            neighbour.layers--;
            if (neighbour.layers == 0)
            {
                neighbour.gameObject.SetActive(false);
                if (placedPirates[neighbour.gridCoords] != null)
                    placedPirates[neighbour.gridCoords].RemoveArmorBuff();
            }
        }
    }

    private void InitializeBuffTilesData()
    {
        foreach (Transform childTransform in buffTilesParent.transform)
        {
            BuffTile currentBuffTile = childTransform.GetComponent<BuffTile>();
            buffTiles.Add(currentBuffTile.gridCoords, currentBuffTile);
        }
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
            mainUI.AddPirateProfile(cellPos, instantiatedPirate);
            instantiatedPirate.transform.position = cellCenterPos;
            
            placedPirates[cellPos] = instantiatedPirate;
            instantiatedPirate.gridPosition = cellPos;
            
            instantiatedPirate.placeBuffTiles.AddListener(PlaceBuffTiles);
            instantiatedPirate.removeBuffTiles.AddListener(RemoveBuffTiles);

            if (buffTiles[instantiatedPirate.gridPosition].gameObject.activeSelf == true)
                instantiatedPirate.GrantArmorBuff();

            economySystem.purchaseMade.Invoke(instantiatedPirate.price);
            economySystem.RegisterCharacterDeath(instantiatedPirate);
        
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
                    mainUI.ShowPirateProfile(pirateSelector);

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
