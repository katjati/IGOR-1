using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Igor.Library.Abstract;

namespace Igor.Library.Services
{
    public class SchoolService : ISchoolService
    {
        #region Properties

        /// <summary>
        /// Larp data service instance.
        /// </summary>
        private readonly ILarpDataService _larpDataService;

        #endregion

        #region Constructors
        /// <summary>
        /// Default instance.
        /// </summary>
        /// <param name="uow"></param>
        public SchoolService(ILarpDataService uow)
        {
            _uow = uow;
        }
        #endregion

        #region 

        #endregion
    }
}
