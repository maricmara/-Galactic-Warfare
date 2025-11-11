using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform alvo;   // o jogador
    public Vector3 offset;   // distância da câmera para o jogador

    void LateUpdate()
    {
        transform.position = alvo.position + offset;
    }
}
