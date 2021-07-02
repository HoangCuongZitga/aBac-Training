private float time = 0.0f;
 public float interpolationPeriod = 0.1f;
 
 void Update () {
     time += Time.deltaTime;
 
     if (time >= interpolationPeriod) {
         time = 0.0f;
 
         // execute block of code here
     }
 }
