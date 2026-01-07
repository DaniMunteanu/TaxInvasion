using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Tooltip : MonoBehaviour
{
    [SerializeField]
    TMP_Text header;
    [SerializeField]
    TMP_Text description;
    [SerializeField]
    TMP_Text costField;
    [SerializeField]
    TMP_Text costNumber;

    [SerializeField]
    LayoutElement layoutElement;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetText(string headerText, string descriptionText, string costText)
    {
        if (String.IsNullOrEmpty(headerText))
        {
            header.gameObject.SetActive(false);
        }
        else
        {
            header.gameObject.SetActive(true);
            header.text = headerText;
        }

        description.text = descriptionText;

        if (String.IsNullOrEmpty(costText))
        {
            costField.gameObject.SetActive(false);
        }
        else
        {
            costField.gameObject.SetActive(true);
            costNumber.text = costText;
        }

        UpdateLayout();
    }

    void UpdateLayout()
    {
        Vector2 mousePosition = Mouse.current.position.ReadValue();
        float pivotX = mousePosition.x / Screen.width;
        float pivotY = mousePosition.y / Screen.height;

        layoutElement.enabled = Math.Max(header.preferredWidth, description.preferredWidth) >= layoutElement.preferredWidth;
        
        gameObject.GetComponent<RectTransform>().pivot = new Vector2(pivotX, pivotY); 
        transform.position = mousePosition;
    }
}
