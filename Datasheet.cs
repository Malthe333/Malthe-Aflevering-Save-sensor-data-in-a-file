using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Datasheet : MonoBehaviour
{
    string filename = "";
    [System.Serializable]
    public class Data
    {
        public float x;
        public float y;
        public float z;
    }
    [System.Serializable]
    public class GyroList
    {
        public Data[] gyro;
    }

    public GyroList newGyroList = new GyroList();
    public bool stealing = false;

    // Start is called before the first frame update
    void Start()
    {
        filename = Application.dataPath + "/test3000.csv";
        TextWriter tw = new StreamWriter(filename, false);
        tw.WriteLine("x,y,z");
        tw.Close();
    }

    // Update is called once per frame
    void Update()
    {
        if (stealing == true)
        {
            StealAllAccelerationData();
        }

    }

    public void PressButton()
    {
        if (stealing == true)
        {
            stealing = false;
        }
        else if (stealing == false)
        {
            stealing = true;

        }
    }
    public void StealAllAccelerationData()
    {
        if (newGyroList.gyro.Length > 0)
        {
            TextWriter tw = new StreamWriter(filename, true);
            //tw.WriteLine("x,y,z");
            tw.Close();

            tw = new StreamWriter(filename, true);



            for (int i = 0; i < newGyroList.gyro.Length; i++)
            {
                //doesn't actually check the gyroscope, but the acceleration
                newGyroList.gyro[i].x = Input.acceleration.x;
                newGyroList.gyro[i].y = Input.acceleration.y;
                newGyroList.gyro[i].z = Input.acceleration.z;
                tw.WriteLine(newGyroList.gyro[i].x + "," + newGyroList.gyro[i].y + "," + newGyroList.gyro[i].z);


            }
            tw.Close();
            Debug.Log("recording");
        }

    }
}
