using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionReaction : MonoBehaviour
{
    //[SerializeField] private GameObject Attaque;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<UnitScript>().HasAttacked)
        {
            Debug.Log("le passif action réaction s'active");
            //int unitTile = Attaque.GetComponent<Attaque>()._selectedTiles[0];

            int unitTile = Attaque.Instance._selectedTiles[0];
            GameObject Unit = TilesManager.Instance.TileList[unitTile].GetComponent<TileScript>().Unit;

            Debug.Log(Unit);

            Vector2 AttaquantPos = this.transform.position;
            Vector2 CiblePos = Unit.transform.position;

            /*Debug.Log("Attaquant x : " + AttaquantPos.x);
            Debug.Log("Attaquant y : " + AttaquantPos.y);

            Debug.Log("Cible x : " + CiblePos.x);
            Debug.Log("Cible y : " + CiblePos.y);*/

                List<GameObject> tiles = new List<GameObject>();
            foreach (int i in PlayerStatic.GetNeighbourDiag(this.GetComponent<UnitScript>().ActualTiledId, TilesManager.Instance.TileList[this.GetComponent<UnitScript>().ActualTiledId].GetComponent<TileScript>().Line, true))
            {
                GameObject UnitInList = TilesManager.Instance.TileList[i].GetComponent<TileScript>().Unit;
                if (UnitInList = null)
                {
                    tiles.Add(TilesManager.Instance.TileList[i]);
                    Debug.Log(TilesManager.Instance.TileList[i]);
                }
            }

            
            if (AttaquantPos.x > CiblePos.x)
            {
                //les trois cases à droite de l'attaquant

                

            }
            if (AttaquantPos.x < CiblePos.x)
            {
                //les trois cases à gauche de l'attaquant

            }
            if (AttaquantPos.x == AttaquantPos.y)
            {
                if (AttaquantPos.y > CiblePos.y)
                {
                    //les trois cases au dessus de l'attaquant

                }
                if (AttaquantPos.y < CiblePos.y)
                {
                    //les trois cases en dessus de l'attaquant

                }
                if (AttaquantPos.y == CiblePos.y)
                {
                    Debug.LogError("Pas normal ça");
                }
            }

        }
    }
}
