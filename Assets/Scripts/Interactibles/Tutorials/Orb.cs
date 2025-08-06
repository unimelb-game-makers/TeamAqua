using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Orb : MonoBehaviour
{
    /*
        Fade in when activated
        Play spin animation
    */
    public Orb nextOrb;
    public float normalRotationSpeed = 10;
    public float fastRotationSpeed = 500;

    [SerializeField]
    SpriteRenderer spriteRenderer;

    [SerializeField]
    PostProcessManager postProcessVolume;

    private float rotateSpeed;
    private bool playerIn = false;

    private void Start()
    {
        // Fade out for now
        // Color tmp = spriteRenderer.color;
        // tmp.a = 0f;
        // spriteRenderer.color = tmp;

        rotateSpeed = normalRotationSpeed;
    }

    private void Update()
    {
        transform.Rotate(0, 0, rotateSpeed * Time.deltaTime);

        if (playerIn && Input.GetKeyDown(KeyCode.E) && (!nextOrb))
        {   
            FadeController.FadeToScene("NoonIsland");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Tween scale big
        if (other.CompareTag("Player"))
        {
            LeanTween.scale(gameObject, new Vector3(.1f, .1f, 1), .75f);
            LeanTween.value(
                postProcessVolume.gameObject,
                postProcessVolume.bloom.intensity.value,
                3,
                1
            );
            rotateSpeed = fastRotationSpeed;
            playerIn = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Tween scale small
        if (other.CompareTag("Player"))
        {
            LeanTween.scale(gameObject, new Vector3(.05f, .05f, 1), .75f);
            LeanTween.value(
                postProcessVolume.gameObject,
                postProcessVolume.bloom.intensity.value,
                1,
                1
            );
            rotateSpeed = normalRotationSpeed;
            playerIn = false;
        }
    }
}
