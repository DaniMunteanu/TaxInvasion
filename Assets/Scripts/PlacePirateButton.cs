using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlacePirateButton : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField]
    Image pirate;

    public Transform canvasTransform;
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
        rt.anchoredPosition = Mouse.current.position.ReadValue();
        Debug.Log(Mouse.current.position.ReadValue());
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("Drag ended");
        Destroy(instantiatedPirate);
        instantiatedPirate = null;
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
