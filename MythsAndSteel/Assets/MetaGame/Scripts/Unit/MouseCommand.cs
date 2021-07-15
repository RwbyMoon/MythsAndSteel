using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Script contenant les controles clavier+souris pour l'UI ainsi que le MouseOver quand la souris survole un �l�ment d'UI.
/// </summary>
public class MouseCommand : MonoBehaviour
{

    #region Variables
    [Header("UNIT REFERENCE")]
    public UnitReference unitReference = null;

    public bool _checkIfPlayerAsClic;

    public bool _hasCheckUnit = false;

    [Header("UI STATIQUE UNITE")]
    //Le panneau � afficher lorsqu'on souhaite voir les statistiques de l'unit� en cliquant.
    [SerializeField] private GameObject _mouseOverUI;
    public GameObject MouseOverUI => _mouseOverUI;
    //Le panneau ou les panneaux � afficher lorsqu'on souhaite le shift click sur l'unit�
    [SerializeField] private List<GameObject> _shiftUI;
    public List<GameObject> ShiftUI => _shiftUI;

    [Header("DELAI ATTENTE MOUSE OVER")]
    //Param�tre de d�lai qui s'applique � la coroutine.
    [SerializeField] private float _timeToWait = 2f;
    public float TimeToWait => _timeToWait;

    [Header("VALEUR POSITION UI")]
    //Permet de modifier la position de l'UI dans l'espace
    [SerializeField] private float _offsetXActivationMenu;
    [SerializeField] private float _offsetYActivationMenu;
    [Space]
    [SerializeField] private float _offsetXMouseOver;
    [SerializeField] private float _offsetYMouseOver;
    [Space]
    [SerializeField] private float _offsetXStatPlus;
    [SerializeField] private float _offsetYStatPlus;
    [Space]
    [SerializeField] private Vector2 _xOffsetMin;
    [SerializeField] private Vector2 _yOffsetMin;
    [SerializeField] private Vector2 _xOffset;
    [SerializeField] private Vector2 _yOffset;
    [SerializeField] private Vector2 _xOffsetMax;
    [SerializeField] private Vector2 _yOffsetMax;

    [Header("UI RENFORT UNITE")]
    [SerializeField] private GameObject _renfortUI;
    public GameObject RenfortUI => _renfortUI;

    [SerializeField] private List<GameObject> _elementMenuRenfort = null;
    public List<GameObject> ElementOfMenuRenfort => _elementMenuRenfort;

    [Header("UNIT ICONE")]
    [SerializeField] private UnitIcon _iconeUnit = null;
    #endregion Variables

    #region UpdateStats
    void UpdateUIStats()
    {

        //Si la tile ne contient pas d'effet de terrain, on n'affiche pas d'information. Si la tile contient 1 effet, on affiche et met � jour l'effet de la case. Si la tile contient 2 effets, on affiche les 2 Effets.
        UIInstance UI = UIInstance.Instance;

        //Statistique pour le MouseOver.
        UnitScript unit = RaycastManager.Instance.UnitInTile.GetComponent<UnitScript>();
        
        UI.TitlePanelMouseOver.GetComponent<TextMeshProUGUI>().text = unit.UnitSO.UnitName;
        UI.MouseOverStats._lifeGam.GetComponent<TextMeshProUGUI>().text = (unit.Life + unit.Shield).ToString();
        UI.MouseOverStats._rangeGam.GetComponent<TextMeshProUGUI>().text = (unit.AttackRange + unit.AttackRangeBonus).ToString();
        UI.MouseOverStats._moveGam.GetComponent<TextMeshProUGUI>().text = (unit.MoveSpeed + unit.MoveSpeedBonus).ToString();
     
        switch (unit.UnitSO.typeUnite)
        {
            case MYthsAndSteel_Enum.TypeUnite.Infanterie:
                UI.PageUnitStat._unitSpriteGam.GetComponent<Image>().sprite = _iconeUnit.infanterieSprite;
                UI.PageUnitStat._unitSpriteGam.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = "Infanterie";
                break;
            case MYthsAndSteel_Enum.TypeUnite.Artillerie:
                UI.PageUnitStat._unitSpriteGam.GetComponent<Image>().sprite = _iconeUnit.ArtillerieSprite;
                UI.PageUnitStat._unitSpriteGam.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = "Artillerie";
                break;
            case MYthsAndSteel_Enum.TypeUnite.Vehicule:
                UI.PageUnitStat._unitSpriteGam.GetComponent<Image>().sprite = _iconeUnit.VehiculeSprite;
                UI.PageUnitStat._unitSpriteGam.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = "Char";
                break;
            case MYthsAndSteel_Enum.TypeUnite.Mythe:
                UI.PageUnitStat._unitSpriteGam.GetComponent<Image>().sprite = _iconeUnit.MytheSprite;
                UI.PageUnitStat._unitSpriteGam.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = "Mythe";
                break;
            case MYthsAndSteel_Enum.TypeUnite.Mecha:
                UI.PageUnitStat._unitSpriteGam.GetComponent<Image>().sprite = _iconeUnit.MechaSprite;
                UI.PageUnitStat._unitSpriteGam.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = "M�cha";
                break;
            case MYthsAndSteel_Enum.TypeUnite.Leader:
                UI.PageUnitStat._unitSpriteGam.GetComponent<Image>().sprite = _iconeUnit.LeaderSprite;
                UI.PageUnitStat._unitSpriteGam.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = "Leader";
                break;
        }



        //Statistique de la Page 1 du Carnet.
        //Synchronise le texte du titre.
        UI.TitlePanelShiftClicPage1.GetComponent<TextMeshProUGUI>().text = unit.UnitSO.UnitName;
        //Synchronise le texte de la vie avec l'emplacement d'UI.
        UI.PageUnitStat._lifeGam.GetComponent<TextMeshProUGUI>().text = (unit.Life + unit.Shield).ToString();
        //Synchronise le texte de la valeur de la distance d'attaque de l'unit� avec l'emplacement d'UI.
        //  UI.PageUnitStat._rangeGam.GetComponent<TextMeshProUGUI>().text = unit.AttackRange.ToString();
        //Synchronise le texte de la valeur de la vitesse de l'unit� avec l'emplacement d'UI.
        UI.PageUnitStat._rangeGam.GetComponent<TextMeshProUGUI>().text = (unit.AttackRange+ unit.AttackRangeBonus).ToString();
        UI.PageUnitStat._moveGam.GetComponent<TextMeshProUGUI>().text = (unit.MoveSpeed+unit.MoveSpeedBonus).ToString();

        UpdateMiniJauge(unit);


        for (int i = UI.capacityList.Count - 1; i >= 0; i--)
        {
            Destroy(UI.capacityList[UI.capacityList.Count - 1]);
            UI.capacityList.RemoveAt(UI.capacityList.Count - 1);
        }

        if (RaycastManager.Instance.Tile.GetComponent<TileScript>().Unit.TryGetComponent<InfoCarnet>(out InfoCarnet Capa))
        {
            int contentSize = 0;
            // CAPACITY 1.             
            if (Capa.ReturnInfo(UI.capacityPrefab, 0) != null)
            {
                UI.capacityParent.transform.parent.parent.GetComponent<ScrollRect>().verticalScrollbar.value = 1;
                GameObject CAPA1 = Instantiate(Capa.ReturnInfo(UI.capacityPrefab, 0), Vector2.zero, Quaternion.identity);
                CAPA1.transform.SetParent(UI.capacityParent.transform);
                CAPA1.transform.localScale = new Vector3(.9f, .9f, .9f);
                UI.capacityList.Add(CAPA1);

                int lengthTxt = CAPA1.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text.Length;
                float LengthLine = (float)lengthTxt / 21;
                int truncateLine = (int)LengthLine;
                int capaSize = 130 + (20 * truncateLine);
                contentSize += capaSize;
            }
            //CAPACITY 2
            if(Capa.ReturnInfo(UI.capacityPrefab, 1) != null)
            {
                GameObject CAPA2 = Instantiate(Capa.ReturnInfo(UI.capacityPrefab, 1), Vector2.zero, Quaternion.identity);
                CAPA2.transform.SetParent(UI.capacityParent.transform);
                CAPA2.transform.localScale = new Vector3(.9f, .9f, .9f);
                UI.capacityList.Add(CAPA2);

                int lengthTxt = CAPA2.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text.Length;
                float LengthLine = (float)lengthTxt / 21;
                int truncateLine = (int)LengthLine;
                int capaSize = 130 + (20 * truncateLine);
                contentSize += capaSize;
            }

            UI.capacityParent.GetComponent<RectTransform>().sizeDelta = new Vector2(UI.capacityParent.GetComponent<RectTransform>().sizeDelta.x, contentSize);
        }

        //Attributs
        MYthsAndSteel_Enum.Attributs[] _UnitAttributs = RaycastManager.Instance.UnitInTile.GetComponent<UnitScript>().UnitSO.UnitAttributs;
        for (int i = 0; i < 3; i++)
        {
            if (_UnitAttributs.Length > i)
            {
                if (_UnitAttributs[i] == MYthsAndSteel_Enum.Attributs.Aucun)
                {
                    UIInstance.Instance.objectsAttributs[i].MainObjects.SetActive(false);
                    continue;
                }

                foreach (TextSpriteAttributUnit attribut in UIInstance.Instance.textSpriteAttributUnit)
                {
                    if (attribut._attributs == _UnitAttributs[i])
                    {
                        GameObject gam = UIInstance.Instance.objectsAttributs[i].MainObjects;
                        gam.SetActive(true);
                        gam.transform.GetChild(0).GetComponent<Image>().sprite = attribut.SpriteAttributUnit;

                        UIInstance.Instance.objectsAttributs[i].Description.GetComponent<TextMeshProUGUI>().text = attribut._name + " :" + attribut.TextAttributUnit;
                        continue;
                    }
                }
            }
            else
            {
                UIInstance.Instance.objectsAttributs[i].MainObjects.SetActive(false);
                continue;
            }
        }

        //Statistique de la Page 2 du Carnet.  
        //Compl�ter avec les Images des Tiles.

        for (int i = UI.effetDeTerrain.Count - 1; i >= 0; i--)
        {
            Destroy(UI.effetDeTerrain[UI.effetDeTerrain.Count - 1]);
            UI.effetDeTerrain.RemoveAt(UI.effetDeTerrain.Count - 1);
        }

        UI.parentSlotEffetDeTerrain.transform.parent.parent.GetComponent<ScrollRect>().verticalScrollbar.value = 1;
        foreach (MYthsAndSteel_Enum.TerrainType Terrain in RaycastManager.Instance.Tile.GetComponent<TileScript>().TerrainEffectList)
        {
            GameObject Effet = Instantiate(UI.Terrain.ReturnInfo(UI.prefabSlotEffetDeTerrain, Terrain), UI.parentSlotEffetDeTerrain.transform.position, Quaternion.identity);
            Effet.transform.SetParent(UI.parentSlotEffetDeTerrain.transform);
            Effet.transform.localScale = new Vector3(.9f, .9f, .9f);
            UI.effetDeTerrain.Add(Effet);

            UI.parentSlotEffetDeTerrain.GetComponent<RectTransform>().sizeDelta = new Vector2(UI.parentSlotEffetDeTerrain.GetComponent<RectTransform>().sizeDelta.x, 212 * UI.effetDeTerrain.Count);
        }

        if (RaycastManager.Instance.Tile.GetComponent<TileScript>().TerrainEffectList.Count == 0)
        {
            GameObject Effet = Instantiate(UI.prefabSlotEffetDeTerrain, UI.parentSlotEffetDeTerrain.transform.position, Quaternion.identity);
            Effet.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "Liste vide.";
            Effet.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = "Cette unit� n'a actuellement aucun pouvoir.";
            Effet.transform.SetParent(UI.parentSlotEffetDeTerrain.transform);
            Effet.transform.localScale = new Vector3(.9f, .9f, .9f);
            UI.parentSlotEffetDeTerrain.GetComponent<RectTransform>().sizeDelta = new Vector2(UI.parentSlotEffetDeTerrain.GetComponent<RectTransform>().sizeDelta.x, 212 * UI.Statuts.Count);
        }

        for (int i = UI.Statuts.Count - 1; i >= 0; i--)
        {
            Destroy(UI.Statuts[UI.Statuts.Count - 1]);
            UI.Statuts.RemoveAt(UI.Statuts.Count - 1);
        }

        UI.parentSlotStatuts.transform.parent.parent.GetComponent<ScrollRect>().verticalScrollbar.value = 1;
        if(RaycastManager.Instance.UnitInTile != null)
        {
            foreach (MYthsAndSteel_Enum.UnitStatut status in RaycastManager.Instance.UnitInTile.GetComponent<UnitScript>().UnitStatuts)
            {
                GameObject Effet = Instantiate(UI.StatusSc.ReturnInfo(UI.prefabSlotStatuts, status), UI.parentSlotStatuts.transform.position, Quaternion.identity);
                Effet.transform.SetParent(UI.parentSlotStatuts.transform);
                Effet.transform.localScale = new Vector3(.9f, .9f, .9f);
                UI.Statuts.Add(Effet);

                UI.parentSlotStatuts.GetComponent<RectTransform>().sizeDelta = new Vector2(UI.parentSlotStatuts.GetComponent<RectTransform>().sizeDelta.x, 212 * UI.Statuts.Count);
            }
        }
    }

    #endregion UpdateStats

    public void UpdateMiniJauge(UnitScript Unit)
    {
        bool Done = false;
        List<int> Min = new List<int>();
        List<int> Max = new List<int>();
        List<int> Temp = new List<int>();

        int StartMin = (int)Unit.NumberRangeMin.x;
        int EndMin = (int)Unit.NumberRangeMin.y;
        int StartMax = (int)Unit.NumberRangeMax.x;
        int EndMax = (int)Unit.NumberRangeMax.y;
        int DiceBonus = Unit.DiceBonus;

        for (int u = 0; u < UIInstance.Instance.MiniJaugeSlot.Count; u++)
        {
            Temp.Add(u + 2);
        }
        if (StartMin >= 2 && EndMin <= 12)
        {
            for (int i = StartMin - DiceBonus; i <= EndMin - DiceBonus; i++)
            {
                int u = i;
                if (i < 2)
                {
                    continue;
                }
                if (i > 12)
                {
                    continue;
                }
                if (!Done)
                {
                    Done = true;
                    UIInstance.Instance.MinSlider.SetActive(true);
                    UIInstance.Instance.MinSlider.transform.position = new Vector3(UIInstance.Instance.MiniJaugeSlot[u - 2].transform.position.x, UIInstance.Instance.MinSlider.transform.position.y, UIInstance.Instance.MinSlider.transform.position.z);
                    UIInstance.Instance.MinSlider.GetComponentInChildren<TextMeshProUGUI>().text = (Unit.DamageMinimum + Unit.DamageBonus).ToString();
                }
                UIInstance.Instance.MiniJaugeSlot[u - 2].sprite = UIInstance.Instance.Minimum;
                Min.Add(u);
                Temp.Remove(u);
            }
        }
        Done = false;
        if (StartMax >= 2 && EndMax <= 12)
        {
            for (int i = StartMax - DiceBonus; i <= EndMax; i++)
            {
                int u = i;
                if (i < 2)
                {
                    continue;
                }
                if (i > 12)
                {
                    continue;
                }
                if (!Done)
                {
                    Done = true;
                    UIInstance.Instance.MaxSlider.SetActive(true);
                    UIInstance.Instance.MaxSlider.transform.position = new Vector3(UIInstance.Instance.MiniJaugeSlot[u - 2].transform.position.x, UIInstance.Instance.MaxSlider.transform.position.y, UIInstance.Instance.MaxSlider.transform.position.z);
                    UIInstance.Instance.MaxSlider.GetComponentInChildren<TextMeshProUGUI>().text = (Unit.DamageMaximum + Unit.DamageBonus).ToString();
                }
                UIInstance.Instance.MiniJaugeSlot[u - 2].sprite = UIInstance.Instance.Maximum;
                Max.Add(u);
                Temp.Remove(u);
            }
        }
        foreach (int I in Temp)
        {
            UIInstance.Instance.MiniJaugeSlot[I - 2].sprite = UIInstance.Instance.None;
        }

        if (Max.Count == 0)
        {
            UIInstance.Instance.MaxSlider.SetActive(false);
        }
        if (Min.Count == 0)
        {
            UIInstance.Instance.MinSlider.SetActive(false);
        }
    }

    private void Update()
    {
        UIInstance UI = UIInstance.Instance;
        GameObject Tile = RaycastManager.Instance.Tile;

        //Update des info de la tile sur le pannel du bas quand
        UI.CallUpdateUI(Tile);
    }

    #region ActivateUI
    /// <summary>
    /// Permet d'activer un �l�ment de l'UI en utilisant un Raycast distint de la position et d'assigner une position custom par rapport au Canvas (Conflit avec le Canvas).
    /// </summary>
    /// <param name="uiElements"></param>
    /// <param name="offSetX"></param>
    /// <param name="offSetY"></param>
    public void ActivateUI(GameObject uiElements, float lastPosX = 0, float lastPosY = 0, bool switchPage = false, bool activationMenu = false, bool mouseOver = false, bool bigStat = false)
    {
        //Reprendre la position du raycast qui a s�lectionn� la tile
        RaycastHit2D hit = RaycastManager.Instance.GetRaycastHit();

        //Je stop l'ensemble des coroutines en cours.
        Vector3 pos = Vector3.zero;
        StopAllCoroutines();

        //Menu d'activation d'une unit�
        if (activationMenu)
        {
            if (hit.transform.position.x >= _xOffset.y)
            {
                if (hit.transform.position.x >= _xOffsetMax.y)
                {
                    if (hit.transform.position.y >= _yOffset.y)
                    {
                        if (hit.transform.position.y >= _yOffsetMax.y)
                        {
                            pos = Camera.main.WorldToScreenPoint(new Vector3(hit.transform.position.x - _offsetXActivationMenu, hit.transform.position.y - _offsetYActivationMenu + .5f, hit.transform.position.z));
                        }
                        else
                        {
                            pos = Camera.main.WorldToScreenPoint(new Vector3(hit.transform.position.x - _offsetXActivationMenu, hit.transform.position.y, hit.transform.position.z));
                        }
                    }
                    else if (hit.transform.position.y <= _yOffset.x)
                    {
                        if (hit.transform.position.y <= _yOffsetMax.x)
                        {
                            pos = Camera.main.WorldToScreenPoint(new Vector3(hit.transform.position.x - _offsetXActivationMenu, hit.transform.position.y + _offsetYActivationMenu - .5f, hit.transform.position.z));
                        }
                        else
                        {
                            pos = Camera.main.WorldToScreenPoint(new Vector3(hit.transform.position.x - _offsetXActivationMenu, hit.transform.position.y, hit.transform.position.z));
                        }
                    }
                    else
                    {
                        pos = Camera.main.WorldToScreenPoint(new Vector3(hit.transform.position.x - _offsetXActivationMenu, hit.transform.position.y, hit.transform.position.z));
                    }
                }
                else
                {
                    if (hit.transform.position.y >= _yOffset.y)
                    {
                        if (hit.transform.position.y >= _yOffsetMax.y)
                        {
                            pos = Camera.main.WorldToScreenPoint(new Vector3(hit.transform.position.x - _offsetXActivationMenu, hit.transform.position.y - _offsetYActivationMenu + .5f, hit.transform.position.z));
                        }
                        else
                        {
                            pos = Camera.main.WorldToScreenPoint(new Vector3(hit.transform.position.x - _offsetXActivationMenu, hit.transform.position.y, hit.transform.position.z));
                        }
                    }
                    else if (hit.transform.position.y <= _yOffset.x)
                    {
                        if (hit.transform.position.y <= _yOffsetMax.x)
                        {
                            pos = Camera.main.WorldToScreenPoint(new Vector3(hit.transform.position.x - _offsetXActivationMenu, hit.transform.position.y + _offsetYActivationMenu - .5f, hit.transform.position.z));
                        }
                        else
                        {
                            pos = Camera.main.WorldToScreenPoint(new Vector3(hit.transform.position.x - _offsetXActivationMenu, hit.transform.position.y, hit.transform.position.z));
                        }
                    }
                    else
                    {
                        pos = Camera.main.WorldToScreenPoint(new Vector3(hit.transform.position.x - _offsetXActivationMenu, hit.transform.position.y, hit.transform.position.z));
                    }
                }
            }

            else if (hit.transform.position.x <= _xOffset.x)
            {
                if (hit.transform.position.x <= _xOffsetMax.x)
                {
                    if (hit.transform.position.y >= _yOffset.y)
                    {
                        if (hit.transform.position.y >= _yOffsetMax.y)
                        {
                            pos = Camera.main.WorldToScreenPoint(new Vector3(hit.transform.position.x + _offsetXActivationMenu, hit.transform.position.y - _offsetYActivationMenu + .5f, hit.transform.position.z));
                        }
                        else
                        {
                            pos = Camera.main.WorldToScreenPoint(new Vector3(hit.transform.position.x + _offsetXActivationMenu, hit.transform.position.y, hit.transform.position.z));
                        }
                    }
                    else if (hit.transform.position.y <= _yOffset.x)
                    {
                        if (hit.transform.position.y <= _yOffsetMax.x)
                        {
                            pos = Camera.main.WorldToScreenPoint(new Vector3(hit.transform.position.x + _offsetXActivationMenu, hit.transform.position.y + _offsetYActivationMenu - .5f, hit.transform.position.z));
                        }
                        else
                        {
                            pos = Camera.main.WorldToScreenPoint(new Vector3(hit.transform.position.x + _offsetXActivationMenu, hit.transform.position.y, hit.transform.position.z));
                        }
                    }
                    else
                    {
                        pos = Camera.main.WorldToScreenPoint(new Vector3(hit.transform.position.x + _offsetXActivationMenu, hit.transform.position.y, hit.transform.position.z));
                    }
                }
                else
                {
                    if (hit.transform.position.y >= _yOffset.y)
                    {
                        if (hit.transform.position.y >= _yOffsetMax.y)
                        {
                            pos = Camera.main.WorldToScreenPoint(new Vector3(hit.transform.position.x + _offsetXActivationMenu, hit.transform.position.y - _offsetYActivationMenu + .5f, hit.transform.position.z));
                        }
                        else
                        {
                            pos = Camera.main.WorldToScreenPoint(new Vector3(hit.transform.position.x + _offsetXActivationMenu, hit.transform.position.y, hit.transform.position.z));
                        }
                    }
                    else if (hit.transform.position.y <= _yOffset.x)
                    {
                        if (hit.transform.position.y <= _yOffsetMax.x)
                        {
                            pos = Camera.main.WorldToScreenPoint(new Vector3(hit.transform.position.x + _offsetXActivationMenu, hit.transform.position.y + _offsetYActivationMenu - .5f, hit.transform.position.z));
                        }
                        else
                        {
                            pos = Camera.main.WorldToScreenPoint(new Vector3(hit.transform.position.x + _offsetXActivationMenu, hit.transform.position.y, hit.transform.position.z));
                        }
                    }
                    else
                    {
                        pos = Camera.main.WorldToScreenPoint(new Vector3(hit.transform.position.x + _offsetXActivationMenu, hit.transform.position.y, hit.transform.position.z));
                    }
                }
            }
            else
            {
                if (hit.transform.position.y >= _yOffsetMax.y)
                {
                    pos = Camera.main.WorldToScreenPoint(new Vector3(hit.transform.position.x + _offsetXActivationMenu, hit.transform.position.y - _offsetYActivationMenu, hit.transform.position.z));
                }
                else if (hit.transform.position.y <= _yOffsetMax.x)
                {
                    pos = Camera.main.WorldToScreenPoint(new Vector3(hit.transform.position.x + _offsetXActivationMenu, hit.transform.position.y + _offsetYActivationMenu, hit.transform.position.z));
                }
                else
                {
                    pos = Camera.main.WorldToScreenPoint(new Vector3(hit.transform.position.x + _offsetXActivationMenu, hit.transform.position.y, hit.transform.position.z));
                }
            }
        }

        //Menu mouseOver
        else if (mouseOver)
        {
            if (hit.transform.position.x >= _xOffset.y)
            {
                if (hit.transform.position.x >= _xOffsetMax.y)
                {
                    if (hit.transform.position.y >= _yOffsetMin.y)
                    {
                        if (hit.transform.position.y >= _yOffsetMax.y)
                        {
                            pos = Camera.main.WorldToScreenPoint(new Vector3(hit.transform.position.x - _offsetXMouseOver, hit.transform.position.y - _offsetYMouseOver - 1, hit.transform.position.z));
                        }
                        else if (hit.transform.position.y <= _yOffsetMax.y && hit.transform.position.y >= _yOffset.y)
                        {
                            pos = Camera.main.WorldToScreenPoint(new Vector3(hit.transform.position.x - _offsetXMouseOver, hit.transform.position.y - _offsetYMouseOver - .5f, hit.transform.position.z));
                        }
                        else
                        {
                            pos = Camera.main.WorldToScreenPoint(new Vector3(hit.transform.position.x - _offsetXMouseOver, hit.transform.position.y - _offsetYMouseOver - .5f, hit.transform.position.z));
                        }
                    }
                    else if (hit.transform.position.y <= _yOffsetMin.x)
                    {
                        if (hit.transform.position.y <= _yOffsetMax.x)
                        {
                            pos = Camera.main.WorldToScreenPoint(new Vector3(hit.transform.position.x - _offsetXMouseOver, hit.transform.position.y + _offsetYMouseOver + 1, hit.transform.position.z));
                        }
                        else if (hit.transform.position.y >= _yOffsetMax.x && hit.transform.position.y <= _yOffset.x)
                        {
                            pos = Camera.main.WorldToScreenPoint(new Vector3(hit.transform.position.x - _offsetXMouseOver, hit.transform.position.y + _offsetYMouseOver + .5f, hit.transform.position.z));
                        }
                        else
                        {
                            pos = Camera.main.WorldToScreenPoint(new Vector3(hit.transform.position.x - _offsetXMouseOver, hit.transform.position.y + _offsetYMouseOver + .5f, hit.transform.position.z));
                        }
                    }
                    else
                    {
                        pos = Camera.main.WorldToScreenPoint(new Vector3(hit.transform.position.x - _offsetXMouseOver, hit.transform.position.y - _offsetYMouseOver - .5f, hit.transform.position.z));
                    }
                }
                else
                {
                    if (hit.transform.position.y >= _yOffsetMin.y)
                    {
                        if (hit.transform.position.y >= _yOffsetMax.y)
                        {
                            pos = Camera.main.WorldToScreenPoint(new Vector3(hit.transform.position.x, hit.transform.position.y - _offsetYMouseOver - 1, hit.transform.position.z));
                        }
                        else if (hit.transform.position.y >= _yOffset.y && hit.transform.position.y <= _yOffsetMax.y)
                        {
                            pos = Camera.main.WorldToScreenPoint(new Vector3(hit.transform.position.x, hit.transform.position.y - _offsetYMouseOver - .5f, hit.transform.position.z));
                        }
                        else
                        {
                            pos = Camera.main.WorldToScreenPoint(new Vector3(hit.transform.position.x, hit.transform.position.y - _offsetYMouseOver - .5f, hit.transform.position.z));
                        }
                    }
                    else if (hit.transform.position.y <= _yOffsetMin.x)
                    {
                        if (hit.transform.position.y <= _yOffsetMax.x)
                        {
                            pos = Camera.main.WorldToScreenPoint(new Vector3(hit.transform.position.x, hit.transform.position.y + _offsetYMouseOver + 1, hit.transform.position.z));
                        }
                        else if (hit.transform.position.y >= _yOffsetMax.x && hit.transform.position.y <= _yOffset.x)
                        {
                            pos = Camera.main.WorldToScreenPoint(new Vector3(hit.transform.position.x, hit.transform.position.y + _offsetYMouseOver + .5f, hit.transform.position.z));
                        }
                        else
                        {
                            pos = Camera.main.WorldToScreenPoint(new Vector3(hit.transform.position.x, hit.transform.position.y + _offsetYMouseOver + .5f, hit.transform.position.z));
                        }
                    }
                    else
                    {
                        pos = Camera.main.WorldToScreenPoint(new Vector3(hit.transform.position.x, hit.transform.position.y - _offsetYMouseOver - .5f, hit.transform.position.z));
                    }
                }
            }

            else if (hit.transform.position.x <= _xOffset.x)
            {
                if (hit.transform.position.x <= _xOffsetMax.x)
                {
                    if (hit.transform.position.y >= _yOffsetMin.y)
                    {
                        if (hit.transform.position.y >= _yOffsetMax.y)
                        {
                            pos = Camera.main.WorldToScreenPoint(new Vector3(hit.transform.position.x + _offsetXMouseOver, hit.transform.position.y - _offsetYMouseOver - 1, hit.transform.position.z));
                        }
                        else if (hit.transform.position.y >= _yOffset.y && hit.transform.position.y <= _yOffsetMax.y)
                        {
                            pos = Camera.main.WorldToScreenPoint(new Vector3(hit.transform.position.x + _offsetXMouseOver, hit.transform.position.y - _offsetYMouseOver - .5f, hit.transform.position.z));
                        }
                        else
                        {
                            pos = Camera.main.WorldToScreenPoint(new Vector3(hit.transform.position.x + _offsetXMouseOver, hit.transform.position.y - _offsetYMouseOver - .5f, hit.transform.position.z));
                        }
                    }
                    else if (hit.transform.position.y <= _yOffsetMin.x)
                    {
                        if (hit.transform.position.y <= _yOffsetMax.x)
                        {
                            pos = Camera.main.WorldToScreenPoint(new Vector3(hit.transform.position.x + _offsetXMouseOver, hit.transform.position.y + _offsetYMouseOver + 1, hit.transform.position.z));
                        }
                        else if (hit.transform.position.y >= _yOffsetMax.x && hit.transform.position.y <= _yOffset.x)
                        {
                            pos = Camera.main.WorldToScreenPoint(new Vector3(hit.transform.position.x + _offsetXMouseOver, hit.transform.position.y + _offsetYMouseOver + .5f, hit.transform.position.z));
                        }
                        else
                        {
                            pos = Camera.main.WorldToScreenPoint(new Vector3(hit.transform.position.x + _offsetXMouseOver, hit.transform.position.y + _offsetYMouseOver + .5f, hit.transform.position.z));
                        }
                    }
                    else
                    {
                        pos = Camera.main.WorldToScreenPoint(new Vector3(hit.transform.position.x, hit.transform.position.y - _offsetYMouseOver - .5f, hit.transform.position.z));
                    }
                }
                else
                {
                    if (hit.transform.position.y >= _yOffsetMin.y)
                    {
                        if (hit.transform.position.y >= _yOffsetMax.y)
                        {
                            pos = Camera.main.WorldToScreenPoint(new Vector3(hit.transform.position.x + _offsetXMouseOver, hit.transform.position.y - _offsetYMouseOver - 1, hit.transform.position.z));
                        }
                        else if (hit.transform.position.y >= _yOffset.y && hit.transform.position.y <= _yOffsetMax.y)
                        {
                            pos = Camera.main.WorldToScreenPoint(new Vector3(hit.transform.position.x + _offsetXMouseOver, hit.transform.position.y - _offsetYMouseOver - .5f, hit.transform.position.z));
                        }
                        else
                        {
                            pos = Camera.main.WorldToScreenPoint(new Vector3(hit.transform.position.x + _offsetXMouseOver, hit.transform.position.y - _offsetYMouseOver - .5f, hit.transform.position.z));
                        }
                    }
                    else if (hit.transform.position.y <= _yOffsetMin.x)
                    {
                        if (hit.transform.position.y <= _yOffsetMax.x)
                        {
                            pos = Camera.main.WorldToScreenPoint(new Vector3(hit.transform.position.x + _offsetXMouseOver, hit.transform.position.y + _offsetYMouseOver + 1, hit.transform.position.z));
                        }
                        else if (hit.transform.position.y >= _yOffsetMax.x && hit.transform.position.y <= _yOffset.x)
                        {
                            pos = Camera.main.WorldToScreenPoint(new Vector3(hit.transform.position.x + _offsetXMouseOver, hit.transform.position.y + _offsetYMouseOver + .5f, hit.transform.position.z));
                        }
                        else
                        {
                            pos = Camera.main.WorldToScreenPoint(new Vector3(hit.transform.position.x + _offsetXMouseOver, hit.transform.position.y + _offsetYMouseOver + .5f, hit.transform.position.z));
                        }
                    }
                    else
                    {
                        pos = Camera.main.WorldToScreenPoint(new Vector3(hit.transform.position.x, hit.transform.position.y - _offsetYMouseOver - .5f, hit.transform.position.z));
                    }
                }
            }
            else
            {
                if (hit.transform.position.y >= _yOffsetMax.y)
                {
                    pos = Camera.main.WorldToScreenPoint(new Vector3(hit.transform.position.x - _offsetXMouseOver / 2, hit.transform.position.y - _offsetYMouseOver - .5f, hit.transform.position.z));
                }
                else if (hit.transform.position.y <= _yOffsetMax.x)
                {
                    pos = Camera.main.WorldToScreenPoint(new Vector3(hit.transform.position.x - _offsetXMouseOver / 2, hit.transform.position.y + _offsetYMouseOver + .5f, hit.transform.position.z));
                }
                else
                {
                    pos = Camera.main.WorldToScreenPoint(new Vector3(hit.transform.position.x - _offsetXMouseOver * 2.5f, hit.transform.position.y, hit.transform.position.z));
                }
            }
        }

        //Menu avec toutes les stats
        else if (bigStat)
        {
            if (hit.transform.position.x >= _xOffset.y)
            {
                if (hit.transform.position.x >= _xOffsetMax.y)
                {
                    if (hit.transform.position.y >= _yOffsetMin.y)
                    {
                        if (hit.transform.position.y >= _yOffsetMax.y)
                        {
                            pos = Camera.main.WorldToScreenPoint(new Vector3(hit.transform.position.x - _offsetXStatPlus, hit.transform.position.y - _offsetYStatPlus - 1, hit.transform.position.z));
                        }
                        else if (hit.transform.position.y <= _yOffsetMax.y && hit.transform.position.y >= _yOffset.y)
                        {
                            pos = Camera.main.WorldToScreenPoint(new Vector3(hit.transform.position.x - _offsetXStatPlus, hit.transform.position.y - _offsetYStatPlus - .5f, hit.transform.position.z));
                        }
                        else
                        {
                            pos = Camera.main.WorldToScreenPoint(new Vector3(hit.transform.position.x - _offsetXStatPlus, hit.transform.position.y - _offsetYStatPlus - .5f, hit.transform.position.z));
                        }
                    }
                    else if (hit.transform.position.y <= _yOffsetMin.x)
                    {
                        if (hit.transform.position.y <= _yOffsetMax.x)
                        {
                            pos = Camera.main.WorldToScreenPoint(new Vector3(hit.transform.position.x - _offsetXStatPlus, hit.transform.position.y + _offsetYStatPlus + 1, hit.transform.position.z));
                        }
                        else if (hit.transform.position.y >= _yOffsetMax.x && hit.transform.position.y <= _yOffset.x)
                        {
                            pos = Camera.main.WorldToScreenPoint(new Vector3(hit.transform.position.x - _offsetXStatPlus, hit.transform.position.y + _offsetYStatPlus + .5f, hit.transform.position.z));
                        }
                        else
                        {
                            pos = Camera.main.WorldToScreenPoint(new Vector3(hit.transform.position.x - _offsetXStatPlus, hit.transform.position.y + _offsetYStatPlus + .5f, hit.transform.position.z));
                        }
                    }
                    else
                    {
                        pos = Camera.main.WorldToScreenPoint(new Vector3(hit.transform.position.x - _offsetXStatPlus, hit.transform.position.y - _offsetYStatPlus - .5f, hit.transform.position.z));
                    }
                }
                else
                {
                    if (hit.transform.position.y >= _yOffsetMin.y)
                    {
                        if (hit.transform.position.y >= _yOffsetMax.y)
                        {
                            pos = Camera.main.WorldToScreenPoint(new Vector3(hit.transform.position.x, hit.transform.position.y - _offsetYStatPlus - 1, hit.transform.position.z));
                        }
                        else if (hit.transform.position.y >= _yOffset.y && hit.transform.position.y <= _yOffsetMax.y)
                        {
                            pos = Camera.main.WorldToScreenPoint(new Vector3(hit.transform.position.x, hit.transform.position.y - _offsetYStatPlus - .5f, hit.transform.position.z));
                        }
                        else
                        {
                            pos = Camera.main.WorldToScreenPoint(new Vector3(hit.transform.position.x, hit.transform.position.y - _offsetYStatPlus - .5f, hit.transform.position.z));
                        }
                    }
                    else if (hit.transform.position.y <= _yOffsetMin.x)
                    {
                        if (hit.transform.position.y <= _yOffsetMax.x)
                        {
                            pos = Camera.main.WorldToScreenPoint(new Vector3(hit.transform.position.x, hit.transform.position.y + _offsetYStatPlus + 1, hit.transform.position.z));
                        }
                        else if (hit.transform.position.y >= _yOffsetMax.x && hit.transform.position.y <= _yOffset.x)
                        {
                            pos = Camera.main.WorldToScreenPoint(new Vector3(hit.transform.position.x, hit.transform.position.y + _offsetYStatPlus + .5f, hit.transform.position.z));
                        }
                        else
                        {
                            pos = Camera.main.WorldToScreenPoint(new Vector3(hit.transform.position.x, hit.transform.position.y + _offsetYStatPlus + .5f, hit.transform.position.z));
                        }
                    }
                    else
                    {
                        pos = Camera.main.WorldToScreenPoint(new Vector3(hit.transform.position.x, hit.transform.position.y - _offsetYStatPlus - .5f, hit.transform.position.z));
                    }
                }
            }

            else if (hit.transform.position.x <= _xOffset.x)
            {
                if (hit.transform.position.x <= _xOffsetMax.x)
                {
                    if (hit.transform.position.y >= _yOffsetMin.y)
                    {
                        if (hit.transform.position.y >= _yOffsetMax.y)
                        {
                            pos = Camera.main.WorldToScreenPoint(new Vector3(hit.transform.position.x + _offsetXStatPlus, hit.transform.position.y - _offsetYStatPlus - 1, hit.transform.position.z));
                        }
                        else if (hit.transform.position.y >= _yOffset.y && hit.transform.position.y <= _yOffsetMax.y)
                        {
                            pos = Camera.main.WorldToScreenPoint(new Vector3(hit.transform.position.x + _offsetXStatPlus, hit.transform.position.y - _offsetYStatPlus - .5f, hit.transform.position.z));
                        }
                        else
                        {
                            pos = Camera.main.WorldToScreenPoint(new Vector3(hit.transform.position.x + _offsetXStatPlus, hit.transform.position.y - _offsetYStatPlus - .5f, hit.transform.position.z));
                        }
                    }
                    else if (hit.transform.position.y <= _yOffsetMin.x)
                    {
                        if (hit.transform.position.y <= _yOffsetMax.x)
                        {
                            pos = Camera.main.WorldToScreenPoint(new Vector3(hit.transform.position.x + _offsetXStatPlus, hit.transform.position.y + _offsetYStatPlus + 1, hit.transform.position.z));
                        }
                        else if (hit.transform.position.y >= _yOffsetMax.x && hit.transform.position.y <= _yOffset.x)
                        {
                            pos = Camera.main.WorldToScreenPoint(new Vector3(hit.transform.position.x + _offsetXStatPlus, hit.transform.position.y + _offsetYStatPlus + .5f, hit.transform.position.z));
                        }
                        else
                        {
                            pos = Camera.main.WorldToScreenPoint(new Vector3(hit.transform.position.x + _offsetXStatPlus, hit.transform.position.y + _offsetYStatPlus + .5f, hit.transform.position.z));
                        }
                    }
                    else
                    {
                        pos = Camera.main.WorldToScreenPoint(new Vector3(hit.transform.position.x, hit.transform.position.y - _offsetYStatPlus - .5f, hit.transform.position.z));
                    }
                }
                else
                {
                    if (hit.transform.position.y >= _yOffsetMin.y)
                    {
                        if (hit.transform.position.y >= _yOffsetMax.y)
                        {
                            pos = Camera.main.WorldToScreenPoint(new Vector3(hit.transform.position.x + _offsetXStatPlus, hit.transform.position.y - _offsetYStatPlus - 1, hit.transform.position.z));
                        }
                        else if (hit.transform.position.y >= _yOffset.y && hit.transform.position.y <= _yOffsetMax.y)
                        {
                            pos = Camera.main.WorldToScreenPoint(new Vector3(hit.transform.position.x + _offsetXStatPlus, hit.transform.position.y - _offsetYStatPlus - .5f, hit.transform.position.z));
                        }
                        else
                        {
                            pos = Camera.main.WorldToScreenPoint(new Vector3(hit.transform.position.x + _offsetXStatPlus, hit.transform.position.y - _offsetYStatPlus - .5f, hit.transform.position.z));
                        }
                    }
                    else if (hit.transform.position.y <= _yOffsetMin.x)
                    {
                        if (hit.transform.position.y <= _yOffsetMax.x)
                        {
                            pos = Camera.main.WorldToScreenPoint(new Vector3(hit.transform.position.x + _offsetXStatPlus, hit.transform.position.y + _offsetYStatPlus + 1, hit.transform.position.z));
                        }
                        else if (hit.transform.position.y >= _yOffsetMax.x && hit.transform.position.y <= _yOffset.x)
                        {
                            pos = Camera.main.WorldToScreenPoint(new Vector3(hit.transform.position.x + _offsetXStatPlus, hit.transform.position.y + _offsetYStatPlus + .5f, hit.transform.position.z));
                        }
                        else
                        {
                            pos = Camera.main.WorldToScreenPoint(new Vector3(hit.transform.position.x + _offsetXStatPlus, hit.transform.position.y + _offsetYStatPlus + .5f, hit.transform.position.z));
                        }
                    }
                    else
                    {
                        pos = Camera.main.WorldToScreenPoint(new Vector3(hit.transform.position.x, hit.transform.position.y - _offsetYStatPlus - .5f, hit.transform.position.z));
                    }
                }
            }
            else
            {
                if (hit.transform.position.y >= _yOffsetMax.y)
                {
                    pos = Camera.main.WorldToScreenPoint(new Vector3(hit.transform.position.x - _offsetXStatPlus / 2, hit.transform.position.y - _offsetYStatPlus - .5f, hit.transform.position.z));
                }
                else if (hit.transform.position.y <= _yOffsetMax.x)
                {
                    pos = Camera.main.WorldToScreenPoint(new Vector3(hit.transform.position.x - _offsetXStatPlus / 2, hit.transform.position.y + _offsetYStatPlus + .5f, hit.transform.position.z));
                }
                else
                {
                    pos = Camera.main.WorldToScreenPoint(new Vector3(hit.transform.position.x - _offsetXStatPlus * 2.5f, hit.transform.position.y, hit.transform.position.z));
                }
            }
        }
        else if (switchPage)
        {
            pos = new Vector3(lastPosX, lastPosY, ShiftUI[0].transform.position.z);
        }
        else
        {
            Debug.LogError("Vous essayez de positionner un objet qui ne peut pas se positionner autour de l'unit�");
        }

        //Rendre l'�l�ment visible.
        uiElements.SetActive(true);

        //Si la position de l'UI est diff�rente de celle de la position de r�f�rence alors tu prends cette position comme r�f�rence.
        if (uiElements.transform.position != pos)
        {
            uiElements.transform.position = pos;
        }
    }
    #endregion ActivateUI

    #region ControleDesClicks
    /// <summary>
    /// Permet de d�terminer quand le joueur appuie sur le Shift puis le clic Gauche de la souris.
    /// </summary>
    public void ShiftClick()
    {
        ActivateUI(ShiftUI[0], 0, 0, false, false, false, true);
        UpdateUIStats();
    }

    /// <summary>
    /// Permet de d�terminer et d'afficher un �l�ment quand la souris passe au dessus d'une tile poss�dant une unit�.
    /// </summary>
    public void MouseOverWithoutClick()
    {
        if (GameManager.Instance.activationDone == false)
        {
            if (!_hasCheckUnit)
            {

                //Si le joueur n'a pas cliqu�, alors tu lances la coroutine.
                if (_checkIfPlayerAsClic == false)
                {
                    //Coroutine : Une coroutine est une fonction qui peut suspendre son ex�cution (yield) jusqu'� la fin de la YieldInstruction donn�e.
                    StartCoroutine(ShowObject(TimeToWait));
                    UpdateUIStats();
                    _hasCheckUnit = true;
                }

                if (_checkIfPlayerAsClic)
                {
                    MouseExitWithoutClick();
                }
            }
        }
    }

    /// <summary>
    /// Fonction pour d�sactiver en MouseOver.
    /// </summary>
    public void MouseExitWithoutClick()
    {
        if(GameManager.Instance.activationDone == false)
        {
        //Arrete l'ensemble des coroutines dans la sc�ne.
        StopAllCoroutines();
        _mouseOverUI.SetActive(false);
        _hasCheckUnit = false;

        }
    }

    /// <summary>
    /// Correspond au param�tre qu'on rentre dans la coroutine.
    /// </summary>
    /// <param name="Timer"></param>
    /// <returns></returns>
    IEnumerator ShowObject(float TimeToWait)
    {
        //J'utilise un d�lai pour que le boutton apparaisse apr�s un d�lai.
        yield return new WaitForSeconds(TimeToWait);
        //J'active l'�l�ment et je lui assigne des param�tres.
        ActivateUI(MouseOverUI, 0, 0, false, false, true);
    }
    #endregion ControleDesClicks

    #region SwitchPages
    /// <summary>
    /// Fonction qui permet de cacher les Pages 1 et 2 du carnet.
    /// </summary>
    public void QuitShiftPanel()
    {
        //Je retourne la valeur comme quoi il a click� � false car il a fini son action de Shift+Clic et d�sactive les 2 pages.
        for (int i = 0; i < 3; i++)
        {
            UIInstance.Instance.objectsAttributs[i].MainObjects.transform.GetChild(0).GetComponent<MouseOverUI>().StopOver();
        }

        _checkIfPlayerAsClic = false;
        ShiftUI[0].SetActive(false);
        ShiftUI[1].SetActive(false);

    }

    /// <summary>
    /// Permet de switch entre la page 1 et la page 2
    /// </summary>
    public void switchWindows1()
    {
        //J'active le Panneau 2 car le joueur a cliqu� sur le bouton permettant de transitionner de la page 1 � la page 2. De plus, je masque la page 1.
        ActivateUI(ShiftUI[1], ShiftUI[0].transform.position.x, ShiftUI[0].transform.position.y, true);
        ShiftUI[0].SetActive(false);
        SoundController.Instance.PlaySound(SoundController.Instance.AudioClips[13]);
    }

    /// <summary>
    /// Switch entre la page 2 et la page 1.
    /// </summary>
    public void switchWindows2()
    {
        //J'active le Panneau 1 car le joueur a cliqu� sur le bouton permettant de transitionner de la page 2 � la page 1. De plus, je masque la page 2.
        ActivateUI(ShiftUI[0], ShiftUI[1].transform.position.x, ShiftUI[1].transform.position.y, true);
        ShiftUI[1].SetActive(false);
        SoundController.Instance.PlaySound(SoundController.Instance.AudioClips[13]);
    }
    #endregion SwitchPages

    #region MenuRenfortFunction
    /// <summary>
    /// Quitte le menu renfort
    /// </summary>
    public void QuitRenfortPanel()
    {
        if (!GameManager.Instance.IsPlayerRedTurn)
        {
            GameManager.Instance.RenfortPhase.CreateTileJ2.Clear();
            GameManager.Instance.RenfortPhase.CreateLeader2.Clear();
        }
        else if (GameManager.Instance.IsPlayerRedTurn)
        {
            GameManager.Instance.RenfortPhase.CreateTileJ1.Clear();
            GameManager.Instance.RenfortPhase.CreateLeader1.Clear();
        }

        UIInstance.Instance.ButtonRenfort._clicSurUnit�1.GetComponent<CanvasGroup>().interactable = false;
        UIInstance.Instance.ButtonRenfort._clicSurUnit�1.GetComponent<CanvasGroup>().blocksRaycasts = false;
        UIInstance.Instance.ButtonRenfort._clicSurUnit�1.GetComponent<RenfortBtnUI>().HideCanvas();

        UIInstance.Instance.ButtonRenfort._clicSurUnit�2.GetComponent<CanvasGroup>().interactable = false;
        UIInstance.Instance.ButtonRenfort._clicSurUnit�2.GetComponent<CanvasGroup>().blocksRaycasts = false;
        UIInstance.Instance.ButtonRenfort._clicSurUnit�2.GetComponent<RenfortBtnUI>().HideCanvas();

        UIInstance.Instance.ButtonRenfort._clicSurUnit�3.GetComponent<CanvasGroup>().interactable = false;
        UIInstance.Instance.ButtonRenfort._clicSurUnit�3.GetComponent<CanvasGroup>().blocksRaycasts = false;
        UIInstance.Instance.ButtonRenfort._clicSurUnit�3.GetComponent<RenfortBtnUI>().HideCanvas();

        UIInstance.Instance.ButtonRenfort._clicSurUnit�4.GetComponent<CanvasGroup>().interactable = false;
        UIInstance.Instance.ButtonRenfort._clicSurUnit�4.GetComponent<CanvasGroup>().blocksRaycasts = false;
        UIInstance.Instance.ButtonRenfort._clicSurUnit�4.GetComponent<RenfortBtnUI>().HideCanvas();

        UIInstance.Instance.ButtonRenfort._clicSurUnit�5.GetComponent<CanvasGroup>().interactable = false;
        UIInstance.Instance.ButtonRenfort._clicSurUnit�5.GetComponent<CanvasGroup>().blocksRaycasts = false;
        UIInstance.Instance.ButtonRenfort._clicSurUnit�5.GetComponent<RenfortBtnUI>().HideCanvas();

        UIInstance.Instance.ButtonRenfort._clicSurUnit�6.GetComponent<CanvasGroup>().interactable = false;
        UIInstance.Instance.ButtonRenfort._clicSurUnit�6.GetComponent<CanvasGroup>().blocksRaycasts = false;
        UIInstance.Instance.ButtonRenfort._clicSurUnit�6.GetComponent<RenfortBtnUI>().HideCanvas();


        RenfortUI.SetActive(false);
        UIInstance.Instance.RenfortBlockant.SetActive(false);
      
    }

    /// <summary>
    /// Update les stats du menu renfort
    /// </summary>
    void UpdateStatsMenuRenforts(bool player)
    {
        if (player)
        {

            UIInstance.Instance.PageUnit�Renfort._ressourceJoueur.GetComponent<TextMeshProUGUI>().text = PlayerScript.Instance.RedPlayerInfos.Ressource.ToString();

            //Permet de d�terminer le nombre d'emplacements � mettre � jour sur le menu Renfort de l'Arm�e Rouge.
            for (int i = 0; i < unitReference.UnitClassCreableListRedPlayer.Count; i++)
            {
                #region UpdateTexteRenfort1a3
                //Active les diff�rents UI des unit�s de 1 � 3.


                if (i >= 0)
                {
                _elementMenuRenfort[0].SetActive(true);


                //Statistique pour l'unit�1
                UIInstance.Instance.PageUnit�Renfort._nameUnit1.GetComponent<TextMeshProUGUI>().text = unitReference.UnitClassCreableListRedPlayer[0].GetComponent<UnitScript>().UnitSO.UnitName.ToString();
                UIInstance.Instance.PageUnit�Renfort._lifeValor1.GetComponent<TextMeshProUGUI>().text = unitReference.UnitClassCreableListRedPlayer[0].GetComponent<UnitScript>().UnitSO.LifeMax.ToString();
                UIInstance.Instance.PageUnit�Renfort._rangeValor1.GetComponent<TextMeshProUGUI>().text = unitReference.UnitClassCreableListRedPlayer[0].GetComponent<UnitScript>().UnitSO.AttackRange.ToString();
                UIInstance.Instance.PageUnit�Renfort._moveValor1.GetComponent<TextMeshProUGUI>().text = unitReference.UnitClassCreableListRedPlayer[0].GetComponent<UnitScript>().UnitSO.MoveSpeed.ToString();
                UIInstance.Instance.EmplacementImageMenuRenfort._imageUnit�1.GetComponent<Image>().sprite = unitReference.UnitClassCreableListRedPlayer[0].GetComponent<UnitScript>().UnitSO.Sprite;
                if (unitReference.UnitClassCreableListRedPlayer[0].GetComponent<UnitScript>().UnitSO.CreationCost > 2)
                    {
                        UIInstance.Instance.RessourceUnit_PasTouche._unit�1Ressource[2].SetActive(true);
                        UIInstance.Instance.RessourceUnit_PasTouche._unit�1Ressource[3].SetActive(true);
                    }
                    else
                    {
                        UIInstance.Instance.RessourceUnit_PasTouche._unit�1Ressource[2].SetActive(false);
                        UIInstance.Instance.RessourceUnit_PasTouche._unit�1Ressource[3].SetActive(false);
                    }

                    UIInstance.Instance.RessourceUnit_PasTouche._unit�1Ressource[0].SetActive(true);
                    UIInstance.Instance.RessourceUnit_PasTouche._unit�1Ressource[1].SetActive(true);
                    //Image Ressource pour l'unit� 2 de l'arm�e Rouge
                    UIInstance.Instance.RessourceUnit_PasTouche._unit�2Ressource[0].SetActive(true);
                    UIInstance.Instance.RessourceUnit_PasTouche._unit�2Ressource[1].SetActive(true);

                }
                if (i >= 1)
                {
                _elementMenuRenfort[1].SetActive(true);


                //Statistique pour l'unit�2
                UIInstance.Instance.PageUnit�Renfort._nameUnit2.GetComponent<TextMeshProUGUI>().text = unitReference.UnitClassCreableListRedPlayer[1].GetComponent<UnitScript>().UnitSO.UnitName.ToString();
                UIInstance.Instance.PageUnit�Renfort._lifeValor2.GetComponent<TextMeshProUGUI>().text = unitReference.UnitClassCreableListRedPlayer[1].GetComponent<UnitScript>().UnitSO.LifeMax.ToString();
                UIInstance.Instance.PageUnit�Renfort._rangeValor2.GetComponent<TextMeshProUGUI>().text = unitReference.UnitClassCreableListRedPlayer[1].GetComponent<UnitScript>().UnitSO.AttackRange.ToString();
                UIInstance.Instance.PageUnit�Renfort._moveValor2.GetComponent<TextMeshProUGUI>().text = unitReference.UnitClassCreableListRedPlayer[1].GetComponent<UnitScript>().UnitSO.MoveSpeed.ToString();
                UIInstance.Instance.EmplacementImageMenuRenfort._imageUnit�2.GetComponent<Image>().sprite = unitReference.UnitClassCreableListRedPlayer[1].GetComponent<UnitScript>().UnitSO.Sprite;
                    if (unitReference.UnitClassCreableListRedPlayer[1].GetComponent<UnitScript>().UnitSO.CreationCost > 2)
                    {
                        UIInstance.Instance.RessourceUnit_PasTouche._unit�2Ressource[2].SetActive(true);
                        UIInstance.Instance.RessourceUnit_PasTouche._unit�2Ressource[3].SetActive(true);
                    }
                    else
                    {
                        UIInstance.Instance.RessourceUnit_PasTouche._unit�2Ressource[2].SetActive(false);
                        UIInstance.Instance.RessourceUnit_PasTouche._unit�2Ressource[3].SetActive(false);
                    }
                    //Image Ressource pour l'unit� 3 de l'arm�e Rouge
                    UIInstance.Instance.RessourceUnit_PasTouche._unit�3Ressource[0].SetActive(true);
                    UIInstance.Instance.RessourceUnit_PasTouche._unit�3Ressource[1].SetActive(true);

                }
                if (i >= 2)
                {
                    Debug.Log("fds"); 
                _elementMenuRenfort[2].SetActive(true);
                //Statistique pour l'unit�3
                UIInstance.Instance.PageUnit�Renfort._nameUnit3.GetComponent<TextMeshProUGUI>().text = unitReference.UnitClassCreableListRedPlayer[2].GetComponent<UnitScript>().UnitSO.UnitName.ToString();
                UIInstance.Instance.PageUnit�Renfort._lifeValor3.GetComponent<TextMeshProUGUI>().text = unitReference.UnitClassCreableListRedPlayer[2].GetComponent<UnitScript>().UnitSO.LifeMax.ToString();
                UIInstance.Instance.PageUnit�Renfort._rangeValor3.GetComponent<TextMeshProUGUI>().text = unitReference.UnitClassCreableListRedPlayer[2].GetComponent<UnitScript>().UnitSO.AttackRange.ToString();
                UIInstance.Instance.PageUnit�Renfort._moveValor3.GetComponent<TextMeshProUGUI>().text = unitReference.UnitClassCreableListRedPlayer[2].GetComponent<UnitScript>().UnitSO.MoveSpeed.ToString();
                UIInstance.Instance.EmplacementImageMenuRenfort._imageUnit�3.GetComponent<Image>().sprite = unitReference.UnitClassCreableListRedPlayer[2].GetComponent<UnitScript>().UnitSO.Sprite;
                    if (unitReference.UnitClassCreableListRedPlayer[2].GetComponent<UnitScript>().UnitSO.CreationCost > 2)
                    {
                        UIInstance.Instance.RessourceUnit_PasTouche._unit�3Ressource[2].SetActive(true);
                        UIInstance.Instance.RessourceUnit_PasTouche._unit�3Ressource[3].SetActive(true);
                    }
                    else
                    {
                        UIInstance.Instance.RessourceUnit_PasTouche._unit�3Ressource[2].SetActive(false);
                        UIInstance.Instance.RessourceUnit_PasTouche._unit�3Ressource[3].SetActive(false);
                    }
                    UIInstance.Instance.RessourceUnit_PasTouche._unit�3Ressource[0].SetActive(true);
                    UIInstance.Instance.RessourceUnit_PasTouche._unit�3Ressource[1].SetActive(true);
                }
                if (unitReference.UnitClassCreableListRedPlayer.Count <3)
                {
                    _elementMenuRenfort[2].SetActive(false);
                }





                #endregion UpdateTexteRenfort1a3

                #region UpdateImageRenfort1a3
                //Update Ressource en fonction du nombre.


                //Si la premi�re unit� de l'arm�e Rouge a besoin de plus de 2 ressources.

                    //Si la deuxi�me unit� de l'arm�e Rouge a besoin de plus de 2 ressources.

                    //Si la troisi�me unit� de l'arm�e Rouge a besoin de plus de 2 ressources.



                    #endregion UpdateImageRenfort1a3

                    #region Update Textuelle et Image Renforts de 4 � 6 pour l'�quipe Rouge
                    //Si la liste des unit�s cr�ables comportent plus de 3 unit�s dans la liste de l'�quipe Rouge.
                    if (i >= 3)
                    {
                        //Active l'UI de l'unit� 4 de l'arrm�e Rouge.
                        _elementMenuRenfort[3].SetActive(true);

                        //Statistique pour l'unit�4
                        UIInstance.Instance.PageUnit�Renfort._nameUnit4.GetComponent<TextMeshProUGUI>().text = unitReference.UnitClassCreableListRedPlayer[3].GetComponent<UnitScript>().UnitSO.UnitName.ToString();
                        UIInstance.Instance.PageUnit�Renfort._lifeValor4.GetComponent<TextMeshProUGUI>().text = unitReference.UnitClassCreableListRedPlayer[3].GetComponent<UnitScript>().UnitSO.LifeMax.ToString();
                        UIInstance.Instance.PageUnit�Renfort._rangeValor4.GetComponent<TextMeshProUGUI>().text = unitReference.UnitClassCreableListRedPlayer[3].GetComponent<UnitScript>().UnitSO.AttackRange.ToString();
                        UIInstance.Instance.PageUnit�Renfort._moveValor4.GetComponent<TextMeshProUGUI>().text = unitReference.UnitClassCreableListRedPlayer[3].GetComponent<UnitScript>().UnitSO.MoveSpeed.ToString();
                    UIInstance.Instance.EmplacementImageMenuRenfort._imageUnit�4.GetComponent<Image>().sprite = unitReference.UnitClassCreableListRedPlayer[3].GetComponent<UnitScript>().UnitSO.Sprite;


                    //Image Ressource pour l'unit� 4 de l'arm�e Rouge.
                    UIInstance.Instance.RessourceUnit_PasTouche._unit�4Ressource[0].SetActive(true);
                        UIInstance.Instance.RessourceUnit_PasTouche._unit�4Ressource[1].SetActive(true);

                        //Si la quatri�me unit� de l'arm�e Rouge a besoin de plus de 2 ressources.
                        if (unitReference.UnitClassCreableListRedPlayer[3].GetComponent<UnitScript>().UnitSO.CreationCost > 2)
                        {
                            UIInstance.Instance.RessourceUnit_PasTouche._unit�4Ressource[2].SetActive(true);
                            UIInstance.Instance.RessourceUnit_PasTouche._unit�4Ressource[3].SetActive(true);
                        }
                        else
                        {
                            UIInstance.Instance.RessourceUnit_PasTouche._unit�4Ressource[2].SetActive(false);
                            UIInstance.Instance.RessourceUnit_PasTouche._unit�4Ressource[3].SetActive(false);
                        }

                        //Si la liste des unit�s cr�ables comportent plus de 4 unit�s dans la liste de l'�quipe Rouge.
                        if (i >= 4)
                        {
                            //Active l'UI de l'unit� 5 de l'arrm�e Rouge.
                            _elementMenuRenfort[4].SetActive(true);

                            //Statistique pour l'unit� 5 de l'arm�e Rouge.
                            UIInstance.Instance.PageUnit�Renfort._nameUnit5.GetComponent<TextMeshProUGUI>().text = unitReference.UnitClassCreableListRedPlayer[4].GetComponent<UnitScript>().UnitSO.UnitName.ToString();
                            UIInstance.Instance.PageUnit�Renfort._lifeValor5.GetComponent<TextMeshProUGUI>().text = unitReference.UnitClassCreableListRedPlayer[4].GetComponent<UnitScript>().UnitSO.LifeMax.ToString();
                            UIInstance.Instance.PageUnit�Renfort._rangeValor5.GetComponent<TextMeshProUGUI>().text = unitReference.UnitClassCreableListRedPlayer[4].GetComponent<UnitScript>().UnitSO.AttackRange.ToString();
                            UIInstance.Instance.PageUnit�Renfort._moveValor5.GetComponent<TextMeshProUGUI>().text = unitReference.UnitClassCreableListRedPlayer[4].GetComponent<UnitScript>().UnitSO.MoveSpeed.ToString();
                        UIInstance.Instance.EmplacementImageMenuRenfort._imageUnit�5.GetComponent<Image>().sprite = unitReference.UnitClassCreableListRedPlayer[4].GetComponent<UnitScript>().UnitSO.Sprite;


                        //Image Ressource pour l'unit� 5 de l'arm�e Rouge.
                        UIInstance.Instance.RessourceUnit_PasTouche._unit�5Ressource[0].SetActive(true);
                            UIInstance.Instance.RessourceUnit_PasTouche._unit�5Ressource[1].SetActive(true);

                            //Si la cinqui�me unit� de l'arm�e Rouge a besoin de plus de 2 ressources.
                            if (unitReference.UnitClassCreableListRedPlayer[4].GetComponent<UnitScript>().UnitSO.CreationCost > 2)
                            {
                                UIInstance.Instance.RessourceUnit_PasTouche._unit�5Ressource[2].SetActive(true);
                                UIInstance.Instance.RessourceUnit_PasTouche._unit�5Ressource[3].SetActive(true);
                            }
                            else
                            {
                                UIInstance.Instance.RessourceUnit_PasTouche._unit�5Ressource[2].SetActive(false);
                                UIInstance.Instance.RessourceUnit_PasTouche._unit�5Ressource[3].SetActive(false);
                            }

                            //Si la liste des unit�s cr�ables comportent plus de 5 unit�s dans la liste de l'�quipe Rouge.
                            if (i >= 5)
                            {
                                //Active l'UI de l'unit� 6 de l'arrm�e Rouge.
                                _elementMenuRenfort[5].SetActive(true);

                                //Statistique pour l'unit�6 de l'arm�e Rouge.
                                UIInstance.Instance.PageUnit�Renfort._nameUnit6.GetComponent<TextMeshProUGUI>().text = unitReference.UnitClassCreableListRedPlayer[5].GetComponent<UnitScript>().UnitSO.UnitName.ToString();
                                UIInstance.Instance.PageUnit�Renfort._lifeValor6.GetComponent<TextMeshProUGUI>().text = unitReference.UnitClassCreableListRedPlayer[5].GetComponent<UnitScript>().UnitSO.LifeMax.ToString();
                                UIInstance.Instance.PageUnit�Renfort._rangeValor6.GetComponent<TextMeshProUGUI>().text = unitReference.UnitClassCreableListRedPlayer[5].GetComponent<UnitScript>().UnitSO.AttackRange.ToString();
                                UIInstance.Instance.PageUnit�Renfort._moveValor6.GetComponent<TextMeshProUGUI>().text = unitReference.UnitClassCreableListRedPlayer[5].GetComponent<UnitScript>().UnitSO.MoveSpeed.ToString();
                            UIInstance.Instance.EmplacementImageMenuRenfort._imageUnit�6.GetComponent<Image>().sprite = unitReference.UnitClassCreableListRedPlayer[5].GetComponent<UnitScript>().UnitSO.Sprite;


                            //Image Ressource pour l'unit�6 de l'arm�e Rouge.
                            UIInstance.Instance.RessourceUnit_PasTouche._unit�6Ressource[0].SetActive(true);
                                UIInstance.Instance.RessourceUnit_PasTouche._unit�6Ressource[1].SetActive(true);

                                //Si la sixi�me unit� de l'arm�e Rouge a besoin de plus de 2 ressources.
                                if (unitReference.UnitClassCreableListRedPlayer[5].GetComponent<UnitScript>().UnitSO.CreationCost > 2)
                                {
                                    UIInstance.Instance.RessourceUnit_PasTouche._unit�6Ressource[2].SetActive(true);
                                    UIInstance.Instance.RessourceUnit_PasTouche._unit�6Ressource[3].SetActive(true);
                                }
                                else
                                {
                                    UIInstance.Instance.RessourceUnit_PasTouche._unit�6Ressource[2].SetActive(false);
                                    UIInstance.Instance.RessourceUnit_PasTouche._unit�6Ressource[3].SetActive(false);
                                }

                            }
                            else
                            {
                                _elementMenuRenfort[5].SetActive(false);
                            }
                        }
                        else
                        {
                            _elementMenuRenfort[4].SetActive(false);
                            _elementMenuRenfort[5].SetActive(false);
                        }
                    }
                    else
                    {
                        _elementMenuRenfort[3].SetActive(false);
                        _elementMenuRenfort[4].SetActive(false);
                        _elementMenuRenfort[5].SetActive(false);
                    }
                #endregion Update Textuelle et Image Renforts de 4 � 6 pour l'�quipe Rouge

                if (unitReference.UnitClassCreableListBluePlayer.Count < 3)
                {
                    _elementMenuRenfort[2].SetActive(false);
                }
            }
        }

        else if (!player)
        {

            UIInstance.Instance.PageUnit�Renfort._ressourceJoueur.GetComponent<TextMeshProUGUI>().text = PlayerScript.Instance.BluePlayerInfos.Ressource.ToString();
            //Permet de d�terminer le nombre d'emplacements � mettre � jour sur le menu Renfort de l'Arm�e Bleu.
            for (int i = 0; i < unitReference.UnitClassCreableListBluePlayer.Count; i++)
            {

                #region Update Textuelle et Image Renforts de 1 � 3 pour l'�quipe Bleu
                #region Update Textuelle Renforts de 1 � 3 pour l'�quipe Bleu
                if (i >= 0)
                {
                _elementMenuRenfort[0].SetActive(true);

                //Active les diff�rents UI des unit�s 1 � 3 de l'arm�e Bleu.

                //Statistique pour l'unit� 1 de l'arm�e Bleu.
                UIInstance.Instance.PageUnit�Renfort._nameUnit1.GetComponent<TextMeshProUGUI>().text = unitReference.UnitClassCreableListBluePlayer[0].GetComponent<UnitScript>().UnitSO.UnitName.ToString();
                UIInstance.Instance.PageUnit�Renfort._lifeValor1.GetComponent<TextMeshProUGUI>().text = unitReference.UnitClassCreableListBluePlayer[0].GetComponent<UnitScript>().UnitSO.LifeMax.ToString();
                UIInstance.Instance.PageUnit�Renfort._rangeValor1.GetComponent<TextMeshProUGUI>().text = unitReference.UnitClassCreableListBluePlayer[0].GetComponent<UnitScript>().UnitSO.AttackRange.ToString();
                UIInstance.Instance.PageUnit�Renfort._moveValor1.GetComponent<TextMeshProUGUI>().text = unitReference.UnitClassCreableListBluePlayer[0].GetComponent<UnitScript>().UnitSO.MoveSpeed.ToString();
                UIInstance.Instance.EmplacementImageMenuRenfort._imageUnit�1.GetComponent<Image>().sprite = unitReference.UnitClassCreableListBluePlayer[0].GetComponent<UnitScript>().UnitSO.Sprite;
                UIInstance.Instance.RessourceUnit_PasTouche._unit�1Ressource[0].SetActive(true);
                UIInstance.Instance.RessourceUnit_PasTouche._unit�1Ressource[1].SetActive(true);

                //Si la premi�re unit� de l'arm�e Bleu a besoin de plus de 2 ressources.
                if (unitReference.UnitClassCreableListBluePlayer[0].GetComponent<UnitScript>().UnitSO.CreationCost > 2)
                {
                    UIInstance.Instance.RessourceUnit_PasTouche._unit�1Ressource[2].SetActive(true);
                    UIInstance.Instance.RessourceUnit_PasTouche._unit�1Ressource[3].SetActive(true);
                }
                else
                {
                    UIInstance.Instance.RessourceUnit_PasTouche._unit�1Ressource[2].SetActive(false);
                    UIInstance.Instance.RessourceUnit_PasTouche._unit�1Ressource[3].SetActive(false);
                }

                }
                if (i >= 1)
                {
                //Statistique pour l'unit� 2 de l'arm�e Bleu.
                _elementMenuRenfort[1].SetActive(true);

                UIInstance.Instance.PageUnit�Renfort._nameUnit2.GetComponent<TextMeshProUGUI>().text = unitReference.UnitClassCreableListBluePlayer[1].GetComponent<UnitScript>().UnitSO.UnitName.ToString();
                UIInstance.Instance.PageUnit�Renfort._lifeValor2.GetComponent<TextMeshProUGUI>().text = unitReference.UnitClassCreableListBluePlayer[1].GetComponent<UnitScript>().UnitSO.LifeMax.ToString();
                UIInstance.Instance.PageUnit�Renfort._rangeValor2.GetComponent<TextMeshProUGUI>().text = unitReference.UnitClassCreableListBluePlayer[1].GetComponent<UnitScript>().UnitSO.AttackRange.ToString();
                UIInstance.Instance.PageUnit�Renfort._moveValor2.GetComponent<TextMeshProUGUI>().text = unitReference.UnitClassCreableListBluePlayer[1].GetComponent<UnitScript>().UnitSO.MoveSpeed.ToString();
                UIInstance.Instance.EmplacementImageMenuRenfort._imageUnit�2.GetComponent<Image>().sprite = unitReference.UnitClassCreableListBluePlayer[1].GetComponent<UnitScript>().UnitSO.Sprite;
                UIInstance.Instance.RessourceUnit_PasTouche._unit�2Ressource[0].SetActive(true);
                UIInstance.Instance.RessourceUnit_PasTouche._unit�2Ressource[1].SetActive(true);

                //Si la deuxi�me unit� de l'arm�e Bleu a besoin de plus de 2 ressources.
                if (unitReference.UnitClassCreableListBluePlayer[1].GetComponent<UnitScript>().UnitSO.CreationCost > 2)
                {
                    UIInstance.Instance.RessourceUnit_PasTouche._unit�2Ressource[2].SetActive(true);
                    UIInstance.Instance.RessourceUnit_PasTouche._unit�2Ressource[3].SetActive(true);
                }
                else
                {
                    UIInstance.Instance.RessourceUnit_PasTouche._unit�2Ressource[2].SetActive(false);
                    UIInstance.Instance.RessourceUnit_PasTouche._unit�2Ressource[3].SetActive(false);
                }
                }
                if (i >= 2)
                {
                _elementMenuRenfort[2].SetActive(true);

                //Statistique pour l'unit� 3 de l'arm�e Bleu.
                UIInstance.Instance.PageUnit�Renfort._nameUnit3.GetComponent<TextMeshProUGUI>().text = unitReference.UnitClassCreableListBluePlayer[2].GetComponent<UnitScript>().UnitSO.UnitName.ToString();
                UIInstance.Instance.PageUnit�Renfort._lifeValor3.GetComponent<TextMeshProUGUI>().text = unitReference.UnitClassCreableListBluePlayer[2].GetComponent<UnitScript>().UnitSO.LifeMax.ToString();
                UIInstance.Instance.PageUnit�Renfort._rangeValor3.GetComponent<TextMeshProUGUI>().text = unitReference.UnitClassCreableListBluePlayer[2].GetComponent<UnitScript>().UnitSO.AttackRange.ToString();
                UIInstance.Instance.PageUnit�Renfort._moveValor3.GetComponent<TextMeshProUGUI>().text = unitReference.UnitClassCreableListBluePlayer[2].GetComponent<UnitScript>().UnitSO.MoveSpeed.ToString();
                UIInstance.Instance.EmplacementImageMenuRenfort._imageUnit�3.GetComponent<Image>().sprite = unitReference.UnitClassCreableListBluePlayer[2].GetComponent<UnitScript>().UnitSO.Sprite;
                //Image Ressource pour l'unit� 3 de l'arm�e Bleu.
                UIInstance.Instance.RessourceUnit_PasTouche._unit�3Ressource[0].SetActive(true);
                UIInstance.Instance.RessourceUnit_PasTouche._unit�3Ressource[1].SetActive(true);
                //Image Ressource pour l'unit� 2 de l'arm�e Bleu.


                //Si la troisi�me unit� de l'arm�e Bleu a besoin de plus de 2 ressources.
                if (unitReference.UnitClassCreableListBluePlayer[2].GetComponent<UnitScript>().UnitSO.CreationCost > 2)
                {
                    UIInstance.Instance.RessourceUnit_PasTouche._unit�3Ressource[2].SetActive(true);
                    UIInstance.Instance.RessourceUnit_PasTouche._unit�3Ressource[3].SetActive(true);
                }
                else
                {
                    UIInstance.Instance.RessourceUnit_PasTouche._unit�3Ressource[2].SetActive(false);
                    UIInstance.Instance.RessourceUnit_PasTouche._unit�3Ressource[3].SetActive(false);
                }
                }








                #endregion Update Textuelle Renforts de 1 � 3 pour l'�quipe Bleu

                #region Update Image Renforts de 1 � 3 pour l'�quipe Bleu
                //Update Ressource en fonction du nombre.
                //Image Ressource pour l'unit� 1 de l'arm�e Bleu.



                #endregion Update Image Renforts de 1 � 3 pour l'�quipe Bleu
                #endregion Update Textuelle et Image Renforts de 1 � 3 pour l'�quipe Bleu

                #region Update Image Textuelle et Image de 4 � 6 pour l'�quipe Bleu
                //Update Ressource en fonction du nombre.
                //Si la liste des unit�s cr�ables comportent plus de 3 unit�s dans la liste de l'�quipe Rouge.
                if (i >= 3)
                {
                    //Active l'UI de l'unit� 4 (oui 4 car dans une liste, le 0 est pris en compte comme l'emplacement 1).
                    _elementMenuRenfort[3].SetActive(true);

                    //Statistique pour l'unit� 4 de l'arm�e Bleu.
                    UIInstance.Instance.PageUnit�Renfort._nameUnit4.GetComponent<TextMeshProUGUI>().text = unitReference.UnitClassCreableListBluePlayer[3].GetComponent<UnitScript>().UnitSO.UnitName.ToString();
                    UIInstance.Instance.PageUnit�Renfort._lifeValor4.GetComponent<TextMeshProUGUI>().text = unitReference.UnitClassCreableListBluePlayer[3].GetComponent<UnitScript>().UnitSO.LifeMax.ToString();
                    UIInstance.Instance.PageUnit�Renfort._rangeValor4.GetComponent<TextMeshProUGUI>().text = unitReference.UnitClassCreableListBluePlayer[3].GetComponent<UnitScript>().UnitSO.AttackRange.ToString();
                    UIInstance.Instance.PageUnit�Renfort._moveValor4.GetComponent<TextMeshProUGUI>().text = unitReference.UnitClassCreableListBluePlayer[3].GetComponent<UnitScript>().UnitSO.MoveSpeed.ToString();
                    UIInstance.Instance.EmplacementImageMenuRenfort._imageUnit�4.GetComponent<Image>().sprite = unitReference.UnitClassCreableListBluePlayer[3].GetComponent<UnitScript>().UnitSO.Sprite;

                    //Image Ressource pour l'unit� 4 de l'arm�e Bleu.
                    UIInstance.Instance.RessourceUnit_PasTouche._unit�4Ressource[0].SetActive(true);
                    UIInstance.Instance.RessourceUnit_PasTouche._unit�4Ressource[1].SetActive(true);

                    //Si la quatri�me unit� de l'arm�e Bleu a besoin de plus de 2 ressources.
                    if (unitReference.UnitClassCreableListBluePlayer[3].GetComponent<UnitScript>().UnitSO.CreationCost > 2)
                    {
                        UIInstance.Instance.RessourceUnit_PasTouche._unit�4Ressource[2].SetActive(true);
                        UIInstance.Instance.RessourceUnit_PasTouche._unit�4Ressource[3].SetActive(true);
                    }
                    else
                    {
                        UIInstance.Instance.RessourceUnit_PasTouche._unit�4Ressource[2].SetActive(false);
                        UIInstance.Instance.RessourceUnit_PasTouche._unit�4Ressource[3].SetActive(false);
                    }

                    //Si la liste des unit�s cr�ables comportent plus de 4 unit�s dans la liste de l'�quipe Bleu.
                    if (i >= 4)
                    {
                        //Active l'UI de l'unit� 5 (oui 5 car dans une liste, le 0 est pris en compte comme l'emplacement 1).
                        _elementMenuRenfort[4].SetActive(true);

                        //Statistique pour l'unit�5
                        UIInstance.Instance.PageUnit�Renfort._nameUnit5.GetComponent<TextMeshProUGUI>().text = unitReference.UnitClassCreableListBluePlayer[4].GetComponent<UnitScript>().UnitSO.UnitName.ToString();
                        UIInstance.Instance.PageUnit�Renfort._lifeValor5.GetComponent<TextMeshProUGUI>().text = unitReference.UnitClassCreableListBluePlayer[4].GetComponent<UnitScript>().UnitSO.LifeMax.ToString();
                        UIInstance.Instance.PageUnit�Renfort._rangeValor5.GetComponent<TextMeshProUGUI>().text = unitReference.UnitClassCreableListBluePlayer[4].GetComponent<UnitScript>().UnitSO.AttackRange.ToString();
                        UIInstance.Instance.PageUnit�Renfort._moveValor5.GetComponent<TextMeshProUGUI>().text = unitReference.UnitClassCreableListBluePlayer[4].GetComponent<UnitScript>().UnitSO.MoveSpeed.ToString();
                        UIInstance.Instance.EmplacementImageMenuRenfort._imageUnit�5.GetComponent<Image>().sprite = unitReference.UnitClassCreableListBluePlayer[4].GetComponent<UnitScript>().UnitSO.Sprite;


                        //Image Ressource pour l'unit� 5 de l'arm�e Bleu.
                        UIInstance.Instance.RessourceUnit_PasTouche._unit�5Ressource[0].SetActive(true);
                        UIInstance.Instance.RessourceUnit_PasTouche._unit�5Ressource[1].SetActive(true);

                        //Si la cinqui�me unit� de l'arm�e Bleu a besoin de plus de 2 ressources.
                        if (unitReference.UnitClassCreableListBluePlayer[4].GetComponent<UnitScript>().UnitSO.CreationCost > 2)
                        {
                            UIInstance.Instance.RessourceUnit_PasTouche._unit�5Ressource[2].SetActive(true);
                            UIInstance.Instance.RessourceUnit_PasTouche._unit�5Ressource[3].SetActive(true);
                        }
                        else
                        {
                            UIInstance.Instance.RessourceUnit_PasTouche._unit�5Ressource[2].SetActive(false);
                            UIInstance.Instance.RessourceUnit_PasTouche._unit�5Ressource[3].SetActive(false);
                        }

                        //Si la liste des unit�s cr�ables comportent plus de 5 unit�s dans la liste de l'�quipe Bleu.
                        if (i >= 5)
                        {
                            //Active l'UI de l'unit� 6 (oui 6 car dans une liste, le 0 est pris en compte comme l'emplacement 1).
                            _elementMenuRenfort[5].SetActive(true);

                            //Statistique pour l'unit�6
                            UIInstance.Instance.PageUnit�Renfort._nameUnit6.GetComponent<TextMeshProUGUI>().text = unitReference.UnitClassCreableListBluePlayer[5].GetComponent<UnitScript>().UnitSO.UnitName.ToString();
                            UIInstance.Instance.PageUnit�Renfort._lifeValor6.GetComponent<TextMeshProUGUI>().text = unitReference.UnitClassCreableListBluePlayer[5].GetComponent<UnitScript>().UnitSO.LifeMax.ToString();
                            UIInstance.Instance.PageUnit�Renfort._rangeValor6.GetComponent<TextMeshProUGUI>().text = unitReference.UnitClassCreableListBluePlayer[5].GetComponent<UnitScript>().UnitSO.AttackRange.ToString();
                            UIInstance.Instance.PageUnit�Renfort._moveValor6.GetComponent<TextMeshProUGUI>().text = unitReference.UnitClassCreableListBluePlayer[5].GetComponent<UnitScript>().UnitSO.MoveSpeed.ToString();
                            UIInstance.Instance.EmplacementImageMenuRenfort._imageUnit�6.GetComponent<Image>().sprite = unitReference.UnitClassCreableListBluePlayer[5].GetComponent<UnitScript>().UnitSO.Sprite;


                            //Image Ressource pour l'unit� 6 de l'arm�e Bleu.
                            UIInstance.Instance.RessourceUnit_PasTouche._unit�6Ressource[0].SetActive(true);
                            UIInstance.Instance.RessourceUnit_PasTouche._unit�6Ressource[1].SetActive(true);

                            //Si la sixi�me unit� de l'arm�e Bleu a besoin de plus de 2 ressources.
                            if (unitReference.UnitClassCreableListBluePlayer[5].GetComponent<UnitScript>().UnitSO.CreationCost > 2)
                            {
                                UIInstance.Instance.RessourceUnit_PasTouche._unit�6Ressource[2].SetActive(true);
                                UIInstance.Instance.RessourceUnit_PasTouche._unit�6Ressource[3].SetActive(true);
                            }
                            else
                            {
                                UIInstance.Instance.RessourceUnit_PasTouche._unit�6Ressource[2].SetActive(false);
                                UIInstance.Instance.RessourceUnit_PasTouche._unit�6Ressource[3].SetActive(false);
                            }
                        }
                        else
                        {
                            _elementMenuRenfort[5].SetActive(false);
                        }
                    }
                    else
                    {
                        _elementMenuRenfort[4].SetActive(false);
                        _elementMenuRenfort[5].SetActive(false);
                    }
                  
                }
                else
                {
                    _elementMenuRenfort[3].SetActive(false);
                    _elementMenuRenfort[4].SetActive(false);
                    _elementMenuRenfort[5].SetActive(false);
                }

                #endregion Update Image Textuelle et Image de 4 � 6 pour l'�quipe Bleu
            }

        }
    }

    /// <summary>
    /// Actives le menu renfort
    /// </summary>
    public void MenuRenfortUI(bool player)
    {
        RenfortUI.SetActive(true);
        UIInstance.Instance.RenfortBlockant.SetActive(true);
        UpdateStatsMenuRenforts(player);
    }
    #endregion MenuRenfortFunction
}

[System.Serializable]
public class UnitIcon
{
    public Sprite infanterieSprite;
    public Sprite ArtillerieSprite;
    public Sprite VehiculeSprite;
    public Sprite LeaderSprite;
    public Sprite MytheSprite;
    public Sprite MechaSprite;
}