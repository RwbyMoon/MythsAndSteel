using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eruption : Capacity
{
    //Cette capacité appartient à l'unité "Etna" de l'armée Italienne sur le plateau de Husky
    [SerializeField] private FireGestion fr;

    public override void StartCpty()
    {
        //check si le joueur a les ressources pour la capacité
        int ressourcePlayer = GetComponent<UnitScript>().UnitSO.IsInRedArmy ? PlayerScript.Instance.RedPlayerInfos.Ressource : PlayerScript.Instance.BluePlayerInfos.Ressource;
        if (ressourcePlayer >= Capacity1Cost)
        {
        //choppe les cases adjacentes a Etna
            int tileId = RaycastManager.Instance.ActualUnitSelected.GetComponent<UnitScript>().ActualTiledId;
            List<GameObject> tiles = new List<GameObject>();    //liste des tiles selectionnable
            
            //ajoute toutes les tiles adjacentes a Etna deux fois dans la lsite des tiles a selectionner
            //pour chaque tiles voisine de Etna
            foreach (int T in PlayerStatic.GetNeighbourDiag(tileId, TilesManager.Instance.TileList[tileId].GetComponent<TileScript>().Line, false))
            {
                //si elle existe
                if (TilesManager.Instance.TileList[T] != null)
                {
                    
                    //pour chaque tile voisine d'une case voisine d'Etna
                    foreach (int I in PlayerStatic.GetNeighbourDiag(T, TilesManager.Instance.TileList[T].GetComponent<TileScript>().Line, false))
                    {
                        //si elle n'est pas deja dans la lsite l'ajoute a la liste
                        if (!tiles.Contains(TilesManager.Instance.TileList[I]))
                        {
                            tiles.Add(TilesManager.Instance.TileList[I]);
                        }                        
                    }

                    //ajoute la tile voisine d'Etna a la liste
                    if (!tiles.Contains(TilesManager.Instance.TileList[T]))
                    {
                        tiles.Add(TilesManager.Instance.TileList[T]);
                    }
                    
                }
            }

            //retire les cases voisines de cases voisine d'Etna qui ne devrait pas etre dans la liste, et retire la case d'Etna de la liste
            int[] ToRemoveId = { 10, 19, 11, 8, 7, 17, -8, -7, -17, -10, -19 - 11 };
            foreach (int T in ToRemoveId)
            {
                if (GetComponent<UnitScript>().ActualTiledId + T >= 0 && GetComponent<UnitScript>().ActualTiledId + T <= 80)
                {
                    tiles.Remove(TilesManager.Instance.TileList[GetComponent<UnitScript>().ActualTiledId + T]);
                }
            }
            tiles.Remove(TilesManager.Instance.TileList[GetComponent<UnitScript>().ActualTiledId]);


            GameManager.Instance._eventCall += EndCpty;
            GameManager.Instance._eventCallCancel += StopCpty;
            GameManager.Instance.StartEventModeTiles(1, GetComponent<UnitScript>().UnitSO.IsInRedArmy, tiles, "Éruption", "Voulez-vous vraiment faire tout brûler?");
            base.StartCpty();
        }
    }

    public override void EndCpty()
    {
        //le joueur paye la capacité en ressource
        if (GetComponent<UnitScript>().UnitSO.IsInRedArmy)
        {
            PlayerScript.Instance.RedPlayerInfos.Ressource -= Capacity1Cost;
        }
        else
        {
            PlayerScript.Instance.BluePlayerInfos.Ressource -= Capacity1Cost;
        }


        //change la case selectionnée a une case adjacente à Etna
        int centerTileId = GameManager.Instance.TileChooseList[0].GetComponent<TileScript>().TileId;
        switch (GetComponent<UnitScript>().ActualTiledId - centerTileId)
        {
            case 2:
                centerTileId = centerTileId + 1;
                break;

            case -2:
                centerTileId = centerTileId - 1;
                break;

            case +18:
                centerTileId = centerTileId + 9;
                break;

            case -18:
                centerTileId = centerTileId - 9;
                break;

            default:
                break;

        }

        //a partir de la case selectionnée, ajoute toutes les cases adjacentes (diag incluse) a la liste des cases a embraser
        List<GameObject> FireTiles = new List<GameObject>();
        foreach (int T in PlayerStatic.GetNeighbourDiag(centerTileId, TilesManager.Instance.TileList[centerTileId].GetComponent<TileScript>().Line, true))
        {           
            FireTiles.Add(TilesManager.Instance.TileList[T]);
        }
        //ajouter la case selectionnée a la liste des cases a embraser
        FireTiles.Add(TilesManager.Instance.TileList[centerTileId]);
        
        //enlever la case d'Etna a la liste des cases a embraser
        FireTiles.Remove(TilesManager.Instance.TileList[GetComponent<UnitScript>().ActualTiledId]);

        //si la zone est envoyée vers le haut ou le bas
        if (GetComponent<UnitScript>().ActualTiledId - centerTileId == 9 || GetComponent<UnitScript>().ActualTiledId - centerTileId == -9)
        {
            //enleve les cases a gauche et a droite d'Etna de la liste des cases a embraser
            if (GetComponent<UnitScript>().ActualTiledId - 1 >= 0)
            {
                FireTiles.Remove(TilesManager.Instance.TileList[GetComponent<UnitScript>().ActualTiledId - 1]);
            }
            if (GetComponent<UnitScript>().ActualTiledId + 1 <= 80)
            {
                FireTiles.Remove(TilesManager.Instance.TileList[GetComponent<UnitScript>().ActualTiledId + 1]);
            }
                           
        }
        else
        {
            //enleve les cases au dessus et en dessous d'Etna de la liste des cases a embraser
            if (GetComponent<UnitScript>().ActualTiledId - 9 >= 0)
            {
                FireTiles.Remove(TilesManager.Instance.TileList[GetComponent<UnitScript>().ActualTiledId - 9]);
            }

            if (GetComponent<UnitScript>().ActualTiledId + 9 <= 80)
            {
                FireTiles.Remove(TilesManager.Instance.TileList[GetComponent<UnitScript>().ActualTiledId + 9]);
            }
        }

        //pour chaque cases dans la liste des cases a embraser, embrase la case
        foreach (GameObject item in FireTiles)
        {
            fr.CreateFire(item.GetComponent<TileScript>().TileId);
        }

        GameManager.Instance._eventCall -= EndCpty;
        GetComponent<UnitScript>().EndCapacity();
        GameManager.Instance.TileChooseList.Clear();
    }

    public override void StopCpty()
    {
        GameManager.Instance.StopEventModeTile();
        GameManager.Instance.TileChooseList.Clear();
        GetComponent<UnitScript>().StopCapacity(true);
        base.StopCpty();
    }
}
