using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Encyclopedie : MonoBehaviour
{
    [SerializeField] GameObject[] LayoutButton;
    [SerializeField] GameObject[] ArmyPanel;
    int CurrentButtonIndex = 0;
    [SerializeField] bool isLayer1Right = true;

    [SerializeField] GameObject Layer1;
    [SerializeField] GameObject Layer2;



    private void Start()
    {
        LayoutButton[CurrentButtonIndex].GetComponent<Canvas>().sortingOrder = 1000;
        ArmyPanel[CurrentButtonIndex].SetActive(true);
    }

    public void Click(int index)
    {
        if (isLayer1Right & index > 3)
        {
            Vector2 SaveTransform = Layer1.transform.position;
            Layer1.transform.position = Layer2.transform.position;
            Layer2.transform.position = SaveTransform;
            isLayer1Right = false;
            Layer2.GetComponent<Canvas>().sortingOrder = 2;
            
        }
        else if (!isLayer1Right & index <= 3)
        {
            Vector2 SaveTransform = Layer1.transform.position;
            Layer1.transform.position = Layer2.transform.position;
            Layer2.transform.position = SaveTransform;
            isLayer1Right = true;
            Layer2.GetComponent<Canvas>().sortingOrder = 0;

        }

        if (CurrentButtonIndex != index)
        {
            ArmyPanel[index].SetActive(true);
            ArmyPanel[CurrentButtonIndex].SetActive(false);

            LayoutButton[CurrentButtonIndex].GetComponent<Canvas>().sortingOrder = -(CurrentButtonIndex + 10);
            CurrentButtonIndex = index;
            LayoutButton[CurrentButtonIndex].GetComponent<Canvas>().sortingOrder = 1000;
        }

        
    }

}
