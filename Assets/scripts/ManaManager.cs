using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaManager : MonoBehaviour
{
    [SerializeField] public Unbalancing manaBar;
    public float mana;
    public float maxMana;

    // Start is called before the first frame update
    void Start()
    {
        mana = maxMana;
    }

    // Update is called once per frame


    // MANA

    public void SubMana(float spent){ //Chamada ao usar o Thrust
        mana -= spent * Time.deltaTime; // Recuperação gradual
        mana = Mathf.Max(mana, 0f); //MGarante que não ultrapasse o máximo

        manaBar.DesBalance(spent * Time.deltaTime, maxMana); // Atualiza a barra
    }

    public void RecoverMana(float recoveryRate){ //Ainda não sei kkkkkk
        mana += recoveryRate * Time.deltaTime; // Recuperação gradual
        mana = Mathf.Min(mana, maxMana); //MGarante que não ultrapasse o máximo

        manaBar.DesBalance(-recoveryRate * Time.deltaTime, maxMana); // Atualiza a barra
    }

}
