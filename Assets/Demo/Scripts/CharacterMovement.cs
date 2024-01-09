using UnityEngine;

namespace Kha.UI.Demo
{
    public class CharacterMovement : MonoBehaviour
    {
        [SerializeField] private float _movementSpeed = 2.0f;
        [SerializeField] private float _rotationSpeed = 2.0f;

        private void Update()
        {
            var moveHorizontal = Input.GetAxis("Horizontal");
            var moveVertical = Input.GetAxis("Vertical");

            transform.position += transform.forward * (moveVertical * Time.deltaTime * _movementSpeed);
            transform.Rotate(0, moveHorizontal * _rotationSpeed * Time.deltaTime, 0, Space.Self);
        }
    }
}