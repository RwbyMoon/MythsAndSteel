using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColereDuKhodok : MonoBehaviour
{
    #region Info
    [SerializeField] string _Capacity1Name = "";
    public string Capacity1Name => _Capacity1Name;
    [SerializeField] private Sprite render1;

    [TextArea]
    [SerializeField] string _Capacity1Description = "";
    public string Capacity1Description => _Capacity1Description;
    #endregion
    
    public int actualLife;
    bool SetLifeDone;
    public int actualBonus;
    public AudioClip AttaqueUp;
    public AudioClip AttaqueDown;
    public AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        StartCoroutine(SetFirstLife());
    }

    private void Update()
    {
        if(actualLife > GetComponent<UnitScript>()._life && SetLifeDone && GetComponent<UnitScript>()._life > 0)
        {
            GetComponent<UnitScript>().DiceBonus += 2 * (actualLife - GetComponent<UnitScript>()._life);
            GetComponent<UnitScript>().PermaDiceBoost += 2 * (actualLife - GetComponent<UnitScript>()._life);
            actualBonus += 2 * (actualLife - GetComponent<UnitScript>()._life);
            actualLife = GetComponent<UnitScript>()._life;
            transform.GetChild(2).GetComponent<Animator>().SetBool("Active", true);
            audioSource.PlayOneShot(AttaqueUp, 10f);
        }
        if (GetComponent<UnitScript>().HasAttacked)
        {
            GetComponent<UnitScript>().DiceBonus -= actualBonus;
            GetComponent<UnitScript>().PermaDiceBoost -= actualBonus;
            actualBonus = 0;
            transform.GetChild(2).GetComponent<Animator>().SetBool("Active", false);
            audioSource.PlayOneShot(AttaqueDown, 1f);
        }
        if(actualLife < GetComponent<UnitScript>()._life)
        {
            actualLife = GetComponent<UnitScript>()._life;
        }
    }

    IEnumerator SetFirstLife()
    {
        actualLife = GetComponent<UnitScript>()._life;
        yield return new WaitForSeconds(1);
        SetLifeDone = true;
    }
}
