using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class sl_InfoDishTFP : MonoBehaviour
{
    public Sprite[] pictures;

    public Image pic;
    public TextMeshProUGUI dishName;
    public TextMeshProUGUI dishInfo;

    int rand;

    void Start()
    {
        rand = Random.Range(0, pictures.Length);

    }

    private void Update()
    {
        //dishes
        #region
        if(rand == 1)
        {
            pic.sprite = pictures[0];

            dishName.text = "Bird's Nest Soup";
            dishInfo.text = "A dish made with an edible bird’s nest from specific types of Swiftlet birds that uses its saliva to make the nest, that is sometimes known as the “Caviar of the East”. The dish is said to be very nutritional, able to help maintain youth and a strong body and was typically consumed only by imperial nobility back in the Ming Dynasty. Today, the dish is served mostly in high-end restaurants around China with a small bowl portion going for around $33 and some even reaching $100 and above.";
        }
        if (rand == 2)
        {
            pic.sprite = pictures[1];
            dishName.text = "Buddha jumps over the wall";
            dishInfo.text = "Also known as Fo Tiao Qiang, this dish is a very extravagant and luxury herbal soup in China that is made with premium or high-end ingredients, such as chicken, abalone and duck kidney that if ordered in a restaurant, would require a day in advance to prepare.The dish received its name according to historical records, from a meditating monk that jumped over a wall just to eat this dish upon smelling it being cooked.";
        }
        if (rand == 3)
        {
            pic.sprite = pictures[2];
            dishName.text = "Hassun/ Seasonal Platter";
            dishInfo.text = "Part of a traditional multi-course Japanese dinner called Kaiseki which is a well-known part of high-class Japanese cuisine. A Hassun is served as part of the main course that consists of seasonal meats and vegetables cooked simply in order to let the ingredient’s flavour shine and plated up in a very extravagant manner as a feast for the diner’s eyes.";
        }
        if (rand == 4)
        {
            pic.sprite = pictures[3];
            dishName.text = "Mukozuke/ Sashimi Platter";
            dishInfo.text = "A plate of elegantly served and prepared sashimi served as part of a Kaiseki’s main course. Also known as Otsukuri at times, this platter is prepared using elaborate techniques by the chef in order to make the dish not only look beautiful but also to maximise the fish or seafood’s flavour. The types of seafood used as part of this dish varies from location as well as the season.";
        }
        if (rand == 5)
        {
            pic.sprite = pictures[4];
            dishName.text = "Tojangjochi/ Soybean Paste Stew";
            dishInfo.text = "Served as part of daily court meals to the royal family which was known as Surasang. This dish was made with beef, vegetables and simmered in a soybean paste broth. According to Korean history scholars, this dish has since evolved into Doenjang-Jjigae, a similar type of stew enjoyed by Koreans in their daily lives";
        }
        if (rand == 6)
        {
            pic.sprite = pictures[5];
            dishName.text = "Sinseollo/ Royal Hotpot";
            dishInfo.text = "Offered to guests of the Korean royal family as part of a banquet meal called Myeonsang, back in the Joseon period. This particular dish is normally eaten during birthday celebrations of royal family members or during certain holidays. Typically made with a type of meatballs, fish and other seasonal vegetables, all simmered in a beef broth.";

        }
        if (rand == 7)
        {
            pic.sprite = pictures[6];
            dishName.text = "Foxtail millet pumpkin cake";
            dishInfo.text = "This particular dish was inspired from a dish served in a restaurant in Taiwan called Akame. The restaurant specialises in serving high end dishes made using typical ingredients used by the aboriginal people of Taiwan, in hopes of helping preserve and share the culture of the aboriginal people of Taiwan. This particular dish is a dessert made using foxtail millet and pumpkin which were both very important foods to the indegenous people of Taiwan in the past and is still eaten by them to this day.";

        }
        if (rand == 8)
        {
            pic.sprite = pictures[7];
            dishName.text = "Raw Stinky Tofu & century eggs salad";
            dishInfo.text = "This unique dish was taken inspiration from a particular restaurant in Taipei named, “Dai’s House of Unique Stink” which happens to be the only place in Taiwan that serves this unique dish. The dish is made using a specific variant of stinky tofu that is specially made by the restaurant with their own special brine with the tofu being so specific, it has a stamp placed on them. The salad is then made by the restaurant’s owner, Ms. Wu by taking the raw stinky tofu, slicing it and serving it on a plate with a few halves of century eggs, topped with a sort of soy sauce glaze, and scallion based topping.";

        }
        #endregion

        //food
        if (rand == 9)
        {
            pic.sprite = pictures[8];

            dishName.text = "Nian Gao";
            dishInfo.text = "Traditional Chinese food is made with glutinous rice flour and eaten during the Chinese New Year. Believed to carry auspicious meaning and can help with sending good wishes for a better year.";
        }
        if (rand == 10)
        {
            pic.sprite = pictures[9];

            dishName.text = "Spring Rolls";
            dishInfo.text = "Also known as Chun Juan, it is typically eaten during Chinese New Year as a symbol of wealth and prosperity. Made by filling a thin flour wrapper with vegetable or meat and either fried, baked or served as is.";
        }
        if (rand == 11)
        {
            pic.sprite = pictures[10];

            dishName.text = "Wonton Soup";
            dishInfo.text = "Made with a small amount of filling, which can be ground pork and shrimp, wrapped in a square wrapper. Said to bring a streak of good luck in the new year when eaten on the 2nd day of the upcoming year.";
        }
        if (rand == 12)
        {
            pic.sprite = pictures[11];

            dishName.text = "Ichigo Daifuku";
            dishInfo.text = "Made with strawberries and mochi stuffed or filled with red bean paste that is commonly available between winter and spring in Japan. Can be found in sweet shops, supermarkets or food stalls at festivals.";
        }
        if (rand == 13)
        {
            pic.sprite = pictures[12];

            dishName.text = "Ikanago";
            dishInfo.text = "Made from Japanese sand eels caught in the Kansai region in Japan between February and March. Consists of eels, caramelised soy sauce, mirin sugar and ginger. Eaten with rice or as a snack.";
        }
        if (rand == 14)
        {
            pic.sprite = pictures[13];

            dishName.text = "Sakura Mochi";
            dishInfo.text = "A traditional Japanese sweet is made of pink-coloured mochi stuffed with red bean paste and wrapped in a pickled sakura leaf that is popular throughout spring in Japan.";
        }
        if (rand == 15)
        {
            pic.sprite = pictures[14];

            dishName.text = "Bap Burger (Korean Rice Burger)";
            dishInfo.text = "Trendy street food in Korea that is made using seasoned or flavoured Korean rice balls as the burger’s buns and the filling consisting of typical Korean foods such as kimchi and tuna.";
        }
        if (rand == 16)
        {
            pic.sprite = pictures[15];

            dishName.text = "Japchae";
            dishInfo.text = "Typically eaten during Korean festive holidays such as New Year or the Harvest festival, but is also enjoyed on other common days. Made with glass noodles, mixed vegetables and seasoned meats.";
        }
        if (rand == 17)
        {
            pic.sprite = pictures[16];

            dishName.text = "Tteokbokki";
            dishInfo.text = "This food is one of the most popular street foods in Korea which is primarily made with Korean rice cakes called Tteok and gochujang or Korean chilli paste.";
        }
        if (rand == 18)
        {
            pic.sprite = pictures[17];

            dishName.text = "Bubble Tea";
            dishInfo.text = "Invented in the 1940s by Chang Fan Shu and sold at tea shops in Taiwan that sold hand-shaken tea made with cocktail shakers. A very popular drink that has gone global, made using tapioca pearls.";
        }
        if (rand == 19)
        {
            pic.sprite = pictures[18];

            dishName.text = "Pineapple Cake";
            dishInfo.text = "A square or rectangular shortcrust pastry filled with a jammy pineapple filling popular in Taiwan that is enjoyed all year round, but popularly eaten during Chinese New Year for good luck.";
        }
        if (rand == 20)
        {
            pic.sprite = pictures[19];

            dishName.text = "Taro Balls";
            dishInfo.text = "A classic Taiwanese dessert that originated from Jiufen made with taro, sweet potato and tapioca starch. Served in multiple ways, either hot or cold, depending on the season with jellies, cream and/or sugar syrup.";
        }
    }

    public void ToServerLobby()
    {
        SceneManager.LoadScene("sl_ServerLobby");
    }
}
