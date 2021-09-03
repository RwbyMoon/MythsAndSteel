using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoleilLevant : MonoBehaviour
{
    #region Info
    [SerializeField] string _Capacity1Name = "";
    public string Capacity1Name => _Capacity1Name;
    [SerializeField] private Sprite render1;

    [TextArea]
    [SerializeField] string _Capacity1Description = "";
    public string Capacity1Description => _Capacity1Description;
    #endregion

    public int HasAtkBoost = 0;
    public bool BoostApplied = false;
    public bool ReduceDone = false;
    public AudioClip AttaqueUp;
    public AudioClip AttaqueDown;
    public AudioSource audioSource;

    private void Update()
    {
        if(GetComponent<UnitScript>().HasAttacked == true)
        {
            StartCoroutine(UpdateStatsAtk());
        }
        if(GetComponent<UnitScript>().NewTurnHasStart == true && HasAtkBoost > 0)
        {
            StartCoroutine(UpdateStatsAtk());
        }
        if(HasAtkBoost > 0 && BoostApplied == false)
        {
            BoostApplied = true;
            GetComponent<UnitScript>().AddDamageToUnit(1);
            transform.GetChild(2).GetComponent<Animator>().SetBool("Enabled", true);
            audioSource.PlayOneShot(AttaqueUp, 10f);
        }
        if(HasAtkBoost == 0 && BoostApplied == true)
        {
            BoostApplied = false;
            GetComponent<UnitScript>().AddDamageToUnit(-1);
            transform.GetChild(2).GetComponent<Animator>().SetBool("Enabled", false);
            audioSource.PlayOneShot(AttaqueDown, 1f);
        }
    }

    IEnumerator UpdateStatsAtk()
    {
        if(GetComponent<UnitScript>().HasAttacked == true)
        {
            yield return new WaitForSeconds(2);
            HasAtkBoost = 2;
        }
        if(GetComponent<UnitScript>().NewTurnHasStart == true && ReduceDone == false)
        {
            ReduceDone = true;
            yield return new WaitForSeconds(2);
            HasAtkBoost--;
            ReduceDone = false;
        }
    }
}
