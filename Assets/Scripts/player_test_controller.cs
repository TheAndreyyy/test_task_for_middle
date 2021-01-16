using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityStandardAssets.Characters.ThirdPerson;

public class player_test_controller : MonoBehaviour
{
    //карточка настройки персонажа
    public Card card;

    #region настройки игрока

    //агент для анимации игрока
    private NavMeshAgent myAgent;

    #endregion

    #region настройки пули, выстрела

    //сила выстрела
    int shot_force;

    //можем стрелять или нет
    public bool Can_shoot;

    //префаб пули
    public GameObject prefab;

    //позиция выстрела
    public Transform shoot_position;

    //префаб точки выстрела
    public GameObject GO_Cursor;

    #endregion

    #region настройки окружения

    //где можем кликнуть (условно где земля)
    public LayerMask whatCanBeClickedOn;

    //дистанция определения курсора
    public float distance = 10.0f;

    SpriteRenderer spriteRenderer;
    int layerMask;

    #endregion

    void Start()
    {
        GO_Cursor = GameObject.Find("Cursor");

        myAgent = GetComponent<NavMeshAgent>();

        spriteRenderer = GetComponent<SpriteRenderer>();
        layerMask = LayerMask.GetMask("Ground");
    }

    void Update()
    {
        //ПКМ - передвижение
        if (Input.GetMouseButton(1))
        {
            Ray myRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;

            if (Physics.Raycast(myRay, out hitInfo, 100, whatCanBeClickedOn))
            {
                myAgent.speed = card.player_speed;
                myAgent.SetDestination(hitInfo.point);
            }
        }

        void LookOnCursor()
        {
            //заставляет персонажа следить за курсором мышки 		
            Plane playerPlane = new Plane(Vector3.up, transform.position);
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            float hitdist = 0;
            if (playerPlane.Raycast(ray, out hitdist))
            {
                Vector3 targetPoint = ray.GetPoint(hitdist);
                Quaternion targetRotation = Quaternion.LookRotation(targetPoint - transform.position);
                //transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, lookspeed * Time.deltaTime);
                transform.rotation = targetRotation;
            }
        }

        //ЛКМ - стрельба
        if (Input.GetMouseButton(0) && Can_shoot)
        {
            shoot();
            LookOnCursor();
        }

    }

    void shoot()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 1000, layerMask))
        {
            GO_Cursor.transform.position = new Vector3(hit.point.x, hit.point.y, hit.point.z);
            Debug.DrawRay(Camera.main.transform.position, GO_Cursor.transform.position - Camera.main.transform.position, Color.green);
            shoot_position.LookAt(GO_Cursor.transform.position);
            GameObject go = Instantiate(prefab, shoot_position.position, shoot_position.rotation);
            go.GetComponent<Rigidbody>().AddForce(go.transform.forward * card.shot_multiplyer);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "shoot_zone")
        {
            Can_shoot = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "shoot_zone")
        {
            Can_shoot = false;
        }
    }
}
