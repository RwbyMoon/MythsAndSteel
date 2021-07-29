using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeCreuse: Capacity
{
    [SerializeField] RuntimeAnimatorController TransformationAnimator;
    public override void StartCpty()
    {
      
            int tileId = RaycastManager.Instance.ActualUnitSelected.GetComponent<UnitScript>().ActualTiledId;
            List<GameObject> unitList = new List<GameObject>();

            foreach (int T in PlayerStatic.GetNeighbourDiag(tileId, TilesManager.Instance.TileList[tileId].GetComponent<TileScript>().Line, false))
            {
                if (TilesManager.Instance.TileList[T] != null)
                {
                    if (TilesManager.Instance.TileList[T].GetComponent<TileScript>().Unit != RaycastManager.Instance.ActualUnitSelected && TilesManager.Instance.TileList[T].GetComponent<TileScript>().Unit != null)
                    {
                    if(TilesManager.Instance.TileList[T].GetComponent<TileScript>().Unit.GetComponent<UnitScript>().UnitSO.IsInJ1Army != GameManager.Instance.IsJ1Turn)
                    {

                        unitList.Add(TilesManager.Instance.TileList[T]);
                    }
                    }
                }
            }

            GameManager.Instance._eventCall += EndCpty;
            GameManager.Instance._eventCallCancel += StopCpty;
            GameManager.Instance.StartEventModeTiles(1, GetComponent<UnitScript>().UnitSO.IsInJ1Army, unitList, "Charge creuse", "Voulez-vous vraiment infliger 3 d�gats � cette unit� ?");
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
       
        
            GameManager.Instance.TileChooseList[0].GetComponent<TileScript>().Unit.GetComponent<UnitScript>().TakeDamage(3);
            GameManager.Instance._eventCall -= EndCpty;
            GetComponent<UnitScript>().EndCapacity();
            base.EndCpty();
            GameManager.Instance.TileChooseList.Clear();
            Destroy(GetComponent<ChargeCreuse>());
        GetComponent<Animator>().runtimeAnimatorController = null;
        GetComponent<Animator>().runtimeAnimatorController = TransformationAnimator;

        GameManager.Instance._eventCall -= EndCpty;
            GetComponent<UnitScript>().EndCapacity();
            base.EndCpty();
            GameManager.Instance.TileChooseList.Clear();
        
    }
}
