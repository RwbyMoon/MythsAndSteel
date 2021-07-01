using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ConsoleManager : MonoSingleton<ConsoleManager>
{
    [Header("Prefab.")]
    [SerializeField] private GameObject AutoCompPrefab;
    [SerializeField] private GameObject LogPrefab;

    [Header("Console go.")]
    [SerializeField] private GameObject Console;
    [SerializeField] private GameObject LogParent;
    [SerializeField] private GameObject AutoCompParent;
    [SerializeField] private TMP_InputField CmdEntry;

    [SerializeField] string[] newList;

    [SerializeField] Dictionary<string, string> commandList = new Dictionary<string, string>();
    private bool opened = false;

    private bool God1 = false;
    private bool God2 = false;

    void Start()
    {
        commandList.Add("GIVE_ORGONE", "Exemple: GIVE_ORGONE {int}player {int}1-5");
        commandList.Add("GIVE_RESSOURCE", "Exemple: GIVE_RESSOURCE {int}player {int}1-64");
        commandList.Add("GIVE_ACTIVATION", "Exemple: GIVE_ACTIVATION {int}player {int}1-64");
        commandList.Add("GOD_MODE", "Exemple: GOD_MODE {int}player"); 
        commandList.Add("WIN", "Exemple: WIN {int}player");
        commandList.Add("GOTOPHASE", "Exemple: GOTOPHASE {int}phase (1: Début, 2: Activation, 3: OrgoneJ1, 4: ActionJ1, 5: OrgoneJ2, 6: ActionJ2, 7: Strategie");
        commandList.Add("RESSOURCEPOINT", "Exemple: RESSOURCEPOINT {int}-999-999");
        commandList.Add("GIVE_EVENTCARDS", "Exemple: GIVE_EVENTCARDS {int}player {int}1-7");
    }

    void Update()
    {
        Open();
    }

    /// <summary>
    /// AutoComp button click.
    /// </summary>
    /// <param name="txt"></param>
    public void AutoCompButton(TextMeshProUGUI txt)
    {
        CmdEntry.text = txt.text;
        CmdEntry.Select();
        CmdEntry.ActivateInputField();
    }

    /// <summary>
    /// Create auto compil.
    /// </summary>
    public void AutoComp()
    {
        CmdEntry.text = CmdEntry.text.ToUpper();

        if (AutoCompParent.transform.childCount > 0)
        {
            int ChildsCount = AutoCompParent.transform.childCount;
            for (int i = ChildsCount - 1; i >= 0; i--)
            {
                Destroy(AutoCompParent.transform.GetChild(i).gameObject);
                if (i == 0)
                {
                    CreateCommandList();
                }
            }
        }
        else
        {
            CreateCommandList();
        }
    }

    /// <summary>
    /// Open console panel.
    /// </summary>
    private void Open()
    {
        if (Input.GetKeyDown(KeyCode.AltGr))
        {
            Console.GetComponent<CanvasGroup>().alpha = 1;
            CmdEntry.text = "";

            opened = !opened;
            Console.SetActive(opened);
            if (opened)
            {
                CmdEntry.ActivateInputField();
                CmdEntry.Select();
            }
        }

        if(Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.O))
        {
            Console.GetComponent<CanvasGroup>().alpha = 0;
            opened = !opened;
            Console.SetActive(opened);
            if(opened)
            {
                CmdEntry.ActivateInputField();
                CmdEntry.Select();
            }
        }

        if (opened && Input.GetKeyDown(KeyCode.Return))
        {
            Command(CmdEntry.text);
            CmdEntry.text = "";
            CmdEntry.Select();
            CmdEntry.ActivateInputField();
        }
    }

    /// <summary>
    /// Liste des commandes.
    /// </summary>
    /// <param name="cmd"></param>
    protected void Command(string cmd)
    {
        // Chaque commande doit être ajoutée à la liste commandList.
        string[] split = cmd.Split(' ');
        switch (split[0])
        {
            case "GIVE_RESSOURCE":
                {
                    if (!CheckCmd(cmd, 1, 2, 1)[cmd])
                    {
                        return;
                    }
                    else if (CheckCmd(cmd, 1, 2, 1)[cmd])
                    {
                        if (CheckCmd(cmd, 1, 64, 2)[cmd])
                        {
                            switch (int.Parse(split[1]))
                            {
                                case 1:
                                    PlayerScript.Instance.RedPlayerInfos.Ressource += int.Parse(split[2]);

                                    break;
                                case 2:
                                    PlayerScript.Instance.RedPlayerInfos.Ressource += int.Parse(split[2]);
                                    break;
                            }
                            Debug.Log("x");
                            Log("Le joueur " + split[1].ToString() + " a gagné " + int.Parse(split[2]) + " ressource(s))", cmd, true);
                        }
                    }
                }
                break;
            case "GIVE_ORGONE":
                {
                    if (!CheckCmd(cmd, 1, 2, 1)[cmd])
                    {
                        return;
                    }
                    else if (CheckCmd(cmd, 1, 2, 1)[cmd])
                    {
                        if (CheckCmd(cmd, 1, 5, 2)[cmd])
                        {
                            switch (int.Parse(split[1]))
                            {
                                case 1:
                                    PlayerScript.Instance.RedPlayerInfos.ChangeOrgone(int.Parse(split[2]), 1);

                                    break;
                                case 2:
                                    PlayerScript.Instance.RedPlayerInfos.ChangeOrgone(int.Parse(split[2]), 2);
                                    break;
                            }
                            Log("Le joueur " + split[1].ToString() + " a gagné " + int.Parse(split[2]) + " d'orgone(s))", cmd, true);
                        }
                    }
                }
                break;
            case "GIVE_EVENTCARDS":
                {
                    if (!CheckCmd(cmd, 1, 2, 1)[cmd])
                    {
                        return;
                    }
                    else if (CheckCmd(cmd, 1, 2, 1)[cmd])
                    {
                        if (!CheckCmd(cmd, 1, 7, 2)[cmd])
                        {
                            return;
                        }
                        if (CheckCmd(cmd, 1, 7, 2)[cmd])
                        {
                            for (int i = 0; i <= int.Parse(split[2]); i++)
                            {
                                PlayerScript.Instance.GiveEventCard(int.Parse(split[1]));
                            }
                            Log("Le joueur " + split[1].ToString() + " a gagné " + int.Parse(split[2]) + " carte(s) event.", cmd, true);
                        }
                    }
                }
                break;
            case "GOD_MODE":
                {
                    if (!CheckCmd(cmd, 1, 2, 1)[cmd])
                    {
                        return;
                    }
                    switch (int.Parse(split[1]))
                    {
                        case 1:
                            foreach (GameObject Unit in PlayerScript.Instance.UnitRef.UnitListRedPlayer)
                            {
                                if (!God1)
                                {
                                    Unit.GetComponent<UnitScript>().AddStatutToUnit(MYthsAndSteel_Enum.UnitStatut.Invincible);
                                }
                                else
                                {
                                    Unit.GetComponent<UnitScript>().UnitStatuts.Remove(MYthsAndSteel_Enum.UnitStatut.Invincible);
                                }
                            }
                            if (!God1)
                            {
                                Log("Godmode activé pour le joueur " + split[1].ToString(), cmd, true);
                                God1 = true;
                            }
                            else
                            {
                                Log("Godmode désactivé pour le joueur " + split[1].ToString(), cmd, true);
                                God1 = false;
                            }
                            break;
                        case 2:
                            foreach (GameObject Unit in PlayerScript.Instance.UnitRef.UnitListBluePlayer)
                            {
                                if (!God2)
                                {
                                    Unit.GetComponent<UnitScript>().AddStatutToUnit(MYthsAndSteel_Enum.UnitStatut.Invincible);
                                }
                                else
                                {
                                    Unit.GetComponent<UnitScript>().UnitStatuts.Remove(MYthsAndSteel_Enum.UnitStatut.Invincible);
                                }
                            }
                            if (!God2)
                            {
                                Log("Godmode activé pour le joueur " + split[1].ToString(), cmd, true);
                                God2 = true;
                            }
                            else
                            {
                                Log("Godmode désactivé pour le joueur " + split[1].ToString(), cmd, true);
                                God2 = false;
                            }
                            break;
                    }
                    break;
                }
            case "GIVE_ACTIVATION":
                {
                    if (!CheckCmd(cmd, 1, 2, 1)[cmd])
                    {
                        return;
                    }
                    else if (CheckCmd(cmd, 1, 2, 1)[cmd])
                    {
                        if (CheckCmd(cmd, 1, 64, 2)[cmd])
                        {
                            switch (int.Parse(split[1]))
                            {
                                case 1:
                                    PlayerScript.Instance.RedPlayerInfos.ActivationLeft += int.Parse(split[2]);

                                    break;
                                case 2:
                                    PlayerScript.Instance.RedPlayerInfos.ActivationLeft += int.Parse(split[2]);
                                    break;
                            }
                            Debug.Log("x");
                            Log("Le joueur " + split[1].ToString() + " a gagné " + int.Parse(split[2]) + " activation(s))", cmd, true);
                        }
                    }
                    break;
                }
            case "WIN":
                {
                    if (!CheckCmd(cmd, 1, 2, 1)[cmd])
                    {
                        return;
                    }
                    switch (int.Parse(split[1]))
                    {
                        case 1:
                            GameManager.Instance.VictoryForArmy(1);
                            break;
                        case 2:
                            GameManager.Instance.VictoryForArmy(2);
                            break;
                    }
                    Log("Victoire pour le joueur " + split[1].ToString(), cmd, true);
                    break;
                }
            case "GOTOPHASE":
                {
                    if (!CheckCmd(cmd, 1, 7, 1)[cmd])
                    {
                        return;
                    }
                    switch (int.Parse(split[1]))
                    {
                        case 1:
                            GameManager.Instance.ManagerSO.GoToPhase(MYthsAndSteel_Enum.PhaseDeJeu.Debut, true);
                            break;
                        case 2:

                            GameManager.Instance.ManagerSO.GoToPhase(MYthsAndSteel_Enum.PhaseDeJeu.Activation, true);
                            break;
                        case 3:

                            GameManager.Instance.ManagerSO.GoToPhase(MYthsAndSteel_Enum.PhaseDeJeu.OrgoneJ1, true);
                            break;
                        case 4:

                            GameManager.Instance.ManagerSO.GoToPhase(MYthsAndSteel_Enum.PhaseDeJeu.ActionJ1, true);
                            break;
                        case 5:

                            GameManager.Instance.ManagerSO.GoToPhase(MYthsAndSteel_Enum.PhaseDeJeu.OrgoneJ2, true);
                            break;
                        case 6:

                            GameManager.Instance.ManagerSO.GoToPhase(MYthsAndSteel_Enum.PhaseDeJeu.ActionJ2, true);
                            break;
                        case 7:

                            GameManager.Instance.ManagerSO.GoToPhase(MYthsAndSteel_Enum.PhaseDeJeu.Strategie, true);
                            break;
                    }
                    Log("Téléportation à la phase " + split[1].ToString(), cmd, true);
                    break;
                }
            case "OVR":
                {
                    if (!CheckCmd(cmd, 1, 2, 1)[cmd])
                    {
                        return;
                    }
                    switch (int.Parse(split[1]))
                    {
                        case 1:
                            PlayerScript.Instance.RedPlayerInfos.dontTouchThis = true;
                            break;
                        case 2:
                            PlayerScript.Instance.BluePlayerInfos.dontTouchThis = true;
                            break;
                    }
                    break;
                }
            case "RESSOURCEPOINT":
                {
                    if (!CheckCmd(cmd, -999, 999, 1)[cmd])
                    {
                        return;
                    }
                    foreach (GameObject GO in TilesManager.Instance.TileList)
                    {
                        if (GO.GetComponent<TileScript>().TerrainEffectList.Contains(MYthsAndSteel_Enum.TerrainType.Point_de_ressource))
                        {
                            GO.GetComponent<TileScript>().ResourcesCounter += int.Parse(split[1]);
                        }
                    }
                    break;
                }
        }
        CmdEntry.text = "";
        CmdEntry.Select();
        CmdEntry.ActivateInputField();
    }

    /// <summary>
    /// Check si les paramètres sont bons.
    /// </summary>
    /// <param name="cmd">Commande en question.</param>
    /// <param name="minsplitcount">Valeur min. (include)</param>
    /// <param name="maxsplitcount">Valeur max. (include)</param>
    /// <returns></returns>
    protected Dictionary<string, bool> CheckCmd(string cmd, int minValue, int maxValue, int ValueSplitCount)
    {
        string[] splited = cmd.Split(' ');
        if (splited.Length > 1)
        {
            int Parse;
            if(int.TryParse(splited[ValueSplitCount], out Parse))
            {
                if(Parse < minValue)
                {
                    Debug.Log("x");
                    Dictionary<string, bool> ReturnMin = new Dictionary<string, bool>();
                    ReturnMin.Add(cmd, false);
                    Log("Une valeur est trop basse.", cmd, false);
                    return ReturnMin;
                }
            }
            else
            {
                Dictionary<string, bool> NoValue = new Dictionary<string, bool>();
                NoValue.Add(cmd, false);
                Log("Int32 format only.", cmd, false);
                return NoValue;
            }
        }
        else
        {
            Dictionary<string, bool> NoValue = new Dictionary<string, bool>();
            NoValue.Add(cmd, false);
            Log("Miss parameter.", cmd, false);
            return NoValue;
        }
        if (splited.Length > 1)
        {
            int Parse;
            if (int.TryParse(splited[ValueSplitCount], out Parse))
            {
                if (Parse > maxValue)
                {
                    Debug.Log("x");
                    Dictionary<string, bool> ReturnMax = new Dictionary<string, bool>();
                    ReturnMax.Add(cmd, false);
                    Log("Une valeur est trop haute.", cmd, false);
                    return ReturnMax;
                }
            }
            else
            {
                Dictionary<string, bool> NoValue = new Dictionary<string, bool>();
                NoValue.Add(cmd, false);
                Log("Int32 format only.", cmd, false);
                return NoValue;
            }
        }
        else
        {
            Debug.Log("x");
            Dictionary<string, bool> NoValue = new Dictionary<string, bool>();
            NoValue.Add(cmd, false);
            Log("Miss parameter.", cmd, false);
            return NoValue;
        }
        Dictionary<string, bool> Return = new Dictionary<string, bool>();
        Return.Add(cmd, true);
        return Return;
    } 

    /// <summary>
    /// Create log UI.
    /// </summary>
    /// <param name="Message"></param>
    /// <param name="cmd"></param>
    /// <param name="done"></param>
    protected void Log(string Message, string cmd, bool done)
    {
        if (done)
        {
            var cmdenter = Instantiate(LogPrefab, LogParent.transform);
            cmdenter.GetComponentInChildren<TextMeshProUGUI>().text = Message + " " + cmd + ": done.";
        }
        else if (!done)
        {
            var cmdenter = Instantiate(LogPrefab, LogParent.transform);
            cmdenter.GetComponentInChildren<TextMeshProUGUI>().text = Message;
        }
    }

    GameObject autocompenter;

    /// <summary>
    /// AutoComp creationlist.
    /// </summary>
    protected void CreateCommandList()
    {
        if (!string.IsNullOrEmpty(CmdEntry.text))
        {
            List<string> found = new List<string>();
            foreach(KeyValuePair<string, string> f in commandList)
            {
                string u = f.Key;
                string w = f.Value;

                 newList = CmdEntry.text.Split(' ');
                if (u.StartsWith(newList[0]))
                {
                    found.Add(u);
                    autocompenter = Instantiate(AutoCompPrefab, AutoCompParent.transform);
                    autocompenter.GetComponent<AutoCompContainer>().TitleText.GetComponent<TextMeshProUGUI>().text = u;
                    autocompenter.GetComponent<AutoCompContainer>().TextDetails.GetComponent<TextMeshProUGUI>().text = w;
                }
            }
        }
        if (CmdEntry.text == "HELP")
        {
            List<string> found = new List<string>();
            foreach (KeyValuePair<string, string> f in commandList)
            {
                string u = f.Key;
                string w = f.Value;
                found.Add(u);
                autocompenter = Instantiate(AutoCompPrefab, AutoCompParent.transform);
                autocompenter.GetComponent<AutoCompContainer>().TitleText.GetComponent<TextMeshProUGUI>().text = u;
                autocompenter.GetComponent<AutoCompContainer>().TextDetails.GetComponent<TextMeshProUGUI>().text = w;
            }
        }
    }
}