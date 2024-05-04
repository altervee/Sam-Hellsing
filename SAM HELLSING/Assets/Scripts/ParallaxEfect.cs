
using UnityEngine;

public class ParallaxEfect : MonoBehaviour
{
    [SerializeField] private float parallaxMultiplayer;
    private Transform cameraTransform;
    private Vector3 previusCameraPosition;
    private float spriteWidth;
    private float starPosition;
    void Start()
    {
        cameraTransform = Camera.main.transform;
        previusCameraPosition = cameraTransform.position;
        spriteWidth = GetComponent<SpriteRenderer>().bounds.size.x;
        starPosition = transform.position.x;
    }

    void LateUpdate()
    {
     // the res of the position betewn frames
        float deltaX = (cameraTransform.position.x - previusCameraPosition.x)* parallaxMultiplayer;
        float moveAmount = cameraTransform.position.x * (1 - parallaxMultiplayer);// cuanto se movio la camara resspecto a la capa
        transform.Translate(new Vector3(deltaX,0,0));
        previusCameraPosition = cameraTransform.position;
        if(moveAmount > starPosition + spriteWidth)
        {
            transform.Translate(new Vector3(spriteWidth,0,0));
            starPosition += spriteWidth;
        }else if(moveAmount < starPosition - spriteWidth) {
            transform.Translate(new Vector3(-spriteWidth, 0,0));
            starPosition -= spriteWidth;
            }
    }
}
