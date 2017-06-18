using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour {

  public float shakeDecay = 0.002f;
  public float intensity = .3f;
  public Vector3 initialPosition;

  public Vector3 originPosition;
  public Quaternion originRotation;
  
 
  private float shakeIntensity = 0;
  void Start()
  {
      
  }

  void Update() {
    if (shakeIntensity > 0) {
      transform.position = originPosition + Random.insideUnitSphere * shakeIntensity;
      transform.rotation = new Quaternion(
      originRotation.x + Random.Range(-shakeIntensity, shakeIntensity) * .2f,
      originRotation.y + Random.Range(-shakeIntensity, shakeIntensity) * .2f,
      originRotation.z + Random.Range(-shakeIntensity, shakeIntensity) * .2f,
      originRotation.w + Random.Range(-shakeIntensity, shakeIntensity) * .2f);
      shakeIntensity -= shakeDecay;
      initialPosition = new Vector3(2, 23, -45);//Go back to original position
      StartCoroutine(Restore());
    }
  

 
   
    
     
    
    }
   
  

  public void Shake() {
    
      originPosition = transform.position;
    originRotation = transform.rotation;
    shakeIntensity = intensity;
     
  }
    IEnumerator Restore()
  {
      yield return new WaitForSeconds(0.5f);// take 0.5 second to go back original position
      transform.localPosition =initialPosition;
    }
 
}
