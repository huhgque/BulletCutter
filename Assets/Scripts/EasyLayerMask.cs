using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EasyLayerMask
{
    public int Value {get; private set; }
    public static EasyLayerMask I {get;} = new();

    public EasyLayerMask HitAll(){
        Value = ~0;
        return this;
    }

    public EasyLayerMask HitOnly(string layerName){
        int layerMask = LayerMask.NameToLayer(layerName);
        Value = (1 << layerMask);
        return this;
    }
    public EasyLayerMask HitAllExcept(string layerName){
        int layerMask = LayerMask.NameToLayer(layerName);
        Value = ~layerMask;
        return this;
    }
    public EasyLayerMask And(string layerName){
        int addLayerMask = EasyLayerMask.I.HitOnly(layerName).Value;
        Value = Value | addLayerMask;
        return this;
    }
}
