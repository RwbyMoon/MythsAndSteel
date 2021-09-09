using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CabaneIsatabu : MonoBehaviour
{
    bool LaunchSpawn;
    int tileID;
    public GameObject IsatabuBleu;
    public GameObject IsatabuRouge;
    private void Start()
    {
        tileID = GetComponent<TileScript>().TileId;
    }
    void Update()
    {
        if(GetComponent<TileScript>().Unit != null)
        {
            if (GetComponent<TileScript>().Unit.GetComponent<UnitScript>().HasAttacked && !LaunchSpawn)
            {
                LaunchSpawn = true;
                List<GameObject> EmptyTile = new List<GameObject>();
                foreach (int i in PlayerStatic.GetNeighbourDiag(tileID, GetComponent<TileScript>().Line, false))
                {
                    if(TilesManager.Instance.TileList[i].GetComponent<TileScript>().Unit == null)
                    {
                        EmptyTile.Add(TilesManager.Instance.TileList[i]);
                    }
                }
                if(EmptyTile.Count > 0)
                {
                    int TileChoice;
                    TileChoice = (Random.Range(0, EmptyTile.Count));
                    if (GetComponent<TileScript>().Unit.GetComponent<UnitScript>().UnitSO.IsInRedArmy)
                    {
                        GameObject obj = Instantiate(IsatabuBleu, EmptyTile[TileChoice].transform.position, Quaternion.identity);
                        obj.GetComponent<UnitScript>().UnitSO.IsInRedArmy = false;
                        EmptyTile[TileChoice].GetComponent<TileScript>().AddUnitToTile(obj);
                        PlayerScript.Instance.UnitRef.UnitListBluePlayer.Add(obj);
                    }
                    else
                    {
                        GameObject obj = Instantiate(IsatabuRouge, EmptyTile[TileChoice].transform.position, Quaternion.identity);
                        obj.GetComponent<UnitScript>().UnitSO.IsInRedArmy = true;
                        EmptyTile[TileChoice].GetComponent<TileScript>().AddUnitToTile(obj);
                        PlayerScript.Instance.UnitRef.UnitListRedPlayer.Add(obj);
                    }
                    EmptyTile.Clear();
                    StartCoroutine(ResetLaunch());
                }
            }
        }
    }
    IEnumerator ResetLaunch()
    {
        yield return new WaitForSeconds(3);
        LaunchSpawn = false;
    }
}
