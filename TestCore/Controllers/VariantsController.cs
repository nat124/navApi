using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Localdb;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;

namespace TestCore.Controllers
{
    [Route("api/variants")]
    [ApiController]
    public class VariantsController : ControllerBase
    {

        private readonly PistisContext db;
        public VariantsController(PistisContext pistis)
        {
            db = pistis;
        }


        [HttpGet]
        [Route("getAll")]
        public IActionResult GetVariants(int page, int pageSize, string search,int categoryId)
        {
            var skipData = pageSize * (page - 1);
            var data = db.CategoryVariants.Where(c => c.IsActive == true).Include(v => v.ProductCategory).Include(c => c.Variant).Include(c => c.Variant.VariantOptions).Select(b => new CategoryVariantModel
            {
                Id = b.Id,
                ProductCategoryId = b.ProductCategoryId,
                VariantId = b.VariantId,
                VariantData = new CategoriesVariantModel
                {
                    Name = b.Variant.Name,
                    Id = b.Id,
                    VariantOptions = b.Variant.VariantOptions.Select(m => new CategoriesVariantoprionsModel
                    {
                        Id = m.Id,
                        Name = m.Name,
                        IsActive = m.IsActive,
                        VariantId = m.VariantId,
                    }).ToList()
                },
                ProductCategory = new ProductCategoryModel
                {
                    Id = b.ProductCategory.Id,
                    Name = b.ProductCategory.Name,
                }
            }).ToList();

            if (categoryId > 0)
                data = data.Where(c => c.ProductCategoryId == categoryId).ToList();

            if (search != null)
            {
                data = data.Where(c => c.ProductCategory.Name.ToLower().Contains(search.ToLower())
                || c.VariantData.Name.ToLower().Contains(search.ToLower())
                ).ToList();
            }

            var response = new
            {
                data = data.Skip(skipData).Take(pageSize).OrderByDescending(c => c.Id).ToList(),
                count = data.Count
            };
            return Ok(response);
        }


        [HttpGet]
        [Route("getVariantByCategory")]
        public IActionResult VariantByCategory(int id)
        {
            var data = db.CategoryVariants.Where(c => c.IsActive == true && c.ProductCategoryId == id).Include(v => v.ProductCategory).Include(c => c.Variant).Include(c => c.Variant.VariantOptions).Select(b => new CategoryVariantModel
            {
                Id = b.Id,
                ProductCategoryId = b.ProductCategoryId,
                VariantId = b.VariantId,
                VariantData = new CategoriesVariantModel
                {
                    Name = b.Variant.Name,
                    Id = b.Id,
                    VariantOptions = b.Variant.VariantOptions.Where(n=>n.IsActive==true).Select(m => new CategoriesVariantoprionsModel
                    {
                        Id = m.Id,
                        Name = m.Name,
                        IsActive = m.IsActive,
                        VariantId = m.VariantId,
                    }).ToList(),
                    IsSearchOption = b.IsSearchOption
                },
                ProductCategory = new ProductCategoryModel
                {
                    Id = b.ProductCategory.Id,
                    Name = b.ProductCategory.Name,
                }
            }).ToList();
            return Ok(data.OrderByDescending(c => c.Id));
        }



        [HttpGet]
        [Route("getById")]
        public IActionResult getById(int id)
        {
            var data = db.CategoryVariants.Where(c => c.Id == id).Include(v => v.ProductCategory).Include(c => c.Variant).Include(c => c.Variant.VariantOptions).Select(b => new CategoryVariantModel
            {
                Id = b.Id,
                ProductCategoryId = b.ProductCategoryId,
                VariantId = b.VariantId,
                IsSearchOption = b.IsSearchOption,
                IsMain = b.IsMain,
                VariantData = new CategoriesVariantModel
                {
                    Name = b.Variant.Name,
                    Id = b.Id,
                    IsMain = b.IsMain,
                    VariantOptions = b.Variant.VariantOptions.Where(a => a.IsActive == true).Select(m => new CategoriesVariantoprionsModel
                    {
                        Id = m.Id,
                        Name = m.Name,
                        IsActive = m.IsActive,
                        VariantId = m.VariantId,
                    }).ToList()
                },
                ProductCategory = new ProductCategoryModel
                {
                    Id = b.ProductCategory.Id,
                    Name = b.ProductCategory.Name,
                }
            }).FirstOrDefault();
            if (db.CategoryVariants.Where(c => c.ProductCategoryId == data.ProductCategoryId && c.IsActive == true).Any(c => c.IsMain == true))
                data.MainSelected = true;
            else
                data.MainSelected = false;
            return Ok(data);
        }


        [HttpDelete]
        [Route("delete")]
        public IActionResult deleteById(int id)
        {
            var data = db.CategoryVariants.Where(c => c.Id == id).Include(c => c.Variant).Include(v => v.Variant.VariantOptions).FirstOrDefault();
            if (data != null)
            {
                data.IsActive = false;
                if (data.Variant != null)
                {
                    data.IsActive = false;
                    if (data.Variant.VariantOptions != null && data.Variant.VariantOptions.Count > 0)
                    {
                        foreach (var item in data.Variant.VariantOptions)
                            item.IsActive = false;
                    }
                }
                db.SaveChanges();
            }
            return Ok(true);
        }


        [HttpGet]
        [Route("checkMainSelected")]
        public IActionResult checkMainSelected(int id)
        {
            return Ok(db.CategoryVariants.Where(c => c.ProductCategoryId == id && c.IsActive == true).Any(c => c.IsMain == true));
        }

        [HttpGet]
        [Route("checkExistingName")]
        public IActionResult checkExistingName(string name, int catId, int? variantId = 0)
        {
            var data = db.CategoryVariants.Where(c => c.IsActive == true && c.ProductCategoryId == catId).Include(v => v.Variant).ToList();
            var filterdData = data.Where(b => b.Variant.Id != variantId).ToList();
            if (filterdData.Any(v => v.Variant.Name.ToLower() == name.ToLower()))
                return Ok(true);
            else
                return Ok(false);
        }


        [HttpPost]
        [Route("addVariant")]
        public IActionResult Add(CategoriesVariantModel model)
        {
            try
            {
                if (model != null)
                {
                    var data = db.CategoryVariants.Where(v => v.IsActive == true && v.ProductCategoryId == model.CategoryId).Include(v => v.Variant).ToList();
                    if (data.Any(v => v.Variant.Name.ToLower() == model.Name.ToLower()))
                        return Ok("exist");

                    //--getting data to string array
                    var optionsData = model.AllOptions.Split(',');
                    List<VariantOption> options = new List<VariantOption>();
                    Variant vari = new Variant();
                    vari.Name = model.Name;
                    vari.IsActive = true;
                    vari.IsMain = model.IsMain;
                    db.Variants.Add(vari);
                    db.SaveChanges();
                    if (optionsData.Length > 0)
                    {
                        foreach (var item in optionsData)
                        {
                            VariantOption vop = new VariantOption();
                            if (item != "")
                            {
                                vop.Name = item;
                                vop.IsActive = true;
                                vop.VariantId = vari.Id;
                                options.Add(vop);
                            }
                        }
                    }
                    db.VariantOptions.AddRange(options);

                    if (model.CategoryId > 0)
                    {
                        var catData = new CategoryVariant();
                        catData.ProductCategoryId = model.CategoryId;
                        catData.VariantId = vari.Id;
                        catData.IsActive = true;
                        catData.IsSearchOption = model.IsSearchOption;
                        catData.IsMain = model.IsMain;
                        db.CategoryVariants.Add(catData);
                    }
                    db.SaveChanges();
                }
                return Ok(true);
            }
            catch (Exception ex)
            {
                return Ok(ex);
            }
        }


        [HttpPost]
        [Route("updateVariant")]
        public IActionResult Update(CategoriesVariantModel model)
        {
            try
            {
                var optionsData = model.AllOptions.Split(',');
                List<VariantOption> options = new List<VariantOption>();

                var data = db.CategoryVariants.Where(c => c.Id == model.Id).Include(v => v.ProductCategory).Include(c => c.Variant).Include(c => c.Variant.VariantOptions).FirstOrDefault();
                if (data != null)
                {
                    data.ProductCategoryId = model.CategoryId;
                    data.IsSearchOption = model.IsSearchOption;
                    data.IsMain = model.IsMain;

                    if (data.Variant != null && data.Variant.Id == model.VariantId)
                    {
                        data.Variant.Name = model.Name;
                        data.Variant.IsActive = true;
                        db.SaveChanges();

                        if (data.Variant.VariantOptions.Count > 0)
                            foreach (var item in data.Variant.VariantOptions)
                                item.IsActive = false;

                        if (optionsData.Length > 0)
                        {
                            foreach (var item in optionsData)
                            {
                                VariantOption vop = new VariantOption();
                                if (item != "")
                                {
                                    vop.Name = item;
                                    vop.IsActive = true;
                                    vop.VariantId = data.Variant.Id;
                                    options.Add(vop);
                                }
                            }
                            db.VariantOptions.AddRange(options);
                        }
                    }

                    data.ProductCategoryId = model.CategoryId;
                    db.SaveChanges();
                    return Ok(true);
                }
                else
                    return Ok(false);
            }
            catch (Exception ex)
            {
                return Ok(ex);
            }
        }




    }
}