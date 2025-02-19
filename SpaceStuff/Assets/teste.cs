using UnityEngine;

public class MoverImagem : MonoBehaviour
{
    public float velocidade = 5f;  // Velocidade de movimento

    // Update é chamado uma vez por quadro
    void Update()
    {
        // Movimento para a direita usando as teclas direcionais (ou outras teclas)
        float movimentoHorizontal = Input.GetAxis("Horizontal");

        // Mover o sprite na direção horizontal
        transform.Translate(Vector3.right * movimentoHorizontal * velocidade * Time.deltaTime);
    }
}

