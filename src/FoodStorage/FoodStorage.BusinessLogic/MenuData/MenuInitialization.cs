using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

using UnitsNet;
using UnitsNet.Units;

namespace PseudoMenu.MenuData
{


    internal class MenuInitialization
    {


        MenuCollection menuCollection = new MenuCollection();



        internal MenuInitialization()
        {
           

         Dish dish1 = new Dish("Мясное ассорти", "Закуски"); // Закуска
            dish1.AddProduct(new Product() { name = "Мясо куриное филе", quantity = Mass.FromGrams(600) }); 
            dish1.AddProduct(new Product() { name = "Мясо свинина", quantity = Mass.FromKilograms(1) }); ;
            dish1.AddProduct(new Product() { name = "Мясо говядина", quantity = Mass.FromKilograms(0.5) }); ;
            dish1.AddProduct(new Product() { name = "Мясо телятина", quantity = Mass.FromKilograms(0.5) }); ;
            dish1.AddProduct(new Product() { name = "Соль", quantity = Mass.FromGrams(5) }); ;
            dish1.AddProduct(new Product() { name = "Перец красный молотый", quantity = Mass.FromGrams(5) }); ;

            Dish dish2 = new Dish("Рыбное ассорти", "Закуски"); // Закуска
            dish2.AddProduct(new Product() { name = "Рыба Скумбрия", quantity = Mass.FromGrams(500) }); ;
            dish2.AddProduct(new Product() { name = "Рыба Горбуша", quantity = Mass.FromGrams(500) }); ;
            dish2.AddProduct(new Product() { name = "Рыба Белый Амур", quantity = Mass.FromGrams(500) }); ;
            dish2.AddProduct(new Product() { name = "Морковь свежая", quantity = Mass.FromGrams(100) });
        
            dish2.AddProduct(new Product() { name = "Лук репчатый свежий", quantity = Mass.FromGrams(100) }); 
            dish2.AddProduct(new Product() { name = "Соль", quantity = Mass.FromGrams(10) }); ;
            dish2.AddProduct(new Product() { name = "Специи для рыбы", quantity = Mass.FromGrams(10) }); 
            dish2.AddProduct(new Product() { name = "Перец красный молотый", quantity = Mass.FromGrams(5) }); 
            dish2.AddProduct(new Product() { name = "Лавровый лист", quantity = Mass.FromGrams(5) }); 


            Dish dish3 = new Dish("Салат Оливье", "Салаты"); // Салат
            dish3.AddProduct(new Product() { name = "Колбаса вареная", quantity = Mass.FromGrams(300) }); 
            dish3.AddProduct(new Product() { name = "Горошек консервированный", quantity = Mass.FromGrams(200) });
            dish3.AddProduct(new Product() { name = "Яйца куриные", quantity = Scalar.FromAmount(4) }); 
            dish3.AddProduct(new Product() { name = "Картофель свежий", quantity = Mass.FromGrams(200) }); 
            dish3.AddProduct(new Product() { name = "Огурци консервированные", quantity = Mass.FromGrams(200) }); 
            dish3.AddProduct(new Product() { name = "Соль", quantity = Mass.FromGrams(5) }); ;
            dish3.AddProduct(new Product() { name = "Перец черный молотый", quantity = Mass.FromGrams(5) }); 

            Dish dish4 = new Dish("Салат Греческий", "Салаты"); // Салат
            dish4.AddProduct(new Product() { name = "Огурец свежий", quantity = Mass.FromGrams(100) }); 
            dish4.AddProduct(new Product() { name = "Помидор свежий", quantity = Mass.FromGrams(200) }); 
            dish4.AddProduct(new Product() { name = "Перец болгарский свежий", quantity = Mass.FromGrams(100) }); 
            dish4.AddProduct(new Product() { name = "Маслины консервированные", quantity = Mass.FromGrams(20) }); 
            dish4.AddProduct(new Product() { name = "Лук репчатый свежий", quantity = Mass.FromGrams(100) }); 
            dish4.AddProduct(new Product() { name = "Сыр Фета", quantity = Mass.FromGrams(100) }); ;
            dish4.AddProduct(new Product() { name = "Петрушка свежая", quantity = Mass.FromGrams(10) }); 
            dish4.AddProduct(new Product() { name = "Специи Итальянские травы", quantity = Mass.FromGrams(10) }); 
            
            dish4.AddProduct(new Product() { name = "Масло оливковое", quantity = Volume.FromMilliliters(10) }); 

            Dish dish5 = new Dish("Салат Цезарь", "Салаты"); // Салат
            dish5.AddProduct(new Product() { name = "Листья салата айсберг свежие", quantity = Mass.FromGrams(30) }); 
            dish5.AddProduct(new Product() { name = "Лимон свежий", quantity = Mass.FromGrams(100) }); 
            dish5.AddProduct(new Product() { name = "Сыр Пормезан", quantity = Mass.FromGrams(100) });
            dish5.AddProduct(new Product() { name = "Мясо куриная грудка", quantity = Mass.FromGrams(400) });
            dish5.AddProduct(new Product() { name = "Яйца куриные", quantity = Scalar.FromAmount(2) });
            dish5.AddProduct(new Product() { name = "Масло оливковое", quantity = Volume.FromMilliliters(10) });
            dish5.AddProduct(new Product() { name = "Чеснок свежий", quantity = Mass.FromGrams(20) }); 
            dish5.AddProduct(new Product() { name = "Горчица соус", quantity = Mass.FromGrams(20) }); 
            dish5.AddProduct(new Product() { name = "Помидор Черрии свежий", quantity = Mass.FromGrams(100) } ); 
            dish5.AddProduct(new Product() { name = "Соль", quantity = Mass.FromGrams(10) }); ;

            Dish dish6 = new Dish("Свинной шашлык", "Горячие блюда"); // Горячие блюда
            dish6.AddProduct(new Product() { name = "Мясо свинина", quantity = Mass.FromKilograms(2) } ); 
            dish6.AddProduct(new Product() { name = "Соус Аджика", quantity = Mass.FromGrams(15) } ); 
            dish6.AddProduct(new Product() { name = "Соус майонез", quantity = Mass.FromGrams(20) } ); 
            dish6.AddProduct(new Product() { name = "Лук репчатый свежий", quantity = Mass.FromGrams(100) }); 
            dish6.AddProduct(new Product() { name = "Соль", quantity = Mass.FromGrams(5) }); 

            Dish dish7 = new Dish("Куриный шашлык", "Горячие блюда"); // Горячие блюда
            dish7.AddProduct(new Product() { name = "Мясо куриное филе", quantity = Mass.FromKilograms(1) }); //1 кг
            dish7.AddProduct(new Product() { name = "Лук репчатый свежий", quantity = Mass.FromGrams(200) }); // 2 шт.
            dish7.AddProduct(new Product() { name = "Соус майонез", quantity = Volume.FromMilliliters(150) }); // 150 мл.
            dish7.AddProduct(new Product() { name = "Специи для курици", quantity = Mass.FromGrams(10) }); // 10- мг
            dish7.AddProduct(new Product() { name = "Соль", quantity = Mass.FromGrams(10) }); // 10 мг
            dish7.AddProduct(new Product() { name = "Перец черный молотый", quantity = Mass.FromGrams(10) }); // 10 мг
            dish7.AddProduct(new Product() { name = "Лимон свежий", quantity = Mass.FromGrams(100) }); // 1 шт.
            dish7.AddProduct(new Product() { name = "Масло растительное", quantity = Volume.FromMilliliters(100) }); // 1 шт.

            Dish dish8 = new Dish("Жаркое с грибами", "Горячие блюда"); // Горячие блюда
            dish8.AddProduct(new Product() { name = "Картофель свежий", quantity = Mass.FromKilograms(1) }); // 1 кг
            dish8.AddProduct(new Product() { name = "Морковь свежая", quantity = Mass.FromGrams(100) }); // 2 шт.
            dish8.AddProduct(new Product() { name = "Грибы Шампиньоны", quantity = Mass.FromGrams(400) }); // 400 г
            dish8.AddProduct(new Product() { name = "Горошек консервированный", quantity = Mass.FromGrams(250) }); //250 г.
            dish8.AddProduct(new Product() { name = "Масло растительное", quantity = Volume.FromMilliliters(100) }); // 6 ложек
            dish8.AddProduct(new Product() { name = "Соус соевый", quantity = Volume.FromMilliliters(100) }); // 4 ложки
            dish8.AddProduct(new Product() { name = "Соль", quantity = Mass.FromGrams(10) }); // 10 г.
            dish8.AddProduct(new Product() { name = "Перец красный молотый", quantity = Mass.FromGrams(10) }); // 10 г.
         




            Dish dish9 = new Dish("Свиной стейк", "Горячие блюда"); // Горячие блюда
            dish9.AddProduct(new Product() { name = "Мясо свинина вырезка", quantity = Mass.FromGrams(450) }); // 450 г
            dish9.AddProduct(new Product() { name = "Сыр твердый", quantity = Mass.FromGrams(60) }); // 60 г.
            dish9.AddProduct(new Product() { name = "Помидор свежий", quantity = Mass.FromGrams(150) }); // 1 шт.
            dish9.AddProduct(new Product() { name = "Лук репчатый свежий", quantity = Mass.FromGrams(100) }); // 1 шт.
            dish9.AddProduct(new Product() { name = "Масло растительное", quantity = Volume.FromMilliliters(20) }); // 2 ч лож
            dish9.AddProduct(new Product() { name = "Горчица в зернах", quantity = Mass.FromGrams(10) }); // 1 ч лож
            dish9.AddProduct(new Product() { name = "Соус гранатовый", quantity = Volume.FromMilliliters(20) }); // 1 ч лож
            dish9.AddProduct(new Product() { name = "Чеснок свежий", quantity = Mass.FromGrams(20) }); // 2 зуб
            dish9.AddProduct(new Product() { name = "Специи Паприка", quantity = Mass.FromGrams(20) }); // по вкусу
            dish9.AddProduct(new Product() { name = "Специи Кориандр", quantity = Mass.FromGrams(10) }); // по вкусу
            dish9.AddProduct(new Product() { name = "Соль", quantity = Mass.FromGrams(10) }); // по вкусу
            dish9.AddProduct(new Product() { name = "Перец черный молотый", quantity = Mass.FromGrams(10) }); // по вкусу


            Dish dish10 = new Dish("Трюфеля мясные с грибами ", "Горячие блюда"); // Горячие блюда
            dish10.AddProduct(new Product() { name = "Мясо фарш говяжий", quantity = Mass.FromGrams(600) }); //600 г.
            dish10.AddProduct(new Product() { name = "Лук репчатый свежий", quantity = Mass.FromGrams(100) }); // 2 шт.
            dish10.AddProduct(new Product() { name = "Картофель свежий", quantity = Mass.FromGrams(200) });  // картофель 6 шт.
            dish10.AddProduct(new Product() { name = "Морковь свежая свежая", quantity = Mass.FromGrams(100) }); // 2 шт.
            dish10.AddProduct(new Product() { name = "Масло сливочное", quantity = Volume.FromMilliliters(50) }); // 7 ч ложек
            dish10.AddProduct(new Product() { name = "Сыр Ломбер", quantity = Mass.FromGrams(150) }); // сыр ломбер тертый 6 ст. ложек
            dish10.AddProduct(new Product() { name = "Соль", quantity = Mass.FromGrams(10) }); // 10 г.


            Dish dish11 = new Dish("Картофель фри", "Гарниры"); //Гарнир
            dish11.AddProduct(new Product() { name = "Картофель", quantity = Mass.FromKilograms(1) }); 
            dish11.AddProduct(new Product() { name = "Масло растительное", quantity = Volume.FromLiters(1) });
            dish11.AddProduct(new Product() { name = "соль", quantity = Mass.FromGrams(50) });

            Dish dish12 = new Dish("Картофель запеченый по домашнему", "Гарниры"); //Гарнир
            dish12.AddProduct(new Product() { name = "Картофель свежий", quantity = Mass.FromKilograms(1.5) });
            dish12.AddProduct(new Product() { name = "Чеснок свежий", quantity = Mass.FromGrams(100) });
            dish12.AddProduct(new Product() { name = "Масло растительное", quantity = Volume.FromMilliliters(100) });
            dish12.AddProduct(new Product() { name = "Соль", quantity = Mass.FromGrams(50) });

            Dish dish13 = new Dish("Рис", "Гарниры"); //Гарнир
            dish13.AddProduct(new Product() { name = "Рис", quantity = Mass.FromGrams(150) });
            dish13.AddProduct(new Product() { name = "Соль", quantity = Mass.FromGrams(5) });

            Dish dish14 = new Dish("Овощи на гриле", "Гарниры"); //Гарнир
            dish14.AddProduct(new Product() { name = "Кабачек свежий", quantity = Mass.FromGrams(500) });
            dish14.AddProduct(new Product() { name = "Баклажан свежий", quantity = Mass.FromGrams(500) });
            dish14.AddProduct(new Product() { name = "Помидор свежий", quantity = Mass.FromGrams(300) });
            dish14.AddProduct(new Product() { name = "Кукуруза в зернах", quantity = Mass.FromGrams(80) });

            Dish dish15 = new Dish("Голубци", "Промежуточные блюда"); // Промежуточные блюда
            dish15.AddProduct(new Product() { name = "Мясо свинина", quantity = Mass.FromGrams(500) });
            dish15.AddProduct(new Product() { name = "Рис", quantity = Mass.FromGrams(150) });
            dish15.AddProduct(new Product() { name = "Капуста свежая цельная", quantity = Mass.FromGrams(500) });
            dish15.AddProduct(new Product() { name = "Лук репчатый", quantity = Mass.FromGrams(100) });
            dish15.AddProduct(new Product() { name = "Морковь свежая", quantity = Mass.FromGrams(100) });
            dish15.AddProduct(new Product() { name = "Томатная паста", quantity = Volume.FromMilliliters(50) });
            dish15.AddProduct(new Product() { name = "Сметана", quantity = Volume.FromMilliliters(50) });
            dish15.AddProduct(new Product() { name = "Лавровый лист", quantity = Mass.FromGrams(20) });
            dish15.AddProduct(new Product() { name = "Соль", quantity = Mass.FromGrams(20) });
            dish15.AddProduct(new Product() { name = "Масло растительное", quantity = Volume.FromMilliliters(20) });

            Dish dish16 = new Dish("Блинчики с мясом", "Промежуточные блюда"); // Промежуточные блюда
            dish16.AddProduct(new Product() { name = "Молоко", quantity = Volume.FromMilliliters(200) });
            dish16.AddProduct(new Product() { name = "Яйца куриные", quantity = Scalar.FromAmount(2) });
            dish16.AddProduct(new Product() { name = "Мука", quantity = Mass.FromGrams(160) });
            dish16.AddProduct(new Product() { name = "Сахар", quantity = Mass.FromGrams(40) });
            dish16.AddProduct(new Product() { name = "Масло растительное", quantity = Volume.FromMilliliters(100) });
            dish16.AddProduct(new Product() { name = "Соль", quantity = Mass.FromGrams(4) });

            Dish dish17 = new Dish("Блинчики с творогом", "Промежуточные блюда");  // Промежуточные блюда
            dish17.AddProduct(new Product() { name = "Мука", quantity = Mass.FromGrams(150) });
            dish17.AddProduct(new Product() { name = "Молоко", quantity = Volume.FromMilliliters(200) });
            dish17.AddProduct(new Product() { name = "Яйца куриные", quantity = Scalar.FromAmount(3) });
            dish17.AddProduct(new Product() { name = "Масло растительное", quantity = Volume.FromMilliliters(100) });
            dish17.AddProduct(new Product() { name = "Соль", quantity = Mass.FromGrams(4) });
            dish17.AddProduct(new Product() { name = "Творог", quantity = Mass.FromGrams(200) });
            dish17.AddProduct(new Product() { name = "Сметана", quantity = Volume.FromMilliliters(50) });
            dish17.AddProduct(new Product() { name = "Сахар", quantity = Mass.FromGrams(40) });

            Dish dish18 = new Dish("Фарель форшированная", "Промежуточные блюда"); // Промежуточные блюда
            dish18.AddProduct(new Product() { name = "Рыба Форель", quantity = Mass.FromGrams(800) });
            dish18.AddProduct(new Product() { name = "Авокадо свежий", quantity = Mass.FromGrams(200) });
            dish18.AddProduct(new Product() { name = "Гранат свежий", quantity = Mass.FromGrams(200) });
            dish18.AddProduct(new Product() { name = "Вино белое сухое", quantity = Volume.FromMilliliters(200) });
            dish18.AddProduct(new Product() { name = "Соль", quantity = Mass.FromGrams(10) });

            Dish dish19 = new Dish("Жульен", "Промежуточные блюда"); // Промежуточные блюда
            dish19.AddProduct(new Product() { name = "Мясо филе куриное", quantity = Mass.FromGrams(400) });
            dish19.AddProduct(new Product() { name = "Грибы Шампиньоны", quantity = Mass.FromGrams(100) });
            dish19.AddProduct(new Product() { name = "Лук репчатый свежий", quantity = Mass.FromGrams(100) });
            dish19.AddProduct(new Product() { name = "Сливки", quantity = Volume.FromMilliliters(200) });
            dish19.AddProduct(new Product() { name = "Масло растительное", quantity = Volume.FromMilliliters(20) });
            dish19.AddProduct(new Product() { name = "Соль", quantity = Mass.FromGrams(10) });


            menuCollection.addMenu(dish1);
            menuCollection.addMenu(dish2);
            menuCollection.addMenu(dish3);
            menuCollection.addMenu(dish4);
            menuCollection.addMenu(dish5);
            menuCollection.addMenu(dish6);
            menuCollection.addMenu(dish7);
            menuCollection.addMenu(dish8);
            menuCollection.addMenu(dish9);
            menuCollection.addMenu(dish10);
            menuCollection.addMenu(dish11);
            menuCollection.addMenu(dish12);
            menuCollection.addMenu(dish13);
            menuCollection.addMenu(dish14);
            menuCollection.addMenu(dish15);
            menuCollection.addMenu(dish16);
            menuCollection.addMenu(dish17);
            menuCollection.addMenu(dish18);
            menuCollection.addMenu(dish19);

        }



        internal void print() 
        {



            foreach (string s in menuCollection.menu.Keys)
            { 
             Console.WriteLine(s);
            
            
            }



        }





    }
}
