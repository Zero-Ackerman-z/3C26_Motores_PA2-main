using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private InputActionsController controls;
    private Rigidbody2D myRB;
    [SerializeField] private float speed;
    private float limitSuperior;
    private float limitInferior;
    public int player_lives = 4;
    private Vector2 movimientoInput;
    private bool isInvulnerable = false;

    private void Awake()
    {
        controls = new InputActionsController();
        controls.Game.Move.performed += ctx => Movimiento(ctx);

    }
    private void OnEnable()
    {
        controls.Game.Enable();
    }

    private void OnDisable()
    {
        controls.Game.Disable();
    }
    void Start()
    {
        myRB = GetComponent<Rigidbody2D>();
        SetMinMax();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isInvulnerable)
        {
            if (movimientoInput.y > 0 && transform.position.y < limitSuperior)
            {
                myRB.velocity = new Vector2(0f, speed);
            }
            else if (movimientoInput.y < 0 && transform.position.y > limitInferior)
            {
                myRB.velocity = new Vector2(0f, -speed);
            }
            else
            {
                myRB.velocity = Vector2.zero;
            }
        }
    }

    public void Movimiento(InputAction.CallbackContext context)
    {
        // Obtener el valor de entrada del movimiento vertical
        movimientoInput = context.ReadValue<Vector2>();
    }
    void SetMinMax()
    {
        Vector3 bounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        limitInferior = -bounds.y;
        limitSuperior = bounds.y;
    }
    // Método para activar temporalmente la invulnerabilidad del jugador
    public void ActivateInvulnerability(float duration)
    {
        isInvulnerable = true;
        Invoke("DeactivateInvulnerability", duration);
    }

    // Método para desactivar la invulnerabilidad del jugador
    private void DeactivateInvulnerability()
    {
        isInvulnerable = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Candy")
        {
            ObjectGenerator.instance.ManageObjects(other.gameObject.GetComponent<ObjectsController>(), this);
        }
        else if (other.tag == "Enemy")
        {
            ObjectGenerator.instance.ManageObjects(other.gameObject.GetComponent<ObjectsController>(), this);
            StartCoroutine(HandlePlayerHit());
        }
    }
    IEnumerator HandlePlayerHit()
    {
        isInvulnerable = true;
        // Regresar al jugador al centro
        transform.position = new Vector3(transform.position.x, 0f, transform.position.z);
        // Esperar 1 segundo
        yield return new WaitForSeconds(1f);
        isInvulnerable = false;
    }
}
