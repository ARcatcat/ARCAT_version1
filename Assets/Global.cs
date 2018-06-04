using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using LitJson;
using System.Text;

public class Global{

    static Global _instance;
    private int dollor = 20;   //当前金币 Dynamic
    private int Grade = 0;    //当前级数  Dynamic
    private int jingli = 0;  //当前精力  Dynamic
    private int jingyan = 0; //当前经验  Dynamic
    private int[] bag = { 2, 1, 4, 0, 0, 0, 0, 0 }; //背包中每样东西的个数 Dynamic

    private int[] maxjingli = { 20, 30, 40, 50, 60, 70, 80, 90, 100, 110, 70}; //每级对应最大精力
    private int[] maxjingyan = { 200, 300, 400, 500, 600, 700, 800, 900, 1000, 1500, 2000 }; //每级对应最大经验值
    private int maxGrade = 10; //最高级数
    private int[] shop_price = { 1, 2, 3, 4, 5, 6, 7, 8 };  //商店中每样东西价格
    private int[] shop_jingli = { 2, 3, 4, 5, 6, 7, 8, 9 }; //商店中每样东西可加精力
    private int[] shop_jishu = { 0, 1, 3, 4, 5, 6, 7, 8 }; //购买商品所需要级数
    private int[] playgame_jingli = { 5, 5 }; //游戏0、1所需要精力

    public int localIndex = 0; //当前选择的物品的index (Dynamic but not need save)
    public int localIndex_usenum = 0; //所选择物体的个数_用于useobj
    public int localIndex_shopnum = 0; //所选择物体的个数_用于shopobj
    //public data Data;

    public static Global GetInstance()
    {
        if (_instance == null)
        {
            //create a _instance
            _instance = new Global();

            //read the json document
            string path = Application.dataPath + "/data.json";
            if (File.Exists(path))
            {
                data Data = ParseFile(path);
                _instance.dollor = Data.dollor;
                _instance.Grade = Data.Grade;
                _instance.jingli = Data.jingli;
                _instance.jingyan = Data.jingyan;
                _instance.bag[0] = Data.bag0;
                _instance.bag[1] = Data.bag1;
                _instance.bag[2] = Data.bag2;
                _instance.bag[3] = Data.bag3;
                _instance.bag[4] = Data.bag4;
                _instance.bag[5] = Data.bag5;
                _instance.bag[6] = Data.bag6;
                _instance.bag[7] = Data.bag7;
            }
        }
        return _instance;
    }
	
    public void readJson()
    {
        //read the json document
        string path = Application.dataPath + "/data.json";
        if (File.Exists(path))
        {
            data Data = ParseFile(path);
            _instance.dollor = Data.dollor;
            _instance.Grade = Data.Grade;
            _instance.jingli = Data.jingli;
            _instance.jingyan = Data.jingyan;
            _instance.bag[0] = Data.bag0;
            _instance.bag[1] = Data.bag1;
            _instance.bag[2] = Data.bag2;
            _instance.bag[3] = Data.bag3;
            _instance.bag[4] = Data.bag4;
            _instance.bag[5] = Data.bag5;
            _instance.bag[6] = Data.bag6;
            _instance.bag[7] = Data.bag7;
        }
    }

    void writeJson()
    {
        //write back to json data
        string path = Application.dataPath + "/data.json";
        data Data = new data(_instance.dollor, _instance.Grade, _instance.jingli, _instance.jingyan, _instance.bag);
        CreateFile(path, Data);
        //if (File.Exists(path))
        //{
            //直接写入
        //}
        //else
        //{
            //新建文件再写入

        //}
    }

    //购买商品，输入商品编号（0-7）以及商品数量，返回值为是否成功购买
    public bool buy(int index, int num)
    {
        if (_instance.shop_price[index] * num <= _instance.dollor && _instance.Grade >= _instance.shop_jishu[index])
        {
            //金币消耗
            _instance.dollor -= _instance.shop_price[index] * num;
            //数目增加
            _instance.bag[index] += num;
            //write
            writeJson();
            return true;
        }
        return false;
    }

    //使用物品恢复精力值，恢复成功返回true，恢复失败（背包数目不足）则返回false
    public bool use(int index, int num)
    {
        if (num <= _instance.bag[index])
        {
            _instance.bag[index] -= num;
            _instance.jingli += _instance.shop_jingli[index] * num;
            if (_instance.jingli > _instance.maxjingli[_instance.Grade])
            {
                //满精力
                _instance.jingli = _instance.maxjingli[_instance.Grade];
            }
            writeJson();
            return true;
        }
        return false;
    }

    //是否精力足够玩游戏,index为[0,1]，代表第几个游戏
    public bool EnoughPlayGame(int index)
    {
        return _instance.jingli >= _instance.playgame_jingli[index];
    }

    //消耗精力玩游戏获取金币和经验值，此前应判断是否足够精力
    public void playGame(int index, int score)
    {
        //精力只减少
        _instance.jingli -= _instance.playgame_jingli[index];
        if(index == 0)
        {
            //游戏0的奖励方式 ：0表示足球游戏
            //经验值增加，注意升级

            _instance.jingyan += score;
            while(_instance.jingyan / _instance.maxjingyan[_instance.Grade] != 0)
            {
                //升级
                _instance.Grade += 1;
                _instance.jingyan = _instance.jingyan - _instance.maxjingyan[_instance.Grade - 1];
            }
            //金币增加
            _instance.dollor += 10;
        }
        else
        {
            //游戏1的奖励方式：游戏1是鬼屋抓老鼠游戏
            //经验值增加，注意升级
            _instance.jingyan += score;
            while (_instance.jingyan / _instance.maxjingyan[_instance.Grade] != 0)
            {
                //升级
                _instance.Grade += 1;
                _instance.jingyan = _instance.jingyan - _instance.maxjingyan[_instance.Grade - 1];
            }
            //金币增加
            _instance.dollor += 10;
        }
        writeJson();
    }

    public int getDollars()
    {
        return _instance.dollor;
    }

    public int getJingyan()
    {
        return _instance.jingyan;
    }

    public int getGrade()
    {
        return _instance.Grade;
    }

    public int getJingli()
    {
        return _instance.jingli;
    }

    public int getBag(int index)
    {
        return _instance.bag[index];
    }

    public int getShopPrice(int index)
    {
        return _instance.shop_price[index];
    }

    public int getShopJingli(int index)
    {
        return _instance.shop_jingli[index];
    }

    public int getShopJishu(int index)
    {
        return _instance.shop_jishu[index];
    }

    public int getMaxJinyan(int index)
    {
        return _instance.maxjingyan[index];
    }

    //解析json文件  
    private static data ParseFile(string path)
    {
        data Data = new data();

        FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
        StreamReader streamReader = new StreamReader(fileStream);
        string strData = streamReader.ReadToEnd();
        JsonData jsonData = JsonMapper.ToObject(strData);

        Data.dollor = int.Parse(jsonData["dollor"].ToString());
        Data.Grade = int.Parse(jsonData["Grade"].ToString());
        Data.jingli = int.Parse(jsonData["jingli"].ToString());
        Data.jingyan = int.Parse(jsonData["jingyan"].ToString());
        Data.bag0 = int.Parse(jsonData["bag0"].ToString());
        Data.bag1 = int.Parse(jsonData["bag1"].ToString());
        Data.bag2 = int.Parse(jsonData["bag2"].ToString());
        Data.bag3 = int.Parse(jsonData["bag3"].ToString());
        Data.bag4 = int.Parse(jsonData["bag4"].ToString());
        Data.bag5 = int.Parse(jsonData["bag5"].ToString());
        Data.bag6 = int.Parse(jsonData["bag6"].ToString());
        Data.bag7 = int.Parse(jsonData["bag7"].ToString());

        return Data;
    }

    //创建json文件  
    void CreateFile(string filePath, data studentData)
    {
        StringBuilder stringBuilder = new StringBuilder();
        JsonWriter jsonWriter = new JsonWriter(stringBuilder);
        jsonWriter.WriteObjectStart();

        jsonWriter.WritePropertyName("dollor");
        jsonWriter.Write(studentData.dollor);
        jsonWriter.WritePropertyName("Grade");
        jsonWriter.Write(studentData.Grade);
        jsonWriter.WritePropertyName("jingli");
        jsonWriter.Write(studentData.jingli);
        jsonWriter.WritePropertyName("jingyan");
        jsonWriter.Write(studentData.jingyan);
        jsonWriter.WritePropertyName("bag0");
        jsonWriter.Write(studentData.bag0);
        jsonWriter.WritePropertyName("bag1");
        jsonWriter.Write(studentData.bag1);
        jsonWriter.WritePropertyName("bag2");
        jsonWriter.Write(studentData.bag2);
        jsonWriter.WritePropertyName("bag3");
        jsonWriter.Write(studentData.bag3);
        jsonWriter.WritePropertyName("bag4");
        jsonWriter.Write(studentData.bag4);
        jsonWriter.WritePropertyName("bag5");
        jsonWriter.Write(studentData.bag5);
        jsonWriter.WritePropertyName("bag6");
        jsonWriter.Write(studentData.bag6);
        jsonWriter.WritePropertyName("bag7");
        jsonWriter.Write(studentData.bag7);

        jsonWriter.WriteObjectEnd();

        FileStream fileStream = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Write, FileShare.ReadWrite);
        StreamWriter streamWriter = new StreamWriter(fileStream);
        streamWriter.WriteLine(stringBuilder.ToString());
        streamWriter.Close();
        fileStream.Close();
        fileStream.Dispose();
    }
}
