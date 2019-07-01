using UnityEngine;
using System.Collections;


public class Sphare : MonoBehaviour {

    Rigidbody rb;
    ConstantForce cf;
    GameManager manager;
    bool point = false;
    public GameObject fracturedFruit;

    void Start() {
        rb = GetComponent<Rigidbody>();
        cf = GetComponent<ConstantForce>();
        //Debug.Log(rb);
        rb.AddForce(10f,0,0);
    }
	
	// Update is called once per frame
	void Update () {
        if (manager == null)
            manager = FindObjectOfType<GameManager>();

        if ((transform.position.x > 5) || (!manager.runningGame && transform.position.x < 0))
        {
            Destroy(gameObject);
            if (manager.extralife > 0)
                manager.addExtralife(-1);
            point = true;
        }

        if (manager.extralife <= 0)
        {
            manager.extralife = 0; // be sure we dont get a negative one
            manager.EndGame();
            return;
        }
    }

    public void OnCollisionEnter(Collision collisionInfo)
    {
        if (collisionInfo.collider.tag == "Player")
        {
            Destroy(gameObject);
            GameObject fractObj = Instantiate(fracturedFruit) as GameObject;
            Debug.Log(fractObj.name);
            fractObj.GetComponent<ExplodeFruitsScript>().ExplodeFruits();
            manager.addScore(1);
        }
        
    }
}
