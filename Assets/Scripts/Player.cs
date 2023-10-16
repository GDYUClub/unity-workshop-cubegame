using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    private Inputs playerInputs;
    private Rigidbody rb;

    public float moveForce;
    public ParticleSystem deathParts;

    private bool isDead;

    // Start is called before the first frame update
    void Start()
    {
        playerInputs = new Inputs();
        playerInputs.Enable();
        rb = GetComponent<Rigidbody>();
    }

    // FixedUpdate is called 50 times per sec
    void FixedUpdate() 
    {
        if (isDead == true) // Prevent movement when dead
        {
            return;
        }

        Vector2 moveInput = playerInputs.Player.Movement.ReadValue<Vector2>();
        Vector3 moveVect = new Vector3(moveInput.x, 0, moveInput.y);

        rb.AddForce(moveVect * moveForce);

        if (transform.position.y < -1) // Kill player if they fall off the map
        {
            StartCoroutine(Die());
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Enemy")
        {
            StartCoroutine(Die());
        }
        else if (col.tag == "Goal")
        {
            int buildIndex = SceneManager.GetActiveScene().buildIndex + 1;
            if (buildIndex > SceneManager.sceneCount)
            {
                buildIndex = 0;
            }
            SceneManager.LoadScene(buildIndex);
        }
    }

    public IEnumerator Die()
    {
        if (isDead == true) // Prevent death when already dead
        {
            yield break;
        }

        isDead = true;
        GetComponent<MeshRenderer>().enabled = false;
        rb.constraints = RigidbodyConstraints.FreezeAll;
        deathParts.Play();

        yield return new WaitForSeconds(2f); // Wait 2 sec

        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reload the current scene/level
    }

}
