using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Charbonnage : Capacity
{
    //Cette capacit� appartient � l'unit� "Khodok" de l'arm�e Sovi�tique sur le plateau de Stalingrad
    public int NbUse = 0;
    public AudioClip SpeedUp;
    AudioSource audioSource;

    public override void StartCpty()
    {
        audioSource = GetComponent<AudioSource>();
        GetComponent<UnitScript>().IsActifNotConsumeAction = true;
        int tileId = RaycastManager.Instance.ActualUnitSelected.GetComponent<UnitScript>().ActualTiledId;
        List<GameObject> tile = new List<GameObject>();

        int ressourcePlayer = GetComponent<UnitScript>().UnitSO.IsInJ1Army ? PlayerScript.Instance.J1Infos.Ressource : PlayerScript.Instance.J2Infos.Ressource;
        if(ressourcePlayer >= Capacity1Cost && NbUse < 2 && GetComponent<UnitScript>().ActifUsedThisTurn == false)
        {
            tile.Add(TilesManager.Instance.TileList[GetComponent<UnitScript>().ActualTiledId]);

            GameManager.Instance._eventCall += EndCpty;
            GameManager.Instance._eventCallCancel += StopCpty;
            GameManager.Instance.StartEventModeTiles(1, GetComponent<UnitScript>().UnitSO.IsInJ1Army, tile, "Charbonnage", "Voulez-vous vraiment utiliser cette capacit�?");
        }
        base.StartCpty();
    }
    public override void StopCpty()
    {
        GetComponent<UnitScript>().IsActifNotConsumeAction = false;
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
        }
        else
        {
            PlayerScript.Instance.J2Infos.Ressource -= Capacity1Cost;
        }
        audioSource.PlayOneShot(SpeedUp, 1f);
        GetComponent<UnitScript>().ActifUsedThisTurn = true;
        NbUse++;
        GetComponent<UnitScript>().PermaSpeedBoost++;
        GetComponent<UnitScript>()._MoveSpeedBonus++;
        GetComponent<Animator>().SetInteger("Level", NbUse);
        GameManager.Instance._eventCall -= EndCpty;
        GetComponent<UnitScript>().EndCapacity();
        base.EndCpty();
        GameManager.Instance.TileChooseList.Clear();
        GetComponent<UnitScript>().IsActifNotConsumeAction = false;
    }
}
