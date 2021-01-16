using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_script : MonoBehaviour
{

    //карточка настройки персонажа
    public Card card;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            if (collision.gameObject.GetComponentInParent<Animator>().enabled == true)
            {
                collision.gameObject.GetComponentInParent<Animator>().enabled = false;
            }
            if (collision.gameObject.GetComponentInParent<Rigidbody>().isKinematic == true)
            {
                collision.gameObject.GetComponentInParent<Rigidbody>().isKinematic = false;
            }

            //ipt = gameObject.GetComponentInParent<Card>();
            player_test_controller script = GameObject.Find("Player_test").GetComponent<player_test_controller>();
            Card card = script.card;
            //script.card.shoot_speed

            collision.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.forward * script.card.shoot_speed /*card.shoot_speed*/, ForceMode.Impulse);

        }
    }
}
