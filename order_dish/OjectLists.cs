using order_dish;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;


namespace ObjectLists
{
    class IngredientList
    {
        public List<Ingredient>? ingredients { get; private set; }
        public float dishPrice { get; private set; }

        public IngredientList()
        { 
            ingredients = new List<Ingredient>();
        }
        public void add(Ingredient ingredient)
        {
            if (ingredient == null) throw new NullReferenceException("Ingredient is null");
            ingredients?.Add(ingredient);
        }
    }



    class CookList
    {
        public List<Cook> cooks;
        public int CooksCount
        {
            get
            {
                return cooks.Count;
            }
        }

        public CookList()
        {
            cooks = new List<Cook>();
        }

        public void addCookToList(Cook cook)
        {
            if (cook == null) throw new NullReferenceException("Cook is null");
            cooks.Add(cook);
        }

        public bool addDishesToSortedCooks(Dish dish)
        {
            cooks.Sort((x, y) => x.numberOfDishesAssigned.CompareTo(y.numberOfDishesAssigned));//This method sort implements QuickSort.On average, this method is an O(n log n) operation, where n is Count; in the worst case it is an O(n ^ 2) operation.
            Cook cook = cooks[0];
            if (cook.numberOfDishesAssigned >= 5) { return false; }
            cook.addDishesToCook(dish);
            return true;
        }

        public Cook returnCurrectCookWithNrOfDishes()
        {
            cooks.Sort((x, y) => -x.numberOfDishesAssigned.CompareTo(y.numberOfDishesAssigned));
            return cooks[0];
        }

        public int calculateTotalCookTime()
        {
            int totalCookTime = 0;
            foreach (Cook cook in cooks)
            {
                foreach (Dish dish in cook.returnDisheList())
                {
                    totalCookTime += dish.minutesToFinishDish;
                }
            }
            return totalCookTime;
        }

        public float calculateTotalDishPrice()
        {
            int totalDishPrice = 0;
            foreach (var cook in cooks)
            {
                foreach (var dish in cook.returnDisheList())
                {
                    totalDishPrice += (int)dish.dishPrice;
                }
            }
            return totalDishPrice;
        }
    }

    class DishIngredientList
        {
        private Dictionary<string, IngredientList> _ingredients;

        public DishIngredientList()
        {
            _ingredients = new Dictionary<string, IngredientList>();
        }

        public void set_ingredients(string name, IngredientList i)
        {
            if (name == null) throw new NullReferenceException("The ingredients list is null");
            _ingredients[name] = i;
        }
        public IngredientList getCurrentIngredientsList(string name)
        {
            if (_ingredients.ContainsKey(name))
                return _ingredients[name];
            else throw new Exception("this name is nonexistent");
        }

        public float calculateTotalPrice(string name)
        {
            IngredientList dishLstget = getCurrentIngredientsList(name);
            float total = 0;
            foreach (var ingredient in dishLstget.ingredients)
            {
                total += ingredient.ingredinetPrice;
            }
            return total;
        }

    }

    class objLists
    {
        public static DishIngredientList ReturnIngredentslists()
        {

            //here is a list with all ingresients, after this list is created, the owner of this list can find the best choice for the dish, as an self with ingredients.

            //Eggs, milk/cream , salt, pepper, optional: cheese, veggies (onions, spinach) - order dish 
            IngredientList Eggs = new IngredientList();
            Eggs.add(new Ingredient { ingredientName = "Eggs", ingredinetPrice = 7 });
            Eggs.add(new Ingredient { ingredientName = "milk/cream", ingredinetPrice = 8 });
            Eggs.add(new Ingredient { ingredientName = "salt", ingredinetPrice = 3 });
            Eggs.add(new Ingredient { ingredientName = "oil", ingredinetPrice = 5 });

            //Pasta, canned tomatoes, garlic, onion, olive oil, herbs (basil, oregano)
            IngredientList Pasta = new IngredientList();
            Pasta.add(new Ingredient { ingredientName = "Pasta", ingredinetPrice = 9 });
            Pasta.add(new Ingredient { ingredientName = "canned tomatoes", ingredinetPrice = 10 });
            Pasta.add(new Ingredient { ingredientName = "garlic", ingredinetPrice = 11 });
            Pasta.add(new Ingredient { ingredientName = "onion", ingredinetPrice = 12 });
            Pasta.add(new Ingredient { ingredientName = "olive oil", ingredinetPrice = 13 });
            Pasta.add(new Ingredient { ingredientName = "herbs", ingredinetPrice = 12 });

            //Lentil Soup: Lentils, carrots, onion, celery, vegetable broth, herbs (thyme, bay leaf)
            IngredientList LentilSoup = new IngredientList();
            LentilSoup.add(new Ingredient { ingredientName = "Lentils", ingredinetPrice = 9 });
            LentilSoup.add(new Ingredient { ingredientName = "carrots", ingredinetPrice = 10 });
            LentilSoup.add(Pasta.ingredients?.Find(x => x.ingredientName == "onion") ?? new Ingredient { ingredientName = "onion", ingredinetPrice = 12 }); // in pasta we already have onion ingredinet
            LentilSoup.add(new Ingredient { ingredientName = "celery", ingredinetPrice = 8 });
            LentilSoup.add(new Ingredient { ingredientName = "vegetable broth", ingredinetPrice = 12 });
            LentilSoup.add(new Ingredient { ingredientName = "herbs", ingredinetPrice = 19 });

            //Grilled Cheese: Bread, cheese, butter
            IngredientList GrilledCheese = new IngredientList();
            GrilledCheese.add(new Ingredient { ingredientName = "Bread", ingredinetPrice = 9 });
            GrilledCheese.add(new Ingredient { ingredientName = "Cheese", ingredinetPrice = 10 });
            GrilledCheese.add(new Ingredient { ingredientName = "butter", ingredinetPrice = 12 });


            //Tuna Salad Sandwich: Canned tuna, mayonnaise, onion, celery, lettuce, bread
            IngredientList TunaSaladSandwich = new IngredientList();
            GrilledCheese.add(new Ingredient { ingredientName = "Canned tuna", ingredinetPrice = 10 });
            GrilledCheese.add(new Ingredient { ingredientName = "mayonnaise", ingredinetPrice = 11 });
            GrilledCheese.add(Pasta.ingredients?.Find(x => x.ingredientName == "onion") ?? new Ingredient { ingredientName = "onion", ingredinetPrice = 12 }); // in pasta we already have onion ingredinet
            GrilledCheese.add(new Ingredient { ingredientName = "celery", ingredinetPrice = 11 });
            GrilledCheese.add(new Ingredient { ingredientName = "lettuce", ingredinetPrice = 11 });
            GrilledCheese.add(new Ingredient { ingredientName = "bread", ingredinetPrice = 11 });

            DishIngredientList ingredientList = new DishIngredientList();
            ingredientList.set_ingredients("Eggs", Eggs);
            ingredientList.set_ingredients("Pasta", Pasta);
            ingredientList.set_ingredients("LentilSoup", LentilSoup);
            ingredientList.set_ingredients("GrilledCheese", GrilledCheese);
            ingredientList.set_ingredients("TunaSaladSandwich", TunaSaladSandwich);


            return ingredientList;

        }


    }

}
