using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilesManager : MonoSingleton<TilesManager>
{
    //List des tiles sur le plateau (rangée dans l'ordre)
    [Tooltip("NE TOUCHEZ A RIEN SI VOUS INVERSEZ L'ORDRE DES TILES DANS LA LISTE CA NE MARCHERA PLUS!")]
    [SerializeField] private List<GameObject> _tileList;
    public List<GameObject> TileList => _tileList;

    [SerializeField] private List<GameObject> _resourcesList = new List<GameObject>();
    public List<GameObject> ResourcesList => _resourcesList;


    private void Start() 
    { 
        foreach(GameObject gam in _tileList)
        {
            if(gam.GetComponent<TileScript>().TerrainEffectList.Contains(MYthsAndSteel_Enum.TerrainType.Point_de_ressource))
            {
                gam.GetComponent<TileScript>().CreateEffect(MYthsAndSteel_Enum.TerrainType.Point_de_ressource);
                _resourcesList.Add(gam);
            }
        }
    }
}



