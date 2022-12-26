using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ParticleSystemJobs;

public class PlayerManagement: MonoSingleton<PlayerManagement>
{
    [SerializeField] private GameObject SnowBall;
    [SerializeField] private GameObject lowSnowMan;
    [SerializeField] private GameObject averageSnowMan;
    [SerializeField] private GameObject highSnowMan;


    [SerializeField] private PlayerRunner playerRunner;
    [SerializeField] private GameObject player;

    private bool canRun = true;
    private bool isStarted = false;

    public bool isFinished = false;

    private Vector3 scaleChange;
    private float scaleChangeAmount = 0.1f;
    private int status = 0;
    [SerializeField] private float UpScaleAmount = 2f;
    [SerializeField] private float DownScaleAmount = 0.7f;

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && canRun)
        {
            playerRunner.StartToRun();
            SnowBall.GetComponent<Animator>().enabled = true;
            isStarted = true;
            canRun = false;
        }
        Debug.Log("Status: " + status);
    }

    public void ChangeScale(float scaleAmount) {
        scaleChange = new Vector3(scaleAmount, scaleAmount, scaleAmount);
        player.transform.localScale += scaleChange;
        if (status == 3 && player.transform.localScale.x >= 1f) {
            player.transform.localScale = new Vector3(1f, 1f, 1f);
        }
        if (status == 0) { //SNOWBALL
            
            if (player.transform.localScale.x > UpScaleAmount) {
                player.transform.localScale = new Vector3(1f, 1f, 1f);
                status = 1;
                ChangeApperance(status);
                PlayParticle();
                return;
            }
            else if (player.transform.localScale.x < scaleChangeAmount) {
                FailedLvl();
            }
        }
        if (status == 1) {//lowSNOWMAN
            
            if (player.transform.localScale.x < DownScaleAmount)
            {
                status = 0;
                ChangeApperance(status);
                PlayParticle();
                player.transform.localScale = new Vector3(1f, 1f, 1f);
                return;
            }
            else if(player.transform.localScale.x > UpScaleAmount) {
                status = 2;
                ChangeApperance(status);
                PlayParticle();
                player.transform.localScale = new Vector3(1f, 1f, 1f);
                return;
            }
        }
        if (status == 2) { //averageSnowMan
            if (player.transform.localScale.x < DownScaleAmount)
            {
                
                status = 1;
                ChangeApperance(status);
                PlayParticle();
                player.transform.localScale = new Vector3(1f, 1f, 1f);
                return;
            }
            else if (player.transform.localScale.x > UpScaleAmount)
            {
                status = 3;
                ChangeApperance(status);
                PlayParticle();
                player.transform.localScale = new Vector3(1f, 1f, 1f);
                return;
            }

        }
        if (status == 3) { //highSnowMan
            if (player.transform.localScale.x < DownScaleAmount)
            {
                status = 2;
                ChangeApperance(status);
                PlayParticle();
                player.transform.localScale = new Vector3(1f, 1f, 1f);
                return;
            }
          
        }

    }

    void ChangeApperance(int statusValue) {
        if (status == 0) {
            lowSnowMan.SetActive(false);
            SnowBall.SetActive(true);
        }
        if (status == 1) {
            SnowBall.SetActive(false);
            lowSnowMan.SetActive(true);
            averageSnowMan.SetActive(false);
            highSnowMan.SetActive(false);
        }
        if (status == 2)
        {
            lowSnowMan.SetActive(false);
            averageSnowMan.SetActive(true);
            highSnowMan.SetActive(false);
        }
        if (status == 3)
        {
            lowSnowMan.SetActive(false);
            averageSnowMan.SetActive(false);
            highSnowMan.SetActive(true);
        }
    }
    public void CanRun()
    {
        canRun = true;
    }
    public void FailedLvl()
    {
        playerRunner.SetRunning(false);
        playerRunner.SetShoot(false);
        SnowBall.GetComponent<Animator>().enabled = false;
        UIManager.Instance.RestartButtonUI();
    }
    public void StartToNextLvl()
    {
        playerRunner.SetRunning(false);
        playerRunner.SetShoot(false);
        SnowBall.GetComponent<Animator>().enabled = false;
        canRun = false;
        UIManager.Instance.NextLvlUI();
    }
    void PlayParticle() {
        var particle = ObjectPooler.Instance.GetPooledObject("ChangeParticle");
        particle.transform.position = player.transform.position+new Vector3(0f,1f,-1f);
        particle.transform.rotation = player.transform.rotation;
        particle.SetActive(true);
        particle.GetComponent<ParticleSystem>().Play();
    }

    public void CharacterReset()
    {
        //change status
        status = 0;
        isStarted = false;
        SnowBall.SetActive(true);
        lowSnowMan.SetActive(false);
        averageSnowMan.SetActive(false);
        highSnowMan.SetActive(false);
        transform.position = new Vector3(0f, 0.125f, 0f);
        player.transform.localScale = new Vector3(1f, 1f, 1f);
        PlayerManagement.Instance.isFinished = false;
        UIManager.Instance.TapToPlay();
    }

    public int GetStatus() {
        return status;
    }
}
