using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Animations;

public class Plane : MonoBehaviour
{
    public bool alive = true;
    public bool inGame = true;

    [Header("Normal Controls")]
    public bool canFly = true;
    public Transform nextPoint;
    public float distanceToNext;
    public Vector3 vecDif;
    public float speed;
    public float timeToFlyAgain = 2f;

    [Header("Juice")]
    public float impulseDelay = 0.2f;
    public float arriveDelay = 0.3f;

    [Header("Rotation")]
    public float rotSpeed = 3f;

    [Header("Animations")]
    public Animator animator;
    public string takeOffName = "TakeOff";
    public string arriveName = "Arrive";
    public float takeoffAnimDuration;
    public string deadName = "Dead";
    public float deadTime = 0.25f;
    public float timeToShowEndPanel = 0.5f;

    [Header("Camera")]
    public CamFollow camFollow;

    [Space]
    public bool waitingForNext;

    [Header("Color")]
    public ColorInfo currColor;
    public SpriteRenderer SR;
    public int colorOrbsLayer;

    [Header("Obstacles")]
    public int obstaclesLayer;


    [Header("Collider")]
    public Collider2D shipColl;

   

    public UnityEvent OnStartTavel;
    public UnityEvent OnReachPoint;
    public UnityEvent OnDead;

    void Start()
    {
        
    }


    void Update()
    {
        if (!inGame) return;

        GetTargetDifference();
        if (!waitingForNext)
        { RotateTo(nextPoint.transform.position); }

    }

    public void ActiveGoToNext()
    {
        if (!inGame) return;
        if (!canFly) return;
        StartCoroutine(GoToNext());
    }

    IEnumerator GoToNext()
    {
        #region old
        ////get Impulse
        //float t = 0f;
        //float v = 0f;

        //while (t < impulseDelay)
        //{
        //    t += Time.deltaTime;

        //    this.transform.Translate(-GetTargetDifference().normalized * v * Time.deltaTime);

        //    v += (speed / impulseDelay) * Time.deltaTime;

        //    yield return null;

        //}
        //waitingForNext = true;

        ////Normal movement
        //GetDistanceToNext();
        //while (distanceToNext >= speed * Time.deltaTime)
        //{
        //    Vector3 pos = this.transform.position;
        //    //pos = Vector3.Lerp(pos, nextPoint.position , speed * Time.deltaTime);
        //    pos += GetTargetDifference().normalized * speed * Time.deltaTime;
        //    this.transform.position = pos;
        //    GetDistanceToNext();
        //    yield return null;
        //}

        ////Debug.Log (this.transform.position);
        //v = speed;

        ////Arrive Offset
        //float arriveT = 0f;

        //while (arriveT <= arriveDelay)
        //{
        //    arriveT += Time.deltaTime;
        //    if (arriveT <= arriveDelay / 2f)
        //    {
        //        this.transform.Translate(this.transform.up * v * Time.deltaTime);
        //    }
        //    else
        //    {
        //        this.transform.Translate(-this.transform.up * v * Time.deltaTime);
        //    }

        //    v -= (speed / arriveDelay) * Time.deltaTime;

        //    yield return null;

        //}
        ////Debug.Log(this.transform.position);

        //Vector3 endPos = this.transform.position;
        //endPos = nextPoint.position;
        //this.transform.position = endPos;

        ////just for test
        //SetNextpoint();
        #endregion

       
        canFly = false;

        animator.SetTrigger(takeOffName);
        yield return new WaitForSeconds(takeoffAnimDuration);

        SoundManager.instance.PlayFly();

        //Normal movement
        GetDistanceToNext();
        while (distanceToNext >= speed * Time.deltaTime && alive)
        {
            Vector3 pos = this.transform.position;
            //pos = Vector3.Lerp(pos, nextPoint.position , speed * Time.deltaTime);
            pos += GetTargetDifference().normalized * speed * Time.deltaTime;
            this.transform.position = pos;
            GetDistanceToNext();
            yield return null;
        }

        if (alive)
        {
            this.transform.position = nextPoint.transform.position;
            camFollow.StartMovement(nextPoint);//shall tell the camera to look at the point I am in
            WayPointsManager.instance.HandleReachPos();
            nextPoint = WayPointsManager.instance.GetFirstPoint(); // get next Point
            waitingForNext = true;

            animator.SetTrigger(arriveName);

            yield return new WaitForSeconds(timeToFlyAgain);
            canFly = true;
            waitingForNext = false;

        }
       

       
        
    }

    public float GetDistanceToNext()
    {
        distanceToNext = Vector3.Distance(nextPoint.transform.position , this.transform.position);
        return distanceToNext;
    }

    public Vector3 GetTargetDifference()
    {
        //if (nextPoint.transform.position.x > this.transform.position.x)
        //{
        //    vecDif.x = nextPoint.transform.position.x - this.transform.position.x;
        //}
        //else
        //{
        //    vecDif.x =  this.transform.position.x - nextPoint.transform.position.x;
        //}

        //if (nextPoint.transform.position.y > this.transform.position.y)
        //{
        //    vecDif.y = nextPoint.transform.position.y - this.transform.position.y;
        //}
        //else
        //{
        //    vecDif.y = this.transform.position.y - nextPoint.transform.position.y;
        //}

        vecDif.x = nextPoint.transform.position.x - this.transform.position.x;
        vecDif.y = nextPoint.transform.position.y - this.transform.position.y;

        return vecDif;
    }

    public void RotateTo(Vector3 point)
    {
        Vector3 dif = new Vector3();
        dif.x = point.x - this.transform.position.x;
        dif.y = point.y - this.transform.position.y;

        float angle = Mathf.Atan2(dif.x, dif.y) * Mathf.Rad2Deg;

        Quaternion lookQ = Quaternion.Euler(new Vector3(0, 0, -angle));

        this.transform.rotation = Quaternion.Lerp(this.transform.rotation, lookQ, Time.deltaTime * rotSpeed);
    }

    public void RotateToDir(Vector3 dir)
    {
        float angle = Mathf.Atan2(dir.x, dir.y) * Mathf.Rad2Deg;

        Quaternion lookQ = Quaternion.Euler(new Vector3(0, 0, -angle));

        this.transform.rotation = Quaternion.Lerp(this.transform.rotation, lookQ, Time.deltaTime * rotSpeed);
    }

    public void SetNextpoint()
    {
        waitingForNext = false;
        //set next point
    }

    public void GetCurrentpoint()
    {
        SetNextpoint();
        //enableColls?
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        //print("Trigger");
        //Orbs
        if (collision.gameObject.layer == 11)
        {
            var waypoint = collision.gameObject.GetComponent<Waypoint>();
            if (waypoint == null) return;

            if (waypoint.colorChanger)
            {
                print("Color Changer");
                this.ChangeColor(waypoint.color);
                SoundManager.instance.PlayChangeColor();
            }
            else
            {
                SoundManager.instance.PlayGetPoint();
            }

            //Sumar un punto
            OnReachPoint.Invoke();
          
        }
        else if (collision.gameObject.layer == 12)
        {
            if (this.currColor.index == 0) return;

            var colorElemment = collision.gameObject.GetComponent<ColorElement>();
            if (colorElemment == null) return;

            if (colorElemment.color.index == this.currColor.index)
            {
                return;
            }
            else
            {
                Debug.Log("Perdiste, tocaste a:  " + colorElemment.gameObject.name);
                animator.SetTrigger(deadName);
                SoundManager.instance.PlayCrash();

                alive = false;
                inGame = false;

                shipColl.enabled = false;
                Invoke("CallDeadRoutine", deadTime);
            }
        }

    }

    public void CallDeadRoutine()
    {
        StartCoroutine(DeadCoroutine());
    }

    IEnumerator DeadCoroutine()
    {
        yield return new WaitForSeconds(timeToShowEndPanel );

        OnDead.Invoke();
        //for (int i = 0; i < this.transform.childCount; i++)
        //{
        //    this.transform.GetChild (i).gameObject.SetActive (false);
        //}
        //this.gameObject.SetActive(false);

        shipColl.enabled = false;

    }

    public void ChangeColor(ColorInfo newColor)
    {
        currColor = newColor;
        SR.color = currColor.color;
    }

    public void Resurect()
    {


        animator.CrossFade("Resurect", 0.01f);
        Vector3 pos = this.transform.position;
        pos = nextPoint.transform.position;
        this.transform.position = pos;

        for (int i = 0; i < this.transform.childCount; i++)
        {
            this.transform.GetChild(i).gameObject.SetActive(true);
        }

        //Continue journey

        //Check if the next point shall change color
        var nextWayPointScript = nextPoint.gameObject.GetComponent<Waypoint>();
        if (nextWayPointScript.colorChanger)
        {
            this.currColor = nextWayPointScript.color;
        }
        //or use White Color

        camFollow.StartMovement(nextPoint);//shall tell the camera to look at the point I am in
        WayPointsManager.instance.HandleReachPos();
        nextPoint = WayPointsManager.instance.GetFirstPoint(); // get next Point

        

        canFly = true;
        waitingForNext = false;

        alive = true;
        inGame = true;

        shipColl.enabled = true;


    }


}
