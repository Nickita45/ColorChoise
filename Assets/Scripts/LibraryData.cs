using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LibraryData : MonoBehaviour
{
    public int count_life = 0; 
    public int count_record = 0;
    public int count_record_insane = 0;
    public TextMeshProUGUI text_count_life,text_count_life_lose,text_count_diamonds,text_count_diamonds_shop, text_count_record,
    text_count_record_insane;
    public int[] index_shop = {0,0};
    public int count_diamonds = 0;
    public List<int> shop_timers_color = new List<int>{0};
    public List<int> shop_timers_particles = new List<int>{0};
    
    public List<int> cost_item_color = new List<int>{0,3,3,3,3,3,3,
                                                    3,3,3, 3,};
    public List<int> cost_item_particles = new List<int>{0,2,1,4};
    public string test_1;
    private Dictionary<int,int> generatedLevelChance = new Dictionary<int,int>{
    {0,50},//No
    {1,50},//Color
    {2,50},//No
    {3,50},//Figure
    {4,50},//No
    {5,50},//Side
    {6,90}//Procent of generate more true answer
    };
    public Dictionary<int,int> getGenerateLevelChance(int count)
    {
        
        if(count < 10)
        generatedLevelChance = new Dictionary<int,int>{
    {0,50},//No
    {1,100},//Color
    {2,50},//No
    {3,50},//Figure
    {4,0},//No
    {5,0},//Side
    {6,0}};
        else if(count > 10 && count < 20)
        generatedLevelChance = new Dictionary<int,int>{
    {0,50},//No
    {1,100},//Color
    {2,50},//No
    {3,100},//Figure
    {4,0},//No
    {5,0},//Side
    {6,20}};
        else if(count > 20 && count < 35)
        generatedLevelChance = new Dictionary<int,int>{
    {0,75},//No
    {1,100},//Color
    {2,75},//No
    {3,100},//Figure
    {4,0},//No
    {5,50},//Side
    {6,30}};
        else if(count > 35 && count < 50)
        generatedLevelChance = new Dictionary<int,int>{
    {0,90},//No
    {1,100},//Color
    {2,90},//No
    {3,100},//Figure
    {4,60},//No
    {5,100},//Side
    {6,50}};
        else if(count > 50)
        generatedLevelChance = new Dictionary<int,int>{
    {0,90},//No
    {1,100},//Color
    {2,90},//No
    {3,100},//Figure
    {4,90},//No
    {5,100},//Side
    {6,80}};
        return generatedLevelChance;
    }
    public void Start()
    {
        //shop_timers_color.Add(0);
        //shop_timers_particles.Add(0);
        count_diamonds = PlayerPrefsSafe.GetInt("diamond");
        text_count_diamonds.text=""+count_diamonds;
        text_count_diamonds_shop.text=""+count_diamonds;
        count_life= PlayerPrefsSafe.GetInt("health");
        count_record = PlayerPrefsSafe.GetInt("recordNormal");
        text_count_record.text = ""+count_record;

        count_record_insane = PlayerPrefsSafe.GetInt("recordInsane");
        text_count_record_insane.text = ""+count_record_insane;

        setCountLifeText();

        //PlayerPrefsSafe.SetString("test","aaaa");
        //print(PlayerPrefsSafe.GetString("test"));
        //PlayerPrefs.DeleteAll();
        List<int> stringList = getStringToList(PlayerPrefsSafe.GetString("shopTC"));
        stringList.ForEach((item)=>
        {
            shop_timers_color.Add(item);
        });
        List<int> stringListParticle = getStringToList(PlayerPrefsSafe.GetString("shopTP"));
        stringListParticle.ForEach((item)=>
        {
            shop_timers_particles.Add(item);
        });
        List<int> stringListCollection = getStringToList(PlayerPrefsSafe.GetString("collection"));
        for(int i=0;i<index_shop.Length;i++)
            if(stringListCollection.Count != 1)
                index_shop[i] = stringListCollection[i];
            else
                index_shop[i]=0;

       

        //string str = getListToString(shop_timers_color);
        //string str = getListToString(shop_timers_color);
        //print(str.Split(',').Length);

    }
    string getListToString(List<int> list)
    {
        string str = "";
        for(int i=0;i<list.Count;i++)
        {   
            if(i==list.Count-1)
                str+=list[i];
            else
                str+=list[i]+",";
        }
        //string str1= "test1"+str;
        return str;
    }
    List<int> getStringToList(string str)
    {
        List<int> list = new List<int>();
        if(str == "None" )
        {
            list.Add(0);
            return list;
        }
        //print(str);
        //str.Replace("test","");
        string[] str1 = str.Split(',');
        for(int i = 0; i < str1.Length;i++)
        {

            //print(str1[i]);
            list.Add(System.Convert.ToInt32(str1[i]));
            
        }
        return list;
    }
    public void setCountLifeText()
    {
        text_count_life.text = ""+count_life;
        text_count_life_lose.text= ""+count_life;
        PlayerPrefsSafe.SetInt("health",count_life);
    }
    public void increaseHealth()
    {
        count_life++;
        //PlayerPrefsSafe.SetString("shopTimersColor","AAAA");
        //test_1="AAA";
    }
    public void setCollection(int catalog,int index)
    {
        if(catalog==0)
        {    
            shop_timers_color.Add(index);
            //print(getListToString(shop_timers_color));
            PlayerPrefsSafe.SetString("shopTC",getListToString(shop_timers_color));
            print(PlayerPrefsSafe.GetString("shopTC"));
        }
        else
        {
            
            shop_timers_particles.Add(index);
            PlayerPrefsSafe.SetString("shopTP",getListToString(shop_timers_particles));
        }
    }
    public void setDiamonds(int count)
    {
        count_diamonds+=count;
        text_count_diamonds.text=""+count_diamonds;
        text_count_diamonds_shop.text=""+count_diamonds;
        PlayerPrefsSafe.SetInt("diamond",count_diamonds);
    }
    public void setItemCollection(int catalog, int index)
    {
        
            index_shop[catalog] = index;
            PlayerPrefsSafe.SetString("collection",getListToString(new List<int> (index_shop)));

    }
    public void setRecord(int record,bool isInsane)
    {
        if(!isInsane)
        {    if(record > PlayerPrefsSafe.GetInt("recordNormal"))
            {
                count_record=record;
                PlayerPrefsSafe.SetInt("recordNormal",count_record);
                text_count_record.text = ""+count_record;
            }
        }
        else {
            if(record > PlayerPrefsSafe.GetInt("recordInsane"))
            {
                count_record_insane=record;
                PlayerPrefsSafe.SetInt("recordInsane",count_record_insane);
                text_count_record_insane.text = ""+count_record_insane;  
            }
        }
    }
}
