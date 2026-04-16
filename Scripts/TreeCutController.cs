using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TreeCutController : MonoBehaviour
{
    [Header("Setup")]
    public Transform treeVisualRoot;
    public EnvironmentController environmentController;
    public Transform player;
    public FadeManager fadeManager;

    [Header("Effects")]
    public bool scaleDown = true;
    public float scaleTime = 0.5f;

    [Header("Debris Effects")]
    public GameObject trunkChunkPrefab;
    public GameObject leafBurstPrefab;
    [Range(1, 30)] public int chunkCount = 10;
    public float spawnSpread = 2.5f;
    public bool keepChunks = true;

    private bool hasCut = false;

    void Start()
    {
        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player")?.transform;

        if (environmentController == null)
            environmentController = FindAnyObjectByType<EnvironmentController>();
    }

    // This method is now PUBLIC so Unity can see it in XR Interactable Events
    public void CutTree()
    {
        if (hasCut) return;

        hasCut = true;

        // Play sounds + environmental effects
        SoundManager.Instance?.PlayAxe();
        SoundManager.Instance?.PlayAmbient(false);
        SoundManager.Instance?.PlayThunder();

        SubtitleManager.Instance?.ShowLine(
            "The forest falls silent... The sky begins to darken.",
            3.5f
        );

        environmentController?.SetDark(true);

        // Optional fade
        if (fadeManager != null)
            StartCoroutine(fadeManager.FadeOut(3f));

        // Start scene transition
        StartCoroutine(LoadDroughtSceneAfterDelay(5f));

        // Hide tree
        if (treeVisualRoot != null)
        {
            if (scaleDown)
                StartCoroutine(ScaleDownAndHide());
            else
                treeVisualRoot.gameObject.SetActive(false);
        }

        // Spawn debris effects
        SpawnDebris();
    }
    void Update()
{
    if (hasCut || player == null) return;

    float distance = Vector3.Distance(player.position, transform.position);

    // Press E to cut tree
    if (distance <= 3f && Input.GetKeyDown(KeyCode.E))
    {
        Debug.Log("E pressed - Trying to cut tree");
        CutTree();
    }
}
    IEnumerator LoadDroughtSceneAfterDelay(float delay)
    {
        Debug.Log("[TreeCutController] Transition started...");
        yield return new WaitForSeconds(delay);

        Debug.Log("[TreeCutController] Loading DroughtScene...");
        SceneManager.LoadScene("DroughtScene"); // Correct format
    }

    IEnumerator ScaleDownAndHide()
    {
        Vector3 startScale = treeVisualRoot.localScale;
        float t = 0f;

        while (t < scaleTime)
        {
            t += Time.deltaTime;
            float k = 1 - Mathf.Clamp01(t / scaleTime);
            treeVisualRoot.localScale = startScale * k;
            yield return null;
        }

        treeVisualRoot.gameObject.SetActive(false);
    }

    void SpawnDebris()
    {
        if (trunkChunkPrefab == null && leafBurstPrefab == null)
            return;

        Debug.Log($"[TreeCutController] Spawning {chunkCount} trunk chunks...");

        for (int i = 0; i < chunkCount; i++)
        {
            if (trunkChunkPrefab != null)
            {
                Vector3 pos = transform.position + new Vector3(
                    Random.Range(-spawnSpread, spawnSpread),
                    Random.Range(1.5f, 3.0f),
                    Random.Range(-spawnSpread, spawnSpread)
                );

                GameObject chunk = Instantiate(trunkChunkPrefab, pos, Random.rotation);

                Rigidbody rb = chunk.GetComponent<Rigidbody>() ?? chunk.AddComponent<Rigidbody>();
                rb.mass = Random.Range(0.3f, 0.7f);
                rb.AddExplosionForce(150f, transform.position, 4f);
                rb.angularVelocity = Random.insideUnitSphere * 5f;

                if (!keepChunks)
                    Destroy(chunk, 10f);
            }
        }

        if (leafBurstPrefab != null)
        {
            GameObject leaves = Instantiate(
                leafBurstPrefab,
                transform.position + Vector3.up * 2f,
                Quaternion.identity
            );

            Destroy(leaves, 5f);
        }
    }
}