using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlacePirateButton : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField]
    CharacterStatsSO characterStats;

    [SerializeField]
    Image pirate;

    [SerializeField]
    PlacementSystem placementSystem;
    public int pirateID = 0;

    [SerializeField]
    EconomySystem economySystem;
    private Button buttonComponent;

    public RectTransform canvasTransform;
    private Image instantiatedPirate;
    private RectTransform rt;

    public void OnBeginDrag(PointerEventData eventData)
    {
        if(buttonComponent.interactable == false)
            return;
        
        instantiatedPirate = Instantiate(pirate, canvasTransform);
        rt = instantiatedPirate.GetComponent<RectTransform>();
    }

    public void OnDrag(PointerEventData eventData)
    {
        if(buttonComponent.interactable == false)
            return;
        
        placementSystem.CheckCurrentIndicatorPosition();
        Vector2 newAnchoredPosition;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasTransform, Mouse.current.position.ReadValue(), null, out newAnchoredPosition);
        rt.anchoredPosition = newAnchoredPosition + new Vector2(0,20);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if(buttonComponent.interactable == false)
            return;
        
        placementSystem.CheckCurrentIndicatorPosition();
        DestroyImmediate(instantiatedPirate.gameObject);
        placementSystem.TryPlacing(pirateID);
    }

    void CheckCredits()
    {
        if(economySystem.currentCredits < characterStats.cutlassPirateStats.price)
            buttonComponent.interactable = false;
        else
            buttonComponent.interactable = true;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        buttonComponent = gameObject.GetComponent<Button>();
        CheckCredits();
        economySystem.creditsModified.AddListener(CheckCredits);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
