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
        //cf.force.Set(50f, 0, 0);
        //Debug.Log(rb);
        rb.AddForce(10f,0,0);
    }
	
	// Update is called once per frame
	void Update () {
        if (manager == null)
            manager = FindObjectOfType<GameManager>();

        if (manager.runningGame && transform.position.x > 2 && !point)
        {
            //manager.addScore(1);
            //point = true;
        }
	    if((transform.position.x > 5) || (!manager.runningGame && transform.position.x < 0)){
            Destroy(gameObject);
            manager.addScore(1);
            point = true;
        }
	}

    public void OnCollisionEnter(Collision collisionInfo)
    {
        if (collisionInfo.collider.tag == "Player" /* && manager.remaining_invulnarebility > 0*/)
        {
            Destroy(gameObject);
            GameObject fractObj = Instantiate(fracturedFruit) as GameObject;
            Debug.Log(fractObj.name);
            fractObj.GetComponent<ExplodeFruitsScript>().ExplodeFruits();
        }
        if (collisionInfo.collider.tag == "Player" && !manager.noclip)
        {
            if (manager.remaining_invulnarebility == 0) {
                if (manager.extralife == 0){
                    manager.EndGame(); 
                    return;
                }
                manager.addExtralife(-1);
                manager.setInvulnarebility(20);
                Destroy(gameObject);
            }
        }
        
    }
}
