using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class GameController : MonoBehaviour
{
    class ColorObj{
        public ColorObj(string name,Color color)
        {
            this.name=name;
            this.color=color;
        }
        public ColorObj(ColorObj obj)
        {
            this.color=obj.color;
            this.name=obj.name;
        }
        public string name;
        public Color color;
    }
    
    
    private List<ColorObj> colors = new List<ColorObj>{
        new ColorObj("red",Utility.ToColor("CB281C")),
        new ColorObj("orange",Utility.ToColor("B76218")),
        new ColorObj("yellow",Utility.ToColor("ABA305")),
        new ColorObj("green",Utility.ToColor("109154")),
        new ColorObj("white",Color.white),
        new ColorObj("blue",Utility.ToColor("0866B7")),
        new ColorObj("magenta",Color.magenta),
        new ColorObj("",Color.white)
    };
    private List<string> figures = new List<string>{
        "cube","sphere","cylinder","pyramid",""
    };
    private List<string> side = new List<string>{
        "right","left","up","down",""
    };
    public GameObject[] items;
    public GameObject lose_screen; 
    public GameObject[] spawns_items;
    public GameObject menu;
    public GameObject start_game;
    public TextMeshProUGUI description, result_text, counter_text;
    public TextMeshProUGUI lose_points_count; 
    public int count_guess = 0;
    private int index_color;
    private float maxtime = 5f;
    public Slider sliderTimer;
    public GameObject HandelSlider;
    private float timeleft;
    private bool gameStatus = false;
    private string[] previxs = {"","",""}; // 0 - color, 1 - figure, 2 - side
    private int[] choise_words_index = {0,0,0};
    public bool isHard = false;
    private Dictionary<int,int> generatedLevelChance = new Dictionary<int,int>{
    {0,50},//No
    {1,50},//Color
    {2,50},//No
    {3,50},//Figure
    {4,50},//No
    {5,50},//Side
    {6,90}//Procent of generate more true answer
    };
        
    void Start()
    {
        //generateGuess();
        
    }
    public void setHard(bool dif)
    {
        this.isHard = dif;
    }
    public void setCounterZero()
    {
        count_guess = 0;
        counter_text.text = ""+count_guess;
        gameStatus = true; 
    }
    void setPrefixNull()
    {
        for(int i = 0 ; i < previxs.Length;i++)
            previxs[i] = "";
    }
    bool checkIfZeroArray(int[] arr)
    {
        bool flag = false;
        for(int i=0;i<arr.Length;i++)
        {
            if(arr[i] == 1)
                flag = true;
        }
        return flag;
    }
    int generateRandExcept(int expectNumber,int count)
    {
        int rand = Random.Range(0,count);
            while(expectNumber == rand){
                rand = Random.Range(0,count);
            }
        return rand;
    }
    void printArray(int[] array){
        string str = "";
        for(int i=0;i<array.Length;i++)
            str+=array[i]+" ";
        print(str);
    }
    public void generateRandomNumbers()
    {
        if(isHard)
        generatedLevelChance = GetComponent<LibraryData>().getGenerateLevelChance(60);
        else
        generatedLevelChance = GetComponent<LibraryData>().getGenerateLevelChance(count_guess);

        int[] generated_array = {0,0,0,0,0,0  ,0}; 
        for(int i=1;i<generated_array.Length;i=i+2)
        {
        int random_numb = Random.Range(0,100);
            if(random_numb <= generatedLevelChance[i])
            {
                generated_array[i]=1;
                random_numb = Random.Range(0,100);
               
                if(random_numb <= generatedLevelChance[i-1])
                    generated_array[i-1]=1;
                
            }
        //Передаем параметры
        }
        
        
        printArray(generated_array);
        //print(generatedLevelChance);
        if(checkIfZeroArray(generated_array))
            generateGuess(generated_array);
        else
            generateRandomNumbers();
    }
    public void generateGuess(int[] generated_array){
        
        //ReapetGame();
        //TIMER
        //timerBar.gameObject.SetActive(true);
        LibraryData librarydata = GetComponent<LibraryData>();
        Image[] img = sliderTimer.GetComponentsInChildren<Image>();
        img[1].GetComponent<Image>().color=new ShopController().getColor(librarydata.index_shop[0]);
        timeleft=maxtime;
        //ALL FIGURES OFF
        for(int i=0;i<4;i++)
        {
            int count = items[i].GetComponent<ImageClass>().figures.Length;
            for(int j=0;j<count;j++)
                items[i].GetComponent<ImageClass>().figures[j].SetActive(false);
        }
        //GET COLORS
        List<ColorObj> colors_1 = new List<ColorObj>(colors.Count);
        colors.ForEach((item)=>
        {
            colors_1.Add(new ColorObj(item));
        });
        //RANDOM PREFIX
        setPrefixNull();
        //int random_figure_prefix = Random.Range(0,2);
        if(generated_array[0] == 1)
            previxs[0] = "no";
        //int random_color_prefix = Random.Range(0,2);
        if(generated_array[2] == 1)
            previxs[1] = "no";
        if(generated_array[4] == 1)
            previxs[2] = "no";

        //*ОПтимизировать - либо циклом, либо через функцию 
        if(generated_array[1] == 1)
            choise_words_index[0] = Random.Range(0,colors_1.Count-1);
        else
            choise_words_index[0] = colors_1.Count-1;

        if(generated_array[3] == 1)
            choise_words_index[1] = Random.Range(0,figures.Count-1);
        else 
            choise_words_index[1] = figures.Count-1;

        if(generated_array[5] == 1)
            choise_words_index[2] = Random.Range(0,side.Count-1);
        else 
            choise_words_index[2] = side.Count-1;
        //*
        index_color = Random.Range(0,items.Length);
        int side_rand = Random.Range(0,2);
        //Generate sides
        if(generated_array[5] == 1)
        {
            if(previxs[2] != "no")
                choise_words_index[2] = side.IndexOf(items[index_color].gameObject.GetComponent<ImageClass>().sides[side_rand]);
            else
            {
                string[] str = items[index_color].gameObject.GetComponent<ImageClass>().sides;
                int rand= Random.Range(0,side.Count-1);
                while(side.IndexOf(str[1]) == rand || side.IndexOf(str[0]) == rand){
                rand = Random.Range(0,side.Count-1);
                }
                choise_words_index[2] = rand;
            }
        }  
        else
            choise_words_index[2] = side.Count-1;
        //Text
        description.text = "Choose " +previxs[0] + " " + colors_1[choise_words_index[0]].name + " "+previxs[1]+" "+figures[choise_words_index[1]] + " "+ previxs[2] +" "+side[choise_words_index[2]];
        //Рандом цвета у текста
      //  int text_color_id = generateRandExcept(choise_words_index[0],colors.Count-1); 
      //  description.color = colors[text_color_id].color;
            
        if(previxs[0] == "no")
        {
            
            int rand= Random.Range(0,colors_1.Count-1);
            while(choise_words_index[0] == rand){
                rand = Random.Range(0,colors_1.Count-1);
            }
            items[index_color].GetComponent<ImageClass>().color = colors_1[rand].color;
            items[index_color].GetComponent<ImageClass>().name_color = colors_1[rand].name;
        }
        else
        {
            items[index_color].GetComponent<ImageClass>().color = colors_1[choise_words_index[0]].color;
            items[index_color].GetComponent<ImageClass>().name_color = colors_1[choise_words_index[0]].name;
        }
        
        if(previxs[1] == "no")
        {
            int rand = Random.Range(0,figures.Count-1);
            while(choise_words_index[1] == rand){
                rand = Random.Range(0,figures.Count-1);
            }
            items[index_color].GetComponent<ImageClass>().figures[rand].SetActive(true);
            items[index_color].GetComponent<ImageClass>().figures[rand].GetComponentInChildren<MeshRenderer>().material.color = items[index_color].GetComponent<ImageClass>().color;
            items[index_color].GetComponent<ImageClass>().name_object = figures[rand];
       
        }
        else
        {
            if(generated_array[3] != 1)
                items[index_color].GetComponent<ImageClass>().figures[figures.Count-2].SetActive(true);
            else
                items[index_color].GetComponent<ImageClass>().figures[choise_words_index[1]].SetActive(true);
            if(generated_array[1] ==1)
                {
                    if(generated_array[3] != 1)
                        items[index_color].GetComponent<ImageClass>().figures[figures.Count-2].GetComponentInChildren<MeshRenderer>().material.color = items[index_color].GetComponent<ImageClass>().color;
                    else
                        items[index_color].GetComponent<ImageClass>().figures[choise_words_index[1]].GetComponentInChildren<MeshRenderer>().material.color = items[index_color].GetComponent<ImageClass>().color;
       
                }
            items[index_color].GetComponent<ImageClass>().name_object = figures[choise_words_index[1]];
        }
        //ЗДЕСЬ МОЖНО СДЕЛАТЬ РАНДОМ 
        //if(previxs[0] != "no")
        //    colors_1.RemoveAt(choise_words_index[0]);
        for(int i=0;i<4;i++)
        {   
            int random_fake = Random.Range(0,colors_1.Count);
            int random_figure = Random.Range(0,4);

            int random_num = Random.Range(0,100);
            if(random_num <= generatedLevelChance[6])
            {
                if(Random.Range(0,100) > 50)
                {   if(generated_array[1] == 1)
                        random_fake=choise_words_index[0];
                }
                else{
                    if(generated_array[3] == 1)
                        random_figure=choise_words_index[1];
                }
            }

            if(i != index_color)
            {
                
                items[i].GetComponent<ImageClass>().color = colors_1[random_fake].color;
                items[i].GetComponent<ImageClass>().name_color = colors_1[random_fake].name;
            
                items[i].GetComponent<ImageClass>().figures[random_figure].SetActive(true);
                items[i].GetComponent<ImageClass>().figures[random_figure].GetComponentInChildren<MeshRenderer>().material.color = colors_1[random_fake].color;
                items[i].GetComponent<ImageClass>().name_object = figures[random_figure];
            }

        }
    }
    public void continuePlay()
    {
        if(this.GetComponent<LibraryData>().count_life > 0)
        {
        
        lose_screen.SetActive(false);
        start_game.SetActive(true);
        gameStatus=true;
        generateRandomNumbers();
        ReapetGame();
        this.GetComponent<LibraryData>().count_life--;
        this.GetComponent<LibraryData>().setCountLifeText();
        lose_screen.GetComponentInChildren<Timer>().setTimer();
        }
    }
    public void setDiamondsCount()
    {
        this.GetComponent<LibraryData>().setDiamonds((count_guess/5));
        this.GetComponent<LibraryData>().setRecord(count_guess,isHard);

    }
    public void ReapetGame(){
        GameObject cube = Instantiate(spawns_items[GetComponent<LibraryData>().index_shop[1]],new Vector3( HandelSlider.transform.position.x ,HandelSlider.transform.position.y+ Random.Range(-0.4f, 2.5f),
        HandelSlider.transform.position.z), HandelSlider.transform.rotation);
        LibraryData librarydata = GetComponent<LibraryData>();
        cube.GetComponentInChildren<MeshRenderer>().material.color = new ShopController().getColor(librarydata.index_shop[0]);
        Destroy(cube, 0.5f);
        if(gameStatus)        
            Invoke("ReapetGame",0.06f);
    }
    
    void Update()
    {
       
        if(gameStatus)
        {
            if(timeleft > 0 )
            {
                timeleft -= Time.deltaTime;
                sliderTimer.value = timeleft;
            }
            else 
            {
                lose_screen.SetActive(true);
                lose_points_count.text = "Ваш счёт: " +count_guess;
                Time.timeScale = 1;
              
                gameStatus = false;
            }
            if (Input.GetMouseButtonDown(0)){ 
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                
                ///FOR 3D
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit)){
                    string text_color = hit.collider.transform.parent.parent.gameObject.GetComponent<ImageClass>().name_color;
                    Color color_1 = hit.collider.transform.parent.parent.gameObject.GetComponent<ImageClass>().color;
                    string text_figure = hit.collider.transform.parent.parent.gameObject.GetComponent<ImageClass>().name_object;
                    string[] sides = hit.collider.transform.parent.parent.gameObject.GetComponent<ImageClass>().sides;
                    bool choise_true = true; 

                    if(choise_words_index[0] != colors.Count-1)
                    {
                    if(previxs[0] == "no" && color_1 == colors[choise_words_index[0]].color)
                    {
                        choise_true= false;
                    }
                    else if(previxs[0] != "no" && color_1 != items[index_color].GetComponent<ImageClass>().color)
                    {
                        choise_true= false;
                    }
                    }

                    if(choise_words_index[1] != figures.Count-1){
                    if(previxs[1] == "no" && text_figure == figures[choise_words_index[1]])
                    {
                        choise_true= false;
                    }
                    else if(previxs[1] != "no" && text_figure != items[index_color].GetComponent<ImageClass>().name_object)
                    {
                        choise_true = false;
                    }
                    }

                    if(choise_words_index[2] != side.Count - 1)
                    {
                        
                        if(previxs[2] == "no" && (sides[0]==side[choise_words_index[2]] || sides[1] ==side[choise_words_index[2]]))
                        {
                            choise_true= false;
                        }
                        else if(previxs[2] != "no" && (sides[0]!=side[choise_words_index[2]] && sides[1] !=side[choise_words_index[2]]))
                        {
                            choise_true = false;
                        }
                    }

                    if(choise_true)
                    {
                       //print(true);
                       count_guess++;
                       counter_text.text = ""+count_guess;
                       generateRandomNumbers();//СЮДА ИЗМЕНЯТь
                    }
                    else
                    {
                       //print(false);
                        timeleft = 0;
                    }
                }
                
            
            }
        }
        if (Input.GetMouseButtonDown(0)){ // if left button pressed...
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                
                ///FOR 3D
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                //    print(hit.collider.gameObject.layer);
                    //if(hit.collider.isTrigger)
                    //hit.collider.gameObject.layer=2;
                    if(hit.collider.gameObject.tag == "Play")
                    {
                        
                        menu.SetActive(false);
                        start_game.SetActive(true);
                        if(hit.collider.gameObject.name == "CubeButtonInsane")
                            isHard = true;
                        else
                            isHard = false;
                        setCounterZero();
                        generateRandomNumbers();//СЮДА ИЗМЕНЯТЬ
                        ReapetGame();
                        
                    }
        }




    }
}
