using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CollidableObjects: MonoBehaviour
{

   public ObjectType objectType;
   private bool collidedPlayer;
    [SerializeField] private TextMeshProUGUI obstacleText;
    [SerializeField] private int Obstacle_Count;
   public enum ObjectType{
        Snow,
        Heater,
        Obstacle,
        FinishLine
    }
    private void Start()
    {
        if (objectType == ObjectType.Obstacle)
            obstacleText.text = Obstacle_Count.ToString();
    }

    private void OnCollisionEnter(Collision collision)
    {

        if (objectType == ObjectType.Heater && !collidedPlayer) {
            
            //particle ObjectPool
            PlayerManagement.Instance.ChangeScale(-0.1f);
            collidedPlayer = true;
        }
        if (objectType == ObjectType.Obstacle && !collidedPlayer) {
            //particle ObjectPool
            PlayerManagement.Instance.ChangeScale(-0.1f);
            collidedPlayer = true;
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (objectType == ObjectType.Snow) {
            var particle = ObjectPooler.Instance.GetPooledObject("CollectBall");
            particle.transform.position = transform.position;
            particle.transform.rotation = transform.rotation;
            particle.SetActive(true);
            particle.GetComponent<ParticleSystem>().Play();
            gameObject.SetActive(false);
            PlayerManagement.Instance.ChangeScale(0.1f);
        }
        if (objectType == ObjectType.Obstacle)
        {
            if (other.CompareTag("SnowBall"))
            {
                Obstacle_Count -= 1;
                obstacleText.text = Obstacle_Count.ToString();
                if (Obstacle_Count == 0)
                {
                    gameObject.SetActive(false);
                }
            }
        }
        if (objectType == ObjectType.FinishLine) {
            Invoke("FinishedAction", 1.75f);
        }
    }

    void FinishedAction() {
        PlayerManagement.Instance.StartToNextLvl();
    }


}
