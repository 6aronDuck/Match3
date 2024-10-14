using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    public GameObject clearFXPrefab; 
    public GameObject breakFXPrefab;
    public GameObject doubleBreakFXPrefab;
    
    public void ClearPieceFXAt(int x, int y, int z = 0)
    {
        if (clearFXPrefab != null)
        {
            GameObject clearFX = Instantiate(clearFXPrefab, new Vector3(x, y, z), Quaternion.identity);
            ParticlePlayer particlePlayer = clearFX.GetComponent<ParticlePlayer>();
            
            if (particlePlayer != null)
            {
                particlePlayer.Play();
            }
        }
    }
    
    public void BreakTileFXAt(int breakableValue, int x, int y, int z = 0)
    {
        GameObject breakFX = null;
        ParticlePlayer particlePlayer = null;

        if (breakableValue > 1)
        {
            if (doubleBreakFXPrefab != null)
                breakFX = Instantiate(doubleBreakFXPrefab, new Vector3(x, y, z), Quaternion.identity);
        }
        else
            if(breakFXPrefab != null)
                breakFX = Instantiate(breakFXPrefab, new Vector3(x, y, z), Quaternion.identity);

        if (breakFX == null) return;
        particlePlayer = breakFX.GetComponent<ParticlePlayer>();
        if(particlePlayer != null)
        {
            particlePlayer.Play();
        }
    }
}
