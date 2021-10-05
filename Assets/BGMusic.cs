using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMusic : MonoBehaviour
{
    bool _created = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Awake()
    {
        if (!_created)
        {
            DontDestroyOnLoad(this.gameObject);
            _created = true;
            //Init();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
