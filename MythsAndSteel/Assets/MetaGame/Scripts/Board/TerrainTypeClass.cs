using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[CreateAssetMenu(menuName = "META/Terrain Scriptable")]
public class TerrainTypeClass : ScriptableObject
{
    [SerializeField] private TerrainType[] _EffetDeTerrain;
    public TerrainType[] EffetDeTerrain
    {
        get
        {
            return _EffetDeTerrain;
        }
    }

    /// <summary>
    /// Update le panneau d'effet de terrain avec l'effet principal sur la case
    /// </summary>
    /// <param name="tile"></param>
    /// <param name="Title"></param>
    /// <param name="Desc"></param>
    /// <param name="Ressources"></param>
    /// <param name="Rendu"></param>
    public void Synch(TileScript tile, TextMeshProUGUI Title, TextMeshProUGUI Desc, TextMeshProUGUI Ressources, Image Rendu)
    {
        TerrainType Saved = FindEffect(tile.TerrainEffectList[0]);
        Title.text = Saved._terrainName;
        Desc.text = Saved._description;
        Rendu.sprite = Saved.render;
        if (tile.TerrainEffectList.Contains(MYthsAndSteel_Enum.TerrainType.Point_de_ressource))
        {
            Ressources.text = tile.ResourcesCounter <= 0 ?  "" : "x" + tile.ResourcesCounter;
        }
        else { Ressources.text = ""; }
    }

    public GameObject ReturnInfo(GameObject PrefabEffetDeTerrain, MYthsAndSteel_Enum.TerrainType TR)
    {
        TerrainType Saved = FindEffect(TR);
        if(Saved != null)
        {
            if(Saved.render != null)
            {
                PrefabEffetDeTerrain.transform.GetChild(0).GetComponent<Image>().sprite = Saved.render;
            }
            PrefabEffetDeTerrain.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = Saved._terrainName;
            PrefabEffetDeTerrain.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = Saved._description;
            return PrefabEffetDeTerrain;
        }
        return null;
    }

    protected TerrainType FindEffect(MYthsAndSteel_Enum.TerrainType Type)
    {
        foreach(TerrainType tr in _EffetDeTerrain)
        {
            foreach (MYthsAndSteel_Enum.TerrainType TypeTerrain in tr._eventType)
            {
                if (TypeTerrain == Type)
                {
                    return tr;
                }
            }
        }
        return null;
    }
}

/// <summary>
/// Class qui regroupe toutes les variables pour une tile.
/// </summary>
[System.Serializable]
public class TerrainType 
{
    public string _terrainName = "";
    [TextArea] public string _description = "";
    public List<MYthsAndSteel_Enum.TerrainType> _eventType = new List<MYthsAndSteel_Enum.TerrainType>();
    public Sprite render;
    public bool MustBeInstantiate = true;
    public GameObject Child;
}
