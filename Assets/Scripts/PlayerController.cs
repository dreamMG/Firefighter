using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [Space(0)]
    [Header("Move")]
    [SerializeField] private CharacterController controller;
    [SerializeField] private float speed = 5.0f;
    [SerializeField] private float speedRotation = 75.0f;
    private Vector3 pos;
    [SerializeField] private Vector3 clickPos;

    [Header("Item")]
    public int indexItems = 0;
    public Text info;

    private float timer;
    private bool fireFighting = false;
    [SerializeField] private Extinguisher extinguisher;
    [SerializeField] private GameObject progressBar;

    private Fire fire;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        float v = Input.GetAxis("Vertical");
        float h = Input.GetAxis("Horizontal");

        pos = transform.forward * v + transform.up * -1f;

        transform.eulerAngles += new Vector3(0, h, 0) * Time.deltaTime * speedRotation;
        controller.Move(pos * Time.deltaTime * speed);

        MouseController();

        if (v != 0)
        {
            clickPos = Vector3.zero;
            fireFighting = false;
        }

        if (!fireFighting)
        {
            if (Vector3.Distance(transform.position, clickPos) > 0.2f && clickPos != Vector3.zero)
            {
                var direction = clickPos - transform.position;
                direction.y = transform.position.y;
                if (direction.magnitude > 0.2f)
                {
                    controller.SimpleMove(direction.normalized * speed);
                }
            }
        }
        else
        {
            if (Vector3.Distance(transform.position, clickPos) > 4f && clickPos != Vector3.zero)
            {
                var direction = clickPos - transform.position;
                direction.y = transform.position.y;
                if (direction.magnitude > 1f)
                {
                    controller.SimpleMove(direction.normalized * speed);
                }
            }            
        }

        if (info.gameObject.activeSelf)
        {
            timer += Time.deltaTime;
            if (timer > 1.5f)
            {
                timer = 0;
                info.gameObject.SetActive(false);
            }
        }

        FireFight();

        if (clickPos == Vector3.zero)
            fireFighting = false;
        
    }

    private void FixedUpdate()
    {
        if (extinguisher != null && fire != null && Vector3.Distance(transform.position, clickPos) < 5f && fireFighting)
        {
            extinguisher.Use(fireFighting);
            fire.FireFighter(0.5f);
        }
        else if (extinguisher != null)
            extinguisher.Use(false);
    }

    void MouseController()
    {
        if (Input.GetMouseButton(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 1000.0f, 1))
            {
                if (hit.collider.CompareTag("Item"))
                {
                    if (Vector3.Distance(transform.position, hit.collider.transform.position) < 3f)
                    {
                                hit.collider.gameObject.GetComponent<PickUp>().PickUpItem();
                    } else
                    {
                        clickPos = hit.point;
                    }
                }
                if (hit.collider.CompareTag("Ground"))
                {
                    clickPos = hit.point;

                    Quaternion targetRotation = Quaternion.LookRotation(hit.point - transform.position);
                    transform.rotation = Quaternion.Euler(0, targetRotation.eulerAngles.y, 0);
                    fireFighting = false;
                }
            }
        }

    }

    void FireFight()
    {
        if (Input.GetMouseButton(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (extinguisher == null)
            {
                info.gameObject.SetActive(true);
                info.text = "You need Extinguisher";
            }

            if (Physics.Raycast(ray, out hit, 1000.0f, LayerMask.GetMask("Fire")))
            {
                progressBar.SetActive(true);

                fire = hit.collider.gameObject.GetComponent<Fire>();

                if (indexItems == 3 && Inventory.instance.items.Count == 0)
                {
                    clickPos = hit.point;

                    fireFighting = true;

                    Quaternion targetRotation = Quaternion.LookRotation(hit.point - transform.position);
                    transform.rotation = Quaternion.Euler(0, targetRotation.eulerAngles.y, 0);
                }
                else if (indexItems == 3 && Inventory.instance.items.Count != 0)
                {
                    info.gameObject.SetActive(true);
                    info.text = "YOU MUST WEAR ALL STAFFS";
                }
            }
        }

        if (Input.GetMouseButtonUp(1))
        {
            fireFighting = false;
            clickPos = Vector3.zero;
            progressBar.SetActive(false);

        }
    }

    public void SetExtinguisher(Extinguisher extinguisher)
    {
        this.extinguisher = extinguisher;
    }
}
