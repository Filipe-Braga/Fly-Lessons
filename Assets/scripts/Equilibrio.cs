using System.Collections;
using UnityEngine;

public class Equilibrio : MonoBehaviour
{
    private Rigidbody2D rb;

    [SerializeField] public float maxBalance = 100f;
    [SerializeField] public float balance;
    [SerializeField] public float disturb;
    [SerializeField] public float recoveryRate = 5f; // Velocidade de recuperação por segundo

    [SerializeField] public Unbalancing balanceBar;

    private Coroutine recoveryCoroutine; // Referência da Coroutine

    void Start()
    {
        balance = maxBalance;
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (balance <= 0)
        {
            lostControl();
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.contactCount == 0) return;

        Vector2 collisionNormal = collision.contacts[0].normal;
        float globalAngle = Mathf.Atan2(collisionNormal.y, collisionNormal.x) * Mathf.Rad2Deg;

        if (globalAngle < 80f || globalAngle > 100f)
        {
            SubBalance(disturb);
        }
    }

    void SubBalance(float disturb)
    {
        balance -= disturb;
        balanceBar.DesBalance(disturb, maxBalance);

        // Se já estiver recuperando, reinicia o timer
        if (recoveryCoroutine != null)
        {
            StopCoroutine(recoveryCoroutine);
        }

        // Inicia um novo timer de recuperação
        recoveryCoroutine = StartCoroutine(RecoverBalance());
    }

    IEnumerator RecoverBalance()
    {
        yield return new WaitForSeconds(5f); // Espera 5 segundos antes de recuperar

        while (balance < maxBalance)
        {
            balance += recoveryRate * Time.deltaTime; // Recuperação gradual
            balance = Mathf.Min(balance, maxBalance); // Garante que não ultrapasse o máximo

            balanceBar.DesBalance(-recoveryRate * Time.deltaTime, maxBalance); // Atualiza a barra

            yield return null; // Aguarda o próximo frame
        }

        recoveryCoroutine = null; // Reseta a referência ao terminar
    }

    void lostControl()
    {
        gameObject.SetActive(false);
    }
}
