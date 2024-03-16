using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Test1.DataAccessLayer;
using Test1.Models;
using Test1.Models.DBEntties;
using Microsoft.EntityFrameworkCore;
using System.Text;
using Newtonsoft.Json;

namespace Test1.Controllers
{
    public class NumberController : Controller
    {
        private readonly NumberDbContext _context;

        public NumberController(NumberDbContext context) 
        {
            this._context = context;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var numbers = _context.Numbers.ToList();
            List<NumberViewModel> numbersList = new List<NumberViewModel>();
            //Chk for records
            if(numbers != null)
            {
				foreach (var number in numbers)
				{
                    var NumberViewModel = new NumberViewModel()
                    {
                        Id = number.Id,
                        NumberList=number.NumberList,
                        SortType= number.SortType,
                        TimeTaken= number.TimeTaken,
                        CreatedAt=number.CreatedAt
                    };
                    numbersList.Add(NumberViewModel);
				}
                return View(numbersList);
			}
            //return empty (verified by view)
			return View(numbersList);
        }
        [HttpPost]
		public IActionResult SortInputNum(SortInputModel InputData)
		{
			try
			{
				if (ModelState.IsValid)
				{
					//input Str to Array as numbers

					int[] NumIn = InputData.Numbers.Split(',').Select(int.Parse).ToArray();
					//Start time
					var watch = new Stopwatch();
					watch.Start();
					//Sorting
					if (InputData.SortBy == "ASC")
						Array.Sort(NumIn);
					else
						Array.Sort(NumIn, (a, b) => b.CompareTo(a));
					//End time
					watch.Stop();
					//Convert sorted to Str
					string StrNumIn = string.Join(",", NumIn);
					//Save to DB
					var result = new Numbers()
					{
						NumberList = StrNumIn,
						SortType = InputData.SortBy,
						TimeTaken = watch.Elapsed,
						CreatedAt = DateTime.Now
					};
					_context.Numbers.Add(result);
					_context.SaveChanges();
					TempData["successMsg"] = "Numbers successfully sorted and saved.";
				}
				else
				{
					TempData["successMsg"] = "Numbers sorting operation not successfull.";
				}
				return RedirectToAction("Index");
			}
			catch (Exception ex)
			{
				TempData["errorMsg"] = ex.Message;
				return RedirectToAction("Index");
			}
		}
		[HttpGet]
		public async Task<IActionResult> ExportAsJson(string Btn, int? Indx=null)
		{
			// Set the file name
			var fileName = Indx.HasValue ? $"sortResult_{Indx}.json" : "all_sort_results.json";

			if (Indx.HasValue)
			{
				//Select record wirh Idx
				var sortResults = await _context.Numbers.FirstOrDefaultAsync<Numbers>(s => s.Id == Indx);
								
				switch (Btn)
				{
					case "Exp":
						//return Json(sortResults);
						var jsonBytes = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(sortResults));
						// Return a FileResult with the JSON data
						return File(jsonBytes, "application/json", fileName);
					case "View":
						//Display result
						return Json(sortResults);
					default:
						return View("Index");
				}
			}
			else
			{
				//Select all records
				var sortResults = _context.Numbers.ToList();

				switch (Btn)
				{
					case "Exp":
						//return Json(sortResults);
						var jsonBytes = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(sortResults));
						// Set the file name
						return File(jsonBytes, "application/json", fileName);
					case "View":
						//Display result
						return Json(sortResults);
					default:
						return View("Index");
				}
			}
		}
	}
}
