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
        instantiatedPirate = Instantiate(pirate, canvasTransform);
        rt = instantiatedPirate.GetComponent<RectTransform>();
    }

    public void OnDrag(PointerEventData eventData)
    {
        placementSystem.CheckCurrentIndicatorPosition();
        Vector2 newAnchoredPosition;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasTransform, Mouse.current.position.ReadValue(), null, out newAnchoredPosition);
        rt.anchoredPosition = newAnchoredPosition + new Vector2(0,20);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        placementSystem.CheckCurrentIndicatorPosition();
        DestroyImmediate(instantiatedPirate.gameObject);
        placementSystem.TryPlacing(pirateID);
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
