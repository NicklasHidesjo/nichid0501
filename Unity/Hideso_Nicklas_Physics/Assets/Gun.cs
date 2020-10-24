using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using TMPro;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.EventSystems;

public class Gun : MonoBehaviour
{
    [SerializeField] Bullet bullet = null;
    [SerializeField] float shootinVelocity = 10f;
    [SerializeField] AudioClip shotSound = null;
    [SerializeField] float fireDelay = 1f;

    AudioSource audioPlayer;

    bool firing = false;

    // Start is called before the first frame update
    void Start()
    {
        audioPlayer = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetAxis("Fire1") != 0)
        {
            if(!firing)
            {
                firing = true;
                StartCoroutine(Fire());
            }
        }
    }

    IEnumerator Fire()
    {
        bullet.setBulletSpeed(shootinVelocity);
        Bullet newBullet = bullet;
        Instantiate(newBullet, transform.position, Quaternion.identity);
        audioPlayer.PlayOneShot(shotSound);
        yield return new WaitForSeconds(fireDelay);
        firing = false;
    }
}
