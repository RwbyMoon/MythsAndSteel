using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuerillaUrbaine : Capacity
{
    public override void StartCpty()
    {
        int ressourcePlayer = GetComponent<UnitScript>().UnitSO.IsInJ1Army ? PlayerScript.Instance.J1Infos.Ressource : PlayerScript.Instance.J2Infos.Ressource;
        if (ressourcePlayer >= Capacity1Cost)
        {
            List<GameObject> tile = new List<GameObject>();
            foreach (GameObject gam in TilesManager.Instance.TileList)
            {
                if ((gam.GetComponent<TileScript>().TerrainEffectList.Contains(MYthsAndSteel_Enum.TerrainType.Maison) || gam.GetComponent<TileScript>().TerrainEffectList.Contains(MYthsAndSteel_Enum.TerrainType.Ruines)) && gam.GetComponent<TileScript>().Unit == null)
                {
                    tile.Add(gam);
                }
            }
            GameManager.Instance._eventCall += EndCpty;
            GameManager.Instance._eventCallCancel += StopCpty;
            GameManager.Instance.StartEventModeTiles(1, GetComponent<UnitScript>().UnitSO.IsInJ1Army, tile, "Guérilla Urbaine", "Voulez-vous vraiment appeler une infanterie sur cette case?");
        }
        base.StartCpty();
    }

    public override void StopCpty()
    {
        GameManager.Instance.StopEventModeTile();
        GameManager.Instance.TileChooseList.Clear();
        GetComponent<UnitScript>().StopCapacity(true);
        base.StopCpty();
    }

    public override void EndCpty()
    {
        if (GetComponent<UnitScript>().UnitSO.IsInJ1Army)
        {
            PlayerScript.Instance.J1Infos.Ressource -= Capacity1Cost;
            GameObject obj = Instantiate(PlayerScript.Instance.UnitRef.UnitClassCreableListRedPlayer[1], GameManager.Instance.TileChooseList[0].transform.position, Quaternion.identity);
            GameManager.Instance.TileChooseList[0].GetComponent<TileScript>().AddUnitToTile(obj);
            PlayerScript.Instance.UnitRef.UnitListRedPlayer.Add(obj);
        }
        else
        {
            PlayerScript.Instance.J2Infos.Ressource -= Capacity1Cost;
            GameObject obj = Instantiate(PlayerScript.Instance.UnitRef.UnitClassCreableListBluePlayer[1], GameManager.Instance.TileChooseList[0].transform.position, Quaternion.identity);
            GameManager.Instance.TileChooseList[0].GetComponent<TileScript>().AddUnitToTile(obj);
            PlayerScript.Instance.UnitRef.UnitListBluePlayer.Add(obj);
        }
        GameManager.Instance.TileChooseList.Clear();
        SoundController.Instance.PlaySound(SoundController.Instance.AudioClips[8]);
        GameManager.Instance._eventCall -= EndCpty;
        base.EndCpty();
        GetComponent<UnitScript>().EndCapacity();
    }
}
