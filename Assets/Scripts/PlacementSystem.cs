using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
    private Vector3 cellCenterPos;
    /*
    [SerializeField]
    private Grid grid;
    */

    public void TryPlacing(int pirateID)
    {
        GameObject instantiatedPirate = Instantiate(piratesDatabase.piratesData[pirateID].Prefab);
        instantiatedPirate.transform.position = cellCenterPos;
    }

    private void Update()
    {
        // Get mouse position in world coordinates
        Vector3 worldPos = cam.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        worldPos.z = 0f;
        //Vector3 mousePosition = Mouse.current.position.ReadValue();

        // Convert world position to tile cell position
        Vector3Int cellPos = tilemap.WorldToCell(worldPos);

        //Vector3Int gridPosition = grid.WorldToCell(mousePosition);
        //Debug.Log(gridPosition);

        // Move indicator to cell center
        cellCenterPos = tilemap.GetCellCenterWorld(cellPos);
        hexIndicator.transform.position = cellCenterPos;

        hexIndicator.SetActive(true);

        //hexIndicator.transform.position = grid.CellToWorld(gridPosition);

        //Debug.Log(grid.CellToWorld(gridPosition));
        //hexIndicator.transform.position = new Vector3(0.0f, 0.0f, 0.0f);
    }
}
