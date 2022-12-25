using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRunner: MonoBehaviour
{
    [SerializeField] private float runningSpeed;
    [SerializeField] private float xSpeed;
    [SerializeField] private float limitx;

    [SerializeField] private bool enabled = true;
    [SerializeField] private bool running = false;

   
    private bool isTouched;
    private bool canShoot;
    [SerializeField]
    private Transform firePosition;
    void Update()
    {
        if (running) { 
            SwipeCheck();
            canShoot = true;
        }
        
        if (Input.GetMouseButtonDown(0)&&canShoot) {
            isTouched = true;
            PlayerManagement.Instance.ChangeScale(-0.1f);        
        }
    }

    void FixedUpdate()
    {
        if (isTouched) { 
            fire();
            isTouched = false;
        }
    }


    void fire()
    {
        GameObject obj = ObjectPooler.Instance.GetPooledObject("SnowBall");
        obj.transform.position = firePosition.position;
        obj.transform.rotation = firePosition.rotation;
        obj.SetActive(true);
        obj.GetComponent<Rigidbody>().velocity = Vector3.forward * 50;
    }
    void SwipeCheck()
    {
        float touchXDelta = 0;
        float newX = 0;

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            touchXDelta = Input.GetTouch(0).deltaPosition.x / Screen.width;
        }
        else if (Input.GetMouseButton(0))
        {
            touchXDelta = Input.GetAxis("Mouse X");
        }

        newX = transform.position.x + xSpeed * touchXDelta * Time.deltaTime;
        newX = Mathf.Clamp(newX, -limitx, limitx);

        Vector3 newPosition = new Vector3(newX, transform.position.y, transform.position.z + runningSpeed * Time.deltaTime);
        transform.position = newPosition;
    }

    public void StartToRun()
    {
        if (enabled)
        {
            running = true;

        }
    }
    public void SetEnabled(bool value)
    {
        enabled = value;
    }
    public void SetRunning(bool value)
    {
        running = value;
    }
    public void SetShoot(bool value) {
        canShoot = value;
    }
}
