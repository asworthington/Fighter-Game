using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Animator anim;
    public SpriteRenderer rend;
    public Rigidbody2D rb;
    public TextMeshProUGUI healthText;
    public AudioSource meleeSfx;
    public AudioSource collectionSfx;
    public AudioSource deathSfx;

    public int maxHealth = 100;
    int currentHealth;
    int smallHealth = 5;
    int bigHealth = 10;
    public float despawnRate = 2f;
    public float despawnTime = 4f;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        rend = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();

        healthText.text = "Player 1: " + currentHealth;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("SmallCollectable") && currentHealth != 100)
        {
            Destroy(collision.gameObject);
            currentHealth += smallHealth;
            if (currentHealth > maxHealth)
            {
                currentHealth = maxHealth;
            }
            collectionSfx.Play();
        }

        if (collision.gameObject.CompareTag("BigCollectable") && currentHealth != 100)
        {
            Destroy(collision.gameObject);
            currentHealth += bigHealth;
            if (currentHealth > maxHealth)
            {
                currentHealth = maxHealth;
            }
            collectionSfx.Play();
        }
    }

    void Update()
    {
        healthText.text = "Player 1: " + currentHealth;
    }

    IEnumerator FadeOut()
    {
        for (float f = 1f; f >= -0.05f; f -= 0.05f)
        {
            Color c = rend.material.color;
            c.a = f;
            rend.material.color = c;
            yield return new WaitForSeconds(0.05f);
        }
    }

    public void startFading()
    {
        StartCoroutine("FadeOut");
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        meleeSfx.Play();

        anim.SetTrigger("Hurt");

        if (currentHealth <= 0)
        {
            Die();
            Invoke("startFading", despawnRate);
            Invoke("DespawnEnemy", despawnTime);
        }
        if (currentHealth < 0)
        {
            currentHealth = 0;
        }
    }

    void Die()
    {
        healthText.text = "Player 1: 0";
        Debug.Log("Enemy died!");

        anim.SetBool("IsDead", true);

        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;
        rb.bodyType = RigidbodyType2D.Static;
        deathSfx.Play();
    }

    void DespawnEnemy()
    {
        Destroy(gameObject);
    }
}
