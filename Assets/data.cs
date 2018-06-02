using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class data {
    public int dollor;   //当前金币 Dynamic
    public int Grade;    //当前级数  Dynamic
    public int jingli;  //当前精力  Dynamic
    public int jingyan; //当前经验  Dynamic
    public int bag0; //背包中每样东西的个数 Dynamic
    public int bag1;
    public int bag2;
    public int bag3;
    public int bag4;
    public int bag5;
    public int bag6;
    public int bag7;
    
    public data()
    {

    }

    public data(int d, int gr, int jl, int jy, int[] bag)
    {
        this.dollor = d;
        this.Grade = gr;
        this.jingli = jl;
        this.jingyan = jy;
        bag0 = bag[0];
        bag1 = bag[1];
        bag2 = bag[2];
        bag3 = bag[3];
        bag4 = bag[4];
        bag5 = bag[5];
        bag6 = bag[6];
        bag7 = bag[7];
    }
}
