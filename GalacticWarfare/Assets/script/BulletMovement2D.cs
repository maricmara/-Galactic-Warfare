using UnityEngine;

public class BulletMovement2D : MonoBehaviour
{
    // A velocidade da bala
    public float speed = 25f;
    // Tempo de vida
    public float lifetime = 3f;

    // Rigidbody2D é o ideal para movimento baseado em física
    private Rigidbody2D rb; 

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // 1. Aplica a velocidade usando o Rigidbody2D (melhor performance em 2D).
        // 'transform.right' aponta para a direção "direita" local da bala.
        // Se a bala estiver virada para a direita, ela vai no eixo X positivo.
        rb.linearVelocity = transform.right * speed;
        
        // 2. Agenda a destruição para limpar a cena.
        Destroy(gameObject, lifetime);
    }

    // Usamos OnTriggerEnter2D para detectar colisões 2D (com o Collider2D marcado como "Is Trigger")
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            // Destruir o inimigo e a bala
            // Nota: No seu GDD, inimigos têm HP. Você deve subtrair o HP em vez de destruir imediatamente.
            // Para testar o movimento, essa lógica de destruição serve.
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
    
    // O método Update() não é mais necessário, pois o movimento é controlado pelo Rigidbody2D no Start().
}