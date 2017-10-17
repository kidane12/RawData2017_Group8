﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace DBMapper
{

    public class DataLayer 
    {
       // This method returns all the categories
        public static void Listingcategories()
        {

            using (var db = new NorthwindContext())
            {

                var categories = db.Categories;

                foreach (var category1 in categories)
                {
                    Console.WriteLine((category1.Name));

                  
                }

            }

            
        }
   


}
}
