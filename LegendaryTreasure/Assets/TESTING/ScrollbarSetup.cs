using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollbarSetup : MonoBehaviour
{
    [SerializeField] private GameObject scrollbar;
    [SerializeField] private GameObject scrollrect;
    [SerializeField] private GameObject introductionField;

    void Start()
    {
        scrollbar.GetComponent<Scrollbar>().value = 1;
    }

    void Update()
    {
        AutoAcitvateScrollbar();
    }

    private void AutoAcitvateScrollbar()
    {
        scrollbar.SetActive(introductionField.activeSelf);
    }

    public bool EndOfScroll()
    {
        if (scrollrect.GetComponent<ScrollRect>().verticalNormalizedPosition <= 0.05f)
        {
            return true;
        }
        return false;
    }
}
