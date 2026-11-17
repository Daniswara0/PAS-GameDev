
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Vector2 moveInput;
    private Animator animator; 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>(); 
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.linearVelocity = moveInput * moveSpeed;
    }

    public void Move(InputAction.CallbackContext context)
    {
        animator.SetBool("isWalking", true);

        if (context.canceled)
        {
            animator.SetBool("isWalking", false);
            animator.SetFloat("LastInputX", moveInput.x);
            animator.SetFloat("LastInputY", moveInput.y);
        }

        moveInput = context.ReadValue<Vector2>();
        animator.SetFloat("InputX", moveInput.x);
        animator.SetFloat("InputY", moveInput.y);
    } 
}




// using UnityEngine;
// using UnityEngine.InputSystem;

// public class PlayerMovement : MonoBehaviour
// {
//     [SerializeField] private float moveSpeed = 5f;
//     private Rigidbody2D rb;
//     private Vector2 moveInput;
//     private Vector2 lastMoveInput; // Untuk menyimpan arah terakhir
//     private Animator animator;

//     void Start()
//     {
//         rb = GetComponent<Rigidbody2D>();
//         animator = GetComponent<Animator>();
//     }

//     void FixedUpdate()
//     {
//         // Menggerakkan Rigidbody 2D secara mulus di FixedUpdate
//         rb.linearVelocity = moveInput * moveSpeed;
//     }

//     // Fungsi ini otomatis dipanggil oleh Player Input dengan Behaviour "Send Messages"
//     public void OnMove(InputValue value)
//     {
//         moveInput = value.Get<Vector2>();

//         if (moveInput != Vector2.zero)
//         {
//             lastMoveInput = moveInput; // Simpan arah gerak terakhir
//             animator.SetBool("isWalking", true);
//             animator.SetFloat("InputX", moveInput.x);
//             animator.SetFloat("InputY", moveInput.y);
//         }
//         else
//         {
//             animator.SetBool("isWalking", false);
//             animator.SetFloat("LastInputX", lastMoveInput.x);
//             animator.SetFloat("LastInputY", lastMoveInput.y);
//         }
//     }
// }



// using UnityEngine;
// using UnityEngine.InputSystem;

// public class PlayerMovement : MonoBehaviour
// {
//     [SerializeField] private float moveSpeed = 5f;
//     private Rigidbody2D rb;
//     private Vector2 moveInput;
//     private Animator animator;

//     void Start()
//     {
//         rb = GetComponent<Rigidbody2D>();
//         animator = GetComponent<Animator>();
//     }

//     void FixedUpdate()
//     {
//         // Menggerakkan Rigidbody 2D secara mulus
//         rb.linearVelocity = moveInput * moveSpeed;
//     }

//     public void Move(InputAction.CallbackContext context)
//     {
//         // Ambil nilai input terbaru
//         moveInput = context.ReadValue<Vector2>();

//         if (context.performed && moveInput != Vector2.zero)
//         {
//             animator.SetBool("isWalking", true);
//             animator.SetFloat("InputX", moveInput.x);
//             animator.SetFloat("InputY", moveInput.y);

//             // SIMPAN arah terakhir DI SINI (mumpung nilainya masih ada, bukan 0)
//             animator.SetFloat("LastInputX", moveInput.x);
//             animator.SetFloat("LastInputY", moveInput.y);
//         }
//         else if (context.canceled)
//         {
//             animator.SetBool("isWalking", false);
//             moveInput = Vector2.zero;
//         }
//     }
// }


// using UnityEngine;
// using UnityEngine.InputSystem;

// public class PlayerMovement : MonoBehaviour
// {
//     [SerializeField] private float moveSpeed = 5f;
//     private Rigidbody2D rb;
//     private Vector2 moveInput;
//     private Animator animator;

//     void Start()
//     {
//         rb = GetComponent<Rigidbody2D>();
//         animator = GetComponent<Animator>();
//     }

//     void FixedUpdate()
//     {
//         // Menggerakkan Rigidbody 2D secara mulus
//         rb.linearVelocity = moveInput * moveSpeed;
//     }

//     public void Move(InputAction.CallbackContext context)
//     {
//         // Langsung baca nilai input Vector2 (otomatis mendeteksi tombol ditekan atau dilepas)
//         moveInput = context.ReadValue<Vector2>();

//         if (moveInput != Vector2.zero)
//         {
//             animator.SetBool("isWalking", true);
//             animator.SetFloat("InputX", moveInput.x);
//             animator.SetFloat("InputY", moveInput.y);

//             // Simpan arah terakhir untuk animasi Idle
//             animator.SetFloat("LastInputX", moveInput.x);
//             animator.SetFloat("LastInputY", moveInput.y);
//         }
//         else
//         {
//             animator.SetBool("isWalking", false);
//         }
//     }
// }


// using UnityEngine;
// using UnityEngine.InputSystem;

// public class PlayerMovement : MonoBehaviour
// {
//     [SerializeField] private float moveSpeed = 5f;
//     private Rigidbody2D rb;
//     private Vector2 moveInput;
//     private Animator animator;

//     void Start()
//     {
//         rb = GetComponent<Rigidbody2D>();
//         animator = GetComponent<Animator>();

//         // Cek apakah Rigidbody-nya ketemu atau null
//         if (rb == null)
//         {
//             Debug.LogError("FATAL: Rigidbody2D tidak ditemukan di GameObject Player ini!");
//         }
//     }

//     void FixedUpdate()
//     {
//         // Cek apa yang terjadi di FixedUpdate lewat Console
//         Debug.Log("moveInput: " + moveInput + " | Kecepatan Rigidbody: " + (moveInput * moveSpeed));

//         if (rb != null)
//         {
//             rb.linearVelocity = moveInput * moveSpeed;
//         }
//     }

//     public void Move(InputAction.CallbackContext context)
//     {
//         moveInput = context.ReadValue<Vector2>();

//         if (moveInput != Vector2.zero)
//         {
//             animator.SetBool("isWalking", true);
//             animator.SetFloat("InputX", moveInput.x);
//             animator.SetFloat("InputY", moveInput.y);

//             animator.SetFloat("LastInputX", moveInput.x);
//             animator.SetFloat("LastInputY", moveInput.y);
//         }
//         else
//         {
//             animator.SetBool("isWalking", false);
//         }
//     }
// }