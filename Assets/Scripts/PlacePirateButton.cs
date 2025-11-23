using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlacePirateButton : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField]
    Image pirate;

    [SerializeField]
    PlacementSystem placementSystem;
    public int pirateID = 0;

    public RectTransform canvasTransform;
    private Image instantiatedPirate;
    private RectTransform rt;

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("Drag started");
        instantiatedPirate = Instantiate(pirate, canvasTransform);
        rt = instantiatedPirate.GetComponent<RectTransform>();
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("Dragging");
        Vector2 newAnchoredPosition;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasTransform, Mouse.current.position.ReadValue(), null, out newAnchoredPosition);
        rt.anchoredPosition = newAnchoredPosition;
        Debug.Log("Mouse: " + Mouse.current.position.ReadValue());
        Debug.Log("Pirate: " + rt.anchoredPosition);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("Drag ended");
        DestroyImmediate(instantiatedPirate.gameObject);
        placementSystem.TryPlacing(pirateID);
        //instantiatedPirate = null;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
