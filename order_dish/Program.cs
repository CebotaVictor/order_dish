using System;
using System.IO;
using System.Net;
using System.Net.Quic;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Threading.Channels;
using ObjectLists;

namespace order_dish
{


    class Dish
    {
        private IngredientList _ingredients;

        public string dishName { get; }
        public string dishDescription { get; }
        public int minutesToFinishDish { get; }
        public float dishPrice { get; private set; }

        public Dish(string name, string description, IngredientList ingredients, int minutes, float price)
        {
            _ingredients = ingredients;
            dishName = name;
            dishDescription = description;
            minutesToFinishDish = minutes;
            dishPrice = price;
        }
    }

    class Ingredient
    {
        public string ingredientName;
        public int ingredinetPrice;

        public Ingredient()
        {
            ingredientName = "";
            ingredinetPrice = 0;
        }
    }

 

    class Cook
    {
        private List<Dish> _dishesAssigendToCook;
        public string cookName;
        public int numberOfDishesAssigned
        {
            get
            {
                return _dishesAssigendToCook.Count;
            }
            set { }
        }

        public Cook(string name)
        {
            cookName = name;
            _dishesAssigendToCook = new List<Dish>();
            numberOfDishesAssigned = 0;
        }

        public bool addDishesToCook(Dish dish)
        {
            if (dish == null) throw new NullReferenceException("Dish is null");
            _dishesAssigendToCook.Add(dish);
            return true;
        }
        
        public List<Dish> returnDisheList()
        {
            return _dishesAssigendToCook;
        }

    }
    

    
    internal class Program
    {     


        public static CookList ReturnCookList()
        {
            CookList cookList = new CookList();
            Cook cook = new Cook("John");
            cookList.addCookToList(cook);
            cook = new Cook("Larry");
            cookList.addCookToList(cook);
            return cookList;
        }


        public static int Rand()
        {
            Random random = new Random();
            int rand_number = random.Next(10, 30);
            return rand_number;
        }

        public static void Menu()
        {
            //here is the list of all dishes and their appropriate descriptions 
            string[,] dishes = new string[10, 2];
            dishes[0, 0] = "Scrambled Eggs";
            dishes[0, 1] = "Scrambled eggs are a quick and easy breakfast dish made by mixing eggs, milk/cream, salt, and pepper. Optional ingredients include cheese and veggies like onions and spinach";
            dishes[1, 0] = "Pasta with Tomato Sauce";
            dishes[1, 1] = "Pasta with tomato sauce is a classic Italian dish made by cooking pasta and topping it with a sauce made from canned tomatoes, garlic, onion, olive oil, and herbs like basil and oregano.";
            dishes[2, 0] = "Lentil Soup";
            dishes[2, 1] = "Lentil soup is a hearty and nutritious dish made by cooking lentils with carrots, onion, celery, vegetable broth, and herbs like thyme and bay leaf.";
            dishes[3, 0] = "Grilled Cheese";
            dishes[3, 1] = "Grilled cheese is a simple and delicious sandwich made by grilling bread with cheese and butter";
            dishes[4, 0] = "Tuna Salad Sandwich";
            dishes[4, 1] = "Tuna salad sandwich is a tasty and satisfying sandwich made by mixing canned tuna with mayonnaise, onion, and celery, and serving it on bread with lettuce.";
            DishIngredientList newList = objLists.ReturnIngredentslists();

            string choice;
            Console.WriteLine("Create your order by selecting the dishes by their respective name and your dish will be assigned to the appropriate cook");
            Console.WriteLine($"Scrambled Eggs({newList.calculateTotalPrice("Eggs") + (newList.calculateTotalPrice("Eggs") * 20)/100} mdl). Description - {dishes[0,1]}. \nInput name - Eggs\n");
            Console.WriteLine($"Pasta with Tomato Sauce({newList.calculateTotalPrice("Pasta") + (newList.calculateTotalPrice("Eggs") * 20) / 100} mdl). Description - {dishes[0,1]}. \nInput name - Pasta\n");
            Console.WriteLine($"Lentil Soup({newList.calculateTotalPrice("LentilSoup") + (newList.calculateTotalPrice("LentilSoup") * 20) / 100} mdl). Description - {dishes[0, 1]}.  \nInput name -  LentilSoup\n");
            Console.WriteLine($"Grilled Cheese({newList.calculateTotalPrice("GrilledCheese") + (newList.calculateTotalPrice("GrilledCheese") * 20) / 100} mdl). Description - {dishes[0, 1]}.  \nInput name -  GrilledCheese\n");
            Console.WriteLine($"Tuna Salad Sandwich({newList.calculateTotalPrice("GrilledCheese") + (newList.calculateTotalPrice("GrilledCheese") * 20) / 100} mdl). Description - {dishes[0, 1]}.  \nInput name - TunaSaladSandwich\n");

            Console.WriteLine("How many dishes do you want to order ? ");
            string nrOfDishes = Console.ReadLine() ?? "0";
            if (int.TryParse(nrOfDishes, out int nr))
            {
                DishIngredientList list = objLists.ReturnIngredentslists();
                Dish dish;
                Cook cook;
                CookList cookList = ReturnCookList();
                int i = 0;

                while (i < nr)
                {
                    Console.WriteLine("Enter the dish name: ");
                    choice = Convert.ToString(Console.ReadLine()) ?? "";
                    try
                    {
                        dish = new Dish(dishes[0, 0], dishes[0, 1], list.getCurrentIngredientsList(choice), Rand(), list.calculateTotalPrice(choice));
                        bool isAdded = cookList.addDishesToSortedCooks(dish);
                        if (isAdded == false) { Console.WriteLine("No cooks available"); break; }
                        cook = cookList.returnCurrectCookWithNrOfDishes();
                        Console.WriteLine($"Dish assigned to cook {cook.cookName} the time estimated for {choice} is {dish.minutesToFinishDish} minutes. His order lists include {cook.numberOfDishesAssigned}");
                    }
                    catch (Exception ex) { Console.WriteLine("Incorrect dish name, try again!"); continue; }
                    i++;
                }

              
                TimeSpan estimatedTime = new TimeSpan(0, cookList.calculateTotalCookTime(), 0);

                Console.WriteLine($"Estimated time is {estimatedTime}");
                Console.WriteLine($"Total acumulated price is {cookList.calculateTotalDishPrice() + (cookList.calculateTotalDishPrice() * 20) / 100} mdl");
            }
            else
            {
                Console.WriteLine("Please Input a valid number"); return;
            }
           

        }

        
        static void Main(string[] args)
        {
            Menu();

        }
    }
}
