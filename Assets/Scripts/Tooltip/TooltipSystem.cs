using UnityEngine;

public class TooltipSystem : MonoBehaviour
{
    private static TooltipSystem current;
    public Tooltip tooltip;

    public void Awake()
    {
        current = this;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void Show(string headerText, string descriptionText, string costText)
    {
        current.tooltip.SetText(headerText, descriptionText, costText);
        current.tooltip.gameObject.SetActive(true);
    }

    public static void Hide()
    {
        current.tooltip.gameObject.SetActive(false);
    }

    public static void UpdateText(string headerText, string descriptionText, string costText)
    {
        current.tooltip.SetText(headerText, descriptionText, costText);
    }
}
