using UnityEngine;
using UnityEngine.EventSystems;

public class TooltipTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public string headerText;
    public string descriptionText; 
    public string costText;
    public bool isShown = false;

    public void OnPointerEnter(PointerEventData eventData)
    {
        TooltipSystem.Show(headerText, descriptionText, costText);
        isShown = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        TooltipSystem.Hide();
        isShown = false;
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
