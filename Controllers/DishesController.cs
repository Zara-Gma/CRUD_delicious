using System;
using System.Collections.Generic;
using System.Linq;
using crudDelicious.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace crudDelicious.Controllers
{
    public class DishesController : Controller
    {
        private crudDeliciousContext _db;
        public DishesController(crudDeliciousContext context)
        {
            _db = context;
        }

        [HttpGet("dishes/all")]
        public IActionResult All()
        {
            List<Dish> allDishes = _db.Dishes.ToList();
            return View(allDishes);
        }

        [HttpGet("dish/details/{id}")]
        public IActionResult Details(int id)
        {
            Dish selectedDish = _db.Dishes
                .FirstOrDefault(d => d.DishId == id);

            // in case user manually types URL
            if (selectedDish == null)
                RedirectToAction("All");

            // ViewBag.Uid = _uid;
            return View(selectedDish);
        }


        [HttpGet("dishes/new")]
        public IActionResult New()
        {
            return View();
        }

        [HttpPost("dishes/create")]
        public IActionResult Create(Dish newDish)
        {

            if (ModelState.IsValid)
            {
                _db.Dishes.Add(newDish);
                _db.SaveChanges();
                return RedirectToAction("All");
            }
            else
            {
                return View("New");
            }
        }

        [HttpGet("dishes/edit")]
        public IActionResult Edit(int id)
        {
            Dish toEdit = _db.Dishes.FirstOrDefault(d => d.DishId == id);

            if (toEdit == null)
                return RedirectToAction("All");
            return View(toEdit);
        }

        [HttpPost("dishes/update")]
        public IActionResult Update(Dish editedDish, int id)
        {

            if (ModelState.IsValid)
            {
                Dish dbDish = _db.Dishes.FirstOrDefault(d => d.DishId == id);

                if (dbDish != null)
                {
                    dbDish.Name = editedDish.Name;
                    dbDish.Chef = editedDish.Chef;
                    dbDish.Tastiness = editedDish.Tastiness;
                    dbDish.Calories = editedDish.Calories;
                    dbDish.Description = editedDish.Description;
                    dbDish.UpdatedAt = DateTime.Now;

                    _db.Dishes.Update(dbDish);
                    _db.SaveChanges();

                    return RedirectToAction("Details", new { id = dbDish.DishId });
                }
            }
            // so error messages will be displayed if any
            return View("Edit", editedDish);
        }


        [HttpGet("dishes/delete/{id}")]
        public IActionResult Delete(int id)
        {
            Dish dishromDb = _db.Dishes.FirstOrDefault(dish => dish.DishId == id);

            if (dishromDb != null)
            {
                _db.Dishes.Remove(dishromDb);
                _db.SaveChanges();
            }

            return RedirectToAction("All");
        }
    }
}