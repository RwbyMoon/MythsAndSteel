using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarbelGestion : MonoSingleton<BarbelGestion>
{



    [SerializeField] private int BarbelDamage = 2;

    

    public Sprite Horizontal;
    public Sprite Vertical;
    public List<Barbel> BarbelActive;

    public void CreateBarbel(int tileId, MYthsAndSteel_Enum.Direction Direct)
    {
        List<MYthsAndSteel_Enum.TerrainType> T = TilesManager.Instance.TileList[tileId].GetComponent<TileScript>().TerrainEffectList;
        MYthsAndSteel_Enum.Direction D = MYthsAndSteel_Enum.Direction.None;
        if (T.Contains(MYthsAndSteel_Enum.TerrainType.Barbelé_Est) || T.Contains(MYthsAndSteel_Enum.TerrainType.Barbelé_Nord) || T.Contains(MYthsAndSteel_Enum.TerrainType.Barbelé_Sud) || T.Contains(MYthsAndSteel_Enum.TerrainType.Barbelé_Ouest))
        {
            List<GameObject> G = TilesManager.Instance.TileList[tileId].GetComponent<TileScript>()._Child;
            foreach (GameObject Child in G)
            {
                if (Child.TryGetComponent<Barbel>(out Barbel B))
                {
                    if (B.Direc == Direct)
                    {
                        D = Direct;
                    }
                }
            }
            if (D != MYthsAndSteel_Enum.Direction.None)
            {
                Delete(tileId, D);
            }
            else
            {
                switch (Direct)
                {
                    case MYthsAndSteel_Enum.Direction.Est:
                        if (tileId + 1 <= 80)
                        {
                            List<MYthsAndSteel_Enum.TerrainType> T2 = TilesManager.Instance.TileList[tileId + 1].GetComponent<TileScript>().TerrainEffectList;
                            if (T2.Contains(MYthsAndSteel_Enum.TerrainType.Barbelé_Ouest))
                            {
                                Delete(tileId + 1, MYthsAndSteel_Enum.Direction.Ouest);
                            }
                        }
                        break;
                    case MYthsAndSteel_Enum.Direction.Ouest:
                        if (tileId - 1 >= 0)
                        {
                            List<MYthsAndSteel_Enum.TerrainType> T2 = TilesManager.Instance.TileList[tileId - 1].GetComponent<TileScript>().TerrainEffectList;
                            if (T2.Contains(MYthsAndSteel_Enum.TerrainType.Barbelé_Est))
                            {
                                Delete(tileId - 1, MYthsAndSteel_Enum.Direction.Est);
                            }
                        }
                        break;
                    case MYthsAndSteel_Enum.Direction.Nord:
                        if (tileId + 9 <= 80)
                        {
                            List<MYthsAndSteel_Enum.TerrainType> T2 = TilesManager.Instance.TileList[tileId + 9].GetComponent<TileScript>().TerrainEffectList;
                            if (T2.Contains(MYthsAndSteel_Enum.TerrainType.Barbelé_Sud))
                            {
                                Delete(tileId + 9, MYthsAndSteel_Enum.Direction.Sud);
                            }
                        }
                        break;
                    case MYthsAndSteel_Enum.Direction.Sud:
                        if (tileId - 9 >= 0)
                        {
                            List<MYthsAndSteel_Enum.TerrainType> T2 = TilesManager.Instance.TileList[tileId - 9].GetComponent<TileScript>().TerrainEffectList;
                            if (T2.Contains(MYthsAndSteel_Enum.TerrainType.Barbelé_Nord))
                            {
                                Delete(tileId - 9, MYthsAndSteel_Enum.Direction.Nord);
                            }
                        }
                        break;
                }
            }
            BarbelCreationAfterVerification(tileId, Direct);
        }
        else
        {
            BarbelCreationAfterVerification(tileId, Direct);
        }
    }

    public void Delete(int tileId, MYthsAndSteel_Enum.Direction Direction)
    {
        TileScript TS = TilesManager.Instance.TileList[tileId].GetComponent<TileScript>();
        switch (Direction)
        {
            case MYthsAndSteel_Enum.Direction.Est: 
                TS.RemoveEffect(MYthsAndSteel_Enum.TerrainType.Barbelé_Est);
                if (tileId + 1 <= 80)
                {
                    TileScript TSE = TilesManager.Instance.TileList[tileId + 1].GetComponent<TileScript>();
                    TSE.RemoveEffect(MYthsAndSteel_Enum.TerrainType.Barbelé_Ouest);
                }
                break;
            case MYthsAndSteel_Enum.Direction.Nord:
                TS.RemoveEffect(MYthsAndSteel_Enum.TerrainType.Barbelé_Nord);
                if (tileId + 9 <= 80)
                {
                    TileScript TSN = TilesManager.Instance.TileList[tileId + 9].GetComponent<TileScript>();
                     TSN.RemoveEffect(MYthsAndSteel_Enum.TerrainType.Barbelé_Sud);
                }
                break;
            case MYthsAndSteel_Enum.Direction.Sud:
                TS.RemoveEffect(MYthsAndSteel_Enum.TerrainType.Barbelé_Sud);
                if (tileId - 9 >= 0)
                {
                    TileScript TSS = TilesManager.Instance.TileList[tileId - 9].GetComponent<TileScript>();
                    TSS.RemoveEffect(MYthsAndSteel_Enum.TerrainType.Barbelé_Nord);
                }
                break;
            case MYthsAndSteel_Enum.Direction.Ouest:
                TS.RemoveEffect(MYthsAndSteel_Enum.TerrainType.Barbelé_Ouest);
                if (tileId - 1 >= 0)
                {
                    TileScript TSO = TilesManager.Instance.TileList[tileId - 1].GetComponent<TileScript>();
                     TSO.RemoveEffect(MYthsAndSteel_Enum.TerrainType.Barbelé_Est);
                }
                break;
        }
    }

    MYthsAndSteel_Enum.TerrainType TY;
    protected void BarbelCreationAfterVerification(int tileId, MYthsAndSteel_Enum.Direction Direction)
    {
        foreach (TerrainType T in GameManager.Instance.Terrain.EffetDeTerrain)
        {
            foreach (MYthsAndSteel_Enum.TerrainType T1 in T._eventType)
            {
                if (T1 == MYthsAndSteel_Enum.TerrainType.Barbelé_Nord) // Aucune différence c'est le même enfant.
                {
                    GameObject Child = Instantiate(T.Child, transform.position, Quaternion.identity);
                    Child.transform.parent = TilesManager.Instance.TileList[tileId].GetComponent<TileScript>().transform;
                    Child.transform.localScale = new Vector3(1, 1, 1);
                    TilesManager.Instance.TileList[tileId].GetComponent<TileScript>()._Child.Add(Child);
                    Child.GetComponentInChildren<Barbel>().BarbelG = this;
                    Child.GetComponentInChildren<Barbel>().Direc = Direction;
                    Child.GetComponentInChildren<Barbel>().TurnLeft = 2;
                    BarbelActive.Add(Child.GetComponentInChildren<Barbel>());
                    Child.transform.localPosition = Vector3.zero;

                    TileScript TS = TilesManager.Instance.TileList[tileId].GetComponent<TileScript>();
                    if (Direction == MYthsAndSteel_Enum.Direction.Nord)
                    {
                        TY = MYthsAndSteel_Enum.TerrainType.Barbelé_Nord; TS.TerrainEffectList.Add(MYthsAndSteel_Enum.TerrainType.Barbelé_Nord);
                        if (tileId + 9 <= 80)
                        {
                            TileScript TSN = TilesManager.Instance.TileList[tileId + 9].GetComponent<TileScript>();
                            TSN.TerrainEffectList.Add(MYthsAndSteel_Enum.TerrainType.Barbelé_Sud);
                        }
                    }
                    if (Direction == MYthsAndSteel_Enum.Direction.Sud)
                    {
                        TY = MYthsAndSteel_Enum.TerrainType.Barbelé_Sud; TS.TerrainEffectList.Add(MYthsAndSteel_Enum.TerrainType.Barbelé_Sud);
                        if (tileId - 9 >= 0)
                        {
                            TileScript TSS = TilesManager.Instance.TileList[tileId - 9].GetComponent<TileScript>();
                            TSS.TerrainEffectList.Add(MYthsAndSteel_Enum.TerrainType.Barbelé_Nord);
                        }
                    }
                    if (Direction == MYthsAndSteel_Enum.Direction.Est)
                    {
                        TY = MYthsAndSteel_Enum.TerrainType.Barbelé_Est; TS.TerrainEffectList.Add(MYthsAndSteel_Enum.TerrainType.Barbelé_Est);
                        if (tileId + 1 <= 80)
                        {
                            TileScript TSE = TilesManager.Instance.TileList[tileId + 1].GetComponent<TileScript>();
                            TSE.TerrainEffectList.Add(MYthsAndSteel_Enum.TerrainType.Barbelé_Ouest);
                        }
                    }
                    if (Direction == MYthsAndSteel_Enum.Direction.Ouest)
                    {
                        TY = MYthsAndSteel_Enum.TerrainType.Barbelé_Ouest; TS.TerrainEffectList.Add(MYthsAndSteel_Enum.TerrainType.Barbelé_Ouest); 
                        if (tileId - 1 >= 0)
                        {
                            TileScript TSO = TilesManager.Instance.TileList[tileId - 1].GetComponent<TileScript>();
                            TSO.TerrainEffectList.Add(MYthsAndSteel_Enum.TerrainType.Barbelé_Est); 
                        }
                    }
                    Child.GetComponent<ChildEffect>().Type = TY;
                }
            }
        }
    }




    public void RandomBarbel(int Number, MYthsAndSteel_Enum.Direction Direct)
    {
        for(int p = 0; p <= Number; p++)
        {
        int i = Random.Range(1, 5);
        int w = Random.Range(1, 80);
        MYthsAndSteel_Enum.Direction D;
        switch (i)
        {
            case 1: Direct = MYthsAndSteel_Enum.Direction.Nord; break;
            case 2: Direct = MYthsAndSteel_Enum.Direction.Sud; break;
            case 3: Direct = MYthsAndSteel_Enum.Direction.Est; break;
            case 4: Direct = MYthsAndSteel_Enum.Direction.Ouest; break;
        }
        CreateBarbel(w, Direct);
        }
    }
}
