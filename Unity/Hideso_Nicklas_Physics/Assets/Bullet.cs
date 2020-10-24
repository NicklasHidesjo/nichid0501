using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEditor.U2D;
using UnityEngine;
using UnityEngine.Audio;

public class Bullet : MonoBehaviour
{
    [SerializeField] AudioClip explosionSound = null;
    SpriteRenderer sprite;
    AudioSource audioPlayer;

    bool destroying = false;

    public float speed = 0f;

    // Start is called before the first frame update
    void Start()
    {
        audioPlayer = GetComponent<AudioSource>();
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (destroying) { return; }
        float xPos = transform.position.x + speed * Time.deltaTime;
        transform.position = new Vector3(xPos, transform.position.y, transform.position.z);
    }

    public void setBulletSpeed(float speed)
    {
        this.speed = speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!destroying) 
        {
            StartCoroutine(DestroyBullet());
            destroying = true;
        }
  
    }

    IEnumerator DestroyBullet()
    {
        audioPlayer.PlayOneShot(explosionSound);
        yield return new WaitForSeconds(explosionSound.length);
        Destroy(gameObject);
    }
}
