using UnityEngine;

// Movimiento suave del player en 3D usando las flechas (o WASD).
// Requiere un Rigidbody en el mismo GameObject.
[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movimiento")]
    [SerializeField] private float velocidadMaxima = 6f;     // unidades por segundo
    [SerializeField] private float aceleracion = 20f;        // que tan rapido alcanza la velocidad
    [SerializeField] private float velocidadGiro = 10f;      // que tan rapido gira hacia donde camina

    private Rigidbody rb;
    private Vector3 entrada;          // direccion leida del teclado
    private Vector3 velocidadActual;  // velocidad suavizada

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        // Evita que el player se caiga o gire por las fisicas
        rb.constraints = RigidbodyConstraints.FreezeRotation;
    }

    private void Update()
    {
        // Leer input en Update (cada frame)
        float h = Input.GetAxisRaw("Horizontal"); // flechas izquierda/derecha
        float v = Input.GetAxisRaw("Vertical");   // flechas arriba/abajo
        entrada = new Vector3(h, 0f, v).normalized;
    }

    private void FixedUpdate()
    {
        // Velocidad objetivo segun la entrada
        Vector3 velocidadObjetivo = entrada * velocidadMaxima;

        // Suavizado: acelera y frena de forma progresiva
        velocidadActual = Vector3.MoveTowards(
            velocidadActual,
            velocidadObjetivo,
            aceleracion * Time.fixedDeltaTime
        );

        // Mover con fisicas para que los triggers funcionen bien
        rb.MovePosition(rb.position + velocidadActual * Time.fixedDeltaTime);

        // Girar suavemente hacia la direccion de movimiento
        if (entrada.sqrMagnitude > 0.01f)
        {
            Quaternion rotacionObjetivo = Quaternion.LookRotation(entrada);
            rb.MoveRotation(Quaternion.Slerp(
                rb.rotation,
                rotacionObjetivo,
                velocidadGiro * Time.fixedDeltaTime
            ));
        }
    }
}