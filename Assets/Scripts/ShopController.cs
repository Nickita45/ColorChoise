using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;
public class ShopController : MonoBehaviour
{
    // Start is called before the first frame update
    int pages = 0; 
    int max_page = 0;
    public int catalog = 0;
    public List<GameObject> items_shop = new List<GameObject>();
    public GameObject item_prefab;
    public GameObject button_next,button_back;
    public GameObject Library;
    public GameObject Slider;//In game
    public Text name_page;
    public GameObject scroll;
    List<Color> colors_timer = new List<Color>()
    {
        Color.white, 
        Utility.ToColor("da5c53"),
        Utility.ToColor("F4892B"),
        Utility.ToColor("f1ea65"),
        Utility.ToColor("449187"),
        Utility.ToColor("4EADFF"),
        Color.magenta,

        Utility.ToColor("30D5C8"),
        Utility.ToColor("C8A2C8"),
        Utility.ToColor("FFC0CB"),
        Utility.ToColor("C0C0C0"),
    };
    string[] figures = {"cube","cylinder","sphere","pyramod"};
    public Color getColor(int i)
    {
        return colors_timer[i];
    }
    public void increasePage()
    {
        pages++;
    }
    public void decreasePage()
    {
        pages--;
    }

    
    public void setMainPage()
    {
        catalog=0;
        pages=0;
        name_page.text= "Choose catalog";

        GameObject[] send_items = {items_shop[0],items_shop[0]}; 
        send_items[0].SetActive(true);
        send_items[1].SetActive(true);
        
        Image[] img = send_items[0].GetComponentsInChildren<Image>(true);
        img[4].color = colors_timer[Library.GetComponent<LibraryData>().index_shop[0]];
        send_items[0].GetComponentInChildren<Slider>(true).gameObject.SetActive(true);
        
        Image[] img1 = send_items[1].GetComponentsInChildren<Image>(true);
        img1[4].color = colors_timer[0];
        send_items[1].GetComponentInChildren<Slider>(true).gameObject.SetActive(true);
        send_items[1].GetComponent<TimerAnimShop>().isSpawnCubes = true;
        send_items[1].GetComponent<TimerAnimShop>().ReapetGame();
        send_items[1].GetComponent<TimerAnimShop>().type_figure = Library.GetComponent<LibraryData>().index_shop[1];

        button_back.SetActive(false);
        button_next.SetActive(false);
    }
    public void setColorsTimer()
    {
        name_page.text= "Timers colors";
        int size_type = colors_timer.Count;
        double page = (double)size_type/6;
        max_page = (int)Math.Ceiling(page) - 1;
        catalog=0;
        
        for(int i = 0; i < colors_timer.Count;i++)
        {
            Slider[] slider = item_prefab.GetComponentsInChildren<Slider>(true);
            slider[0].gameObject.SetActive(true);
            item_prefab.GetComponent<TimerAnimShop>().isSpawnCubes = false;
            Image[] img = item_prefab.GetComponentsInChildren<Image>(true);
            img[4].color = colors_timer[i];
            img[0].color=Color.black;
            img[6].gameObject.SetActive(true);
            if(i == Library.GetComponent<LibraryData>().index_shop[0])
                img[0].color=Color.green;
            item_prefab.name="ItemObj_"+i;
            item_prefab.GetComponent<TimerAnimShop>().index=i;

            if(Library.GetComponent<LibraryData>().shop_timers_color.Contains(i))
                img[6].gameObject.SetActive(false);
            else
                img[6].GetComponentInChildren<TextMeshProUGUI>().text = ""+Library.GetComponent<LibraryData>().cost_item_color[i];
            scroll.GetComponent<ScrollViewScriot>().summonObj(item_prefab);  
        }
        //setButtonToPrefab();
        //scroll.GetComponent<ScrollViewScriot>().Populate(gObjs);
        /*if(pages == 0)
            button_back.SetActive(false);
        else
            button_back.SetActive(true);
        if(pages == max_page)
            button_next.SetActive(false);
        else
            button_next.SetActive(true);*/
    }
    public void setFiguresTimer()
    {
        name_page.text= "Figures";
        int size_type = figures.Length;
        double page = (double)size_type/6;
        max_page = (int)Math.Ceiling(page) - 1;
        
        catalog=1;
        for(int i = 0; i < figures.Length;i++)
        {
            
            Slider[] slider = item_prefab.GetComponentsInChildren<Slider>(true);
            slider[0].gameObject.SetActive(true);
            item_prefab.GetComponent<TimerAnimShop>().isSpawnCubes = true;
            item_prefab.GetComponent<TimerAnimShop>().ReapetGame();
            Image[] img = item_prefab.GetComponentsInChildren<Image>(true);
            img[4].color = colors_timer[Library.GetComponent<LibraryData>().index_shop[0]];
            img[0].color=Color.black;
            img[6].gameObject.SetActive(true);
            item_prefab.GetComponent<TimerAnimShop>().type_figure =i;
            item_prefab.name="ItemObj_"+i;
            if(i == Library.GetComponent<LibraryData>().index_shop[1])
                img[0].color=Color.green; 
            item_prefab.GetComponent<TimerAnimShop>().index=i;
            if(Library.GetComponent<LibraryData>().shop_timers_particles.Contains(i))
                img[6].gameObject.SetActive(false);
            else
                img[6].GetComponentInChildren<TextMeshProUGUI>().text = ""+Library.GetComponent<LibraryData>().cost_item_particles[i];
            scroll.GetComponent<ScrollViewScriot>().summonObj(item_prefab);  
            /*
            if(pages == 0)
                button_back.SetActive(false);
            else
                button_back.SetActive(true);
            if(pages == max_page)
                button_next.SetActive(false);
            else
                button_next.SetActive(true);*/
        }
    }
    public void setColortoBlack()
    {
        TimerAnimShop[] items = scroll.GetComponentsInChildren<TimerAnimShop>();
        for(int i=0;i<items.Length;i++)
            {
                items[i].transform.Find("ImageBackground").GetComponent<Image>().color=Color.black;
            }
    }
    public void clearScroll()
    {
        TimerAnimShop[] items = scroll.GetComponentsInChildren<TimerAnimShop>();
        for(int i=items.Length-1;i>=0;i--)
        {
              Destroy(items[i].gameObject);
        }
    }
}
