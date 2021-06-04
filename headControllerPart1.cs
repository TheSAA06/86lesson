using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class headControllerPart1 : MonoBehaviour
{
    float xRotate;
    float yRotate;
    float sens = 3f;
    public GameObject scope;
    public GameObject pressE;
    public Transform hand;
    public ParticleSystem shoot;
    Image scope_image;
   
    void Start()
    {
        scope_image = scope.GetComponent<Image>();
        shoot.Stop();
    }

    // Update is called once per frame
    void Update()
    {
     
        xRotate = xRotate - Input.GetAxis("Mouse Y") * sens;
        yRotate = yRotate + Input.GetAxis("Mouse X") * sens;
        xRotate = Mathf.Clamp(xRotate, -90, 90);
        transform.rotation = Quaternion.Euler(xRotate, yRotate, 0);
        FindObjectOfType<BodyController>().SomeMethod(yRotate);
        scope_image.color = Color.green;
        pressE.SetActive(false);

        Debug.DrawRay(transform.position, transform.forward * 4f, Color.red);

        RaycastHit hit;
        if(Physics.Raycast(transform.position, transform.forward, out hit, 4f))
        {
            if(hit.collider.gameObject.tag == "Bottle")
            { 
                scope_image.color = Color.red;
                pressE.SetActive(true);
                if(Input.GetKeyDown("e"))
                {
                    print("Взял бутылку");
                    hit.transform.position = hand.transform.position;
                    hit.transform.SetParent(hand.transform);
                }
                if(Input.GetMouseButtonDown(0))
                {
                    Destroy(hit.transform.gameObject);
                }
            }
            if(hit.collider.gameObject.tag == "monster")
            { 
                
            }
        }
        if(Input.GetKeyDown("g"))
        {
            print("отпустил бутылку");
            //hand.DetachChildren();
            //transform baby = hand.GetChild(0);

        }
        if(Input.GetMouseButtonDown(0))
        {
            shoot.Play();
        }
    }
    void OnCollisionEnter(Collision obj)
    {

    }
}
