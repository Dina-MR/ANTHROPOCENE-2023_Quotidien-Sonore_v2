using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class CharacterMove : MonoBehaviour
{
    [SerializeField] private Path path;
    private Vector3 currentTarget;
    private int currentTargetIndex;
    public float speed;
    private float pauseTimer;
    public float pauseTimeMax = 10f;
    private bool canMove;

    // Start is called before the first frame update
    void Start()
    {
        pauseTimer = 0f;
        currentTargetIndex = 0;
        canMove = true;
    }

    // Update is called once per frame
    void Update()
    {
        // Seulement quand les positions ont été récupérées
        if (path.nodes.Count > 0 && currentTargetIndex == 0)
            InitTravel();
        if(currentTarget != null && currentTargetIndex < path.nodes.Count)
            Move();
        // Affichage
        if (path.isInitialized)
            path.DrawStaticPath();
    }

    public void InitTravel()
    {
        gameObject.transform.position = path.nodes[0];
        currentTarget = path.nodes[1];
        currentTargetIndex = 1;
    }

    public void Move()
    {
        float step = speed * Time.deltaTime;
        // Le personnage n'est pas encore arrivé à sa prochaine destination
        if (gameObject.transform.position != currentTarget && canMove)
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, currentTarget, step);
        // Le personnage est arrivé à destination. Il fait donc une pause puis se dirige vers la suivante au bout de 10 secondes
        else
        {
            // On vérifie si le personnage a atteint une zone d'intérêt, sinon on continue le trajet
            if (path.targets[currentTargetIndex].CompareTag("Main_Target"))
                CheckPause();
            if(canMove)
            {
                currentTargetIndex++;
                if (currentTargetIndex < path.nodes.Count)
                    currentTarget = path.nodes[currentTargetIndex];
            }
        }
    }

    // Pause
    public void CheckPause()
    {
        Debug.Log("Temps de pause :" + pauseTimer + "s");
        pauseTimer += Time.deltaTime;
        if (pauseTimer <= pauseTimeMax)
            canMove = false;
        else
        {
            canMove = true;
            pauseTimer = 0f;
        }
    }
}
