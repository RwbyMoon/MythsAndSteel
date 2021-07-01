using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireGestion : MonoBehaviour
{
    [SerializeField] public List<Fire> FireActive = new List<Fire>();
    [SerializeField] public int BrasierDamage = 2;
    [SerializeField] public int FireDamage = 1;


    /// <summary>
    /// create a fire on a tile.
    /// </summary>
    /// <param name="tileId"></param>
    public void CreateFire(int tileId)
    {
        foreach (TerrainType T in GameManager.Instance.Terrain.EffetDeTerrain)
        {
            foreach (MYthsAndSteel_Enum.TerrainType T1 in T._eventType)
            {
                if (T1 == MYthsAndSteel_Enum.TerrainType.Brasier)
                {
                    if (!TilesManager.Instance.TileList[tileId].GetComponent<TileScript>().TerrainEffectList.Contains(MYthsAndSteel_Enum.TerrainType.Brasier) && !TilesManager.Instance.TileList[tileId].GetComponent<TileScript>().TerrainEffectList.Contains(MYthsAndSteel_Enum.TerrainType.Feu))
                    {
                        GameObject Child = Instantiate(T.Child, transform.position, Quaternion.identity);
                        Child.transform.parent = TilesManager.Instance.TileList[tileId].GetComponent<TileScript>().transform;
                        Child.transform.localScale = new Vector3(.5f, .5f, .5f);
                        TilesManager.Instance.TileList[tileId].GetComponent<TileScript>()._Child.Add(Child);
                        Child.GetComponentInChildren<Fire>().FireG = this;
                        FireActive.Add(Child.GetComponentInChildren<Fire>());
                        Child.transform.localPosition = Vector3.zero;
                        TilesManager.Instance.TileList[tileId].GetComponent<TileScript>().TerrainEffectList.Add(MYthsAndSteel_Enum.TerrainType.Brasier);
                    }
                    else
                    {
                        TilesManager.Instance.TileList[tileId].GetComponent<TileScript>().RemoveEffect(MYthsAndSteel_Enum.TerrainType.Brasier);
                        TilesManager.Instance.TileList[tileId].GetComponent<TileScript>().RemoveEffect(MYthsAndSteel_Enum.TerrainType.Feu);
                        CreateFire(tileId);
                    }
                }
            }
        }
    }

    public void CreateFireAll()
    {
        foreach (GameObject t in TilesManager.Instance.TileList)
        {
            CreateFire(t.GetComponent<TileScript>().TileId);
        }
    }


    public void Turn()
    {
        foreach (Fire f in FireActive)
        {
            f.TurnLeft--;
        }
    }

    public void RandomFire(int Number)
    {
        for (int p = 0; p <= Number; p++)
        {
            int w = Random.Range(1, 80);
            CreateFire(w);
        }
    }
}