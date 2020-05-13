using Localdb;
using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestCore.Helper
{
    public  class CategoryCommission
    {
        private readonly PistisContext db;
        public CategoryCommission(PistisContext pistis)
        {
            db = pistis;
        }
        public  List<ProductCategoryCommission> getAll()
        {
            return db.ProductCategoryCommission.Where(x => x.IsActive == true).ToList();
        }
        private static List<ProductCategoryCommission> _CategoryCommission = null;
        public  List<ProductCategoryCommission> CategoryCommissions
        {
            get
            {
                if (_CategoryCommission == null)
                {
                    _CategoryCommission = getAll();
                    return _CategoryCommission;
                }
                else
                    return _CategoryCommission;
            }
            set
            {
                _CategoryCommission = value;
            }
        }
    }
}
