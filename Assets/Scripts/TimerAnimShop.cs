using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TimerAnimShop : MonoBehaviour
{
    // Start is called before the first frame update
    private float maxtime = 5f;
    
    public Slider sliderTimer;
   
    private float timeleft;
    public bool isSpawnCubes = false;
    private GameObject game,shop;
    public int type_figure;
    public int index;
    public GameObject handleSlider;
   // public GameObject imageCost;
    public GameObject[] spawningObjects;
    void Start()
    {
        sliderTimer = gameObject.GetComponentInChildren<Slider>(true);
        game = GameObject.Find("Game");
        shop = GameObject.Find("Shop");

        if(isSpawnCubes)
            ReapetGame();
    }

    // Update is called once per frame
    void Update()
    {
        if(timeleft > 0 )
        {
            timeleft -= Time.deltaTime;
            sliderTimer.value = timeleft;
        }
        else 
            timeleft=maxtime;
    }
    public void setByCickValueItem()
    {
        if(shop.GetComponent<ShopController>().catalog == 0)
        {
            if(game.GetComponent<LibraryData>().shop_timers_color.Contains(index)) 
            setClickedItem();
            else
            {
                if(game.GetComponent<LibraryData>().cost_item_color[index] <= game.GetComponent<LibraryData>().count_diamonds)
                {
                    game.GetComponent<LibraryData>().setDiamonds(-1 * game.GetComponent<LibraryData>().cost_item_color[index]);
                    Image[] img = GetComponentsInChildren<Image>(true);
                    img[6].gameObject.SetActive(false);
                    game.GetComponent<LibraryData>().setCollection(shop.GetComponent<ShopController>().catalog,index);
                    setClickedItem();
                }
                else
                    shop.GetComponentInChildren<Animation>().Play();
                    //Debug.Log("No money");

            }
        }
        else
        {
            
            if(game.GetComponent<LibraryData>().shop_timers_particles.Contains(index)) 
            setClickedItem();
            else
            {
                if(game.GetComponent<LibraryData>().cost_item_particles[index] <= game.GetComponent<LibraryData>().count_diamonds)
                {
                    game.GetComponent<LibraryData>().setDiamonds(-1 * game.GetComponent<LibraryData>().cost_item_particles[index]);
                    Image[] img = GetComponentsInChildren<Image>(true);
                    img[6].gameObject.SetActive(false);
                    game.GetComponent<LibraryData>().setCollection(shop.GetComponent<ShopController>().catalog,index);
                    setClickedItem();
                }
                else
                    shop.GetComponentInChildren<Animation>().Play();
            }
        }
        //if(!game.GetComponent<LibraryData>().shop_timers_color.Contains(index) && shop.GetComponent<ShopController>().catalog == 0)
        //    game.GetComponent<LibraryData>().shop_timers_color.Add(index);

        
        
    }
    public void setClickedItem()
    {
        shop.GetComponent<ShopController>().setColortoBlack();
        transform.Find("ImageBackground").GetComponent<Image>().color=Color.green;
        game.GetComponent<LibraryData>().setItemCollection(shop.GetComponent<ShopController>().catalog,index);
        /*if(shop.GetComponent<ShopController>().catalog == 0)
            game.GetComponent<LibraryData>().index_shop[0] = index;
        else
            game.GetComponent<LibraryData>().index_shop[1] = index;*/
    }
    public void ReapetGame(){
        if(game != null)
        {
            GameObject cube = Instantiate(spawningObjects[type_figure],new Vector3( handleSlider.transform.position.x ,handleSlider.transform.position.y+ Random.Range(-0.4f, 2.5f),
            handleSlider.transform.position.z), handleSlider.transform.rotation);
            LibraryData librarydata = game.GetComponent<LibraryData>();
            cube.GetComponentInChildren<MeshRenderer>().material.color = shop.GetComponent<ShopController>().getColor(librarydata.index_shop[0]); 
            Destroy(cube, 0.5f); 
            if(isSpawnCubes)      
                Invoke("ReapetGame",0.18f);
        }
    }
}
