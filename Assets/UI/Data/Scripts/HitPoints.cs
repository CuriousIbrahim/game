using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HitPoints : MonoBehaviour
{
    public int maxHealth = 6;
    public int currentHealth;

    public PlayerController player;

    public float invincLength = 2;
    private float invincCounter;

    public Renderer playerRender;
    private float flashCounter;
    public float flashLength = 0.1f;

    private bool isRespawning;
    private Vector3 respawnPoint;
    public float respawnLength;

    public Text HPText;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        //player = FindObjectOfType<PlayerController>();
        respawnPoint = player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (invincCounter > 0)
        {
            invincCounter -= Time.deltaTime;

            flashCounter -= Time.deltaTime;

            if (flashCounter <= 0)
            {
                playerRender.enabled = !playerRender.enabled;
                flashCounter = flashLength;
            }

            if (invincCounter <= 0)
            {
                playerRender.enabled = true;
            }
        }
        HPText.text = "HP: " + ((currentHealth/3) + 1);
    }

    public void hurtPlayer(int damage, Vector3 direction)
    {
        if (invincCounter <= 0)
        {
            currentHealth -= damage;
        
            if(currentHealth < 0)
            {
                Respawn();
            }
            else
            { 
                player.knockBack(direction);
                invincCounter = invincLength;
                playerRender.enabled = false;
                flashCounter = flashLength;
            }

        }
    }

    public void healPlayer(int healAmmount)
    {
        currentHealth += healAmmount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }

    public void Respawn()
    {
        player.transform.position = respawnPoint;
        currentHealth = maxHealth;

        if(!isRespawning)
        { 
        StartCoroutine("RespawnCo");
        }
    }

    public IEnumerator RespawnCo()
    {
        isRespawning = true;
        player.gameObject.SetActive(false);

        yield return new WaitForSeconds(respawnLength);

        isRespawning = false;
        player.gameObject.SetActive(true);

        GameObject thePlayer = GameObject.Find("Player");
        CharacterController charController = player.GetComponent<CharacterController>();
        charController.enabled = false;
        player.transform.position = respawnPoint;
        charController.enabled = true;

        currentHealth = maxHealth;
        invincCounter = invincLength;
        playerRender.enabled = false;
        flashCounter = flashLength;
    }

    public void SetSpawnPoint(Vector3 newSpawnPosition)
    {
        respawnPoint = newSpawnPosition;
    }
}
